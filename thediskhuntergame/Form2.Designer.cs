namespace thediskhuntergame
{
    partial class Form2
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
            colorDialog1 = new ColorDialog();
            colorDialog2 = new ColorDialog();
            colorDialog3 = new ColorDialog();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.Location = new Point(302, 115);
            button1.Name = "button1";
            button1.Size = new Size(197, 29);
            button1.TabIndex = 0;
            button1.Text = "Background Color";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button2.Location = new Point(302, 161);
            button2.Name = "button2";
            button2.Size = new Size(197, 29);
            button2.TabIndex = 1;
            button2.Text = "Board Color";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button3.Location = new Point(302, 210);
            button3.Name = "button3";
            button3.Size = new Size(197, 29);
            button3.TabIndex = 2;
            button3.Text = "Disk Color";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button4.Location = new Point(302, 260);
            button4.Name = "button4";
            button4.Size = new Size(197, 29);
            button4.TabIndex = 3;
            button4.Text = "Save Changes";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button5.Location = new Point(302, 309);
            button5.Name = "button5";
            button5.Size = new Size(197, 29);
            button5.TabIndex = 4;
            button5.Text = "Cancel Without Save";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(793, 487);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            MaximizeBox = false;
            MaximumSize = new Size(811, 534);
            MinimizeBox = false;
            MinimumSize = new Size(811, 534);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Properties";
            ResumeLayout(false);
        }

        #endregion

        private ColorDialog colorDialog1;
        private ColorDialog colorDialog2;
        private ColorDialog colorDialog3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}