using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Tesseract;
using InputSimulatorEx;
using System.Net.Http;

namespace AdeptiScanner_GI
{
    public partial class ScannerForm : Form
    {
        public static ScannerForm INSTANCE;
        private TesseractEngine tessEngine;
        private Bitmap img_Raw;
        private Bitmap img_Filtered;
        private int[] filtered_rows;
        private Rectangle savedArtifactArea = new Rectangle(0, 0, 1, 1);
        private Rectangle relativeArtifactArea = new Rectangle(0, 0, 1, 1);
        private Rectangle savedGameArea = new Rectangle(0, 0, 1, 1);
        private bool pauseAuto = true;
        private bool softCancelAuto = true;
        private bool hardCancelAuto = true;
        private KeyHandler pauseHotkey; // Escape key, pause auto
        private KeyHandler readHotkey; // P key, read stats
        private DateTime soonestAllowedHotkeyUse = DateTime.MinValue; // Used to avoid spam activations and lockups caused by them

        internal bool autoRunning = false;
        private bool autoCaptureDone = false;
        internal List<Artifact> scannedArtifacts = new List<Artifact>();
        internal List<Weapon> scannedWeapons = new List<Weapon>();
        internal List<Character> scannedCharacters = new List<Character>();
        private bool cancelOCRThreads = false;
        private const int ThreadCount = 6; //--------------------------------------------------------
        private bool[] threadRunning = new bool[ThreadCount];
        private ConcurrentQueue<Bitmap>[] threadQueues = new ConcurrentQueue<Bitmap>[ThreadCount];
        private ConcurrentQueue<Bitmap> badResults = new ConcurrentQueue<Bitmap>();
        private TesseractEngine[] threadEngines = new TesseractEngine[ThreadCount];
        private List<object>[] threadResults = new List<object>[ThreadCount];
        private bool rememberSettings = true;

        internal int minLevel = 0;
        internal int maxLevel = 20;
        internal int minRarity = 5;
        internal int maxRarity = 5;
        internal bool exportAllEquipped = true;
        internal bool useTemplate = false;
        internal string travelerName = "";
        internal string wandererName = "Wanderer";
        internal bool captureOnread = true;
        internal bool saveImagesGlobal = false;
        internal string clickSleepWait_load = "100";
        internal string scrollSleepWait_load = "1500";
        internal string scrollTestWait_load = "100";
        internal string recheckWait_load = "300";
        internal bool? updateData = null;
        internal bool? updateVersion = null;
        internal string ignoredDataVersion = "";
        internal string ignoredProgramVersion = "";
        internal string lastUpdateCheck = "";
        internal string uid = "";
        internal bool exportEquipStatus = true;

        private static InputSimulator sim = new InputSimulator();

        public ScannerForm()
        {
            ScannerForm.INSTANCE = this;
            if (Directory.Exists(Database.appdataPath) && Database.appDir != Database.appdataPath)
            {
                foreach (string filePath in Directory.EnumerateFiles(Database.appdataPath))
                {
                    string fileName = filePath.Replace(Database.appdataPath, "");
                    string localFilePath = Database.appDir + fileName;
                    if (File.Exists(localFilePath))
                        File.Delete(localFilePath);
                    File.Copy(filePath, localFilePath);
                    File.Delete(filePath);
                }
                Directory.Delete(Database.appdataPath);
            }
            loadSettings();
            InitializeComponent();
            finalizeLoadSettings();
            label_dataversion.Text = "Data: V" + Database.dataVersion;
            label_appversion.Text = "Program: V" + Database.programVersion;
            label_admin.Text = "Admin: " + IsAdministrator();
            this.Text = "AdeptiScanner_GI V" + Database.programVersion;
            FormClosing += eventFormClosing;
            Activated += eventGotFocus;
            try
            {
                tessEngine = new TesseractEngine(Database.appDir + @"/tessdata", "genshin")
                {
                    DefaultPageSegMode = PageSegMode.SingleLine
                };
            }
            catch (Exception e)
            {
                MessageBox.Show("Error trying to access Tessdata file" + Environment.NewLine + Environment.NewLine +
                    "Exact error:" + Environment.NewLine + e.ToString(),
                    "Scanner could not start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            //worker thread stuff
            for (int i = 0; i < ThreadCount; i++)
            {
                threadRunning[i] = false;
                threadQueues[i] = new ConcurrentQueue<Bitmap>();
                threadEngines[i] = new TesseractEngine(Database.appDir + @"/tessdata", "genshin")
                {
                    DefaultPageSegMode = PageSegMode.SingleLine
                };
                threadResults[i] = new List<object>();
            }

            //simple junk defaults
            img_Raw = new Bitmap(image_preview.Width, image_preview.Height);
            using (Graphics g = Graphics.FromImage(img_Raw))
            {
                g.FillRectangle(Brushes.Black, 0, 0, img_Raw.Width, img_Raw.Height);
                g.FillRectangle(Brushes.White, img_Raw.Width / 8, img_Raw.Height / 8, img_Raw.Width * 6 / 8, img_Raw.Height * 6 / 8);
            }
            img_Filtered = new Bitmap(img_Raw);
            image_preview.Image = new Bitmap(img_Raw);
            if (!updateData.HasValue || !updateVersion.HasValue)
            {
                Application.Run(new FirstStart());
            }
            else
            {
                searchForUpdates(false);
            }
        }

        private void eventFormClosing(object sender, FormClosingEventArgs e)
        {
            if (rememberSettings)
            {
                saveSettings();
            }

            unregisterPauseKey();
            unregisterReadKey();
        }

        private bool TryPauseAuto()
        {
            if (!pauseAuto && autoRunning)
            {
                pauseAuto = true;
                text_full.AppendText("Auto scanning paused, select action" + Environment.NewLine);
                button_hardCancel.Enabled = true;
                button_softCancel.Enabled = true;
                button_resume.Enabled = true;
                return true;
            }
            return false;
        }

        private void eventGotFocus(object sender, EventArgs e)
        {
            TryPauseAuto();
        }

        public void registerPauseKey()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(registerPauseKey));
                return;
            }
            if (pauseHotkey == null)
            {
                pauseHotkey = new KeyHandler(Keys.Escape, this);
            }
            pauseHotkey.Register();
        }

