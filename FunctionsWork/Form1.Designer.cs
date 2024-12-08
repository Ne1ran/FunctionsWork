namespace FunctionsWork
{
    partial class Functions
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
            pictureBox1 = new PictureBox();
            func1Btn = new Button();
            func2Btn = new Button();
            func3Btn = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(175, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(232, 168);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // func1Btn
            // 
            func1Btn.Location = new Point(33, 123);
            func1Btn.Name = "func1Btn";
            func1Btn.Size = new Size(110, 27);
            func1Btn.TabIndex = 1;
            func1Btn.Text = "Функция 1";
            func1Btn.UseVisualStyleBackColor = true;
            func1Btn.Click += Func1Btn_Click;
            // 
            // func2Btn
            // 
            func2Btn.Location = new Point(33, 350);
            func2Btn.Name = "func2Btn";
            func2Btn.Size = new Size(110, 30);
            func2Btn.TabIndex = 2;
            func2Btn.Text = "Функция 2";
            func2Btn.UseVisualStyleBackColor = true;
            func2Btn.Click += Func2Btn_Click;
            // 
            // func3Btn
            // 
            func3Btn.Location = new Point(33, 576);
            func3Btn.Name = "func3Btn";
            func3Btn.Size = new Size(110, 25);
            func3Btn.TabIndex = 3;
            func3Btn.Text = "Функция 3";
            func3Btn.UseVisualStyleBackColor = true;
            func3Btn.Click += Func3Btn_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(175, 279);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(892, 168);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(175, 505);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(392, 168);
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // Functions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 712);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(func3Btn);
            Controls.Add(func2Btn);
            Controls.Add(func1Btn);
            Controls.Add(pictureBox1);
            Name = "Functions";
            Text = "Functions";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button func1Btn;
        private Button func2Btn;
        private Button func3Btn;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}
