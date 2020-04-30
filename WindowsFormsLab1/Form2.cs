using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsLab1
{
    public partial class Form2 : Form
    {
        bool clicked = false;
        public Form2(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        public Form2()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        
        public bool GetSize(out int width,out int height)
        {
            width = (int)numericUpDown2.Value;
            height = (int)numericUpDown1.Value;
            return clicked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clicked = true;
            Close();
        }
    }
}
