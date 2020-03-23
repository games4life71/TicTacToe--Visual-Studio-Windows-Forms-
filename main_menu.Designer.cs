namespace TicTacToe
{
    partial class main_menu
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
            this.btnPVP = new System.Windows.Forms.Button();
            this.btnPVPC1 = new System.Windows.Forms.Button();
            this.btnPVPC2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPVP
            // 
            this.btnPVP.Location = new System.Drawing.Point(282, 159);
            this.btnPVP.Name = "btnPVP";
            this.btnPVP.Size = new System.Drawing.Size(265, 52);
            this.btnPVP.TabIndex = 0;
            this.btnPVP.Text = "Play versus Player";
            this.btnPVP.UseVisualStyleBackColor = true;
            this.btnPVP.Click += new System.EventHandler(this.btnPVP_Click);
            // 
            // btnPVPC1
            // 
            this.btnPVPC1.Location = new System.Drawing.Point(282, 235);
            this.btnPVPC1.Name = "btnPVPC1";
            this.btnPVPC1.Size = new System.Drawing.Size(265, 52);
            this.btnPVPC1.TabIndex = 1;
            this.btnPVPC1.Text = "Play v Computer 1 ";
            this.btnPVPC1.UseVisualStyleBackColor = true;
            this.btnPVPC1.Click += new System.EventHandler(this.btnPVPC1_Click);
            // 
            // btnPVPC2
            // 
            this.btnPVPC2.Enabled = false;
            this.btnPVPC2.Location = new System.Drawing.Point(282, 308);
            this.btnPVPC2.Name = "btnPVPC2";
            this.btnPVPC2.Size = new System.Drawing.Size(265, 52);
            this.btnPVPC2.TabIndex = 2;
            this.btnPVPC2.Text = "Play v Computer2";
            this.btnPVPC2.UseVisualStyleBackColor = true;
            // 
            // main_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(861, 531);
            this.Controls.Add(this.btnPVPC2);
            this.Controls.Add(this.btnPVPC1);
            this.Controls.Add(this.btnPVP);
            this.Name = "main_menu";
            this.Text = "main_menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPVP;
        private System.Windows.Forms.Button btnPVPC1;
        private System.Windows.Forms.Button btnPVPC2;
    }
}