using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Resources;

namespace WindowsFormsLab1
{
    public partial class Form1 : Form
    {
        ResourceManager rm;
        string Language { get; set; }
        Button activeButton = null;
        string activeButtonName;
        Bitmap DrawArea;
        List<Diagram> diagrams;
        Diagram selected;
        bool blink;
        bool drag;
        LinkNode linkStart = null;
        Bitmap copy;
        Point startDraggingPoint;
        public Form1()
        {
            Language = "pl-PL";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
            rm = new ComponentResourceManager(typeof(Form1));
            InitializeComponent();
            diagrams = new List<Diagram>();
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            activeButtonName = "Rect";
            InitControls();
        }
        void Clear()
        {
            selected = null;
            blink = false;
            drag = false;
            linkStart = null;
            textBox1.Text = "";
            textBox1.Enabled = false;
            diagrams.Clear();
        }
        private void InitControls()
        {
            activeButton = (Button)this.Controls.Find(activeButtonName, true)[0];
            pictureBox1.Image = DrawArea;   
            activeButton.BackColor = Color.Coral;
            if(selected!=null)
            {
                textBox1.Text = selected.Text;
                if(textBox1.Text =="Start" || textBox1.Text =="Stop")
                {
                    textBox1.Enabled = false;
                }
                else
                {
                    textBox1.Enabled = true;
                }
                
            }
        }
        private void UpdateDiagrams()
        {
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            foreach (Diagram d in diagrams)
            {
                d.Draw(DrawArea);
            }
            pictureBox1.Image = DrawArea;
            pictureBox1.Refresh();
        }
        bool FindStart()
        {
            bool result = false;
            foreach(Diagram k in diagrams)
            {
                if (k.Text == "Start")
                    result = true;
            }
            return result;
        }
        private Diagram ClickedDiagram(Point p)
        {
            Diagram result = null;
            foreach (Diagram d in diagrams)
                if (d.Contains(p)) result = d;
            return result;
        }
        private void SelectDiagram(Point e)
        {
            Diagram d = ClickedDiagram(e);
            UnselectDiagrams();
            if (d != null)
            {
                d.Selected = true;
                selected = d;
                textBox1.Text = d.Text;
                if (selected.Text != "Start" && selected.Text != "Stop")
                    textBox1.Enabled = true;
                else
                    textBox1.Enabled = false;
            }
            UpdateDiagrams();
        }
        private void UnselectDiagrams()
        {
            if (selected == null) return;
            selected.Selected = false;
            selected = null;
            textBox1.Text = "";
            textBox1.Enabled = false;
        }
        LinkNode GetLinkStartPoint(Point p)
        {
            Diagram d = ClickedDiagram(p);
            if (d == null) return null;
            return d.GetFreeOutputNodeFromPoint(p);
        }
        LinkNode GetLinkEndPoint(Point p)
        {
            Diagram d = ClickedDiagram(p);
            if (d == null) return null;
            return d.GetFreeInputFromPoint(p);
        }
        void DeleteDiagram(Diagram elem)
        {
            if (elem == null) return;
            if (elem == selected)
            {
                UnselectDiagrams();
            }  
            if(elem.Input != null)
                elem.Input.RemoveLink();
            if (elem.Outputs != null)
                foreach (var k in elem.Outputs)
                    k.RemoveLink();
            diagrams.Remove(elem);
            UpdateDiagrams();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drag || blink) return;
            if(e.Button == MouseButtons.Left)
            {
                Diagram elem = null;
                switch(activeButton.Name)
                {
                    case "Rect":
                        elem = new RectDiagram(e.X, e.Y, 50, 100, rm.GetString("RectString"));
                        break;
                    case "Rhomb":
                        elem = new RhombDiagram(e.X, e.Y, 100, rm.GetString("RhombString"));
                        break;
                    case "Trash":
                        elem = ClickedDiagram(e.Location);
                        DeleteDiagram(elem);
                        return;
                    case "Start":
                        if (FindStart())
                        {
                            MessageBox.Show(rm.GetString("WarningString"),rm.GetString("WarningLabel") , MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }
                        elem = new StartDiagram(e.X, e.Y, 60, 100, "Start");
                        break;
                    case "Stop":
                        elem = new StopDiagram(e.X, e.Y, 60, 100, "Stop"); ;
                        break;
                    case "Link":
                        linkStart = GetLinkStartPoint(e.Location);
                        if (linkStart == null) return;
                        blink = true;
                        copy = (Bitmap)DrawArea.Clone();
                        return;
                }
                diagrams.Add(elem);
                elem.Draw(DrawArea);
                pictureBox1.Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                SelectDiagram(e.Location);
            }
            else if(e.Button == MouseButtons.Middle)
            {
                if (selected == null) return;
                drag = true;
                startDraggingPoint = new Point(e.X, e.Y);
            }
        }
        private void Rect_Click(object sender, EventArgs e)
        {
            if(activeButton!=null)
            {
                activeButton.BackColor = SystemColors.Control;
            }
            activeButton = (Button)sender;
            activeButton.BackColor = Color.Coral;
        }
        private void newButton_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(Language);
            f.ShowDialog();
            int width, height;
            if(f.GetSize(out width,out height))
            {
                DrawArea = new Bitmap(width, height);
                Clear();
                pictureBox1.Image = DrawArea;
                pictureBox1.Refresh();
            }
            f.Dispose();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "diag files (*.diag)|*.diag";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(selected!=null)
                    selected.Selected = false;
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    BinaryFormatter bw = new BinaryFormatter();
                    SavedData saved = new SavedData();
                    saved.diag = diagrams;
                    saved.h = pictureBox1.Height;
                    saved.w = pictureBox1.Width;
                    bw.Serialize(myStream, saved);
                    myStream.Close();
                }
                if(selected!=null)
                    selected.Selected = true; ;
            }
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "diag files (*.diag)|*.diag";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                try 
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if ((myStream = openFileDialog.OpenFile()) != null)
                        {
                            BinaryFormatter br = new BinaryFormatter();
                            SavedData saved;
                            saved = (SavedData)br.Deserialize(myStream);
                            myStream.Close();
                            pictureBox1.Image = new Bitmap(saved.w, saved.h);
                            diagrams = saved.diag;
                            UpdateDiagrams();
                        }
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show(rm.GetString("LoadFail"),rm.GetString("FailLabel"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.selected = null;
                blink = false;
                drag = false;
                linkStart = null;
                textBox1.Text = "";
                textBox1.Enabled = false;
            }
        }
        private void Rect_MouseEnter(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            if (p == activeButton)
                p.BackColor = Color.CadetBlue;
            else
                p.BackColor = SystemColors.ControlDark;
        }
        private void Rect_MouseLeave(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            if (p == activeButton)
                p.BackColor = Color.Coral;
            else
                p.BackColor = SystemColors.Control;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (selected == null) return;
            selected.Text = ((TextBox)textBox1).Text;
            UpdateDiagrams();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(blink && e.Button == MouseButtons.Left)
            {
                blink = false;
                LinkNode linkEnd = GetLinkEndPoint(e.Location);
                if (linkEnd == null)
                {
                    pictureBox1.Image = (Image)DrawArea;
                    return;
                }
                linkStart.CreateLink(linkEnd);
                linkStart = null;
                UpdateDiagrams();
            }
            else if(drag && MouseButtons.Middle == e.Button)
            {
                drag = false;
                //Snap
                Point p = selected.GetCenter();
                int dx=0, dy=0;
                if(p.X < 0)
                {
                    dx = -p.X;
                }
                if(p.Y < 0)
                {
                    dy = -p.Y;
                }
                if(p.X > pictureBox1.Width)
                {
                    dx = pictureBox1.Width - p.X;
                }
                if(p.Y > pictureBox1.Height)
                {
                    dy = pictureBox1.Height - p.Y;
                }
                selected.Move(dx, dy);
                UpdateDiagrams();
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (blink && linkStart!=null)
            {
                copy = (Bitmap)DrawArea.Clone();
                pictureBox1.Image = (Image)copy;
                using (Graphics g = Graphics.FromImage(copy))
                {
                    g.DrawLine(Pens.Black, linkStart.GetCenter(), e.Location);
                    pictureBox1.Refresh();
                }
            }
            else if(drag && startDraggingPoint!=null)
            {
                int dx = startDraggingPoint.X - e.X;
                int dy = startDraggingPoint.Y - e.Y;
                startDraggingPoint = e.Location;
                selected.Move(-dx, -dy);
                UpdateDiagrams();
            }
        }
        private void PolishButton_Click(object sender, EventArgs e)
        {
            Language = "pl-PL";
            ChangeLanguage(Language);
        }
        private void englishButton_Click(object sender, EventArgs e)
        {
            Language = "en-GB";
            ChangeLanguage(Language);
        }
        private void ChangeLanguage(string lang)
        {
            activeButtonName = activeButton.Name;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
            this.Controls.Clear();
            rm = new System.Resources.ResourceManager(typeof(Form1));
            Size size = this.Size;
            InitializeComponent();
            this.Size = size;
            InitControls();
        }
    }
}