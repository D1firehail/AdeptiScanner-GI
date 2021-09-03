﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        private int[] filtered_rows;

        public static string appDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GenshinArtifactOCR";
        public static List<string> Pieces = new List<string>(){
            "Flower of Life",
            "Plume of Death", 
            "Sands of Eon",
            "Goblet on Eonothem",
            "Circlet of Logos"
        };
        public static List<string> MainStats = new List<string>();
        public static List<string> Levels = new List<string>();
        public static List<string> Substats = new List<string>();
        public static List<string> Sets = new List<string>();

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

        private Bitmap filterAndGetArtifactArea(Bitmap img, out int[] rows)
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
                int y = (i / PixelSize - x) / img.Width;
                if (imgBytes[i + 1] > 220 || (imgBytes[i] > 160 && imgBytes[i + 1] > 160 && imgBytes[i + 2] > 160))
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
            rows = new int[img.Height];
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % img.Width;
                int y = (i / PixelSize - x) / img.Width;
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

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            artifactArea.Save(appDir + @"\images\GenshinArtifactArea " + timestamp + ".png");
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
            img.Save(appDir + @"\images\GenshinSC " + timestamp + ".png");
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

        public int LevenshteinDistance(string s, string t)
        {
            // Levenshtein Distance determines how many character changes it takes to form a known result
            // For more info see: https://en.wikipedia.org/wiki/Levenshtein_distance
            s = s.ToLower();
            t = t.ToLower();
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0 || m == 0)
                return n + m;

            d[0, 0] = 0;

            int count = 0;
            for (int i = 1; i <= n; i++)
                d[i, 0] = (s[i - 1] == ' ' ? count : ++count);

            count = 0;
            for (int j = 1; j <= m; j++)
                d[0, j] = (t[j - 1] == ' ' ? count : ++count);

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    // deletion of s
                    int opt1 = d[i - 1, j];
                    if (s[i - 1] != ' ')
                        opt1++;

                    // deletion of t
                    int opt2 = d[i, j - 1];
                    if (t[j - 1] != ' ')
                        opt2++;

                    // swapping s to t
                    int opt3 = d[i - 1, j - 1];
                    if (t[j - 1] != s[i - 1])
                        opt3++;
                    d[i, j] = Math.Min(Math.Min(opt1, opt2), opt3);
                }



            return d[n, m];
        }

        private string FindClosestMatch(string rawText, List<string> validText, out int dist)
        {
            string lowest = "ERROR";
            dist = 9999;
            foreach (string validWord in validText)
            {
                int val = LevenshteinDistance(validWord, rawText);
                if (val < dist)
                {
                    dist = val;
                    lowest = validWord;
                }
            }
            return lowest;
        }

        private string OCRRow(Bitmap img, int start, int stop, List<string> validText, out int dist)
        {

            tessEngine.SetVariable("tessedit_char_whitelist", @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210+%,:() ");
            int height = stop - start;
            Bitmap scanArea = new Bitmap(img.Width, height);
            using (Graphics g = Graphics.FromImage(scanArea))
            {
                Rectangle sourceRect = new Rectangle(0, start, img.Width, height);
                g.DrawImage(img, 0, 0, sourceRect, GraphicsUnit.Pixel);
            }


            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            scanArea.Save(appDir + @"\images\GenshinTextRow " + timestamp + ".png");

            string text = "";
            using ( var page = tessEngine.Process(scanArea, PageSegMode.SparseText))
            {
                using (var iterator = page.GetIterator())
                {
                    iterator.Begin();
                    do
                    {
                        text += iterator.GetText(PageIteratorLevel.TextLine);
                    } while (iterator.Next(PageIteratorLevel.TextLine));
                }
            }

            text = Regex.Replace(text, @"\s+", "");

            string bestMatch = FindClosestMatch(text, validText, out dist);
            Console.WriteLine("\nGot (" + dist + ") \"" + bestMatch + "\" from \"" + text + "\"");

            return bestMatch;
        }

        private void getArtifacts(Bitmap img, int[] rows)
        {
            //get all potential text rows
            List<Tuple<int, int>> textRows = new List<Tuple<int, int>>();
            int i = 0;
            while ( i+1 < img.Height)
            {
                while ( i+1 < img.Height && rows[i] / (double)img.Width < 0.01)
                    i++;
                int rowTop = i;
                while (i+1 < img.Height && !(rows[i] / (double)img.Width < 0.01))
                    i++;
                textRows.Add(Tuple.Create(Math.Max(0, rowTop-3), Math.Min(img.Height-1, i + 3)));
            }

            //remove rows around as tall as artifact name and rows too short to be part of the text
            textRows.RemoveAt(0);
            //int ArtifactNameHeight = textRows[0].Item2 - textRows[0].Item1;
            //for (i = 0; i < textRows.Count; )
            //{
            //    int rowHeight = textRows[i].Item2 - textRows[i].Item1;
            //    if (rowHeight > ArtifactNameHeight * 0.95 || rowHeight < ArtifactNameHeight * 0.5)
            //    {
            //        textRows.RemoveAt(i);
            //    } else
            //    {
            //        i++;
            //    }
            //}


            text_raw.Text = "";
            i = 0;
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Pieces, out int dist);
                if (dist < 3)
                {
                    text_raw.Text += result + Environment.NewLine;
                    text_Type.Text = result;
                    i++;
                    break;
                }
            }

            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, MainStats, out int dist);
                if (dist < 3)
                {
                    text_raw.Text += result + Environment.NewLine;
                    text_statMain.Text = result;
                    i++;
                    break;
                }
            }

            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Levels, out int dist);
                if (dist == 0)
                {
                    text_raw.Text += result + Environment.NewLine;
                    text_Level.Text = result;
                    i++;
                    break;
                }
            }

            int substat = 0;
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Substats, out int dist);
                if (dist < 3)
                {
                    text_raw.Text += result + Environment.NewLine;
                    if (substat == 0)
                    {
                        text_statSub1.Text = result;
                    } else if (substat == 1)
                    {
                        text_statSub2.Text = result;
                    }
                    else if (substat == 2)
                    {
                        text_statSub3.Text = result;
                    }
                    else
                    {
                        text_statSub4.Text = result;
                        i++;
                        break;
                    }
                    substat++;
                } else if (substat > 2)
                {
                    break;
                }
            }

            int startRow = i;
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[startRow].Item1, textRows[i].Item2, Sets, out int dist);
                if (dist < 5)
                {
                    text_raw.Text += result + Environment.NewLine;
                    text_Set.Text = result;
                    break;
                }
                else
                {
                    if (startRow - i > 4)
                        break;
                }
            }


        }

        private void btn_Filter_Click(object sender, EventArgs e)
        {
            img_Filtered = new Bitmap(img_Raw);
            img_Filtered = filterAndGetArtifactArea(img_Filtered, out filtered_rows);

            image_preview.Image = new Bitmap(img_Filtered);
        }

        private void btn_capture_Click(object sender, EventArgs e)
        {
            text_raw.Text = "";
            Bitmap img;
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                img = LoadScreenshot();
            } else
            {
                img = CaptureScreenshot();
            }
            img = findGameArea(img);
            if (img != null)
            {
                img_Raw = new Bitmap(img);
            }
            image_preview.Image = new Bitmap(img_Raw);
        }

        private void btn_OCR_Click(object sender, EventArgs e)
        {
            getArtifacts(img_Filtered, filtered_rows);
        }
    }
}
