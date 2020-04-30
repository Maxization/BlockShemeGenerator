using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace WindowsFormsLab1
{
    [Serializable]
    class SavedData
    {
        public List<Diagram> diag;
        public int w;
        public int h;
    }

    //output------>input
    [Serializable]
    class LinkNode
    {
        public bool Used { get; private set; }
        public Diagram Parent { get; set; }
        Rectangle rc;
        bool output;
        public LinkNode connected;

        public LinkNode(Rectangle rc,Diagram parent, bool output)
        {
            this.rc = rc;
            this.Parent = parent;
            this.output = output;
        }

        public bool Contains(Point p)
        {
            return rc.Contains(p);
        }

        public void RemoveLink()
        {
            if (!Used) return;
            Used = false;
            connected.RemoveLink();
            connected = null;
        }

        public void Move(int dx,int dy)
        {
            rc.X += dx;
            rc.Y += dy;
        }

        public void CreateLink(LinkNode node)
        {
            if (Used || node.Parent == this.Parent) return;
            Used = true;
            connected = node;
            node.CreateLink(this);
        }

        public Point GetCenter()
        {
            return new Point(rc.X + rc.Width / 2, rc.Y + rc.Height / 2);
        }

        public void Draw(Bitmap b)
        {
            DrawLink(b);
            if (Used) return;
            using (Graphics g = Graphics.FromImage(b))
            {
                Pen pen = new Pen(Color.Black, 2);
                if (output)
                    g.FillEllipse(Brushes.Black, rc);
                else
                    g.FillEllipse(Brushes.White, rc);
                g.DrawEllipse(pen, rc);

                pen.Dispose();
                g.Dispose();
            }
        }

        public void DrawLink(Bitmap b)
        {
            if (!Used) return;
            using (Graphics g = Graphics.FromImage(b))
            {
                Point op = GetCenter();
                Point ip = connected.GetCenter();
                if (!output) return;   
                Pen pen = new Pen(Color.Black, 2);
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    capPath.AddLine(-5, -5, 0, 0);
                    capPath.AddLine(0, 0, 5, -5);

                    pen.CustomEndCap = new CustomLineCap(null, capPath);
                    g.DrawLine(pen, op, ip);
                    pen.Dispose();
                    g.Dispose();
                }

            }
        }
    }
    [Serializable]
    abstract class Diagram
    {
        [NonSerialized]
        protected StringFormat stringFormat;
        protected const int linkPointSize = 8;
        protected Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
        public string Text { get; set; }
        public bool Selected { get; set; }
        public abstract LinkNode[] Outputs { get; }
        public abstract LinkNode Input { get; }

        protected int Width { set; get; }
        protected int Height { set; get; }
        protected int X, Y;

        [NonSerialized]
        public Rectangle rc;
        public Diagram(int x,int y,int height, int width,string text)
        {
            rc = new Rectangle(x - width / 2, y - height / 2,width,height);
            X = x - width / 2;
            Y = y - height / 2;
            Width = width;
            Height = height;
            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            this.Text = text;
        }

        ~Diagram()
        {
            font.Dispose();
        }

        public Point GetCenter()
        {
            return new Point(X + Width / 2, Y + Height / 2);
        }

        public void Move(int dx,int dy)
        {
            X += dx;
            Y += dy;
            rc.X = X;
            rc.Y = Y;
            if (Input != null)
                Input.Move(dx, dy);
            if (Outputs != null)
                foreach(var k in Outputs)
                {
                    k.Move(dx, dy);
                }
        }

        public LinkNode GetFreeOutputNodeFromPoint(Point p)
        {
            foreach (var k in Outputs)
                if (k.Contains(p) && !k.Used)
                    return k;
            return null;
        }

        public LinkNode GetFreeInputFromPoint(Point p)
        {
            if (Input.Contains(p) && !Input.Used) return Input;
            return null;
        }

        public abstract bool Contains(Point p);
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            rc = new Rectangle(X, Y, Width, Height);
            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }
        public abstract void Draw(Bitmap b);
    }

    [Serializable]
    class RectDiagram : Diagram
    {
        private LinkNode output;
        private LinkNode input;
        public override LinkNode[] Outputs { get => new LinkNode[] { output}; }
        public override LinkNode Input { get => input; }

        public RectDiagram(int x, int y, int h, int w,string text) : base(x, y, h, w,text)
        {
            Rectangle lout = new Rectangle(rc.X + Width / 2 - linkPointSize / 2, rc.Y + Height - linkPointSize / 2, linkPointSize, linkPointSize);
            Rectangle lin = new Rectangle(rc.X + Width / 2 - linkPointSize / 2, rc.Y - linkPointSize / 2, linkPointSize, linkPointSize);
            output = new LinkNode(lout, this,true);
            input = new LinkNode(lin, this,false);
        }

        public override void Draw(Bitmap b)
        {
            using(Graphics g = Graphics.FromImage(b))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                Pen pen = new Pen(Color.Black, 2);
                if(Selected)
                {
                    pen.DashPattern = new float[] { 4.0F, 2.0F };
                }
                g.FillRectangle(Brushes.White, rc);
                g.DrawRectangle(pen, rc);
                g.DrawString(Text, font, Brushes.Black, rc, stringFormat);

                input.Draw(b);
                output.Draw(b);
                pen.Dispose();
                g.Dispose();
            }
        }

        public override bool Contains(Point p)
        {
            Rectangle rectangle = new Rectangle(rc.X - linkPointSize / 2, rc.Y - linkPointSize / 2, Width + linkPointSize, Height + linkPointSize);
            return rectangle.Contains(p);
        }
    }

    [Serializable]
    class RhombDiagram : Diagram
    {
        private LinkNode outputFalse;
        private LinkNode outputTrue;
        private LinkNode input;

        public override LinkNode Input { get => input; }

        public override LinkNode[] Outputs { get => new LinkNode[] { outputFalse,outputTrue }; }
        public RhombDiagram(int x, int y, int h, string text) : base(x, y, h, h, text)
        {
            Rectangle outFalse = new Rectangle(rc.X - linkPointSize / 2, rc.Y + Height / 2 - linkPointSize / 2, linkPointSize, linkPointSize);
            Rectangle outTrue = new Rectangle(rc.X + Width - linkPointSize / 2, rc.Y + Height / 2 - linkPointSize / 2, linkPointSize, linkPointSize);
            Rectangle lin= new Rectangle(rc.X + Width / 2 - linkPointSize / 2, rc.Y - linkPointSize / 2, linkPointSize, linkPointSize);
            outputFalse = new LinkNode(outFalse, this, true);
            outputTrue = new LinkNode(outTrue, this, true);
            input = new LinkNode(lin, this, false);
        }

        public override void Draw(Bitmap b)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                Pen pen = new Pen(Color.Black, 2);
                if (Selected)
                {
                    pen.DashPattern = new float[] { 4.0F, 2.0F };
                }
                Rectangle rectangle = new Rectangle(rc.X - linkPointSize / 2, rc.Y - linkPointSize / 2, Width + linkPointSize, Height + linkPointSize);
                int hy = (int)((double)rectangle.Height * 1f / 4f);
                int hx = (int)((double)rectangle.Width * 1f / 4f);
                Rectangle drawRect = new Rectangle(rectangle.X + hx - 5, rectangle.Y + hy, rectangle.Width - 2*hx + 10, rectangle.Height - 2*hy);
                Rectangle rect = new Rectangle(rectangle.X, rectangle.Y + hy, rectangle.Width, 2 * hy);
                StringFormat stringF = new StringFormat();
                stringF.Alignment = StringAlignment.Far;
                stringF.LineAlignment = StringAlignment.Near;
                Font font2 = new Font(new FontFamily("Arial"), 10, FontStyle.Regular, GraphicsUnit.Point);
                g.DrawString("F", font2, Brushes.Black,rect, stringF);
                stringF.Alignment = StringAlignment.Near;
                g.DrawString("T", font2, Brushes.Black, rect, stringF);

                Point[] points = { new Point(X + Width/2, Y), new Point(X ,Y+Height/2), new Point(X + Width/2,Y+Height),new Point(X+Width,Y+Height/2)};
                g.FillPolygon(Brushes.White, points);
                g.DrawPolygon(pen, points);
                g.DrawString(Text, font, Brushes.Black, drawRect, stringFormat);

                input.Draw(b);
                outputFalse.Draw(b);
                outputTrue.Draw(b);

                pen.Dispose();
                font2.Dispose();
                g.Dispose();
            }
        }

        public override bool Contains(Point p)
        {
            Rectangle rectangle = new Rectangle(rc.X - linkPointSize / 2, rc.Y - linkPointSize / 2, Width + linkPointSize, Height + linkPointSize);
            Point[] points = { new Point(rectangle.X, rectangle.Y + rectangle.Height / 2), new Point(rectangle.X + rectangle.Width / 2, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height / 2), new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height) };
            for(int i=0;i<points.Length;i++)
            {
                Point p1 = points[i];
                Point p2 = points[(i + 1) % points.Length];

                int a = (int)((double)(p1.Y - p2.Y) / (double)(p1.X - p2.X));
                int b = (int)((double)(p1.X * p2.Y - p2.X * p1.Y) / (double)(p1.X - p2.X));
                if(i<=1)
                {
                    if (p.Y < a * p.X + b)
                        return false;
                }
                else
                {
                    if (p.Y > a * p.X + b)
                        return false;
                }
            }
            return true;
        }
    }

    [Serializable]
    class StartDiagram : Diagram
    {
        private LinkNode output;
        public override LinkNode Input { get => null; }
        public override LinkNode[] Outputs { get => new LinkNode[] { output }; }
        public StartDiagram(int x, int y, int h, int w, string text) : base(x, y, h, w, text)
        {
            Rectangle outl = new Rectangle(rc.X + Width / 2 - linkPointSize / 2, rc.Y + Height - linkPointSize / 2, linkPointSize, linkPointSize);
            output = new LinkNode(outl, this, true);
        }

        public override void Draw(Bitmap b)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                Pen pen = new Pen(Color.Green, 2);
                if (Selected)
                {
                    pen.DashPattern = new float[] { 4.0F, 2.0F };
                }
                g.FillEllipse(Brushes.White, rc);
                g.DrawEllipse(pen, rc);
                g.DrawString(Text, font, Brushes.Black, rc, stringFormat);

                output.Draw(b);
                pen.Dispose();
                g.Dispose();
            }
        }

        public override bool Contains(Point p)
        {
            Rectangle rectangle = new Rectangle(rc.X - linkPointSize / 2, rc.Y - linkPointSize / 2, Width + linkPointSize, Height + linkPointSize);
            int a = rectangle.Width / 2;
            int b = rectangle.Height / 2;
            Point s = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            return (double)((p.X - s.X) * (p.X - s.X)) / (double)(a * a) + (double)((p.Y - s.Y) * (p.Y - s.Y)) / (double)(b * b) <= 1;
        }
    }

    [Serializable]
    class StopDiagram : Diagram
    {
        private LinkNode input;
        public override LinkNode Input { get => input; }
        public override LinkNode[] Outputs { get => null; }
        public StopDiagram(int x, int y, int h, int w, string text) : base(x, y, h, w, text)
        {
            Rectangle inl = new Rectangle(rc.X + Width / 2 - linkPointSize / 2, rc.Y - linkPointSize / 2, linkPointSize, linkPointSize);
            input = new LinkNode(inl, this, false);
        }

        public override void Draw(Bitmap b)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                Pen pen = new Pen(Color.Red, 2);
                if (Selected)
                {
                    pen.DashPattern = new float[] { 4.0F, 2.0F };
                }
                g.FillEllipse(Brushes.White, rc);
                g.DrawEllipse(pen, rc);
                g.DrawString(Text, font, Brushes.Black, rc, stringFormat);

                input.Draw(b);
                pen.Dispose();
                g.Dispose();
            }
        }

        public override bool Contains(Point p)
        {
            Rectangle rectangle = new Rectangle(rc.X - linkPointSize / 2, rc.Y - linkPointSize / 2, Width + linkPointSize, Height + linkPointSize);
            int a = rectangle.Width / 2;
            int b = rectangle.Height / 2;
            Point s = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            return (double)((p.X - s.X) * (p.X - s.X)) / (double)(a * a) + (double)((p.Y - s.Y) * (p.Y - s.Y)) / (double)(b * b) <= 1;
        }
    }
}
