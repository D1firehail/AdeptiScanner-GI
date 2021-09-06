using System;
using System.Collections.Generic;
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
        private bool cancelAuto = true;
        bool autoRunning = false;
        List<InventoryItem> scannedItems = new List<InventoryItem>();


        private static InputSimulator sim = new InputSimulator();

        public GenshinArtifactOCR()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += eventKeyDown;
            KeyUp += eventKeyUp;
            Activated += eventGotFocus;
            tessEngine = new TesseractEngine(Database.appDir + @"/tessdata", "en")
            {
                DefaultPageSegMode = PageSegMode.SingleLine
            };
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
            if (!cancelAuto)
            {
                cancelAuto = true;
                text_full.Text += "Cancelling auto" + Environment.NewLine;
            }
        }

        private void eventKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Escape)
            {
                cancelAuto = true;
            }
        }

        private void eventKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cancelAuto = false;
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

        private void runAuto(bool saveImages, int clickSleepDuration = 100, int scrollSleepDuration = 30)
        {
            if (autoRunning)
            {
                text_full.Text += "Ignored, auto currently running" + Environment.NewLine;
            }
            text_full.Text = "Starting auto-run. HOLD ESCAPE TO CANCEL" + Environment.NewLine + "If no artifact switching happens, you forgot to run as admin" + Environment.NewLine;
            autoRunning = true;
            scannedItems = new List<InventoryItem>();
            Task.Run((() =>
            {
                //TODO: do OCR or image capture on each step, save results somewhere, auto-scroll, refined and random sleep duration
                bool running = true;
                bool firstRun = true;
                int firstY = 0;
                const int maxIterations = 20;
                const int scrollsPerIteration = 2;
                while (running)
                {
                    //load current grid/scroll location
                    Bitmap screen = ImageProcessing.CaptureScreenshot(saveImages);
                    List<Point> artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(screen, savedGameArea, savedArtifactArea, saveImages);
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

                    Console.WriteLine("startbot: " + startBot + ", starttop: " + startTop + ", points: " + artifactLocations.Count);

                    if (!firstRun) 
                    {
                        if (cancelAuto)
                        {
                            autoRunning = false;
                            return;
                        }
                        //test scroll distance
                        sim.Mouse.VerticalScroll(-1);
                        System.Threading.Thread.Sleep(100);
                        screen = ImageProcessing.CaptureScreenshot(saveImages);
                        artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(screen, savedGameArea, savedArtifactArea, saveImages);

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
                        System.Threading.Thread.Sleep(100);
                        screen = ImageProcessing.CaptureScreenshot(saveImages);
                        artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(screen, savedGameArea, savedArtifactArea, saveImages);

                        //fine adjustments
                        while (artifactLocations.Count > 0 && artifactLocations[0].Y <= firstY - distPerScroll) 
                        {
                            if (cancelAuto)
                            {
                                autoRunning = false;
                                return;
                            }
                            sim.Mouse.VerticalScroll(1);
                            System.Threading.Thread.Sleep(scrollSleepDuration);
                            screen = ImageProcessing.CaptureScreenshot(false);
                            artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(screen, savedGameArea, savedArtifactArea, false);
                        }
                        while (artifactLocations.Count > 0 && artifactLocations[0].Y > firstY - distPerScroll)
                        {
                            if (cancelAuto)
                            {
                                autoRunning = false;
                                return;
                            }
                            sim.Mouse.VerticalScroll(-1);
                            System.Threading.Thread.Sleep(scrollSleepDuration);
                            screen = ImageProcessing.CaptureScreenshot(false);
                            artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(screen, savedGameArea, savedArtifactArea, false);
                        }

                        int tmp = artifactLocations.Count > 0 ?  artifactLocations[0].Y + 50 : 99999;
                        Console.WriteLine("tmp: " + tmp);
                        for (int i = 0; i < artifactLocations.Count; i++)
                        {
                            if (artifactLocations[i].Y < tmp)
                            {
                                Console.WriteLine("Removing " + artifactLocations[i].ToString());
                                artifactLocations.RemoveAt(i);
                                i--;
                            }
                        }
                    }


                    firstRun = false;

                    //select and OCR each artifact in list
                    foreach (Point p in artifactLocations)
                    {
                        if (cancelAuto)
                        {
                            autoRunning = false;
                            return;
                        }
                        System.Threading.Thread.Sleep(clickSleepDuration);
                        clickPos(p.X, p.Y - 10);
                        Console.WriteLine("Clicked " + p.ToString());
                        //Bitmap img = ImageProcessing.CaptureScreenshot(saveImages);
                        //Bitmap filtered = ImageProcessing.getArtifactImg_WindowMode(img, savedArtifactArea, out int[] rows, saveImages);
                        InventoryItem item = new InventoryItem();//ImageProcessing.getArtifacts(filtered, rows, saveImages, tessEngine);
                        scannedItems.Add(item);
                        //do something with item
                    }
                    Console.WriteLine("Total items: " + scannedItems.Count);

                }

                Console.WriteLine("Auto Done");
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
                img_Raw = ImageProcessing.CaptureScreenshot(saveImages);
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

            List<Point> artifactLocations = ImageProcessing.getArtifactGrid_WindowMode(img_Raw, savedGameArea, savedArtifactArea, saveImages);

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
                    img_Raw = ImageProcessing.CaptureScreenshot(saveImages);
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

            image_preview.Image = new Bitmap(img_Filtered);

            InventoryItem artifact = ImageProcessing.getArtifacts(img_Filtered, filtered_rows, saveImages, tessEngine, artifactLocked);
            displayInventoryItem(artifact);
        }

        private void button_auto_Click(object sender, EventArgs e)
        {
            bool saveImages = checkbox_saveImages.Checked;
            if (autoRunning)
            {
                text_full.Text += "Ignored, auto currently running" + Environment.NewLine;
            }
            cancelAuto = false;
            runAuto(false, 50, 17);
        }
    }
}
