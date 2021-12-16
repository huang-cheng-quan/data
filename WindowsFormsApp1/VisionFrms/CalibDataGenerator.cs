using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera_Capture_demo
{
    class CalibDataGenerator
    {
        public bool ExportCalibImg(string filePath, int numRows, int numColumns, float diameter, float distance)
        {
            try
            {
                distance = distance - diameter;
                if(distance <= 0)
                {
                    return false;
                }
                int imgWidth = Convert.ToInt32(numRows * (diameter + distance) + distance);
                int imgHeight = Convert.ToInt32(numColumns * (diameter + distance) + distance);
                Bitmap bmp = new Bitmap(imgWidth, imgHeight);
                Graphics gs = Graphics.FromImage(bmp);
                Metafile mf = new Metafile(filePath, gs.GetHdc());
                Graphics g = Graphics.FromImage(mf);
                g.PageUnit = GraphicsUnit.Millimeter;
                g.DrawRectangle(new Pen(Color.Black, 1f), 0, 0, imgWidth, imgHeight);
                Draw(g, numRows, numColumns, diameter, distance );
                g.Save();
                g.Dispose();
                mf.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Draw(Graphics g, int numRows, int numColumns, float diameter, float distance)
        {
            for (int i = 0; i < numRows; i++)
            {
                float x = i * (distance + diameter) + distance;
                for (int j = 0; j < numColumns; j++)
                {
                    float y = j * (distance + diameter) + distance;
                    g.FillEllipse(Brushes.Black, x, y, diameter, diameter);
                    g.DrawLine(new Pen(Color.White, 0.2f), new PointF(x + 1, y + (float)diameter / 2), new PointF(x + diameter - 1, y + (float)diameter / 2));
                    g.DrawLine(new Pen(Color.White, 0.2f), new PointF(x + (float)diameter / 2, y + 1), new PointF(x + (float)diameter / 2, y + diameter-1));
                }
            }
        }
    }
}
