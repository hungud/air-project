using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Collections;

namespace App.Base
{

    /// <summary>
    /// Class for generate Captcha. 
    ///
    ///<Author Name = “Anjul Krishna” />
    ///<Creation Date = “23rd February 2012” />
    ///<Last Modified By = “” />
    ///<Last Modification Date = “” >
    ///<Modification Comments = “” />
    ///
    /// </summary>
    internal class Captcha
    {
        // Public properties (all read-only).
        public string Text
        {
            get { return this.text; }
        }
        public Bitmap Image
        {
            get { return this.image; }
        }
        public int Width
        {
            get { return this.width; }
        }
        public int Height
        {
            get { return this.height; }
        }

        // Internal properties.
        private string text;
        private int width;
        private int height;
        private string familyName;
        private Bitmap image;

        // For generating random numbers.
        private Random random = new Random();

        // ====================================================================
        // Initializes a new instance of the CaptchaImage class using the
        // specified text, width and height.
        // ====================================================================
        public Captcha(string s, int width, int height)
        {
            this.text = s;
            this.SetDimensions(width, height);
            this.GenerateImage();
        }

        // ====================================================================
        // Initializes a new instance of the CaptchaImage class using the
        // specified text, width, height and font family.
        // ====================================================================
        public Captcha(string s, int width, int height, string familyName)
        {
            this.text = s;
            this.SetDimensions(width, height);
            this.SetFamilyName(familyName);
            this.GenerateImage();
        }

        // ====================================================================
        // This member overrides Object.Finalize.
        // ====================================================================
        ~Captcha()
        {
            Dispose(false);
        }

        // ====================================================================
        // Releases all resources used by this object.
        // ====================================================================
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        // ====================================================================
        // Custom Dispose method to clean up unmanaged resources.
        // ====================================================================
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                // Dispose of the bitmap.
                this.image.Dispose();
        }

        // ====================================================================
        // Sets the image width and height.
        // ====================================================================
        private void SetDimensions(int width, int height)
        {
            // Check the width and height.
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
            this.width = width;
            this.height = height;
        }

        // ====================================================================
        // Sets the font used for the image text.
        // ====================================================================
        private void SetFamilyName(string familyName)
        {
            // If the named font is not installed, default to a system font.
            try
            {
                Font font = new Font(this.familyName, 12F,FontStyle.Bold);
                this.familyName = familyName;                
                font.Dispose();
            }
            catch (Exception ex)
            {
                this.familyName = System.Drawing.FontFamily.GenericSerif.Name;
            }
        }

