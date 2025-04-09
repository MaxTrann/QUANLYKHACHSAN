namespace THUEPHONG.MyControls
{
    partial class ucDonVi
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.linkDonVi = new System.Windows.Forms.LinkLabel();
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // linkDonVi
            // 
            this.linkDonVi.AutoSize = true;
            this.linkDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkDonVi.Location = new System.Drawing.Point(23, 20);
            this.linkDonVi.Name = "linkDonVi";
            this.linkDonVi.Size = new System.Drawing.Size(213, 22);
            this.linkDonVi.TabIndex = 0;
            this.linkDonVi.TabStop = true;
            this.linkDonVi.Text = "<<Danh sách đơn vị>>";
            this.linkDonVi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDonVi_LinkClicked);
            // 
            // txtDonVi
            // 
            this.txtDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonVi.ForeColor = System.Drawing.Color.Blue;
            this.txtDonVi.Location = new System.Drawing.Point(23, 57);
            this.txtDonVi.Multiline = true;
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.Size = new System.Drawing.Size(503, 87);
            this.txtDonVi.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ucDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDonVi);
            this.Controls.Add(this.linkDonVi);
            this.Name = "ucDonVi";
            this.Size = new System.Drawing.Size(550, 171);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkDonVi;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.TextBox txtDonVi;
    }
}
