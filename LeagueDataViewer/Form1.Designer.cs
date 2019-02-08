namespace LeagueDataViewer
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
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.panelHeroes = new LeagueDataViewer.MyPanel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            this.SuspendLayout();
            // 
            // logoPic
            // 
            this.logoPic.Location = new System.Drawing.Point(3, 3);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(122, 93);
            this.logoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPic.TabIndex = 1;
            this.logoPic.TabStop = false;
            // 
            // panelHeroes
            // 
            this.panelHeroes.AutoScroll = true;
            this.panelHeroes.Location = new System.Drawing.Point(3, 102);
            this.panelHeroes.Name = "panelHeroes";
            this.panelHeroes.Size = new System.Drawing.Size(1210, 555);
            this.panelHeroes.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(299, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(618, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "CHOOSE A CHAMPION FOR IN-DEPTH STATISTICS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 687);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelHeroes);
            this.Controls.Add(this.logoPic);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.Label label1;
        private MyPanel panelHeroes;
    }
}

