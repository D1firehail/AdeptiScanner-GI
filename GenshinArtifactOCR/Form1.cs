using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Tesseract;

namespace GenshinArtifactOCR
{
    public partial class Form1 : Form
    {
        private TesseractEngine tessEngine;
        private Bitmap img_Raw;
        private Bitmap img_Filtered;

        private static string appDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WFInfo"; //Use WFInfo tesseract version for now
        public Form1()
        {
            InitializeComponent();
            tessEngine = new TesseractEngine(appDir + @"/tessdata", "en")
            {
                DefaultPageSegMode = PageSegMode.SingleLine
            };
            img_Raw = new Bitmap(image_preview.Width, image_preview.Height);
            using (Graphics g = Graphics.FromImage(img_Raw))
            {
                g.FillRectangle(Brushes.Black, 0, 0, img_Raw.Width, img_Raw.Height);
                g.FillRectangle(Brushes.White, img_Raw.Width / 8, img_Raw.Height / 8, img_Raw.Width * 6 / 8, img_Raw.Height * 6 / 8);
            }
            img_Filtered = new Bitmap(img_Raw);
            image_preview.Image = new Bitmap(img_Raw);
        }

        private Bitmap filterAndGetArtifactArea(Bitmap img)
        {
            int[] cols = new int[img.Width];
            BitmapData imgData = img_Filtered.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, img.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % img.Width;
                int y = (i / PixelSize - x) % img.Height;
                if (imgBytes[i + 1] > 220 || (imgBytes[i] > 170 && imgBytes[i + 1] > 170 && imgBytes[i + 2] > 170))
                {
                    //Make Black
                    imgBytes[i] = 0;
                    imgBytes[i + 1] = 0;
                    imgBytes[i + 2] = 0;
                    imgBytes[i + 3] = 255;
                    cols[x]++;
                }
                else
                {
                    //Make White
                    imgBytes[i] = 255;
                    imgBytes[i + 1] = 255;
                    imgBytes[i + 2] = 255;
                    imgBytes[i + 3] = 255;
                }
            }
            Marshal.Copy(imgBytes, 0, imgData.Scan0, numBytes);
            img.UnlockBits(imgData);

            //Find artifact text columns
            int edgewidth = 0;
            while (cols[cols.Length - 1 - edgewidth] / (double)img.Height < 0.01)
                edgewidth++;
            int rightmost = cols.Length - edgewidth;
            int leftmost = rightmost - edgewidth;
            int misses = 0;
            while (leftmost - misses > 0 && misses < edgewidth * 2)
            {
                if (cols[leftmost - misses] / (double)img.Height > 0.01)
                {
                    leftmost -= misses + 1;
                    misses = 0;
                }
                else
                {
                    misses++;
                }
            }
            leftmost -= edgewidth / 2;


            //find artifact text rows
            int[] rows = new int[img.Height];
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % img.Width;
                int y = (i / PixelSize - x) % img.Height;
                if (x > leftmost  && x < rightmost && imgBytes[i] == 0)
                {
                    rows[y]++;
                }
            }

            int top = 0;
            int left = leftmost;
            int width = rightmost - leftmost;
            int height = img.Height;
            Bitmap artifactArea = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(artifactArea))
            {
                Rectangle sourceRect = new Rectangle(left, top, width, height);
                g.DrawImage(img, 0, 0, sourceRect, GraphicsUnit.Pixel);
            }
            return artifactArea;

        }

        private Bitmap CaptureScreenshot()
        {
            Bitmap img = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Size fullSize = new Size(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.CopyFromScreen(0, 0, 0, 0, fullSize);
            }
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            img.Save(appDir + @"\Debug\GenshinSC " + timestamp + ".png");
            return img;
        }

        private Bitmap findGameArea(Bitmap full)
        {
            int minWidth = Screen.PrimaryScreen.Bounds.Width / 4;
            for (int y = full.Height / 2; y > 0; y--)
            {
                int x = full.Width / 2;
                int i_pos = 0;
                int i_neg = 0;
                Color pixel = full.GetPixel(x, y);

                //explore right
                while ( x + i_pos < full.Width*0.99 && pixel.R > 220 && pixel.G > 220 && pixel.B > 220) 
                {
                    i_pos++;
                    pixel = full.GetPixel(x + i_pos, y);
                }

                if (i_pos == 0)
                    continue;

                pixel = full.GetPixel(x, y);
                //explore left
                while (x - i_neg > full.Width * 0.01 && pixel.R > 220 && pixel.G > 220 && pixel.B > 220)
                {
                    i_neg++;
                    pixel = full.GetPixel(x - i_neg, y);
                }

                if (i_pos + i_neg < minWidth)
                    continue;

                int top = y + 1;
                int left = x - i_neg + (i_pos + i_neg) / 2;
                int width = (i_pos + i_neg) / 2;
                int height = (full.Height - y + 1) * 2/3;
                Bitmap gameArea = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(gameArea))
                {
                    Rectangle sourceRect = new Rectangle(left, top, width, height);
                    g.DrawImage(full, 0, 0, sourceRect, GraphicsUnit.Pixel);
                }

                string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
                gameArea.Save(appDir + @"\Debug\GenshinSC_gameonly " + timestamp + ".png");
                return gameArea;
            }
            return null;
        }

        private Bitmap LoadScreenshot()
        {
            Bitmap img = new Bitmap(1, 1);
            // Using WinForms for the openFileDialog because it's simpler and much easier
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "image files (*.png)|*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        foreach (string file in openFileDialog.FileNames)
                        {
                            img = new Bitmap(file);
                            break;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return img;
        }

        private void btn_Filter_Click(object sender, EventArgs e)
        {
            img_Filtered = new Bitmap(img_Raw);
            img_Filtered = filterAndGetArtifactArea(img_Filtered);

            image_preview.Image = new Bitmap(img_Filtered);
        }

        private void btn_capture_Click(object sender, EventArgs e)
        {
            Bitmap img;
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                img = LoadScreenshot();
            } else
            {
                img = CaptureScreenshot();
                img = findGameArea(img);
            }
            img_Raw = new Bitmap(img);
            image_preview.Image = new Bitmap(img);
        }
    }
}