        public void unregisterPauseKey()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(unregisterPauseKey));
                return;
            }
            if (pauseHotkey == null)
            {
                return;
            }
            pauseHotkey.Unregister();
        }

        public void registerReadKey()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(registerReadKey));
                return;
            }
            if (readHotkey == null)
            {
                readHotkey = new KeyHandler(Keys.P, this);
            }
            readHotkey.Register();
        }

        public void unregisterReadKey()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(unregisterReadKey));
                return;
            }
            if (readHotkey == null)
            {
                return;
            }
            readHotkey.Unregister();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == KeyHandler.WM_HOTKEY_MSG_ID)
            {
                // block if last press was too recently. Exception if auto is running and not paused, to be safe
                if ((!pauseAuto && autoRunning) || DateTime.UtcNow > soonestAllowedHotkeyUse)
                {
                    if (pauseHotkey != null && pauseHotkey.GetHashCode() == m.WParam)
                    {
                        TryPauseAuto();
                    }
                    if (readHotkey != null && readHotkey.GetHashCode() == m.WParam)
                    {
                        if (btn_OCR.Enabled)
                        {
                            btn_OCR_Click(this, new HotkeyEventArgs());
                        }
                    }
                }

                soonestAllowedHotkeyUse = DateTime.UtcNow + TimeSpan.FromSeconds(0.2);
            }
            base.WndProc(ref m);
        }

        public void AppendStatusText(string value, bool setButtons)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(AppendStatusText), new object[] { value, setButtons });
                return;
            }
            text_full.AppendText(value);
            if (setButtons)
            {
                btn_capture.Enabled = true;
                btn_OCR.Enabled = true;
                button_auto.Enabled = true;
                button_hardCancel.Enabled = false;
                button_softCancel.Enabled = false;
                button_resume.Enabled = false;
            }
        }


        /// <summary>
        /// Move cursor to position and simulare left click
        /// </summary>
        /// <param name="x">Cursor X position</param>
        /// <param name="y">Cursor Y position</param>
        private void clickPos(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new Point(x, y);
            sim.Mouse.LeftButtonClick();
        }

        /// <summary>
        /// Empties all text boxes
        /// </summary>
        private void resetTextBoxes()
        {
            text_full.Text = "";
        }

        private void displayInventoryItem(object item)
        {
            text_full.Text = item.ToString();
        }

        //https://stackoverflow.com/questions/11660184/c-sharp-check-if-run-as-administrator
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void runOCRThread(int threadIndex, bool weaponMode)
        {
            Task.Run(RunOCRThreadInternal);

            async Task RunOCRThreadInternal()
            {
                threadRunning[threadIndex] = true;
                bool saveImages = false;
                while (autoRunning && !cancelOCRThreads)
                {
                    if (threadQueues[threadIndex].TryDequeue(out Bitmap img))
                    {
                        //string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
                        //if (saveImages)
                        //    img.Save(Path.Join(Database.appDir, "images", "GenshinArtifactImg_" + timestamp + ".png"));
                        Rectangle area = new Rectangle(0, 0, img.Width, img.Height);
                        Bitmap filtered = new Bitmap(img);
                        if (weaponMode)
                        {
                            filtered = ImageProcessing.getWeaponImg(filtered, area, out int[] rows, saveImages, out bool locked, out Rectangle nameArea, out Rectangle statArea, out Rectangle refinementArea, out Rectangle charArea);
                            Weapon weapon = ImageProcessing.getWeapon(filtered, rows, saveImages, threadEngines[threadIndex], locked, nameArea, statArea, refinementArea, charArea);

                            if (Database.weaponInvalid(weapon))
                            {
                                badResults.Enqueue(img);
                            }
                            else
                            {
                                threadResults[threadIndex].Add(weapon);
                            }
                        }
                        else
                        {
                            filtered = ImageProcessing.getArtifactImg(filtered, area, out int[] rows, saveImages, out bool locked, out bool astralMark, out bool elixirCrafted, out int rarity, out Rectangle typeMainArea, out Rectangle levelArea, out Rectangle subArea, out Rectangle setArea, out Rectangle charArea);

                            Artifact item = ImageProcessing.getArtifacts(filtered, rows, saveImages, threadEngines[threadIndex], locked, astralMark, elixirCrafted, rarity, typeMainArea, levelArea, subArea, setArea, charArea);

                            if (Database.artifactInvalid(rarity, item))
                            {
                                badResults.Enqueue(img);
                            }
                            else
                            {
                                threadResults[threadIndex].Add(item);
                            }
                        }

                    }
                    else if (autoCaptureDone || softCancelAuto || hardCancelAuto)
                    {
                        threadRunning[threadIndex] = false;
                        return;
                    }
                    else
                    {
                        await Task.Delay(1000);
                    }
                }
                threadRunning[threadIndex] = false;
            }
        }

        private void artifactAuto(bool saveImages, int clickSleepWait = 100, int scrollSleepWait = 1500, int scrollTestWait = 100, int recheckSleepWait = 300)
        {
            text_full.Text = "Starting auto-run. ---Press ESCAPE to pause---" + Environment.NewLine;
            autoRunning = true;
            autoCaptureDone = false;
            registerPauseKey(); //activate pause auto hotkey
            //start worker threads
            for (int i = 0; i < ThreadCount; i++)
            {
                threadQueues[i] = new ConcurrentQueue<Bitmap>();
                threadResults[i] = new List<object>();
                runOCRThread(i, false);
            }

            Task.Run(ArtifactAutoInternal);

            void ArtifactAutoInternal()
            {
                Stopwatch runtime = new Stopwatch();
                runtime.Start();
                System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                bool running = true;
                bool firstRun = true;
                int firstY = 0;
                int firstRows = 0;
                int nextThread = 0;
                Rectangle gridArea = new Rectangle(savedGameArea.X, savedGameArea.Y, savedArtifactArea.X - savedGameArea.X, savedGameArea.Height);
                Point gridOffset = new Point(gridArea.X, gridArea.Y);
                List<string> foundArtifactHashes = new List<string>();


                GameVisibilityHandler.bringGameToFront();

                //make sure cursor is on the correct screen
                System.Threading.Thread.Sleep(50);
                System.Windows.Forms.Cursor.Position = new Point(savedGameArea.X, savedGameArea.Y);
                System.Threading.Thread.Sleep(50);
                System.Windows.Forms.Cursor.Position = new Point(savedGameArea.X, savedGameArea.Y);
                System.Threading.Thread.Sleep(50);

                while (running)
                {
                    //load current grid/scroll location
                    Bitmap img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                    List<Point> artifactLocations = ImageProcessing.getArtifactGrid(img, saveImages, gridOffset);
                    artifactLocations = ImageProcessing.equalizeGrid(artifactLocations, gridArea.Height / 20, gridArea.Width / 20);

                    if (artifactLocations.Count == 0)
                    {
                        break;
                    }
                    int startTop = artifactLocations[0].Y;
                    int startBot = startTop;
                    int rows = 1;
                    int distToScroll = 0;
                    foreach (Point p in artifactLocations)
                    {
                        if (p.Y > startBot + 3)
                        {
                            startBot = p.Y;
                            rows++;
                        }
                    }
                    if (firstRun)
                    {
                        firstY = startTop;
                        firstRows = rows;
                    }

                    if (rows >= 1)
                    {
                        distToScroll = (int)((startBot - (double)firstY) / rows * (rows + 1));
                    }

                    if (!firstRun)
                    {
                        while (pauseAuto)
                        {
                            if (hardCancelAuto)
                            {
                                goto hard_cancel_pos;
                            }
                            if (softCancelAuto)
                            {
                                running = false;
                                pauseAuto = false;
                                goto soft_cancel_pos;
                            }
                            System.Threading.Thread.Sleep(1000);
                        }
                        //test scroll distance
                        sim.Mouse.VerticalScroll(-1);
                        System.Threading.Thread.Sleep(scrollTestWait);
                        sim.Mouse.VerticalScroll(-1);
                        System.Threading.Thread.Sleep(scrollTestWait);
                        img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                        artifactLocations = ImageProcessing.getArtifactGrid(img, saveImages, gridOffset);
                        artifactLocations = ImageProcessing.equalizeGrid(artifactLocations, gridArea.Height / 20, gridArea.Width / 20);

                        if (artifactLocations.Count == 0)
                        {
                            break;
                        }
                        int distPerScroll = (startTop - artifactLocations[0].Y) / 2;
                        int scrollsNeeded = 0;
                        if (distPerScroll > 0)
                        {
                            scrollsNeeded = distToScroll / distPerScroll;
                        }

                        if (scrollsNeeded <= 0 || distPerScroll == 0 || rows < Math.Max(firstRows - 1, 0))
                        {
                            running = false;
                        }
                        /*Console.WriteLine("firstY " + firstY + Environment.NewLine
                            + "startTop " + startTop + Environment.NewLine
                            + "currTop " + weaponLocations[0].Y + Environment.NewLine
                            + "distPerScroll " + distPerScroll + Environment.NewLine
                            + "distToScroll " + distToScroll + Environment.NewLine
                            + "scrollsNeeded " + scrollsNeeded + Environment.NewLine + Environment.NewLine); */

                        while (scrollsNeeded > 0)
                        {
                            sim.Mouse.VerticalScroll(-1);
                            scrollsNeeded--;
                        }
                        System.Threading.Thread.Sleep(scrollSleepWait);
                        img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                        artifactLocations = ImageProcessing.getArtifactGrid(img, saveImages, gridOffset);
                        artifactLocations = ImageProcessing.equalizeGrid(artifactLocations, gridArea.Height / 20, gridArea.Width / 20);
                    }


                    firstRun = false;

                    //select and OCR each artifact in list
                    bool repeat = false;
                    for (int i = 0; i < artifactLocations.Count;)
                    {
                        Point p = artifactLocations[i];
                        while (pauseAuto)
                        {
                            if (hardCancelAuto)
                            {
                                goto hard_cancel_pos;
                            }

                            if (softCancelAuto)
                            {
                                running = false;
                                pauseAuto = false;
                                goto soft_cancel_pos;
                            }
                            System.Threading.Thread.Sleep(1000);
                        }
                        clickPos(p.X, p.Y);
                        System.Threading.Thread.Sleep(clickSleepWait);

                        Bitmap artifactSC = ImageProcessing.CaptureScreenshot(saveImages, savedArtifactArea, true);

                        //check if artifact already found using hash of pixels, without the right edge due to lock/unlock animation
                        Bitmap withoutLock = new Bitmap(artifactSC.Width * 3 / 4, artifactSC.Height);
                        using (Graphics g = Graphics.FromImage(withoutLock))
                        {
                            g.DrawImage(artifactSC, 0, 0, new Rectangle(0, 0, artifactSC.Width * 3 / 4, artifactSC.Height), GraphicsUnit.Pixel);
                        }
                        BitmapData imgData = withoutLock.LockBits(new Rectangle(0, 0, withoutLock.Width, withoutLock.Height), ImageLockMode.ReadWrite, withoutLock.PixelFormat);
                        int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
                        byte[] imgBytes = new byte[numBytes];
                        Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
                        //int PixelSize = 4; //ARGB, reverse order
                        withoutLock.UnlockBits(imgData);
                        //https://stackoverflow.com/a/800469 with some liberty
                        string hash = string.Concat(sha1.ComputeHash(imgBytes).Select(x => x.ToString("X2")));

                        if (foundArtifactHashes.Contains(hash))
                        {
                            if (repeat)
                            {
                                if (running)
                                {
                                    AppendStatusText("Duplicate artifact found, stopping after this screen" + Environment.NewLine, false);
                                }
                                running = false;
                                Console.WriteLine("Duplicate at " + p.ToString());
                                repeat = false;
                                i++;
                                continue;
                            }
                            else
                            {
                                repeat = true;
                                Console.WriteLine("Rechecking at " + p.ToString());
                                System.Threading.Thread.Sleep(recheckSleepWait);
                                continue;
                            }

                        }
                        foundArtifactHashes.Add(hash);

                        //queue up processing of artifact
                        threadQueues[nextThread].Enqueue(artifactSC);
                        nextThread = (nextThread + 1) % ThreadCount;

                        i++;
                        repeat = false;
                    }

                }

            soft_cancel_pos:

                autoCaptureDone = true;

                //temporarily disable "got focus" event, as that would trigger pause
                Activated -= eventGotFocus;
                GameVisibilityHandler.bringScannerToFront();
                Activated += eventGotFocus;

                AppendStatusText("Scanning complete, awaiting results" + Environment.NewLine
                    + "Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine, false);
                for (int i = 0; i < ThreadCount; i++)
                {
                    while (threadRunning[i] || pauseAuto)
                    {
                        if (hardCancelAuto)
                        {
                            goto hard_cancel_pos;
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                    foreach (object item in threadResults[i])
                    {
                        if (item is Artifact arti)
                        {
                            scannedArtifacts.Add(arti);
                        }
                        else if (item is Weapon wep)
                        {
                            scannedWeapons.Add(wep);
                        }
                    }
                }


                AppendStatusText("Auto finished" + Environment.NewLine
                    + " Good results: " + scannedArtifacts.Count + ", Bad results: " + badResults.Count + Environment.NewLine
                    + "Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine + Environment.NewLine, false);

                while (badResults.TryDequeue(out Bitmap img))
                {
                    Rectangle area = new Rectangle(0, 0, img.Width, img.Height);
                    Bitmap filtered = new Bitmap(img);
                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
                    filtered.Save(Path.Join(Database.appDir, "images", "GenshinArtifactImg_" + timestamp + ".png"));
                    filtered = ImageProcessing.getArtifactImg(filtered, area, out int[] rows, true, out bool locked, out bool astralMark, out bool elixirCrafted, out int rarity, out Rectangle typeMainArea, out Rectangle levelArea, out Rectangle subArea, out Rectangle setArea, out Rectangle charArea);
                    Artifact item = ImageProcessing.getArtifacts(filtered, rows, true, tessEngine, locked, astralMark, elixirCrafted, rarity, typeMainArea, levelArea, subArea, setArea, charArea);
                    AppendStatusText(item.ToString() + Environment.NewLine, false);
                }

                AppendStatusText("All bad results displayed" + Environment.NewLine, false);

            hard_cancel_pos:
                unregisterPauseKey();
                runtime.Stop();
                GameVisibilityHandler.bringScannerToFront();
                AppendStatusText("Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine, true);
                autoRunning = false;
            }
        }

        private void weaponAuto(bool saveImages, int clickSleepWait = 100, int scrollSleepWait = 1500, int scrollTestWait = 100, int recheckSleepWait = 300)
        {
            text_full.Text = "Starting auto-run. ---Press ESCAPE to pause---" + Environment.NewLine;
            autoRunning = true;
            autoCaptureDone = false;
            registerPauseKey(); //activate pause auto hotkey
            //start worker threads
            for (int i = 0; i < ThreadCount; i++)
            {
                threadQueues[i] = new ConcurrentQueue<Bitmap>();
                threadResults[i] = new List<object>();
                runOCRThread(i, true);
            }

            Task.Run(WeaponAutoInternal);

            void WeaponAutoInternal()
            {
                Stopwatch runtime = new Stopwatch();
                runtime.Start();
                System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                bool running = true;
                bool firstRun = true;
                int firstY = 0;
                int firstRows = 0;
                int nextThread = 0;
                Rectangle gridArea = new Rectangle(savedGameArea.X, savedGameArea.Y, savedArtifactArea.X - savedGameArea.X, savedGameArea.Height);
                Point gridOffset = new Point(gridArea.X, gridArea.Y);
                List<string> foundWeapon = new List<string>();


                GameVisibilityHandler.bringGameToFront();

                //make sure cursor is on the correct screen
                System.Threading.Thread.Sleep(50);
                System.Windows.Forms.Cursor.Position = new Point(savedGameArea.X, savedGameArea.Y);
                System.Threading.Thread.Sleep(50);
                System.Windows.Forms.Cursor.Position = new Point(savedGameArea.X, savedGameArea.Y);
                System.Threading.Thread.Sleep(50);

                while (running)
                {
                    //load current grid/scroll location
                    Bitmap img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                    List<Point> weaponLocations = ImageProcessing.getArtifactGrid(img, saveImages, gridOffset);
                    weaponLocations = ImageProcessing.equalizeGrid(weaponLocations, gridArea.Height / 20, gridArea.Width / 20);

                    if (weaponLocations.Count == 0)
                    {
                        break;
                    }
                    int startTop = weaponLocations[0].Y;
                    int startBot = startTop;
                    int rows = 1;
                    int distToScroll = 0;
                    foreach (Point p in weaponLocations)
                    {
                        if (p.Y > startBot + 3)
                        {
                            startBot = p.Y;
                            rows++;
                        }
                    }
                    if (firstRun)
                    {
                        firstY = startTop;
                        firstRows = rows;
                    }

                    if (rows >= 1)
                    {
                        distToScroll = (int)((startBot - (double)firstY) / rows * (rows + 1));
                    }

                    if (!firstRun)
                    {
                        while (pauseAuto)
                        {
                            if (hardCancelAuto)
                            {
                                goto hard_cancel_pos;
                            }
                            if (softCancelAuto)
                            {
                                running = false;
                                pauseAuto = false;
                                goto soft_cancel_pos;
                            }
                            System.Threading.Thread.Sleep(1000);
                        }
                        //test scroll distance
                        sim.Mouse.VerticalScroll(-1);
                        System.Threading.Thread.Sleep(scrollTestWait);
                        sim.Mouse.VerticalScroll(-1);
                        System.Threading.Thread.Sleep(scrollTestWait);
                        img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                        weaponLocations = ImageProcessing.getArtifactGrid(img, saveImages, gridOffset);
                        weaponLocations = ImageProcessing.equalizeGrid(weaponLocations, gridArea.Height / 20, gridArea.Width / 20);

                        if (weaponLocations.Count == 0)
                        {
                            break;
                        }
                        int distPerScroll = (startTop - weaponLocations[0].Y) / 2;
                        int scrollsNeeded = 0;
                        if (distPerScroll > 0)
                        {
                            scrollsNeeded = distToScroll / distPerScroll;
                        }

                        if (scrollsNeeded <= 0 || distPerScroll == 0 || rows < Math.Max(firstRows - 1, 0))
                        {
                            running = false;
                        }

                        while (scrollsNeeded > 0)
                        {
                            sim.Mouse.VerticalScroll(-1);
                            scrollsNeeded--;
                        }
                        System.Threading.Thread.Sleep(scrollSleepWait);
                        img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                        weaponLocations = ImageProcessing.getArtifactGrid(img, saveImages, gridOffset);
                        weaponLocations = ImageProcessing.equalizeGrid(weaponLocations, gridArea.Height / 20, gridArea.Width / 20);
                    }


                    firstRun = false;

                    //select and OCR each artifact in list
                    bool repeat = false;
                    bool hasNonDupes = false;
                    for (int i = 0; i < weaponLocations.Count;)
                    {
                        Point p = weaponLocations[i];
                        while (pauseAuto)
                        {
                            if (hardCancelAuto)
                            {
                                goto hard_cancel_pos;
                            }

                            if (softCancelAuto)
                            {
                                running = false;
                                pauseAuto = false;
                                goto soft_cancel_pos;
                            }
                            System.Threading.Thread.Sleep(1000);
                        }
                        clickPos(p.X, p.Y);
                        System.Threading.Thread.Sleep(clickSleepWait);

                        Bitmap weaponSC = ImageProcessing.CaptureScreenshot(saveImages, savedArtifactArea, true);

                        //check if weapon already found using hash of pixels, without the right edge due to lock/unlock animation
                        Bitmap withoutLock = new Bitmap(weaponSC.Width * 3 / 4, weaponSC.Height);
                        using (Graphics g = Graphics.FromImage(withoutLock))
                        {
                            g.DrawImage(weaponSC, 0, 0, new Rectangle(0, 0, weaponSC.Width * 3 / 4, weaponSC.Height), GraphicsUnit.Pixel);
                        }
                        BitmapData imgData = withoutLock.LockBits(new Rectangle(0, 0, withoutLock.Width, withoutLock.Height), ImageLockMode.ReadWrite, withoutLock.PixelFormat);
                        int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
                        byte[] imgBytes = new byte[numBytes];
                        Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
                        //int PixelSize = 4; //ARGB, reverse order
                        withoutLock.UnlockBits(imgData);
                        //https://stackoverflow.com/a/800469 with some liberty
                        string hash = string.Concat(sha1.ComputeHash(imgBytes).Select(x => x.ToString("X2")));

                        if (foundWeapon.Contains(hash))
                        {
                            if (!repeat)
                            {
                                repeat = true;
                                System.Threading.Thread.Sleep(recheckSleepWait);
                                continue;
                            }

                        }
                        else
                        {
                            hasNonDupes = true;
                        }
                        foundWeapon.Add(hash);

                        //queue up processing of artifact
                        threadQueues[nextThread].Enqueue(weaponSC);
                        nextThread = (nextThread + 1) % ThreadCount;

                        i++;
                        repeat = false;
                    }


                    if (!hasNonDupes)
                    {
                        AppendStatusText("Screen has only duplicate weapons, stopping" + Environment.NewLine, false);
                        running = false;
                    }

                }

            soft_cancel_pos:

                autoCaptureDone = true;

                //temporarily disable "got focus" event, as that would trigger pause
                Activated -= eventGotFocus;
                GameVisibilityHandler.bringScannerToFront();
                Activated += eventGotFocus;

                AppendStatusText("Scanning complete, awaiting results" + Environment.NewLine
                    + "Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine, false);
                for (int i = 0; i < ThreadCount; i++)
                {
                    while (threadRunning[i] || pauseAuto)
                    {
                        if (hardCancelAuto)
                        {
                            goto hard_cancel_pos;
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                    foreach (object item in threadResults[i])
                    {
                        if (item is Artifact arti)
                        {
                            scannedArtifacts.Add(arti);
                        }
                        else if (item is Weapon wep)
                        {
                            scannedWeapons.Add(wep);
                        }
                    }
                }


                AppendStatusText("Auto finished" + Environment.NewLine
                    + " Good results: " + scannedWeapons.Count + ", Bad results: " + badResults.Count + Environment.NewLine
                    + "Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine + Environment.NewLine, false);

                while (badResults.TryDequeue(out Bitmap img))
                {
                    Rectangle area = new Rectangle(0, 0, img.Width, img.Height);
                    Bitmap filtered = new Bitmap(img);
                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
                    filtered.Save(Path.Join(Database.appDir, "images", "GenshinArtifactImg_" + timestamp + ".png"));
                    filtered = ImageProcessing.getWeaponImg(filtered, area, out int[] rows, saveImages, out bool locked, out Rectangle nameArea, out Rectangle statArea, out Rectangle refinementArea, out Rectangle charArea);
                    Weapon item = ImageProcessing.getWeapon(filtered, rows, saveImages, tessEngine, locked, nameArea, statArea, refinementArea, charArea);
                    AppendStatusText(item.ToString() + Environment.NewLine, false);
                }

                AppendStatusText("All bad results displayed" + Environment.NewLine, false);

            hard_cancel_pos:
                unregisterPauseKey();
                runtime.Stop();
                GameVisibilityHandler.bringScannerToFront();
                AppendStatusText("Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine, true);
                autoRunning = false;
            }
        }

        enum CaptureDebugMode
        {
            Off,
            FullScreen,
            GameWindow,
            ArtifactArea
        };

        private void btn_capture_Click(object sender, EventArgs e)
        {
            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.AppendText("Ignored, auto currently running" + Environment.NewLine);
                return;
            }
            resetTextBoxes();

            //Nothing = Normal, LShift + LCtrl = Game, LShift = Artifact, LCtrl = FullScreen
            CaptureDebugMode debugMode = CaptureDebugMode.Off;
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    debugMode = CaptureDebugMode.GameWindow;
                }
                else
                {
                    debugMode = CaptureDebugMode.ArtifactArea;
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                debugMode = CaptureDebugMode.FullScreen;
            }

            Rectangle directGameRect = Rectangle.Empty;
            if (debugMode != CaptureDebugMode.Off)
            {
                img_Raw = ImageProcessing.LoadScreenshot();
            }
            else
            {
                GameVisibilityHandler.captureGameProcess();
                GameVisibilityHandler.bringGameToFront();

                //try to get close estimate of game location, if successfull use that as base instead of the entire primary monitor
                bool areaObtained = GameVisibilityHandler.getGameLocation(out directGameRect);

                img_Raw = ImageProcessing.CaptureScreenshot(saveImages, directGameRect, areaObtained);
                GameVisibilityHandler.bringScannerToFront();
            }

            Rectangle? tmpGameArea = new Rectangle(0, 0, img_Raw.Width, img_Raw.Height);
            if (debugMode == CaptureDebugMode.Off || debugMode == CaptureDebugMode.FullScreen)
            {
                tmpGameArea = ImageProcessing.findGameArea(img_Raw);
                if (tmpGameArea == null)
                {
                    if (directGameRect != Rectangle.Empty)
                    {
                        //assume directGameRect is a close enough estimate, primarily in case the game is in fullscreen
                        tmpGameArea = new Rectangle(0, 0, directGameRect.Width, directGameRect.Height);
                        AppendStatusText("Window header not found, treating whole image area as game" + Environment.NewLine, false);
                    }
                    else
                    {
                        MessageBox.Show("Failed to find Game Area" + Environment.NewLine +
                            "Please make sure you're following the instructions properly."
                            + Environment.NewLine + "If the problem persists, please contact scanner dev", "Failed to find Game Area"
                            , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }

            bool ArtifactAreaCaptured = tmpGameArea != null;
            Rectangle? tmpArtifactArea = null;
            if (debugMode == CaptureDebugMode.ArtifactArea)
            {
                tmpArtifactArea = new Rectangle(0, 0, img_Raw.Width, img_Raw.Height);
            }
            else if (tmpGameArea != null)
            {
                try
                {
                    tmpArtifactArea = ImageProcessing.findArtifactArea(img_Raw, tmpGameArea.Value);
                    if (tmpArtifactArea.Value.Width == 0 || tmpArtifactArea.Value.Height == 0)
                        throw new Exception("Detected artifact are has width or height 0");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Failed to find Artifact Area" + Environment.NewLine +
                        "Please make sure you're following the instructions properly."
                        + Environment.NewLine + "If the problem persists, please contact scanner dev"
                        + Environment.NewLine + Environment.NewLine + "---" + Environment.NewLine + Environment.NewLine + "Exact error message: " + Environment.NewLine + exc.ToString(), "Failed to find Artifact Area"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ArtifactAreaCaptured = false;
                }
            }

            if (ArtifactAreaCaptured)
            {
                savedGameArea = tmpGameArea.Value;
                savedArtifactArea = tmpArtifactArea.Value;
                relativeArtifactArea = tmpArtifactArea.Value;
                if (directGameRect != Rectangle.Empty)
                {
                    savedGameArea.X = savedGameArea.X + directGameRect.X;
                    savedGameArea.Y = savedGameArea.Y + directGameRect.Y;

                    savedArtifactArea.X = savedArtifactArea.X + directGameRect.X;
                    savedArtifactArea.Y = savedArtifactArea.Y + directGameRect.Y;
                }
                btn_OCR.Enabled = true;
                button_auto.Enabled = true;
            }

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
            {
                if (tmpGameArea != null)
                {
                    Bitmap gameImg = new Bitmap(tmpGameArea.Value.Width, tmpGameArea.Value.Height);
                    using (Graphics g = Graphics.FromImage(gameImg))
                    {
                        g.DrawImage(img_Raw, 0, 0, tmpGameArea.Value, GraphicsUnit.Pixel);
                    }
                    gameImg.Save(Path.Join(Database.appDir, "images", "GenshinGameArea_" + timestamp + ".png"));
                }

                if (ArtifactAreaCaptured)
                {
                    Bitmap artifactImg = new Bitmap(tmpArtifactArea.Value.Width, tmpArtifactArea.Value.Height);
                    using (Graphics g = Graphics.FromImage(artifactImg))
                    {
                        g.DrawImage(img_Raw, 0, 0, tmpArtifactArea.Value, GraphicsUnit.Pixel);
                    }
                    artifactImg.Save(Path.Join(Database.appDir, "images", "GenshinArtifactArea_" + timestamp + ".png"));
                }
            }

            if (ArtifactAreaCaptured)
            {
                image_preview.Image = new Bitmap(tmpArtifactArea.Value.Width, tmpArtifactArea.Value.Height);
                using (Graphics g = Graphics.FromImage(image_preview.Image))
                {
                    g.DrawImage(img_Raw, 0, 0, tmpArtifactArea.Value, GraphicsUnit.Pixel);
                }
            }
        }

        private void btn_OCR_Click(object sender, EventArgs e)
        {
            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.AppendText("Ignored, auto currently running" + Environment.NewLine);
                return;
            }

            resetTextBoxes();

            bool capture = checkbox_OCRcapture.Checked || e is HotkeyEventArgs;
            if (capture)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    img_Raw = ImageProcessing.LoadScreenshot();
                    savedGameArea = new Rectangle(0, 0, img_Raw.Width, img_Raw.Height);
                    savedArtifactArea = new Rectangle(0, 0, img_Raw.Width, img_Raw.Height);
                    relativeArtifactArea = new Rectangle(0, 0, img_Raw.Width, img_Raw.Height);
                }
                else
                {
                    bool? alreadyFocused = GameVisibilityHandler.IsGameFocused();
                    GameVisibilityHandler.bringGameToFront();
                    img_Raw = ImageProcessing.CaptureScreenshot(saveImages, savedArtifactArea, GameVisibilityHandler.enabled);
                    if (alreadyFocused == false)
                    {
                        // no need to force scanner into focus if game was already focused (possible if activation is via hotkey)
                        // also no need if we don't know if the game was focused
                        GameVisibilityHandler.bringScannerToFront();
                    }
                }
            }


            img_Filtered = new Bitmap(img_Raw);

            Rectangle readArea = relativeArtifactArea;
            if (GameVisibilityHandler.enabled)
            {
                //using process handle features and the image is exactly the artifact area
                if (capture)
                {
                    readArea = new Rectangle(0, 0, img_Filtered.Width, img_Filtered.Height);
                }
                else
                {
                    readArea = relativeArtifactArea;
                }
            }

            if (checkbox_weaponMode.Checked)
            {

                img_Filtered = ImageProcessing.getWeaponImg(img_Filtered, readArea, out filtered_rows, saveImages, out bool locked, out Rectangle nameArea, out Rectangle statArea, out Rectangle refinementArea, out Rectangle charArea);
                Weapon weapon = ImageProcessing.getWeapon(img_Filtered, filtered_rows, saveImages, tessEngine, locked, nameArea, statArea, refinementArea, charArea);
                if (Database.weaponInvalid(weapon))
                {
                    displayInventoryItem(weapon);
                    text_full.AppendText(Environment.NewLine + "---This weapon is invalid---" + Environment.NewLine);
                }
                else
                {
                    scannedWeapons.Add(weapon);
                    displayInventoryItem(weapon);
                }
                text_full.AppendText(Environment.NewLine + "Total stored weapons:" + scannedWeapons.Count + Environment.NewLine);

                image_preview.Image = new Bitmap(img_Filtered);
            }
            else
            {

                img_Filtered = ImageProcessing.getArtifactImg(img_Filtered, readArea, out filtered_rows, saveImages, out bool locked, out bool astralMark, out bool elixirCrafted, out int rarity, out Rectangle typeMainArea, out Rectangle levelArea, out Rectangle subArea, out Rectangle setArea, out Rectangle charArea);
                Artifact artifact = ImageProcessing.getArtifacts(img_Filtered, filtered_rows, saveImages, tessEngine, locked, astralMark, elixirCrafted, rarity, typeMainArea, levelArea, subArea, setArea, charArea);
                if (Database.artifactInvalid(rarity, artifact))
                {
                    displayInventoryItem(artifact);
                    text_full.AppendText(Environment.NewLine + "---This artifact is invalid---" + Environment.NewLine);
                }
                else
                {
                    scannedArtifacts.Add(artifact);
                    displayInventoryItem(artifact);
                }
                text_full.AppendText(Environment.NewLine + "Total stored artifacts:" + scannedArtifacts.Count + Environment.NewLine);

                image_preview.Image = new Bitmap(img_Filtered);
            }
        }

        private void button_auto_Click(object sender, EventArgs e)
        {
            if (!IsAdministrator() && !Keyboard.IsKeyDown(Key.LeftShift))
            {
                MessageBox.Show("Cannot automatically scroll artifacts without admin perms" + Environment.NewLine + Environment.NewLine
                + "To use auto mode, restart scanner as admin",
                "Insufficient permissions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.AppendText("Ignored, auto currently running" + Environment.NewLine);
                return;
            }
            btn_OCR.Enabled = false;
            btn_capture.Enabled = false;
            button_auto.Enabled = false;
            pauseAuto = false;
            softCancelAuto = false;
            hardCancelAuto = false;

            int.TryParse(text_clickSleepWait.Text, out int clickSleepWait);
            if (clickSleepWait == 0)
                clickSleepWait = 100;
            int.TryParse(text_ScrollSleepWait.Text, out int scrollSleepWait);
            if (scrollSleepWait == 0)
                scrollSleepWait = 1500;
            int.TryParse(text_ScrollTestWait.Text, out int scrollTestWait);
            if (scrollTestWait == 0)
                scrollTestWait = 100;
            int.TryParse(text_RecheckWait.Text, out int recheckWait);
            if (recheckWait == 0)
                recheckWait = 300;
            if (checkbox_weaponMode.Checked)
            {
                weaponAuto(false, clickSleepWait, scrollSleepWait, scrollTestWait, recheckWait);
            }
            else
            {
                artifactAuto(false, clickSleepWait, scrollSleepWait, scrollTestWait, recheckWait);
            }
        }

        private void button_resume_Click(object sender, EventArgs e)
        {
            text_full.AppendText("Resuming auto" + Environment.NewLine);
            GameVisibilityHandler.bringGameToFront();
            pauseAuto = false;
        }

        private void button_softCancel_Click(object sender, EventArgs e)
        {
            text_full.AppendText("New scanning canceled, awaiting results" + Environment.NewLine);
            softCancelAuto = true;
        }

        private void button_hardCancel_Click(object sender, EventArgs e)
        {
            text_full.AppendText("Auto canceled" + Environment.NewLine);
            hardCancelAuto = true;
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            if (autoRunning)
            {
                text_full.AppendText("Ignored, auto currently running" + Environment.NewLine);
                return;
            }
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");

            JObject currData = new JObject();
            if (scannedArtifacts.Count > 0)
            {
                JObject artis = Artifact.listToGOODArtifacts(scannedArtifacts, minLevel, maxLevel, minRarity, maxRarity, exportAllEquipped, exportEquipStatus);
                currData.Add("artifacts", artis["artifacts"]);
            }

            if (scannedWeapons.Count > 0)
            {
                JObject wepData = Weapon.listToGOODWeapons(scannedWeapons, exportEquipStatus);
                currData.Add("weapons", wepData["weapons"]);
            }

            if (scannedCharacters.Count > 0)
            {
                JObject charData = Character.listToGOODCharacter(scannedCharacters);
                currData.Add("characters", charData["characters"]);
            }


            if (useTemplate && !File.Exists(Path.Join(Database.appDir, "ExportTemplate.json")))
            {
                MessageBox.Show("No export template found, exporting without one" + Environment.NewLine + "To use an export template, place valid GOOD-format json in ScannerFiles and rename to \"ExportTemplate.json\"",
                    "No export template found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                useTemplate = false;
            }
            if (useTemplate)
            {
                JObject template = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Join(Database.appDir, "ExportTemplate.json")));
                if (currData.ContainsKey("artifact"))
                {
                    template.Remove("artifacts");
                    template.Add("artifacts", currData["artifacts"]);
                }

                if (currData.ContainsKey("weapons"))
                {
                    template.Remove("weapons");
                    template.Add("weapons", currData["weapons"]);
                }

                if (currData.ContainsKey("characters"))
                {
                    template.Remove("characters");
                    template.Add("characters", currData["characters"]);
                }


                currData = template;
            }
            else
            {
                currData.Add("format", "GOOD");
                currData.Add("version", 1);
                currData.Add("source", "AdeptiScanner");
                //currData.Add("characters", new JArray());
                //currData.Add("weapons", new JArray());
            }
            string fileName = Path.Join(Database.appDir, @"Scan_Results", "export" + timestamp + ".GOOD.json");
            File.WriteAllText(fileName, currData.ToString());
            text_full.AppendText("Exported to \"" + fileName + "\"" + Environment.NewLine);

            Process.Start("explorer.exe", Path.Join(Database.appDir, "Scan_Results"));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process myProcess = new Process();

            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://github.com/D1firehail/AdeptiScanner-GI";
            myProcess.Start();
        }

        private void loadSettings()
        {
            string fileName = Path.Join(Database.appDir, "settings.json");
            JObject settings = null;
            try
            {
                settings = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(fileName));
            }
            catch (Exception)
            {
                return;
            }

            if (settings.ContainsKey("TravelerName"))
            {
                string readTravelerName = settings["TravelerName"].ToObject<string>();
                Database.SetCharacterName(readTravelerName, "Traveler");
                travelerName = readTravelerName;
            }
            if (settings.ContainsKey("WandererName"))
            {
                string readWandererName = settings["WandererName"].ToObject<string>();
                Database.SetCharacterName(readWandererName, "Wanderer");
                wandererName = readWandererName;
            }
            if (settings.ContainsKey("FilterMinLevel"))
            {
                minLevel = settings["FilterMinLevel"].ToObject<int>();
            }
            if (settings.ContainsKey("FilterMaxLevel"))
            {
                maxLevel = settings["FilterMaxLevel"].ToObject<int>();
            }
            if (settings.ContainsKey("FilterMinRarity"))
            {
                minRarity = settings["FilterMinRarity"].ToObject<int>();
            }
            if (settings.ContainsKey("FilterMaxRarity"))
            {
                maxRarity = settings["FilterMaxRarity"].ToObject<int>();
            }
            if (settings.ContainsKey("ExportUseTemplate"))
            {
                useTemplate = settings["ExportUseTemplate"].ToObject<bool>();
            }
            if (settings.ContainsKey("ExportAllEquipped"))
            {
                exportAllEquipped = settings["ExportAllEquipped"].ToObject<bool>();
            }
            if (settings.ContainsKey("CaptureOnRead"))
            {
                captureOnread = settings["CaptureOnRead"].ToObject<bool>();
            }
            if (settings.ContainsKey("saveImagesGlobal"))
            {
                saveImagesGlobal = settings["saveImagesGlobal"].ToObject<bool>();
            }
            if (settings.ContainsKey("clickSleepWait"))
            {
                clickSleepWait_load = settings["clickSleepWait"].ToObject<string>();
            }
            if (settings.ContainsKey("scrollSleepWait"))
            {
                scrollSleepWait_load = settings["scrollSleepWait"].ToObject<string>();
            }
            if (settings.ContainsKey("scrollTestWait"))
            {
                scrollTestWait_load = settings["scrollTestWait"].ToObject<string>();
            }
            if (settings.ContainsKey("recheckWait"))
            {
                recheckWait_load = settings["recheckWait"].ToObject<string>();
            }
            if (settings.ContainsKey("updateData"))
            {
                updateData = settings["updateData"].ToObject<bool>();
            }
            if (settings.ContainsKey("updateVersion"))
            {
                updateVersion = settings["updateVersion"].ToObject<bool>();
            }
            if (settings.ContainsKey("ignoredDataVersion"))
            {
                ignoredDataVersion = settings["ignoredDataVersion"].ToObject<string>();
            }
            if (settings.ContainsKey("ignoredProgramVersion"))
            {
                ignoredProgramVersion = settings["ignoredProgramVersion"].ToObject<string>();
            }
            if (settings.ContainsKey("lastUpdateCheck"))
            {
                lastUpdateCheck = settings["lastUpdateCheck"].ToObject<string>();
            }
            if (settings.ContainsKey("processHandleInteractions"))
            {
                GameVisibilityHandler.enabled = settings["processHandleInteractions"].ToObject<bool>();
            }
            if (settings.ContainsKey("uid"))
            {
                uid = settings["uid"].ToObject<string>();
            }
            if (settings.ContainsKey("ExportEquipStatus"))
            {
                exportEquipStatus = settings["ExportEquipStatus"].ToObject<bool>();
            }
        }

        private void finalizeLoadSettings()
        {
            text_traveler.Text = travelerName;
            text_wanderer.Text = wandererName;
            checkbox_OCRcapture.Checked = captureOnread;
            checkbox_saveImages.Checked = saveImagesGlobal;
            text_clickSleepWait.Text = clickSleepWait_load;
            text_ScrollSleepWait.Text = scrollSleepWait_load;
            text_ScrollTestWait.Text = scrollTestWait_load;
            text_RecheckWait.Text = recheckWait_load;
            enkaTab.text_UID.Text = uid;
            if (updateData.HasValue)
                checkBox_updateData.Checked = updateData.Value;
            if (updateVersion.HasValue)
                checkBox_updateVersion.Checked = updateVersion.Value;
            checkBox_ProcessHandleFeatures.Checked = GameVisibilityHandler.enabled;

        }

        public void SetUpdatePreferences(bool updateData, bool updateVersion)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool, bool>(SetUpdatePreferences), new object[] { updateData, updateVersion });
                return;
            }
            this.updateData = updateData;
            this.updateVersion = updateVersion;
            checkBox_updateData.Checked = updateData;
            checkBox_updateVersion.Checked = updateVersion;
        }

        public void UpdateCharacterList(List<Character> characterList)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<List<Character>>(UpdateCharacterList), new object[] { characterList });
                return;
            }

            int beforeCount = scannedCharacters.Count;
            foreach (Character character in characterList)
            {
                //remove any old copy of the character, then add new one
                scannedCharacters = scannedCharacters.Where(c => !character.key.Equals(c.key)).ToList();
                scannedCharacters.Add(character);
            }

            int diff = scannedCharacters.Count - beforeCount;

            enkaTab.UpdateMissingChars(scannedArtifacts, scannedWeapons, scannedCharacters);

            AppendStatusText("New character info: " + diff + " added, " + (characterList.Count - diff) + " updated, " + scannedCharacters.Count + " total" + Environment.NewLine, false);
        }

        private void searchForUpdates(bool isManual)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (timestamp == lastUpdateCheck && !isManual)
                return;
            lastUpdateCheck = timestamp;
            HttpClient webclient = new HttpClient();
            webclient.DefaultRequestHeaders.Add("user-agent", "AdeptiScanner");

            string programVersionTitle = "";
            string programVersionBody = "";
            bool programVersionPrerelease = false;
            bool programVersionDraft = false;

            if (isManual || (this.updateVersion.HasValue && this.updateVersion.Value))
            {
                try
                {
                    var request = webclient.GetStringAsync("https://api.github.com/repos/D1firehail/AdeptiScanner-GI/releases");
                    bool requestCompleted = request.Wait(TimeSpan.FromMinutes(1));
                    if (!requestCompleted)
                    {
                        throw new TimeoutException("Version update check did not complete within 1 minute, ignoring");
                    }
                    string response = request.Result;
                    JArray releases = JsonConvert.DeserializeObject<JArray>(response);
                    if (releases.First.HasValues)
                    {
                        JObject latest = releases.First.Value<JObject>();
                        programVersionTitle = latest["tag_name"].ToObject<string>();

                        programVersionPrerelease = latest["prerelease"].ToObject<bool>();

                        programVersionDraft = latest["draft"].ToObject<bool>();

                        programVersionBody = latest["body"].ToObject<string>();

                    }
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc.ToString());
                }
                if (programVersionPrerelease || programVersionDraft || programVersionTitle.ToLower().Equals("v" + Database.programVersion))
                {
                    programVersionTitle = "";
                }
            }

            string dataVersionString = "";
            string dataVersionJson = "";

            if (isManual || (this.updateData.HasValue && this.updateData.Value))
            {
                try
                {
                    var request = webclient.GetStringAsync("https://raw.githubusercontent.com/D1firehail/AdeptiScanner-GI/master/AdeptiScanner%20GI/ScannerFiles/ArtifactInfo.json");
                    bool requestCompleted = request.Wait(TimeSpan.FromMinutes(1));
                    if (!requestCompleted)
                    {
                        throw new TimeoutException("Data update check did not complete within 1 minute, ignoring");
                    }
                    string response = request.Result;
                    JObject artifactInfo = JsonConvert.DeserializeObject<JObject>(response);
                    dataVersionString = artifactInfo["DataVersion"].ToObject<string>();
                    dataVersionJson = response;
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc.ToString());
                }
                if (dataVersionString.Equals(Database.dataVersion))
                {
                    dataVersionString = "";
                }
            }
            if ((programVersionTitle.Length > 0 && (isManual || programVersionTitle != ignoredProgramVersion))
                || (dataVersionString.Length > 0 && (isManual || dataVersionString != ignoredDataVersion)))
            {
                UpdatePrompt tmp = new UpdatePrompt(programVersionTitle, programVersionBody, dataVersionString, dataVersionJson);
                tmp.Show();
            }
            else if (isManual)
            {
                MessageBox.Show("No updates found",
                "Update checker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        internal void executeDataUpdate(string newData)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(executeDataUpdate), new object[] { newData });
                return;
            }


            try
            {
                File.WriteAllText(Path.Join(Database.appDir, "ArtifactInfo.json"), newData);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Update failed, error: " + Environment.NewLine + Environment.NewLine + exc.ToString(), "Update failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Application.Restart();
        }

        internal void readyVersionUpdate()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(readyVersionUpdate));
                return;
            }


            saveSettings();

            if (!Directory.Exists(Database.appdataPath))
                Directory.CreateDirectory(Database.appdataPath);

            string[] filesToCopy = { "settings.json", "ExportTemplate.json" };

            foreach (string file in filesToCopy)
            {
                string dirPath = Path.Join(Database.appDir, file);
                string appDataPath = Path.Join(Database.appdataPath, file);
                if (!File.Exists(dirPath))
                    continue;
                if (File.Exists(appDataPath))
                    File.Delete(appDataPath);
                File.Copy(dirPath, appDataPath);
            }
            Process myProcess = new Process();

            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://github.com/D1firehail/AdeptiScanner-GI/releases/latest";
            myProcess.Start();

            Application.Exit();
        }

        internal void setIgnoredVersions(string dataVersion, string programVersion)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string>(setIgnoredVersions), new object[] { dataVersion, programVersion });
                return;
            }
            if (dataVersion.Length > 0)
                ignoredDataVersion = dataVersion;
            if (programVersion.Length > 0)
                ignoredProgramVersion = programVersion;
        }

        private void saveSettings()
        {
            JObject settings = new JObject();
            settings["TravelerName"] = text_traveler.Text;
            settings["WandererName"] = text_wanderer.Text;
            settings["FilterMinLevel"] = minLevel;
            settings["FilterMaxLevel"] = maxLevel;
            settings["FilterMinRarity"] = minRarity;
            settings["FilterMaxRarity"] = maxRarity;
            settings["ExportUseTemplate"] = useTemplate;
            settings["ExportAllEquipped"] = exportAllEquipped;
            settings["CaptureOnRead"] = captureOnread;
            settings["saveImagesGlobal"] = saveImagesGlobal;
            settings["clickSleepWait"] = text_clickSleepWait.Text;
            settings["scrollSleepWait"] = text_ScrollSleepWait.Text;
            settings["scrollTestWait"] = text_ScrollTestWait.Text;
            settings["recheckWait"] = text_RecheckWait.Text;
            if (updateData.HasValue)
                settings["updateData"] = updateData.Value;
            if (updateVersion.HasValue)
                settings["updateVersion"] = updateVersion.Value;
            settings["ignoredDataVersion"] = ignoredDataVersion;
            settings["ignoredProgramVersion"] = ignoredProgramVersion;
            settings["lastUpdateCheck"] = lastUpdateCheck;
            settings["processHandleInteractions"] = GameVisibilityHandler.enabled;
            settings["uid"] = enkaTab.text_UID.Text;
            settings["ExportEquipStatus"] = exportEquipStatus;


            string fileName = Path.Join(Database.appDir, "settings.json");
            File.WriteAllText(fileName, settings.ToString());
        }


        private void text_traveler_TextChanged(object sender, EventArgs e)
        {
            travelerName = text_traveler.Text;
            Database.SetCharacterName(travelerName, "Traveler");
        }


        private void text_wanderer_TextChanged(object sender, EventArgs e)
        {

            wandererName = text_wanderer.Text;
            Database.SetCharacterName(wandererName, "Wanderer");
        }

        private void checkbox_OCRcapture_CheckedChanged(object sender, EventArgs e)
        {
            captureOnread = checkbox_OCRcapture.Checked;
        }

        private void checkbox_saveImages_CheckedChanged(object sender, EventArgs e)
        {
            saveImagesGlobal = checkbox_saveImages.Checked;
        }

        private void button_loadArtifacts_Click(object sender, EventArgs e)
        {
            if (autoRunning)
            {
                text_full.AppendText("Ignored, auto currently running" + Environment.NewLine);
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Database.appDir;
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        try
                        {
                            int startArtiAmount = scannedArtifacts.Count();
                            JObject GOODjson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(file));
                            if (GOODjson.ContainsKey("artifacts"))
                            {
                                JArray artifacts = GOODjson["artifacts"].ToObject<JArray>();
                                foreach (JObject artifact in artifacts)
                                {
                                    Artifact importedArtifact = Artifact.fromGOODArtifact(artifact);
                                    if (importedArtifact != null)
                                    {
                                        scannedArtifacts.Add(importedArtifact);
                                    }
                                }
                            }
                            int endArtiAmount = scannedArtifacts.Count();
                            text_full.AppendText("Imported " + (endArtiAmount - startArtiAmount) + " aritfacts (new total " + endArtiAmount + ") from file: " + file + Environment.NewLine);
                            break;
                        }
                        catch (Exception exc)
                        {
                            text_full.AppendText("Error importing from file: " + file + Environment.NewLine);
                            Debug.WriteLine(exc);
                        }
                    }
                }
            }
        }

        private void button_resetSettings_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will make all settings return to default the next time the scanner is started" + Environment.NewLine + "Are you sure?", "Remove saved Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(Path.Join(Database.appDir, "settings.json"));
                rememberSettings = false;
            }
        }

        private void checkBox_updateData_CheckedChanged(object sender, EventArgs e)
        {
            updateData = checkBox_updateData.Checked;
        }

        private void checkBox_updateVersion_CheckedChanged(object sender, EventArgs e)
        {
            updateVersion = checkBox_updateVersion.Checked;
        }

        private void button_checkUpdateManual_Click(object sender, EventArgs e)
        {
            searchForUpdates(true);
        }

        private void checkBox_ProcessHandleFeatures_CheckedChanged(object sender, EventArgs e)
        {
            GameVisibilityHandler.enabled = checkBox_ProcessHandleFeatures.Checked;
            btn_OCR.Enabled = false;
            button_auto.Enabled = false;
        }

        private void checkbox_weaponMode_CheckedChanged(object sender, EventArgs e)
        {
            btn_OCR.Enabled = false;
            button_auto.Enabled = false;
        }

        private void checkBox_readHotkey_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_readHotkey.Checked)
            {

                registerReadKey();
                if (!IsAdministrator())
                {
                    AppendStatusText(Environment.NewLine + "Read hotkey enabled, HOWEVER while the game is focused it only works if you RUN AS ADMIN." + Environment.NewLine, false);
                }
            }
            else
            {
                unregisterReadKey();
            }
        }
    }
}
