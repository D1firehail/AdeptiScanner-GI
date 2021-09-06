using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Tesseract;
using WindowsInput;

namespace GenshinArtifactOCR
{
    public partial class GenshinArtifactOCR : Form
    {
        private TesseractEngine tessEngine;
        private Bitmap img_Raw;
        private Bitmap img_Filtered;
        private int[] filtered_rows;
        private Rectangle savedArtifactArea;
        private Rectangle savedGameArea;
        private bool pauseAuto = true;
        private bool softCancelAuto = true;
        private bool hardCancelAuto = true;


        bool autoRunning = false;
        bool autoCaptureDone = false;
        List<InventoryItem> scannedItems = new List<InventoryItem>();
        bool cancelOCRThreads = false;
        const int ThreadCount = 4;
        private bool[] threadRunning = new bool[ThreadCount];
        private ConcurrentQueue<Bitmap>[] threadQueues = new ConcurrentQueue<Bitmap>[ThreadCount];
        private TesseractEngine[] threadEngines = new TesseractEngine[ThreadCount];
        private List<InventoryItem>[] threadResults = new List<InventoryItem>[ThreadCount];


        private static InputSimulator sim = new InputSimulator();

        public GenshinArtifactOCR()
        {
            InitializeComponent();
            Activated += eventGotFocus;
            tessEngine = new TesseractEngine(Database.appDir + @"/tessdata", "en")
            {
                DefaultPageSegMode = PageSegMode.SingleLine
            };

            //worker thread stuff
            for (int i = 0; i < ThreadCount; i++)
            {
                threadRunning[i] = false;
                threadQueues[i] = new ConcurrentQueue<Bitmap>();
                threadEngines[i] = new TesseractEngine(Database.appDir + @"/tessdata", "en")
                {
                    DefaultPageSegMode = PageSegMode.SingleLine
                };
                threadResults[i] = new List<InventoryItem>();
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
        }

        private void eventGotFocus(object sender, EventArgs e)
        {
            if (!pauseAuto)
            {
                pauseAuto = true;
                text_full.Text += "Auto scanning paused, select action" + Environment.NewLine;
            }
        }

        public void AppendStatusText(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendStatusText), new object[] { value });
                return;
            }
            text_full.Text += value;
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
        private void resetTextBoxes ()
        {
            text_full.Text = "";
            text_Set.Text = "";
            text_Level.Text = "";
            text_Type.Text = "";
            text_statMain.Text = "";
            text_statSub1.Text = "";
            text_statSub2.Text = "";
            text_statSub3.Text = "";
            text_statSub4.Text = "";
            text_character.Text = "";
        }

        private void displayInventoryItem (InventoryItem item)
        {
            text_full.Text = "";
            if (item.level != null)
            {
                text_full.Text += item.level.Item1 + Environment.NewLine;
                text_Level.Text = item.level.Item1;
            }
            text_full.Text += "Locked: " + item.locked.ToString() + Environment.NewLine;
            text_locked.Text = item.locked.ToString();

            if (item.piece != null)
            {
                text_full.Text += item.piece.Item1 + Environment.NewLine;
                text_Type.Text = item.piece.Item1;
            }
            if (item.main != null)
            {
                text_full.Text += item.main.Item1 + Environment.NewLine;
                text_statMain.Text = item.main.Item1;
            }
            if (item.subs != null)
            {
                if (item.subs.Count > 0)
                {
                    text_full.Text += item.subs[0].Item1 + Environment.NewLine;
                    text_statSub1.Text = item.subs[0].Item1;
                }
                if (item.subs.Count > 1)
                {
                    text_full.Text += item.subs[1].Item1 + Environment.NewLine;
                    text_statSub2.Text = item.subs[1].Item1;
                }
                if (item.subs.Count > 2)
                {
                    text_full.Text += item.subs[2].Item1 + Environment.NewLine;
                    text_statSub3.Text = item.subs[2].Item1;
                }
                if (item.subs.Count > 3)
                {
                    text_full.Text += item.subs[3].Item1 + Environment.NewLine;
                    text_statSub4.Text = item.subs[3].Item1;
                }
            }
            if (item.set != null)
            {
                text_full.Text += item.set.Item1 + Environment.NewLine;
                text_Set.Text = item.set.Item1;
            }
            if (item.character != null)
            {
                text_full.Text += item.character.Item1 + Environment.NewLine;
                text_character.Text = item.character.Item1;
            }
        }

