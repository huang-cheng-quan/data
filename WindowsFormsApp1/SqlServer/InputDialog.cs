using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.SqlServer
{
    public static class InputDialog
    {
        public static DialogResult Show(out string strText)
        {
            string strTemp = string.Empty;

            FormInput inputDialog = new FormInput();
            inputDialog.TextHandler = (str) => { strTemp = str; };

            DialogResult result = inputDialog.ShowDialog();
            strText = strTemp;

            return result;
        }
    }
}
