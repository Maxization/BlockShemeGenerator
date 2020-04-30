namespace WindowsFormsLab1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.fileBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.loadButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.editBox = new System.Windows.Forms.GroupBox();
            this.EditionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Link = new System.Windows.Forms.Button();
            this.Rect = new System.Windows.Forms.Button();
            this.Rhomb = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Trash = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LanguageBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.englishButton = new System.Windows.Forms.Button();
            this.PolishButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.fileBox.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.editBox.SuspendLayout();
            this.EditionPanel.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.LanguageBox.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.fileBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.editBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.LanguageBox, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // fileBox
            // 
            resources.ApplyResources(this.fileBox, "fileBox");
            this.fileBox.Controls.Add(this.tableLayoutPanel3);
            this.fileBox.Name = "fileBox";
            this.fileBox.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.loadButton, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.newButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.saveButton, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // loadButton
            // 
            resources.ApplyResources(this.loadButton, "loadButton");
            this.loadButton.Name = "loadButton";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // newButton
            // 
            resources.ApplyResources(this.newButton, "newButton");
            this.newButton.Name = "newButton";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // editBox
            // 
            this.editBox.Controls.Add(this.EditionPanel);
            this.editBox.Controls.Add(this.label1);
            this.editBox.Controls.Add(this.tableLayoutPanel5);
            resources.ApplyResources(this.editBox, "editBox");
            this.editBox.Name = "editBox";
            this.editBox.TabStop = false;
            // 
            // EditionPanel
            // 
            resources.ApplyResources(this.EditionPanel, "EditionPanel");
            this.EditionPanel.Controls.Add(this.Link, 2, 0);
            this.EditionPanel.Controls.Add(this.Rect, 0, 0);
            this.EditionPanel.Controls.Add(this.Rhomb, 1, 0);
            this.EditionPanel.Controls.Add(this.Start, 0, 1);
            this.EditionPanel.Controls.Add(this.Stop, 1, 1);
            this.EditionPanel.Controls.Add(this.Trash, 2, 1);
            this.EditionPanel.Name = "EditionPanel";
            // 
            // Link
            // 
            this.Link.BackgroundImage = global::WindowsFormsLab1.Properties.Resources.link;
            resources.ApplyResources(this.Link, "Link");
            this.Link.Name = "Link";
            this.Link.UseVisualStyleBackColor = true;
            this.Link.Click += new System.EventHandler(this.Rect_Click);
            this.Link.MouseEnter += new System.EventHandler(this.Rect_MouseEnter);
            this.Link.MouseLeave += new System.EventHandler(this.Rect_MouseLeave);
            // 
            // Rect
            // 
            this.Rect.BackgroundImage = global::WindowsFormsLab1.Properties.Resources.rect;
            resources.ApplyResources(this.Rect, "Rect");
            this.Rect.Name = "Rect";
            this.Rect.UseVisualStyleBackColor = true;
            this.Rect.Click += new System.EventHandler(this.Rect_Click);
            this.Rect.MouseEnter += new System.EventHandler(this.Rect_MouseEnter);
            this.Rect.MouseLeave += new System.EventHandler(this.Rect_MouseLeave);
            // 
            // Rhomb
            // 
            this.Rhomb.BackgroundImage = global::WindowsFormsLab1.Properties.Resources.rhombus;
            resources.ApplyResources(this.Rhomb, "Rhomb");
            this.Rhomb.Name = "Rhomb";
            this.Rhomb.UseVisualStyleBackColor = true;
            this.Rhomb.Click += new System.EventHandler(this.Rect_Click);
            this.Rhomb.MouseEnter += new System.EventHandler(this.Rect_MouseEnter);
            this.Rhomb.MouseLeave += new System.EventHandler(this.Rect_MouseLeave);
            // 
            // Start
            // 
            this.Start.BackgroundImage = global::WindowsFormsLab1.Properties.Resources.start;
            resources.ApplyResources(this.Start, "Start");
            this.Start.Name = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Rect_Click);
            this.Start.MouseEnter += new System.EventHandler(this.Rect_MouseEnter);
            this.Start.MouseLeave += new System.EventHandler(this.Rect_MouseLeave);
            // 
            // Stop
            // 
            this.Stop.BackgroundImage = global::WindowsFormsLab1.Properties.Resources.stop;
            resources.ApplyResources(this.Stop, "Stop");
            this.Stop.Name = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Rect_Click);
            this.Stop.MouseEnter += new System.EventHandler(this.Rect_MouseEnter);
            this.Stop.MouseLeave += new System.EventHandler(this.Rect_MouseLeave);
            // 
            // Trash
            // 
            this.Trash.BackgroundImage = global::WindowsFormsLab1.Properties.Resources.trash;
            resources.ApplyResources(this.Trash, "Trash");
            this.Trash.Name = "Trash";
            this.Trash.UseVisualStyleBackColor = true;
            this.Trash.Click += new System.EventHandler(this.Rect_Click);
            this.Trash.MouseEnter += new System.EventHandler(this.Rect_MouseEnter);
            this.Trash.MouseLeave += new System.EventHandler(this.Rect_MouseLeave);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // LanguageBox
            // 
            this.LanguageBox.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.LanguageBox, "LanguageBox");
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.englishButton, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.PolishButton, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // englishButton
            // 
            resources.ApplyResources(this.englishButton, "englishButton");
            this.englishButton.Name = "englishButton";
            this.englishButton.UseVisualStyleBackColor = true;
            this.englishButton.Click += new System.EventHandler(this.englishButton_Click);
            // 
            // PolishButton
            // 
            resources.ApplyResources(this.PolishButton, "PolishButton");
            this.PolishButton.Name = "PolishButton";
            this.PolishButton.UseVisualStyleBackColor = true;
            this.PolishButton.Click += new System.EventHandler(this.PolishButton_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Name = "panel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.fileBox.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.editBox.ResumeLayout(false);
            this.editBox.PerformLayout();
            this.EditionPanel.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.LanguageBox.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox fileBox;
        private System.Windows.Forms.GroupBox editBox;
        private System.Windows.Forms.GroupBox LanguageBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button englishButton;
        private System.Windows.Forms.Button PolishButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel EditionPanel;
        private System.Windows.Forms.Button Link;
        private System.Windows.Forms.Button Rect;
        private System.Windows.Forms.Button Rhomb;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Trash;
    }
}

