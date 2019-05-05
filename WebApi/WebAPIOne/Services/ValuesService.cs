using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using WebAPIOne.Abstract;

namespace WebAPIOne.Services
{
    public class ValuesService: AbstractValuesService
    {
        #region Methods.
        public override byte[] Bitmap2Byte(Bitmap bitmap, ImageFormat imageFormat)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, imageFormat);
                byte[] imageBytes = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(imageBytes, 0, Convert.ToInt32(stream.Length));
                return imageBytes;
            }
        }
        #endregion
    }
}
