using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebAPIOne.Abstract
{
    public abstract class IService
    {
        protected readonly Guid _serviceGuid;

        public IService()
        {
            _serviceGuid = Guid.NewGuid();
        }
    }

    public abstract class AbstractValuesService: IService
    {
        public abstract byte[] Bitmap2Byte(Bitmap bitmap, ImageFormat imageFormat);
    }
}
