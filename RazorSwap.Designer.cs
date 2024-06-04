namespace RazorSwap
{
    partial class Swap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Swap));
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(-4, -6);
            panel1.Name = "panel1";
            panel1.Size = new Size(479, 341);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaption;
            label2.Location = new Point(21, 60);
            label2.Name = "label2";
            label2.Size = new Size(247, 15);
            label2.TabIndex = 7;
            label2.Text = "Press SWAP to Change DBs and Launch Razor";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaption;
            label1.Location = new Point(159, 14);
            label1.Name = "label1";
            label1.Size = new Size(173, 21);
            label1.TabIndex = 6;
            label1.Text = "RazorGage DB Changer";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(255, 255, 192);
            button1.Location = new Point(172, 144);
            button1.Name = "button1";
            button1.Size = new Size(151, 64);
            button1.TabIndex = 3;
            button1.Text = "SWAP";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OnApplyClick;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(89, 264);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(327, 36);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveCaption;
            label3.Location = new Point(21, 75);
            label3.Name = "label3";
            label3.Size = new Size(445, 15);
            label3.TabIndex = 8;
            label3.Text = "The first time you SWAP, a copy of your RGST DB will be created, and changed into.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ActiveCaption;
            label4.Location = new Point(21, 90);
            label4.Name = "label4";
            label4.Size = new Size(394, 15);
            label4.TabIndex = 9;
            label4.Text = "After that, each time you press SWAP, the two databases will be swapped.";
            // 
            // Swap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(473, 326);
            Controls.Add(panel1);
            Name = "Swap";
            Text = "RazorGage Swap Database Context";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private Label label4;
        private Label label3;
    }
}