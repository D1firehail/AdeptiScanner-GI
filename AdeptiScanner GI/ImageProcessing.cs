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

namespace AdeptiScanner_GI
{
    class ImageProcessing
    {
        public static List<Point> getArtifactGrid(Bitmap areaImg, bool saveImages, Point coordOffset)
        {
            //Rectangle area = new Rectangle(gameArea.X, gameArea.Y, artifactArea.X - gameArea.X, gameArea.Height);
            //Get relevant part of image
            //Bitmap areaImg = new Bitmap(area.Width, area.Height);
            //using (Graphics g = Graphics.FromImage(areaImg))
            //{
            //    g.DrawImage(img, 0, 0, area, GraphicsUnit.Pixel);
            //}
            //Prepare bytewise image processing
            int width = areaImg.Width;
            int height = areaImg.Height;
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order

            if (!saveImages)
            {
                areaImg.UnlockBits(imgData);
            }


            List<Tuple<int, int, int>> artifactListTuple = new List<Tuple<int, int, int>>(); //start and end x, y for found artifacts in grid
            int currStreak = 0;
            int margin = 3;
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % width;
                int y = (i / PixelSize - x) / width;
                int y_low = Math.Min(y + 10, height - 1);
                int i_low = (y_low * width + x) * PixelSize;
                if ((imgBytes[i] < 60 && imgBytes[i + 1] > 100 && imgBytes[i + 2] > 170) //look for yellow (artifact background + stars
                    && ((imgBytes[i_low] > 200 && imgBytes[i_low + 1] > 200 && imgBytes[i_low + 2] > 200) //with white-ish or black-ish line underneath
                    || (imgBytes[i_low] > 65 && imgBytes[i_low] < 110 && imgBytes[i_low + 1] > 65 && imgBytes[i_low + 1] < 110 && imgBytes[i_low + 2] > 65 && imgBytes[i_low + 2] < 110))
                    )
                {
                    if (saveImages)
                    {
                    imgBytes[i] = 255;
                    imgBytes[i + 1] = 0;
                    imgBytes[i + 2] = 255;
                    imgBytes[i + 3] = 255;
                    }
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

                    if (currStreak > width * 0.05)
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
                                if (Math.Abs(artifactListTuple[j].Item3 - y) < width * 0.07)
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
            if (saveImages)
            {
                Marshal.Copy(imgBytes, 0, imgData.Scan0, numBytes);
                areaImg.UnlockBits(imgData);
            }

            List<Point> artifactListPoint = new List<Point>();
            foreach (Tuple<int, int, int> tup in artifactListTuple)
            {
                artifactListPoint.Add(new Point(coordOffset.X + (tup.Item2 + tup.Item1) / 2, coordOffset.Y + tup.Item3));
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
                if ((imgBytes[i] > 40 && imgBytes[i] < 60 && imgBytes[i + 1] > 90 && imgBytes[i + 1] < 110 && imgBytes[i + 2] > 180 && imgBytes[i + 2] < 200) 
                    || (imgBytes[i] > 220 && imgBytes[i] < 230 && imgBytes[i + 1] > 80 && imgBytes[i + 1] < 90 && imgBytes[i + 2] > 155 && imgBytes[i + 2] < 165)
                    || (imgBytes[i] > 200 && imgBytes[i] < 210 && imgBytes[i + 1] > 120 && imgBytes[i + 1] < 130 && imgBytes[i + 2] > 75 && imgBytes[i + 2] < 85)
                    || (imgBytes[i] > 110 && imgBytes[i] < 120 && imgBytes[i + 1] > 140 && imgBytes[i + 1] < 150 && imgBytes[i + 2] > 35 && imgBytes[i + 2] < 45)
                    || (imgBytes[i] > 135 && imgBytes[i] < 145 && imgBytes[i + 1] > 115 && imgBytes[i + 1] < 125 && imgBytes[i + 2] > 110 && imgBytes[i + 2] < 120)) //look for artifact name background colour
                {
                    cols[x]++;
                }
            }

            //Find artifact text columns
            int edgewidth = 0;
            //find right edge
            while (cols[cols.Length - 1 - edgewidth] / (double)gameArea.Height < 0.02)
                edgewidth++;
            int rightmost = cols.Length - edgewidth;
            //find left edge
            int leftmost = rightmost - edgewidth;
            int misses = 0;
            while (leftmost - misses > 0 && misses < edgewidth)
            {
                if (cols[leftmost - misses] / (double)gameArea.Height > 0.01)
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


            int top = 0;
            for (int y = top; y < gameArea.Height; y++)
            {
                int i = (y * gameArea.Width + (leftmost * 3 + rightmost) / 4) * PixelSize;
                if ((imgBytes[i] > 40 && imgBytes[i] < 60 && imgBytes[i + 1] > 90 && imgBytes[i + 1] < 110 && imgBytes[i + 2] > 180 && imgBytes[i + 2] < 200)) //look for artifact name background colour
                {
                    top = y;
                    break;
                }
            }

            int height = gameArea.Height - 1;
            for (int y = height; y > top; y--)
            {
                int i = (y * gameArea.Width + (leftmost*3 + rightmost )/4) * PixelSize;
                if ((imgBytes[i] > 180 && imgBytes[i + 1] > 210 && imgBytes[i + 2] > 210)) //look for white-ish text background
                {
                    height = y - top;
                    break;
                }
            }

            return new Rectangle(gameArea.Left + leftmost, gameArea.Top + top, rightmost - leftmost, height);
        }


        /// <summary>
        /// Extract and filter the artifact area from an image of the backpack
        /// </summary>
        /// <param name="img">Full screenshot containing game</param>
        /// <param name="area">Area containing the artifact info</param>
        /// <param name="rows">Filter results per row</param>
        /// <returns>Filtered image of the artifact area</returns>
        public static Bitmap getArtifactImg(Bitmap img, Rectangle area, out int[] rows, bool saveImages, out bool locked, out int rarity, out Rectangle typeMainArea, out Rectangle levelArea, out Rectangle subArea, out Rectangle setArea, out Rectangle charArea)
        {
            typeMainArea = Rectangle.Empty;
            levelArea = Rectangle.Empty;
            subArea = Rectangle.Empty;
            setArea = Rectangle.Empty;
            charArea = Rectangle.Empty;
            rarity = 0;

            locked = false;
            rows = new int[area.Height];
            //Get relevant part of image
            Bitmap areaImg = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(areaImg))
            {
                g.DrawImage(img, 0, 0, area, GraphicsUnit.Pixel);
            }
            int width = areaImg.Width;
            int height = areaImg.Height;
            //Prepare bytewise image processing
            BitmapData imgData = areaImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, areaImg.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order
            //some variables to keep track of which part of the image we are in
            int section = 0; //0 = top part, 1 = artifact level part, 2 = substats, 3 = set, 4 = character
            int sectionStart = 0;
            int sectionEnd = 0;
            int rightEdge = 0;
            int leftEdge = width - 1;
            int line_rarity = 0;
            for (int i = 0; i < numBytes; i += PixelSize)
            {
                int x = (i / PixelSize) % width;
                int y = (i / PixelSize - x) / width;
                int y_below = Math.Min(((y + 1) * width + x) * PixelSize, numBytes - PixelSize - 1);
                if (
                    (section == 0 && (x < width * 0.55 && imgBytes[i] > 140 && imgBytes[i + 1] > 140 && imgBytes[i + 2] > 140)) //look for white-ish text, skip right edge (artifact image)
                    || (section == 1 && x < width * 0.55 && ((imgBytes[i] > 225 && imgBytes[i + 1] > 225 && imgBytes[i + 2] > 225) || (imgBytes[y_below] > 225 && imgBytes[y_below + 1] > 225 && imgBytes[y_below + 2] > 225))) //look for bright white text, skip right edge
                    || ((section == 2 || section == 4) && (imgBytes[i] < 150 && imgBytes[i + 1] < 150 && imgBytes[i + 2] < 150)) //look for black
                    || (section == 3 && (imgBytes[i] < 130 && imgBytes[i + 1] > 160 && imgBytes[i + 2] < 130)) //look for green
                    )
                {
                    //Make Black
                    imgBytes[i] = 0;
                    imgBytes[i + 1] = 0;
                    imgBytes[i + 2] = 0;
                    imgBytes[i + 3] = 255;
                    rows[y]++;
                    if (x > rightEdge)
                        rightEdge = x;
                    if (x < leftEdge && x != 0)
                        leftEdge = x;
                }
                else
                {
                    if (section == 0 && line_rarity >= 0 && x < width/2 && (imgBytes[i] < 60 && imgBytes[i + 1] > 190 && imgBytes[i + 2] > 240) 
                        && !(imgBytes[i + PixelSize] < 60 && imgBytes[i + PixelSize + 1] > 190 && imgBytes[i + PixelSize + 2] > 240)) 
                    {
                        //if section 0, look for yellow with non-yellow before
                        line_rarity++;
                    }

                    if ( section == 1 && imgBytes[i] < 150 && imgBytes[i + 1] > 120 && imgBytes[i + 2] > 200)
                    {
                        //if section 1, look for red lock
                        locked = true;
                    } else if (section == 2 && (imgBytes[i] < 120 && imgBytes[i + 1] > 160 && imgBytes[i + 2] < 120))
                    {
                        //if section 2, look for green text
                        subArea = new Rectangle(0, levelArea.Bottom, levelArea.Width, y - levelArea.Bottom);
                        setArea = new Rectangle(0, subArea.Bottom, width, height - subArea.Bottom);
                        section = 3;
                    } else if (section == 3 && imgBytes[i + 2] > 250 && imgBytes[i] > 170 && imgBytes[i + 1] > 220)
                    {
                        setArea = new Rectangle(setArea.X, setArea.Y, setArea.Width, y - setArea.Y);
                        charArea = new Rectangle(0, y, width, height - y);
                        section = 4;
                    }
                    //Make White
                    imgBytes[i] = 255;
                    imgBytes[i + 1] = 255;
                    imgBytes[i + 2] = 255;
                    imgBytes[i + 3] = 255;
                }

                if (x == 0)
                {
                    if (line_rarity > 0)
                    {
                        //set rarity on two consecutive identical results
                        if (rarity == line_rarity)
                        {
                            rarity = line_rarity;
                            line_rarity = -1;
                        }
                        else
                        {
                            rarity = line_rarity;
                            line_rarity = 0;
                        }
                    }
                    if (section == 0)
                    {
                        //check if coming row is white-ish, if so move to section 1
                        int tmp = (y * width + (int)(width * 0.05)) * PixelSize;
                        if ((imgBytes[tmp] > 200 && imgBytes[tmp + 1] > 200 && imgBytes[tmp + 2] > 200) && (imgBytes[tmp] < 240 && imgBytes[tmp + 1] < 240 && imgBytes[tmp + 2] < 240))
                        {
                            //Make White
                            imgBytes[i] = 255;
                            imgBytes[i + 1] = 255;
                            imgBytes[i + 2] = 255;
                            imgBytes[i + 3] = 255;

                            typeMainArea = new Rectangle(0, 0, (int)(width * 0.55), y);
                            section = 1;
                            i += width * PixelSize;
                        }

                    }
                    else if (section == 1)
                    {
                        if (y == sectionEnd)
                        {
                            levelArea = new Rectangle(0, sectionStart, (int)(width * 0.55), sectionEnd - sectionStart);
                            section = 2;
                            sectionStart = 0;
                            sectionEnd = 0;
                        }
                        else if (sectionEnd == 0 && sectionStart != 0 && rows[y - 1] == 0)
                        {
                            sectionEnd = y + (y - sectionStart);
                        }
                        else if (sectionStart == 0 && rows[y - 1] != 0)
                        {
                            sectionStart = y - 1;
                        }
                    }
                }
            }
            Marshal.Copy(imgBytes, 0, imgData.Scan0, numBytes);
            areaImg.UnlockBits(imgData);

            Bitmap thinImg = new Bitmap(rightEdge - leftEdge, height);
            using (Graphics g = Graphics.FromImage(thinImg))
            {
                g.DrawImage(areaImg, 0, 0, new Rectangle(leftEdge, 0, rightEdge - leftEdge, height), GraphicsUnit.Pixel);
            }
            areaImg = thinImg;

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
                areaImg.Save(Database.appDir + @"\images\GenshinArtifactImgFiltered " + timestamp + ".png");
            return areaImg;
        }

        /// <summary>
        /// Capture screenshot of main screen
        /// </summary>
        /// <returns>Screenshot of main screen</returns>
        public static Bitmap CaptureScreenshot(bool saveImages, Rectangle area, bool useArea = false )
        {
            if (!useArea)
                area = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Bitmap img = new Bitmap(area.Width, area.Height);
            Size areaSize = new Size(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.CopyFromScreen(area.X, area.Y, 0, 0, areaSize);
            }
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ssff");
            if (saveImages)
                img.Save(Database.appDir + @"\images\GenshinScreen " + timestamp + ".png");
            return img;
        }

        /// <summary>
        /// Find approximate area containing the game
        /// </summary>
        /// <param name="full">Screenshot of main monitor</param>
        /// <returns>Area containing game</returns>
        public static Rectangle? findGameArea(Bitmap full)
        {
            //Prepare bytewise image processing

            int fullHeight = full.Height;
            int fullWidth = full.Width;
            BitmapData imgData = full.LockBits(new Rectangle(0, 0, fullWidth, fullHeight), ImageLockMode.ReadWrite, full.PixelFormat);
            int numBytes = Math.Abs(imgData.Stride) * imgData.Height;
            byte[] imgBytes = new byte[numBytes];
            Marshal.Copy(imgData.Scan0, imgBytes, 0, numBytes);
            int PixelSize = 4; //ARGB, reverse order
            full.UnlockBits(imgData);

            int minWidth = Screen.PrimaryScreen.Bounds.Width / 4;

            int x = fullWidth / 2; //probing via middle of screen, looking for white window header
            for (int y = fullHeight / 2; y > 0; y--)
            {
                int i_pos = 0;
                int i_neg = 0;
                int index = (y * fullWidth + x) * PixelSize;
                Color pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);

                //explore white area right
                while (x + i_pos < fullWidth * 0.99 && pixel.R > 220 && pixel.G > 220 && pixel.B > 220)
                {
                    i_pos++;
                    index = (y * fullWidth + x + i_pos) * PixelSize;
                    pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);
                }

                if (i_pos == 0)
                    continue;

                index = (y * fullWidth + x) * PixelSize;
                pixel = Color.FromArgb(imgBytes[index + 3], imgBytes[index + 2], imgBytes[index + 1], imgBytes[index]);
                //explore white area left
                while (x - i_neg > fullWidth * 0.01 && pixel.R > 220 && pixel.G > 220 && pixel.B > 220)
                {
                    i_neg++;
                    index = (y * fullWidth + x - i_neg) * PixelSize;
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
                while (height < fullHeight - y - 1)
                {
                    int row = 0;
                    int currStreak = 0;
                    int maxStreak = 0;
                    for (int i = 0; i < width * 0.3; i++)
                    {
                        index = ((y + height) * fullWidth + left + i) * PixelSize;
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
            return null;
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

            //tessEngine.SetVariable("tessedit_char_whitelist", @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210+%,:() ");
            //Copy relevant part of image
            int height = stop - start;
            Bitmap scanArea = new Bitmap(img.Width, height);
            using (Graphics g = Graphics.FromImage(scanArea))
            {
                Rectangle sourceRect = new Rectangle(0, start, img.Width, height);
                g.DrawImage(img, 0, 0, sourceRect, GraphicsUnit.Pixel);
            }
            scanArea.SetResolution(96, 96); //make sure DPI doesn't affect OCR results


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
        public static InventoryItem getArtifacts(Bitmap img, int[] rows, bool saveImages, TesseractEngine tessEngine, bool locked, int rarity, Rectangle typeMainArea, Rectangle levelArea, Rectangle subArea, Rectangle setArea, Rectangle charArea)
        {
            //get all potential text rows
            List<Tuple<int, int>> textRows = new List<Tuple<int, int>>();
            int i = 0;
            int height = img.Height;
            int width = img.Width;
            while (i + 1 < img.Height)
            {
                while (i + 1 < height && rows[i] / (double)width < 0.01)
                    i++;
                int rowTop = i;
                while (i + 1 < height && !(rows[i] / (double)width < 0.01))
                    i++;
                textRows.Add(Tuple.Create(Math.Max(0, rowTop - 3), Math.Min(height - 1, i + 3)));
            }

            //first row guaranteed to be of no use (artifact name etc)
            textRows.RemoveAt(0);


            InventoryItem foundArtifact = new InventoryItem();
            foundArtifact.locked = locked;
            foundArtifact.rarity = rarity;
            i = 0;
            //Piece type
            while (i < textRows.Count && textRows[i].Item2 <= typeMainArea.Top)
                i++;
            for (; i < textRows.Count && textRows[i].Item2 > typeMainArea.Top; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Pieces, out int resultIndex, out int dist, out string rawText, "", saveImages, tessEngine);
                if (dist < 3)
                {
                    foundArtifact.piece = Database.Pieces_trans[resultIndex];
                    i++;
                    break;
                }
            }

            //Main stat
            string prevRaw = "";
            while (i < textRows.Count && textRows[i].Item2 <= typeMainArea.Top)
                i++;
            for (; i < textRows.Count && textRows[i].Item2 > typeMainArea.Top; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.rarityData[rarity-1].MainStats, out int resultIndex, out int dist, out string rawText, prevRaw, saveImages, tessEngine);
                
                if (dist < rawText.Length - 2 && rawText.Length - 2 >= Regex.Replace(rawText, @"[0-9]", "").Length)
                {
                    foundArtifact.main = Database.rarityData[rarity-1].MainStats_trans[resultIndex];
                    i++;
                    break;
                }
                prevRaw = rawText;
            }

            //Level
            while (i < textRows.Count && textRows[i].Item2 <= levelArea.Top)
                i++;
            for (; i < textRows.Count && textRows[i].Item2 > levelArea.Top; i++)
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
            while (i < textRows.Count && textRows[i].Item2 <= subArea.Top)
                i++;
            for (; i < textRows.Count && textRows[i].Item2 > subArea.Top; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.rarityData[rarity-1].Substats, out int resultIndex, out int dist, out string rawText, "", saveImages, tessEngine);
                if (dist < 3)
                {
                    foundArtifact.subs.Add(Database.rarityData[rarity-1].Substats_trans[resultIndex]);
                    if (substat > 2)
                    {
                        i++;
                        break;
                    }
                    substat++;
                }
                else if (rawText.Length > 5)
                {
                    break;
                }
            }

            //Set
            int startRow = i;
            prevRaw = "";
            while (i < textRows.Count && textRows[i].Item2 <= setArea.Top)
                i++;
            for (; i < textRows.Count && textRows[i].Item2 > setArea.Top; i++)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.rarityData[rarity-1].Sets, out int resultIndex, out int dist, out string rawText, prevRaw, saveImages, tessEngine);
                if (dist < 5)
                {
                    foundArtifact.set = Database.rarityData[rarity-1].Sets_trans[resultIndex];
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
            for (i = textRows.Count - 1; i > Math.Max(0, textRows.Count - 6) && textRows[i].Item1 > charArea.Top - 10  ; i--)
            {
                string result = OCRRow(img, textRows[i].Item1, textRows[i].Item2, Database.Characters, out int resultIndex, out int dist, out string rawText, "", saveImages, tessEngine);
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
