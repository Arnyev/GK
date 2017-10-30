using System.ComponentModel;
using System.Windows.Forms;

namespace GK1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.constantLengthRelationControl = new System.Windows.Forms.ToolStripMenuItem();
            this.constLengthTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.verticalRelationControl = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalRelationControl = new System.Windows.Forms.ToolStripMenuItem();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.rightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 46);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1308, 488);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constantLengthRelationControl,
            this.verticalRelationControl,
            this.horizontalRelationControl});
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(163, 70);
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
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveButton.Location = new System.Drawing.Point(0, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(1308, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // loadButton
            // 
            this.loadButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadButton.Location = new System.Drawing.Point(0, 23);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(1308, 23);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Wczytaj";
            this.loadButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 534);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox;
        private ContextMenuStrip rightClickMenu;
        private ToolStripMenuItem constantLengthRelationControl;
        private ToolStripMenuItem verticalRelationControl;
        private ToolStripMenuItem horizontalRelationControl;
        private ToolStripTextBox constLengthTextBox;
        private Button saveButton;
        private Button loadButton;
    }
}

