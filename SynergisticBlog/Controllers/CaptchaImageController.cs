using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SynergisticBlog.Controllers
{
    public class CaptchaImageController : Controller
    {
        private const int ImageWidth = 200, ImageHeight = 70;        
        private readonly static Brush Foreground = Brushes.Navy;
        private readonly static Brush Background = Brushes.Navy;

        public void Render(string challengeGuid)
        {
            string SessionKeyPrefix = "_Captcha";
            string solution = Session[SessionKeyPrefix + challengeGuid] as string;

            if (solution != null)
            {
                using (Bitmap bmp = new Bitmap(ImageWidth, ImageHeight))
                using (Graphics g = Graphics.FromImage(bmp))
                using (Font font = new Font("Times New Roman", 12.0f))
                {
                    g.Clear(Color.Silver);

                    SizeF finalSize;
                    SizeF testSize = g.MeasureString(solution, font);

                    float bestFontSize = Math.Min(ImageWidth / testSize.Width,
                        ImageHeight / testSize.Height) * 0.95f;

                    using (Font finalFont = new Font("Times New Roman", bestFontSize))
                    {
                        finalSize = g.MeasureString(solution, finalFont);
                    }

                    g.PageUnit = GraphicsUnit.Point;
                    PointF textTopLeft = new PointF((ImageWidth - finalSize.Width) / 2,
                        (ImageHeight - finalSize.Height) / 2);

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(solution, new FontFamily("Times New Roman"), 0,
                            bestFontSize, textTopLeft, StringFormat.GenericDefault);

                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.FillPath(Foreground, path);
                        g.Flush();

                        Response.ContentType = "image/gif";
                        bmp.Save(Response.OutputStream, ImageFormat.Gif);
                    }
                }
            }            
        }

    }
}
