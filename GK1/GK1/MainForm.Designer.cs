namespace GK1
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.constantLengthRelationControl = new System.Windows.Forms.ToolStripMenuItem();
            this.constLengthTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.verticalRelationControl = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalRelationControl = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.rightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1308, 534);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constantLengthRelationControl,
            this.verticalRelationControl,
            this.horizontalRelationControl});
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(163, 92);
            // 
            // constantLengthRelationControl
            // 
            this.constantLengthRelationControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constLengthTextBox});
            this.constantLengthRelationControl.Name = "constantLengthRelationControl";
            this.constantLengthRelationControl.Size = new System.Drawing.Size(162, 22);
            this.constantLengthRelationControl.Text = "Constant Length";
            // 
            // constLengthTextBox
            // 
            this.constLengthTextBox.Name = "constLengthTextBox";
            this.constLengthTextBox.Size = new System.Drawing.Size(100, 23);
            // 
            // verticalRelationControl
            // 
            this.verticalRelationControl.Name = "verticalRelationControl";
            this.verticalRelationControl.Size = new System.Drawing.Size(162, 22);
            this.verticalRelationControl.Text = "Vertical";
            // 
            // horizontalRelationControl
            // 
            this.horizontalRelationControl.Name = "horizontalRelationControl";
            this.horizontalRelationControl.Size = new System.Drawing.Size(162, 22);
            this.horizontalRelationControl.Text = "Horizontal";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 534);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem constantLengthRelationControl;
        private System.Windows.Forms.ToolStripMenuItem verticalRelationControl;
        private System.Windows.Forms.ToolStripMenuItem horizontalRelationControl;
        private System.Windows.Forms.ToolStripTextBox constLengthTextBox;
    }
}

