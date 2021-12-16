using Camera_Capture_demo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryFeederDemo.VisionFrms
{
    public partial class CalibDataSettingFrm : Form
    {
        static int rows=0, cols=0;
        static float distance=0, diameter=0;

        public CalibDataSettingFrm()
        {
            InitializeComponent();
        }

        private void CalibDataSettingFrm_Load(object sender, EventArgs e)
        {
            txtRows.Text = rows.ToString();
            txtCols.Text = cols.ToString();
            txtDistance.Text = distance.ToString();
            txtDiameter.Text = diameter.ToString();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string pattern = @"^\d*[.]?\d*$";
            if (!Regex.IsMatch(txtRows.Text, pattern)|| !Regex.IsMatch(txtCols.Text, pattern) || !Regex.IsMatch(txtDistance.Text, pattern) || !Regex.IsMatch(txtDiameter.Text, pattern))
            {
                MessageBox.Show("参数输入错误!");
                return;
            }

            rows = Convert.ToInt32(txtRows.Text);
            cols = Convert.ToInt32(txtCols.Text);
            diameter = float.Parse(txtDiameter.Text);
            distance = float.Parse(txtDistance.Text);

            CalibDataGenerator generator = new CalibDataGenerator();
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "矢量图元文件(*.emf)|*.emf";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                generator.ExportCalibImg(fileDialog.FileName, rows, cols, diameter, distance);
            }
        }
    }
}
