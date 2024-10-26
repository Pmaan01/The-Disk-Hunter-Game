namespace thediskhuntergame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            timer = new System.Windows.Forms.Timer(components);
            timer1 = new System.Windows.Forms.Timer(components);
            newgame = new Button();
            time = new Label();
            Top20 = new ListBox();
            properties = new Button();
            scorelabel = new Label();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(12, 51);
            panel1.Name = "panel1";
            panel1.Size = new Size(422, 387);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 600;
            timer1.Tick += timer1_Tick;
            // 
            // newgame
            // 
            newgame.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            newgame.ForeColor = Color.Black;
            newgame.Location = new Point(440, 164);
            newgame.Name = "newgame";
            newgame.Size = new Size(94, 29);
            newgame.TabIndex = 1;
            newgame.Text = "New Game";
            newgame.UseVisualStyleBackColor = true;
            newgame.Click += newgame_Click;
            // 
            // time
            // 
            time.AutoSize = true;
            time.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            time.ForeColor = Color.Black;
            time.Location = new Point(12, 9);
            time.Name = "time";
            time.Size = new Size(171, 23);
            time.TabIndex = 3;
            time.Text = "Time Remaining: 60";
            // 
            // Top20
            // 
            Top20.BackColor = Color.White;
            Top20.BorderStyle = BorderStyle.None;
            Top20.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Top20.ForeColor = Color.Black;
            Top20.FormattingEnabled = true;
            Top20.Location = new Point(601, 51);
            Top20.Name = "Top20";
            Top20.Size = new Size(187, 380);
            Top20.TabIndex = 4;
            Top20.DoubleClick += Top20_DoubleClick;
            // 
            // properties
            // 
            properties.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            properties.ForeColor = Color.Black;
            properties.Location = new Point(440, 199);
            properties.Name = "properties";
            properties.Size = new Size(94, 29);
            properties.TabIndex = 5;
            properties.Text = "Properties";
            properties.UseVisualStyleBackColor = true;
            properties.Click += properties_Click;
            // 
            // scorelabel
            // 
            scorelabel.AutoSize = true;
            scorelabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            scorelabel.ForeColor = Color.Black;
            scorelabel.Location = new Point(440, 124);
            scorelabel.Name = "scorelabel";
            scorelabel.Size = new Size(74, 23);
            scorelabel.TabIndex = 6;
            scorelabel.Text = "Score: 0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(253, 14);
            label1.Name = "label1";
            label1.Size = new Size(281, 21);
            label1.TabIndex = 0;
            label1.Text = "Click The Disk For Points";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(614, 10);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 7;
            label2.Text = "Top 20 Players";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(800, 450);
            Controls.Add(properties);
            Controls.Add(newgame);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(scorelabel);
            Controls.Add(Top20);
            Controls.Add(time);
            Controls.Add(panel1);
            DoubleBuffered = true;
            ForeColor = Color.Maroon;
            MaximizeBox = false;
            MaximumSize = new Size(818, 497);
            MinimizeBox = false;
            MinimumSize = new Size(818, 497);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "The Disk Hunter";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer1;
        private Button newgame;
        private Label time;
        private ListBox Top20;
        private Button properties;
        private Label scorelabel;
        private Label label1;
        private Label label2;
    }
}