        private void runOCRThread(int threadIndex)
        {
            Task.Run((() =>
            {
                threadRunning[threadIndex] = true;
                bool saveImages = false;
                while ( autoRunning && !cancelOCRThreads )
                {
                    if ( threadQueues[threadIndex].TryDequeue(out Bitmap img))
                    {
                        Bitmap filtered = ImageProcessing.getArtifactImg_WindowMode(img, savedArtifactArea, out int[] rows, saveImages, out bool locked);
                        InventoryItem item = ImageProcessing.getArtifacts(filtered, rows, saveImages, threadEngines[threadIndex], locked);
                        threadResults[threadIndex].Add(item);
                    } else if (autoCaptureDone || softCancelAuto || hardCancelAuto)
                    {
                        Console.WriteLine("Thread " + threadIndex + " EXITING----------");
                        threadRunning[threadIndex] = false;
                        return;
                    } else 
                    {
                        Console.WriteLine("Thread " + threadIndex + " sleeping");
                        System.Threading.Thread.Sleep(1000);
                    }
                }

                Console.WriteLine("Thread " + threadIndex + " EXITING----------");
                threadRunning[threadIndex] = false;

            }));
        }

        private void runAuto(bool saveImages, int clickSleepDuration = 100, int scrollSleepDuration = 30)
        {
            text_full.Text = "Starting auto-run. ALT TAB TO PAUSE/CANCEL" + Environment.NewLine + "If no artifact switching happens, you forgot to run as admin" + Environment.NewLine;
            autoRunning = true;
            autoCaptureDone = false;
            //start worker threads
            for (int i = 0; i < ThreadCount; i++)
            {
                threadQueues[i] = new ConcurrentQueue<Bitmap>();
                threadResults[i] = new List<InventoryItem>();
                runOCRThread(i);
            }

            Task.Run((() =>
            {
                Stopwatch runtime = new Stopwatch();
                runtime.Start();
                //TODO: do OCR or image capture on each step, save results somewhere, auto-scroll, refined and random sleep duration
                bool running = true;
                bool firstRun = true;
                int firstY = 0;
                int nextThread = 0;
                Rectangle gridArea = new Rectangle(savedGameArea.X, savedGameArea.Y, savedArtifactArea.X - savedGameArea.X, savedGameArea.Height);
                Point gridOffset = new Point(gridArea.X, gridArea.Y);
                while (running)
                {
                    //load current grid/scroll location
                    Bitmap img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                    List<Point> artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img, saveImages, gridOffset);
                    if (artifactLocations.Count < 3)
                    {
                        running = false;
                        break;
                    }
                    int startTop = artifactLocations[0].Y;
                    if (firstRun)
                    {
                        firstY = startTop;
                    }
                    int startBot = artifactLocations[artifactLocations.Count - 1].Y;
                    int distToScroll = startBot - startTop;

                    if (!firstRun) 
                    {
                        while (pauseAuto)
                        {
                            if (hardCancelAuto)
                            {
                                autoRunning = false;
                                return;
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
                        System.Threading.Thread.Sleep(100);
                        img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                        artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img, saveImages, gridOffset);

                        int distPerScroll = startTop - artifactLocations[0].Y;
                        int scrollsNeeded = 0;
                        if ( distPerScroll <= 0)
                        {
                            running = false;
                        } else
                        {
                            scrollsNeeded = distToScroll / distPerScroll - 1;
                        }

                        if (scrollsNeeded <= 0)
                        {
                            running = false;
                        }

                        while (scrollsNeeded > 0)
                        {
                            sim.Mouse.VerticalScroll(-1);
                            scrollsNeeded--;
                        }
                        System.Threading.Thread.Sleep(scrollSleepDuration);
                        img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                        artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img, saveImages, gridOffset);

                        //fine adjustments
                        while (artifactLocations.Count > 0 && artifactLocations[0].Y <= firstY - distPerScroll) 
                        {
                            while (pauseAuto)
                            {
                                if (hardCancelAuto)
                                {
                                    autoRunning = false;
                                    return;
                                }
                                if (softCancelAuto)
                                {
                                    running = false;
                                    pauseAuto = false;
                                    goto soft_cancel_pos;
                                }
                                System.Threading.Thread.Sleep(1000);
                            }
                            sim.Mouse.VerticalScroll(1);
                            System.Threading.Thread.Sleep(scrollSleepDuration);
                            img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                            artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img, saveImages, gridOffset);
                        }
                        while (artifactLocations.Count > 0 && artifactLocations[0].Y > firstY - distPerScroll)
                        {
                            while (pauseAuto)
                            {
                                if (hardCancelAuto)
                                {
                                    autoRunning = false;
                                    return;
                                }
                                if (softCancelAuto)
                                {
                                    running = false;
                                    pauseAuto = false;
                                    goto soft_cancel_pos;
                                }
                                System.Threading.Thread.Sleep(1000);
                            }
                            sim.Mouse.VerticalScroll(-1);
                            System.Threading.Thread.Sleep(scrollSleepDuration);
                            img = ImageProcessing.CaptureScreenshot(saveImages, gridArea, true);
                            artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img, saveImages, gridOffset);
                        }

                        int tmp = artifactLocations.Count > 0 ?  artifactLocations[0].Y + 50 : 99999;
                        for (int i = 0; i < artifactLocations.Count; i++)
                        {
                            if (artifactLocations[i].Y < tmp)
                            {
                                artifactLocations.RemoveAt(i);
                                i--;
                            }
                        }
                    }


                    firstRun = false;

                    //select and OCR each artifact in list
                    foreach (Point p in artifactLocations)
                    {
                        while (pauseAuto)
                        {
                            if (hardCancelAuto)
                            {
                                autoRunning = false;
                                return;
                            }
                            System.Threading.Thread.Sleep(1000);
                        }
                        clickPos(p.X, p.Y - 10);
                        System.Threading.Thread.Sleep(clickSleepDuration);

                        //queue up processing of artifact
                        threadQueues[nextThread].Enqueue(ImageProcessing.CaptureScreenshot(saveImages, savedArtifactArea, true));
                        nextThread = (nextThread + 1) % ThreadCount;
                    }

                }

                soft_cancel_pos:

                autoCaptureDone = true;
                for (int i = 0; i < ThreadCount; i++)
                {
                    while (threadRunning[i] || pauseAuto)
                    {
                        if (hardCancelAuto)
                        {
                            autoRunning = false;
                            return;
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                    foreach (InventoryItem item in threadResults[i])
                    {
                        scannedItems.Add(item);
                    }
                }

                runtime.Stop();
                AppendStatusText("Auto finished, items scanned: " + scannedItems.Count + Environment.NewLine
                    + "Time elapsed: " + runtime.ElapsedMilliseconds + "ms" + Environment.NewLine);


                autoRunning = false;
            }));
        }

        

        private void btn_capture_Click(object sender, EventArgs e)
        {
            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.Text += "Ignored, auto currently running" + Environment.NewLine;
            }
            resetTextBoxes();
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                img_Raw = ImageProcessing.LoadScreenshot();
            } else
            {
                img_Raw = ImageProcessing.CaptureScreenshot(saveImages, Rectangle.Empty);
            }


            savedGameArea = ImageProcessing.findGameArea(img_Raw);
            if (checkbox_inventorymode.Checked)
            {
                savedArtifactArea = ImageProcessing.findArtifactArea_WindowMode(img_Raw, savedGameArea);
            } else
            {
                savedArtifactArea = ImageProcessing.findArtifactArea(img_Raw, savedGameArea);
            }
            if (savedArtifactArea.Width == 0 || savedArtifactArea.Height == 0)
            {
                savedArtifactArea = savedGameArea;
                image_preview.Image = new Bitmap(img_Raw);
            }

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
            {
                Bitmap gameImg = new Bitmap(savedGameArea.Width, savedGameArea.Height);
                using (Graphics g = Graphics.FromImage(gameImg))
                {
                    g.DrawImage(img_Raw, 0, 0, savedGameArea, GraphicsUnit.Pixel);
                }
                gameImg.Save(Database.appDir + @"\images\GenshinGameArea " + timestamp + ".png");
                Bitmap artifactImg = new Bitmap(savedArtifactArea.Width, savedArtifactArea.Height);
                using (Graphics g = Graphics.FromImage(artifactImg))
                {
                    g.DrawImage(img_Raw, 0, 0, savedArtifactArea, GraphicsUnit.Pixel);
                }
                artifactImg.Save(Database.appDir + @"\images\GenshinArtifactArea " + timestamp + ".png");
            }

            //List<Point> artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img_Raw, savedGameArea, savedArtifactArea, saveImages);

            if (savedArtifactArea.Width == 0 || savedArtifactArea.Height == 0)
            {
                image_preview.Image = new Bitmap(img_Raw);
            }
            image_preview.Image = new Bitmap(savedArtifactArea.Width, savedArtifactArea.Height);
            using (Graphics g = Graphics.FromImage(image_preview.Image))
            {
                g.DrawImage(img_Raw, 0, 0, savedArtifactArea, GraphicsUnit.Pixel);
            }

            text_full.Text += "Items found: " + scannedItems.Count + Environment.NewLine;
        }

        private void btn_OCR_Click(object sender, EventArgs e)
        {
            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.Text += "Ignored, auto currently running" + Environment.NewLine;
            }

            if (checkbox_OCRcapture.Checked)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    img_Raw = ImageProcessing.LoadScreenshot();
                }
                else
                {
                    img_Raw = ImageProcessing.CaptureScreenshot(saveImages, Rectangle.Empty);
                }
            }


            img_Filtered = new Bitmap(img_Raw);
            bool artifactLocked = false;
            if (checkbox_inventorymode.Checked)
            {
                img_Filtered = ImageProcessing.getArtifactImg_WindowMode(img_Filtered, savedArtifactArea, out filtered_rows, saveImages, out artifactLocked);
            } else
            {
                img_Filtered = ImageProcessing.getArtifactImg(img_Filtered, savedArtifactArea, out filtered_rows, saveImages);
            }

            InventoryItem artifact = ImageProcessing.getArtifacts(img_Filtered, filtered_rows, saveImages, tessEngine, artifactLocked);

            image_preview.Image = new Bitmap(img_Filtered);
            displayInventoryItem(artifact);
        }

        private void button_auto_Click(object sender, EventArgs e)
        {
            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.Text += "Ignored, auto currently running" + Environment.NewLine;
            }
            pauseAuto = false;
            softCancelAuto = false;
            hardCancelAuto = false;
            runAuto(false, 50, 17);
        }

        private void button_resume_Click(object sender, EventArgs e)
        {
            text_full.Text += "Resuming auto" + Environment.NewLine;
            pauseAuto = false;
        }

        private void button_softCancel_Click(object sender, EventArgs e)
        {
            text_full.Text += "New scanning canceled, awaiting results" + Environment.NewLine;
            softCancelAuto = true;
        }

        private void button_hardCancel_Click(object sender, EventArgs e)
        {
            text_full.Text += "Auto canceled" + Environment.NewLine;
            hardCancelAuto = true;
        }
    }
}
