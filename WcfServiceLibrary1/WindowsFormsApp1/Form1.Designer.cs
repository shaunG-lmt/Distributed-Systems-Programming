namespace WindowsFormsApp1
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
            this.userInputTB = new System.Windows.Forms.TextBox();
            this.returnValueTB = new System.Windows.Forms.TextBox();
            this.translate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userInputTB
            // 
            this.userInputTB.Location = new System.Drawing.Point(154, 189);
            this.userInputTB.Name = "userInputTB";
            this.userInputTB.Size = new System.Drawing.Size(100, 20);
            this.userInputTB.TabIndex = 0;
            // 
            // returnValueTB
            // 
            this.returnValueTB.Location = new System.Drawing.Point(154, 255);
            this.returnValueTB.Name = "returnValueTB";
            this.returnValueTB.Size = new System.Drawing.Size(100, 20);
            this.returnValueTB.TabIndex = 1;
            // 
            // translate
            // 
            this.translate.Location = new System.Drawing.Point(167, 221);
            this.translate.Name = "translate";
            this.translate.Size = new System.Drawing.Size(75, 23);
            this.translate.TabIndex = 2;
            this.translate.Text = "translate";
            this.translate.UseVisualStyleBackColor = true;
            this.translate.Click += new System.EventHandler(this.translate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 450);
            this.Controls.Add(this.translate);
            this.Controls.Add(this.returnValueTB);
            this.Controls.Add(this.userInputTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userInputTB;
        private System.Windows.Forms.TextBox returnValueTB;
        private System.Windows.Forms.Button translate;
    }
}