        // ====================================================================
        // Creates the bitmap image.
        // ====================================================================
        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);

            // Fill in the background.
            ///////////////////////////////////////////////////
            //HatchBrush hatchBrush = new HatchBrush(HatchStyle.DottedDiamond,Color.White,Color.FromArgb(106,52,28));
            //HatchBrush hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.FromArgb(106, 52, 28), Color.FromArgb(254, 228, 188));

            HatchBrush hatchBrush = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.FromArgb(139, 0, 0), Color.FromArgb(222, 185, 199));

            ///////////////////////////////////////////////////
            g.FillRectangle(hatchBrush, rect);

            // Set up the text font.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(this.familyName, fontSize, FontStyle.Italic);
                size = g.MeasureString(this.text, font);
            }while (size.Width > rect.Width);
            

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            
            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            //path.AddString(this.text, font.FontFamily, (int) font.Style, font.Size, rect, format);
            path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size + 1, rect, format);
            float v = 9F;
            PointF[] points =
            {
                new PointF(this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
                new PointF(this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v)
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Draw the text.
            ///////////////////////////////////////////////////
            //hatchBrush = new HatchBrush(HatchStyle.Min, Color.FromArgb(106, 52, 28), Color.FromArgb(106, 52, 28));
            //hatchBrush = new HatchBrush(HatchStyle.Min, Color.FromArgb(163, 29, 73), Color.FromArgb(163, 29, 73));
            hatchBrush = new HatchBrush(HatchStyle.Min, Color.FromArgb(163, 29, 73), Color.FromArgb(0, 0, 0));

            ///////////////////////////////////////////////////
            g.FillPath(hatchBrush, path);

            // Add some random noise.
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int) (rect.Width * rect.Height / 100F); i++)
            {
                int x = this.random.Next(rect.Width);
                int y = this.random.Next(rect.Height);
                int w = this.random.Next(m / 50);
                int h = this.random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }

        // ====================================================================
        // SAVE the bitmap image.
        // ====================================================================
        public void ByteArrayToImage(byte[] byteArrayIn, string strMapFileName)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                System.Drawing.Image MapImage = System.Drawing.Image.FromStream(ms);
                MapImage.Save(strMapFileName);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }







        // <summary>
        /// Generate an Captcha image and add details in captcha table.
        /// </summary>
        /// <param name="prevCaptchaID">prevCaptchaID</param>
        /// <returns>ARRAY OF CAPTCHA BYTEARRAY AND CAPTCHA ID</returns>
        public ArrayList GenerateCaptcha(Int32 prevCaptchaID)
        {
            ArrayList arrayList = null;
            try
            {
                //delete captcha details from captcha table
                //string strResult = string.Empty;
                //if (prevCaptchaID > 0)
                //    strResult = DeleteCaptchaFromDatabase(prevCaptchaID);

                //string captchaString = string.Empty;

                //captchaString = GetCaptchaString(6);

                //CommonFunctions.WriteLog("...Generate Captcha.......");

                //Captcha ch = new Captcha(captchaString, 170, 30);

                //Bitmap bmp = ch.Image;

                //MemoryStream ms = new MemoryStream();
                //bmp.Save(ms, ImageFormat.Jpeg);

                //byte[] ImageBytes = ms.GetBuffer();

                ////Save captcha Image into CAPTCHA table
                //string captchaID = AddCaptchaInDatabase(ImageBytes, captchaString);

                //arrayList = new ArrayList(2);
                //arrayList.Add(ImageBytes);
                //arrayList.Add(captchaID);

                return arrayList;
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }

        string GetCaptchaString(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = 'A';
            int intZ = 'Z';
            int intCount = 0;
            int intRandomNumber = 0;
            string strCaptchaString = "";

            Random random = new Random(System.DateTime.Now.Millisecond);

            while (intCount < length)
            {
                intRandomNumber = random.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ)))
                {
                    strCaptchaString = strCaptchaString + (char)intRandomNumber;
                    intCount = intCount + 1;
                }
            }
            return strCaptchaString;
        }

        byte[] GetCaptchaImage(string captchaString)
        {
            Bitmap bmp = new Bitmap(100, 30);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.DarkSeaGreen);
            string randomString = GetCaptchaString(6);
            g.DrawString(randomString, new Font("Courier", 16), new SolidBrush(Color.WhiteSmoke), 2, 2);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            bmp.Dispose();

            return ms.GetBuffer();
        }

        string randString(string type, int counter)
        {
            int i = 1;
            String randStr = "";
            decimal randNum = 0;
            String useList = "";
            String alphaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            String secure = "!@$%&*-_=+?~";
            for (i = 1; i <= counter; i++)
            {
                if (type == "alphaChars")
                {
                    useList = alphaChars;
                }
                else if (type == "alphaCharsnum")
                {
                    useList = alphaChars + "0123456789";
                }
                else if (type == "secure")
                {
                    useList = alphaChars + "0123456789" + secure;
                }
                else
                {
                    useList = "0123456789";
                }
                Random random = new Random(100);
                int len = useList.Length;
                decimal temp = random.Next() * len;
                randNum = Math.Floor(temp);
                randStr = randStr + useList.Substring(Convert.ToInt32(Math.Round(randNum, 0)), 1);

            }
            return randStr;
        }
    }
}

