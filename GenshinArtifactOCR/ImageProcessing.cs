using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace GenshinArtifactOCR
{
    class ImageProcessing
    {
        public static List<Point> getArtifactGrid_WindowMode(Bitmap img, Rectangle gameArea, Rectangle artifactArea, bool saveImages)
        {
            Rectangle area = new Rectangle(gameArea.X, gameArea.Y, artifactArea.X - gameArea.X, gameArea.Height);
            //Get relevant part of image
            Bitmap areaImg = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(areaImg))
            {
                g.DrawImage(img, 0, 0, area, GraphicsUnit.Pixel);
            }
            //Prepare bytewise image processing
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, areaImg.Width, areaImg.Height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order

            List<Tuple<int, int, int>> artifactListTuple = new List<Tuple<int, int, int>>(); //start and end x, y for found artifacts in grid
            int currStreak = 0;
            int margin = 3;
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % areaImg.Width;
                int y = (i / PixelSize - x) / areaImg.Width;
                int y_low = Math.Min(y + 10, areaImg.Height - 1);
                int i_low = (y_low * areaImg.Width + x) * PixelSize;
                if ((imgBytes[i] < 60 && imgBytes[i + 1] > 100 && imgBytes[i + 2] > 170) //look for yellow (artifact background + stars
                    && ((imgBytes[i_low] > 200 && imgBytes[i_low + 1] > 200 && imgBytes[i_low + 2] > 200) //with white-ish or black-ish line underneath
                    || (imgBytes[i_low] > 65 && imgBytes[i_low] < 110 && imgBytes[i_low + 1] > 65 && imgBytes[i_low + 1] < 110 && imgBytes[i_low + 2] > 65 && imgBytes[i_low + 2] < 110))
                    )
                {

                    imgBytes[i] = 255;
                    imgBytes[i + 1] = 0;
                    imgBytes[i + 2] = 255;
                    imgBytes[i + 3] = 255;
                    currStreak++;
                    margin = 3;
                }
                else
                {
                    //give some margin of error before skipping
                    if (margin > 0)
                    {
                        margin--;
                        continue;
                    }

                    if (currStreak > areaImg.Width * 0.05)
                    {
                        bool alreadyFound = false;
                        for (int j = 0; j < artifactListTuple.Count; j++)
                        {
                            //skip if start or end x, and y is close to an existing found artifactList
                            if ((artifactListTuple[j].Item1 <= x - currStreak && artifactListTuple[j].Item2 >= x - currStreak)
                                || (artifactListTuple[j].Item1 <= x && artifactListTuple[j].Item2 >= x)
                                || (x - currStreak <= artifactListTuple[j].Item1 && x >= artifactListTuple[j].Item1)
                                || (x - currStreak <= artifactListTuple[j].Item2 && x >= artifactListTuple[j].Item2)
                                && true)
                            {
                                if (Math.Abs(artifactListTuple[j].Item3 - y) < areaImg.Width * 0.07)
                                {
                                    artifactListTuple[j] = Tuple.Create(Math.Min(artifactListTuple[j].Item1, x - currStreak),
                                        Math.Max(artifactListTuple[j].Item2, x), y);
                                    alreadyFound = true;
                                    break;
                                }
                            }
                        }
                        if (!alreadyFound)
                            artifactListTuple.Add(Tuple.Create(x - currStreak, x, y));
                    }
                    currStreak = 0;
                }


            }
            Marshal.Copy(imgBytes, 0, imgData.Scan0, numBytes);
            areaImg.UnlockBits(imgData);

            List<Point> artifactListPoint = new List<Point>();
            foreach (Tuple<int, int, int> tup in artifactListTuple)
            {
                artifactListPoint.Add(new Point(area.X + (tup.Item2 + tup.Item1) / 2, area.Y + tup.Item3));
            }

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
            {
                using (Graphics g = Graphics.FromImage(areaImg))
                {
                    foreach (Tuple<int, int, int> tup in artifactListTuple)
                    {
                        g.FillRectangle(Brushes.Cyan, tup.Item1, tup.Item3, tup.Item2 - tup.Item1, 3);
                    }
                }
                areaImg.Save(Database.appDir + @"\images\GenshinArtifactGridFiltered " + timestamp + ".png");
            }
            return artifactListPoint;
        }

        /// <summary>
        /// Find artifact area from an image of the backpack
        /// </summary>
        /// <param name="img">Full screenshot containing game</param>
        /// <param name="gameArea">Area of the screenshot containing only the game</param>
        /// <returns>Area of <paramref name="img"/> containing the artifact info</returns>
        public static Rectangle findArtifactArea_WindowMode(Bitmap img, Rectangle gameArea)
        {
            //Cut out relevant part of image
            gameArea = new Rectangle(gameArea.X + gameArea.Width / 2, gameArea.Y, gameArea.Width / 2, gameArea.Height);
            Bitmap areaImg = new Bitmap(gameArea.Width, gameArea.Height);
            using (Graphics g = Graphics.FromImage(areaImg))
            {
                g.DrawImage(img, 0, 0, gameArea, GraphicsUnit.Pixel);
            }

            int[] cols = new int[gameArea.Width];
            //prepare bytewise image processing
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, gameArea.Width, gameArea.Height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            areaImg.UnlockBits(imgData);
            int PixelSize = 4; //ARGB, reverse order

            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % gameArea.Width;
                if ((imgBytes[i] > 210 && imgBytes[i + 1] > 210 && imgBytes[i + 2] > 210)) //look for white-ish text background
                {
                    cols[x]++;
                }
            }

            //Find artifact text columns
            int edgewidth = 0;
            //find right edge
            while (cols[cols.Length - 1 - edgewidth] / (double)gameArea.Height < 0.05)
                edgewidth++;
            int rightmost = cols.Length - (int)(edgewidth * 2);
            //find left edge
            int leftmost = rightmost - edgewidth;
            int misses = 0;
            while (leftmost - misses > 0 && misses < edgewidth)
            {
                if (cols[leftmost - misses] / (double)gameArea.Height > 0.20)
                {
                    leftmost -= misses + 1;
                    misses = 0;
                }
                else
                {
                    misses++;
                }
            }
            leftmost += 3;

            return new Rectangle(gameArea.Left + leftmost, gameArea.Top, rightmost - leftmost, gameArea.Height);
        }


        /// <summary>
        /// Find artifact area from an image of the character equipment
        /// </summary>
        /// <param name="img">Full screenshot containing game</param>
        /// <param name="gameArea">Area of the screenshot containing only the game</param>
        /// <returns>Area of <paramref name="img"/> containing the artifact info</returns>
        public static Rectangle findArtifactArea(Bitmap img, Rectangle gameArea)
        {
            //Cut out relevant part of image
            gameArea = new Rectangle(gameArea.X + gameArea.Width / 2, gameArea.Y, gameArea.Width / 2, gameArea.Height);
            Bitmap areaImg = new Bitmap(gameArea.Width, gameArea.Height);
            using (Graphics g = Graphics.FromImage(areaImg))
            {
                g.DrawImage(img, 0, 0, gameArea, GraphicsUnit.Pixel);
            }

            int[] cols = new int[gameArea.Width];

            //prepare bytewise image processing
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, gameArea.Width, gameArea.Height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            areaImg.UnlockBits(imgData);
            int PixelSize = 4; //ARGB, reverse order

            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % gameArea.Width;
                if (imgBytes[i + 1] > 220 || (imgBytes[i] > 160 && imgBytes[i + 1] > 160 && imgBytes[i + 2] > 160)) //look for green or white-ish text
                {
                    cols[x]++;
                }
            }

            //Find artifact text columns
            int edgewidth = 0;
            //find right edge
            while (cols[cols.Length - 1 - edgewidth] / (double)gameArea.Height < 0.01)
                edgewidth++;
            int rightmost = cols.Length - edgewidth * 2;
            //find left edge
            int leftmost = rightmost - edgewidth;
            int misses = 0;
            while (leftmost - misses > 0 && misses < edgewidth * 2)
            {
                if (cols[leftmost - misses] / (double)gameArea.Height > 0.05)
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

            return new Rectangle(gameArea.Left + leftmost, gameArea.Top, rightmost - leftmost, gameArea.Height);
        }


        /// <summary>
        /// Extract and filter the artifact area from an image of the backpack
        /// </summary>
        /// <param name="img">Full screenshot containing game</param>
        /// <param name="area">Area containing the artifact info</param>
        /// <param name="rows">Filter results per row</param>
        /// <returns>Filtered image of the artifact area</returns>
        public static Bitmap getArtifactImg_WindowMode(Bitmap img, Rectangle area, out int[] rows, bool saveImages)
        {
            rows = new int[area.Height];
            //Get relevant part of image
            Bitmap areaImg = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(areaImg))
            {
                g.DrawImage(img, 0, 0, area, GraphicsUnit.Pixel);
            }
            //Prepare bytewise image processing
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, areaImg.Width, areaImg.Height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order
            //some variables to keep track of which part of the image we are in
            int section = 0; //0 = top part, 1 = artifact level part, 2 = substat and set, 3 = character
            int secOneStart = 0;
            int secOneEnd = 0;
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % areaImg.Width;
                int y = (i / PixelSize - x) / areaImg.Width;
                if (
                    (section == 0 && (x < areaImg.Width * 0.65 && imgBytes[i] > 140 && imgBytes[i + 1] > 140 && imgBytes[i + 2] > 140)) //look for white-ish text, skip right edge (artifact image)
                    || (section == 1 && (imgBytes[i] > 240 && imgBytes[i + 1] > 240 && imgBytes[i + 2] > 240)) //look for bright white text
                    || (section == 2 && (imgBytes[i] < 150 && imgBytes[i + 1] < 150 && imgBytes[i + 2] < 150)) //look for black
                    || (section == 3 && ((imgBytes[i] < 120 && imgBytes[i + 1] > 160 && imgBytes[i + 2] < 120)
                        || (imgBytes[i] < 110 && imgBytes[i + 1] < 100 && imgBytes[i + 2] < 100))) //look for green or black
                    )
                {
                    //Make Black
                    imgBytes[i] = 0;
                    imgBytes[i + 1] = 0;
                    imgBytes[i + 2] = 0;
                    imgBytes[i + 3] = 255;

                    rows[y]++;
                }
                else
                {
                    //Make White
                    imgBytes[i] = 255;
                    imgBytes[i + 1] = 255;
                    imgBytes[i + 2] = 255;
                    imgBytes[i + 3] = 255;
                }

                if (x == 0)
                {
                    if (section == 0)
                    {
                        //check if coming row is white-ish, if so move to section 1
                        int tmp = (y * areaImg.Width + (int)(areaImg.Width * 0.05)) * PixelSize;
                        if ((imgBytes[tmp] > 200 && imgBytes[tmp + 1] > 200 && imgBytes[tmp + 2] > 200) && (imgBytes[tmp] < 240 && imgBytes[tmp + 1] < 240 && imgBytes[tmp + 2] < 240))
                        {
                            section = 1;
                            i += areaImg.Width * PixelSize;
                        }

                    }
                    else if (section == 1)
                    {
                        if (y == secOneEnd)
                        {
                            section = 2;
                            secOneStart = 0;
                            secOneEnd = 0;
                        }
                        else if (secOneEnd == 0 && secOneStart != 0 && rows[y - 1] == 0)
                        {
                            secOneEnd = y + (y - secOneStart);
                        }
                        else if (secOneStart == 0 && rows[y - 1] != 0)
                        {
                            secOneStart = y - 1;
                        }

                    }
                    else if (section == 2)
                    {
                        if (y == secOneEnd)
                        {
                            y -= (secOneStart - secOneEnd) / 5;
                            section = 3;
                            secOneStart = 0;
                            secOneEnd = 0;
                        }
                        else if (secOneEnd == 0 && secOneStart != 0 && rows[y - 1] == 0)
                        {
                            secOneEnd = y + (y - secOneStart);
                        }
                        else if (rows[y - 2] == 0 && rows[y - 1] != 0)
                        {
                            secOneStart = y - 1;
                            secOneEnd = 0;
                        }
                    }
                }
            }
            Marshal.Copy(imgBytes, 0, imgData.Scan0, numBytes);
            areaImg.UnlockBits(imgData);

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
                areaImg.Save(Database.appDir + @"\images\GenshinArtifactImgFiltered " + timestamp + ".png");
            return areaImg;
        }

        /// <summary>
        /// Extract and filter the artifact area from an image of the character equipment
        /// </summary>
        /// <param name="img">Full screenshot containing game</param>
        /// <param name="area">Area containing the artifact info</param>
        /// <param name="rows">Filter results per row</param>
        /// <returns>Filtered image of the artifact area</returns>
        public static Bitmap getArtifactImg(Bitmap img, Rectangle area, out int[] rows, bool saveImages)
        {
            rows = new int[area.Height];
            //Get relevant part of image
            Bitmap areaImg = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(areaImg))
            {
                g.DrawImage(img, 0, 0, area, GraphicsUnit.Pixel);
            }
            //Prepare bytewise image processing
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, areaImg.Width, areaImg.Height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order

            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % areaImg.Width;
                int y = (i / PixelSize - x) / areaImg.Width;
                if (imgBytes[i + 1] > 220 || (imgBytes[i] > 160 && imgBytes[i + 1] > 160 && imgBytes[i + 2] > 160)) //look for green or white-ish text
                {
                    //Make Black
                    imgBytes[i] = 0;
                    imgBytes[i + 1] = 0;
                    imgBytes[i + 2] = 0;
                    imgBytes[i + 3] = 255;

                    rows[y]++;
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
            areaImg.UnlockBits(imgData);

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
                areaImg.Save(Database.appDir + @"\images\GenshinArtifactArea " + timestamp + ".png");
            return areaImg;
        }

        /// <summary>
        /// Capture screenshot of main screen
        /// </summary>
        /// <returns>Screenshot of main screen</returns>
        public static Bitmap CaptureScreenshot(bool saveImages)
        {
            Bitmap img = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Size fullSize = new Size(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.CopyFromScreen(0, 0, 0, 0, fullSize);
            }
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
                img.Save(Database.appDir + @"\images\GenshinFullscreen " + timestamp + ".png");
            return img;
        }

        /// <summary>
        /// Find approximate area containing the game
        /// </summary>
        /// <param name="full">Screenshot of main monitor</param>
        /// <returns>Area containing game</returns>
        public static Rectangle findGameArea(Bitmap full)
        {
            //Prepare bytewise image processing
            BitmapData imgData = full.LockBits(new Rectangle(0, 0, full.Width, full.Height), ImageLockMode.ReadWrite, full.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order
            full.UnlockBits(imgData);

            int minWidth = Screen.PrimaryScreen.Bounds.Width / 4;
            int x = full.Width / 2; //probing via middle of screen, looking for white window header
            for (int y = full.Height / 2; y > 0; y--)
            {
                int i_pos = 0;
                int i_neg = 0;
                int index = (y * full.Width + x) * PixelSize;
                Color pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);

                //explore white area right
                while (x + i_pos < full.Width * 0.99 && pixel.R > 220 && pixel.G > 220 && pixel.B > 220)
                {
                    i_pos++;
                    index = (y * full.Width + x + i_pos) * PixelSize;
                    pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);
                }

                if (i_pos == 0)
                    continue;

                index = (y * full.Width + x) * PixelSize;
                pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);
                //explore white area left
                while (x - i_neg > full.Width * 0.01 && pixel.R > 220 && pixel.G > 220 && pixel.B > 220)
                {
                    i_neg++;
                    index = (y * full.Width + x - i_neg) * PixelSize;
                    pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);
                }

                //check if feasible game window size
                if (i_pos + i_neg < minWidth)
                    continue;

                int top = y + 1;
                int left = x - i_neg;
                int width = (i_pos + i_neg);

                //find bottom
                int height = 10;
                while (height < full.Height - y - 1)
                {
                    int row = 0;
                    int currStreak = 0;
                    int maxStreak = 0;
                    for (int i = 0; i < width * 0.3; i++)
                    {
                        index = ((y + height) * full.Width + left + i) * PixelSize;
                        pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);
                        if (pixel.R > 230 && pixel.G > 220 && pixel.B > 210)
                        {
                            row++;
                            currStreak++;
                            if (currStreak > maxStreak)
                                maxStreak = currStreak;
                        }
                        else
                        {
                            currStreak = 0;
                        }
                    }

                    if (row > width * 0.3 * 0.65 && maxStreak > width * 0.3 * 0.25)
                        break;

                    height++;
                }
                return new Rectangle(left, top, width, height);
            }
            return new Rectangle(0, 0, full.Width, full.Height);
        }

        /// <summary>
        /// Select and load screenshot, taken from WFInfo but heavily cut down
        /// </summary>
        /// <returns>Loaded screenshot, or empty 1x1 bitmap on failure</returns>
        public static Bitmap LoadScreenshot()
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


        /// <summary>
        /// Use OCR to read text from row
        /// </summary>
        /// <param name="img">Image to read, filtered</param>
        /// <param name="start">Starting row of <paramref name="img"/> to read</param>
        /// <param name="stop">Ending row of <paramref name="img"/> to read</param>
        /// <param name="validText">List of words to match against</param>
        /// <param name="dist">Levenshtein distance to closest match</param>
        /// <param name="rawText">Raw result from OCR process (appended to <paramref name="prevRaw"/>)</param>
        /// <param name="prevRaw">String to append in front of raw OCR result before searching for match</param>
        /// <returns>Closest matching word</returns>
        public static string OCRRow(Bitmap img, int start, int stop, List<string> validText, out int index, out int dist, out string rawText, string prevRaw, bool saveImages, TesseractEngine tessEngine)
        {

            tessEngine.SetVariable("tessedit_char_whitelist", @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210+%,:() ");
            //Copy relevant part of image
            int height = stop - start;
            Bitmap scanArea = new Bitmap(img.Width, height);
            using (Graphics g = Graphics.FromImage(scanArea))
            {
                Rectangle sourceRect = new Rectangle(0, start, img.Width, height);
                g.DrawImage(img, 0, 0, sourceRect, GraphicsUnit.Pixel);
            }


            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
                scanArea.Save(Database.appDir + @"\images\GenshinTextRow " + timestamp + ".png");

            //Do OCR and append to prevRaw
            string text = prevRaw;
            using (var page = tessEngine.Process(scanArea, PageSegMode.SparseText))
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
            //mild filtering
            text = Regex.Replace(text, @"\s+", "");
            rawText = text;

            string bestMatch = Database.FindClosestMatch(text, validText, out index, out dist);
            //Console.WriteLine("\nGot (" + dist + ") \"" + bestMatch + "\" from \"" + text + "\"");

            return bestMatch;
        }

        /// <summary>
        /// Extract stats from image to window text boxes
        /// </summary>
        /// <param name="img">Image of artifact area, filtered</param>
        /// <param name="rows">Filter results per row</param>
        public static InventoryItem getArtifacts(Bitmap img, int[] rows, bool saveImages, TesseractEngine tessEngine)
        {
            //get all potential text rows
            List<Tuple<int, int>> textRows = new List<Tuple<int, int>>();
            int i = 0;
            while (i + 1 < img.Height)
            {
                while (i + 1 < img.Height && rows[i] / (double)img.Width < 0.01)
                    i++;
                int rowTop = i;
                while (i + 1 < img.Height && !(rows[i] / (double)img.Width < 0.01))
                    i++;
                textRows.Add(Tuple.Create(Math.Max(0, rowTop - 3), Math.Min(img.Height - 1, i + 3)));
            }

            //first row guaranteed to be of no use (artifact name etc)
            textRows.RemoveAt(0);


            InventoryItem foundArtifact = new InventoryItem();
            i = 0;
            //Piece type
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Pieces, out int resultIndex, out int dist, out _, "", saveImages, tessEngine);
                if (dist < 3)
                {
                    foundArtifact.piece = Database.Pieces_trans[resultIndex];
                    i++;
                    break;
                }
            }

            //Main stat
            string prevRaw = "";
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.MainStats, out int resultIndex, out int dist, out string rawText, prevRaw, saveImages, tessEngine);
                if (dist < rawText.Length - 2 && rawText.Any(char.IsDigit))
                {
                    foundArtifact.main = Database.MainStats_trans[resultIndex];
                    i++;
                    break;
                }
                prevRaw = rawText;
            }

            //Level
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Levels, out int resultIndex, out int dist, out string rawText, "", saveImages, tessEngine);
                if (rawText.Length != 0)
                {
                    foundArtifact.level = Database.Levels_trans[resultIndex];
                    i++;
                    break;
                }
            }

            //Substats
            foundArtifact.subs = new List<Tuple<string, string, double>>();
            int substat = 0;
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Substats, out int resultIndex, out int dist, out string rawText, "", saveImages, tessEngine);
                if (dist < 3)
                {
                    foundArtifact.subs.Add(Database.Substats_trans[resultIndex]);
                    if (substat > 2)
                    {
                        i++;
                        break;
                    }
                    substat++;
                }
                else if (substat > 2 && rawText.Length > 5)
                {
                    break;
                }
            }

            //Set
            int startRow = i;
            prevRaw = "";
            for (; i < textRows.Count; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Sets, out int resultIndex, out int dist, out string rawText, prevRaw, saveImages, tessEngine);
                if (dist < 5)
                {
                    foundArtifact.set = Database.Sets_trans[resultIndex];
                    break;
                }
                else
                {
                    prevRaw = rawText;
                    if (startRow - i > 4)
                        break;
                }
            }

            //Character
            for (i = textRows.Count - 1; i > Math.Max(0, textRows.Count - 3); i--)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Characters, out int resultIndex, out int dist, out _, "", saveImages, tessEngine);
                if (dist < 5)
                {
                    foundArtifact.character = Database.Characters_trans[resultIndex];
                    break;
                }
            }

            return foundArtifact;
        }
    }
}
