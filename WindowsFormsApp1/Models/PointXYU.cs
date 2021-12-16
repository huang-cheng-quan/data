using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera_Capture_demo.Models
{
    /// <summary>
    /// 带排序方法的输出坐标点类
    /// </summary>
    public class PointXYU : IComparable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float U { get; set; }

        public int CompareTo(object obj)
        {
            PointXYU p = obj as PointXYU;
            return p.X.CompareTo(X);
        }
    }
}
