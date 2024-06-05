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
            cbPipe = new CheckBox();
            cbRod = new CheckBox();
            cbUnitstrut = new CheckBox();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(cbPipe);
            panel1.Controls.Add(cbRod);
            panel1.Controls.Add(cbUnitstrut);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(-4, -6);
            panel1.Name = "panel1";
            panel1.Size = new Size(479, 526);
            panel1.TabIndex = 0;
            // 
            // cbPipe
            // 
            cbPipe.AutoSize = true;
            cbPipe.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            cbPipe.ForeColor = Color.FromArgb(0, 192, 0);
            cbPipe.Location = new Point(180, 246);
            cbPipe.Name = "cbPipe";
            cbPipe.Size = new Size(88, 41);
            cbPipe.TabIndex = 12;
            cbPipe.Text = "Pipe";
            cbPipe.UseVisualStyleBackColor = true;
            cbPipe.CheckedChanged += cbPipe_CheckedChanged;
            // 
            // cbRod
            // 
            cbRod.AutoSize = true;
            cbRod.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            cbRod.ForeColor = Color.FromArgb(0, 192, 0);
            cbRod.Location = new Point(181, 199);
            cbRod.Name = "cbRod";
            cbRod.Size = new Size(83, 41);
            cbRod.TabIndex = 11;
            cbRod.Text = "Rod";
            cbRod.UseVisualStyleBackColor = true;
            cbRod.CheckedChanged += cbRod_CheckedChanged;
            // 
            // cbUnitstrut
            // 
            cbUnitstrut.AutoSize = true;
            cbUnitstrut.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            cbUnitstrut.ForeColor = Color.FromArgb(0, 192, 0);
            cbUnitstrut.Location = new Point(180, 152);
            cbUnitstrut.Name = "cbUnitstrut";
            cbUnitstrut.Size = new Size(130, 41);
            cbUnitstrut.TabIndex = 10;
            cbUnitstrut.Text = "Unistrut";
            cbUnitstrut.UseVisualStyleBackColor = true;
            cbUnitstrut.CheckedChanged += cbUnitstrut_CheckedChanged;
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
            button1.Location = new Point(181, 345);
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
            pictureBox1.Location = new Point(88, 453);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(327, 36);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Swap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(473, 517);
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
        private CheckBox cbPipe;
        private CheckBox cbRod;
        private CheckBox cbUnitstrut;
    }
}