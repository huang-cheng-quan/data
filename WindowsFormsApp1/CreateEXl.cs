using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class CreateEXl : Form
    {
        public CreateEXl()
        {
            InitializeComponent();
        }
        CreateTheExl Test = new CreateTheExl();
        XLWorkbook G_w = new XLWorkbook();
        IXLWorksheet iws;
        string file;
        DirectoryInfo dr;
        string filename;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();        
            ofd.Title = "请选择文件夹";
            ofd.Filter = "所有文件(*.*)|*.*";
            ofd.InitialDirectory =Application.StartupPath+ @"\2022";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 file = ofd.FileName;
               string[] s=file.Split('\\');
                filename = s[10].Split('.').First();
                dr = Directory.GetParent(file);
                file = dr.ToString();
                this.button2.Enabled = true;

            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Test = Camera_Capture_demo.Helpers.XmlHelper02.DeserializeFromXml<CreateTheExl>(filename, file);
            G_w.AddWorksheet("缺陷1");
            string s = DateTime.Today.ToString("yyyyMMddHHmmss") + ".xlsx";
            G_w.SaveAs(file+"\\"+s);          
            G_w = new XLWorkbook(file + "\\" + s);
            iws = G_w.Worksheet(1);
            iws.ColumnWidth = 30;
            iws.RowHeight = 15;
            iws.Cell(1, 1).Value = "序列号";
            iws.Cell(1, 2).Value = "推理机缺陷结果";
            iws.Cell(1, 3).Value = "Score";
            iws.Cell(1, 4).Value = "序列号";
            iws.Cell(1, 5).Value = "连接器缺陷结果";
            iws.Cell(1, 6).Value = "序列号";
            iws.Cell(1, 7).Value = "连接器钢片缺陷结果";
            iws.Cell(1, 8).Value = "治具号";
            int i = 1, k = 1,j=1,l=1,m=1,n=1,o=1,p=1;
            foreach (var item in Test.ProductionDataIdToExl)
            {
                i++;
                iws.Cell(i, 1).Value = item;
            }
            foreach (var item in Test.ProductionDataResultCodeToExl)
            {
                k++;
                iws.Cell(k, 2).Value = item;
            }
            foreach (var item in Test.ProductionDataResultScoreToExl)
            {
                j++;
                iws.Cell(j, 3).Value = item;
            }
            foreach (var item in Test.ConnectProductionDataResultIdToXmlExl)
            {
                l++;
                iws.Cell(l, 4).Value = item;
            }
            foreach (var item in Test.ConnectProductionDataResultCodeToXmlExl)
            {
                m++;
                iws.Cell(m, 5).Value = item;
            }
            foreach (var item in Test.ConnectProductionDataResultIdToXmlExl1)
            {
                n++;
                iws.Cell(n, 6).Value = item;
            }
            foreach (var item in Test.ConnectProductionDataResultCodeToXmlExl1)
            {
                o++;
                iws.Cell(o, 7).Value = item;
            }
            foreach (var item in Test.FixtureNumberToXml)
            {
                p++;
                iws.Cell(p, 8).Value = item;
            }
            G_w.Save();
            MessageBox.Show("成功");
        }
    }
}
