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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.moveLightConstRadioButton = new System.Windows.Forms.RadioButton();
            this.moveLightMovingRadioButton = new System.Windows.Forms.RadioButton();
            this.moveLightTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.bumpTextureBox = new System.Windows.Forms.PictureBox();
            this.bumpTextureButton = new System.Windows.Forms.Button();
            this.bumpNoneRadioButton = new System.Windows.Forms.RadioButton();
            this.bumpTextureRadioButton = new System.Windows.Forms.RadioButton();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.vectorTextureBox = new System.Windows.Forms.PictureBox();
            this.vectorTextureButton = new System.Windows.Forms.Button();
            this.vectorConstRadioButton = new System.Windows.Forms.RadioButton();
            this.vectorTextureRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lightColorButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lightColorLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.objectColorLabel = new System.Windows.Forms.Label();
            this.objectTextureBox = new System.Windows.Forms.PictureBox();
            this.objectColorButton = new System.Windows.Forms.Button();
            this.objectTextureButton = new System.Windows.Forms.Button();
            this.objectColorRadioButton = new System.Windows.Forms.RadioButton();
            this.objectTextureRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bumbTrack = new System.Windows.Forms.TrackBar();
            this.distributedTrack = new System.Windows.Forms.TrackBar();
            this.mirrorTrack = new System.Windows.Forms.TrackBar();
            this.cosinusTrack = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.rightClickMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bumpTextureBox)).BeginInit();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vectorTextureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectTextureBox)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bumbTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distributedTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirrorTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cosinusTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(303, 3);
            this.pictureBox.Name = "pictureBox";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox, 9);
            this.pictureBox.Size = new System.Drawing.Size(1116, 644);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.table, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1422, 650);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.moveLightConstRadioButton, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.moveLightMovingRadioButton, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.moveLightTextBox, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 371);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(294, 114);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label9, 2);
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Wektor źródła światła";
            // 
            // moveLightConstRadioButton
            // 
            this.moveLightConstRadioButton.AutoSize = true;
            this.moveLightConstRadioButton.Location = new System.Drawing.Point(3, 23);
            this.moveLightConstRadioButton.Name = "moveLightConstRadioButton";
            this.moveLightConstRadioButton.Size = new System.Drawing.Size(83, 17);
            this.moveLightConstRadioButton.TabIndex = 5;
            this.moveLightConstRadioButton.TabStop = true;
            this.moveLightConstRadioButton.Text = "Stały [0,0,1]";
            this.moveLightConstRadioButton.UseVisualStyleBackColor = true;
            // 
            // moveLightMovingRadioButton
            // 
            this.moveLightMovingRadioButton.AutoSize = true;
            this.moveLightMovingRadioButton.Location = new System.Drawing.Point(3, 53);
            this.moveLightMovingRadioButton.Name = "moveLightMovingRadioButton";
            this.moveLightMovingRadioButton.Size = new System.Drawing.Size(125, 17);
            this.moveLightMovingRadioButton.TabIndex = 6;
            this.moveLightMovingRadioButton.TabStop = true;
            this.moveLightMovingRadioButton.Text = "Animowany po sferze";
            this.moveLightMovingRadioButton.UseVisualStyleBackColor = true;
            // 
            // moveLightTextBox
            // 
            this.moveLightTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moveLightTextBox.Location = new System.Drawing.Point(150, 53);
            this.moveLightTextBox.Name = "moveLightTextBox";
            this.moveLightTextBox.Size = new System.Drawing.Size(141, 20);
            this.moveLightTextBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Promień sfery:";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.bumpTextureBox, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.bumpTextureButton, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.bumpNoneRadioButton, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.bumpTextureRadioButton, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 265);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(294, 100);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.label7, 3);
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Zaburzenie";
            // 
            // bumpTextureBox
            // 
            this.bumpTextureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bumpTextureBox.Location = new System.Drawing.Point(73, 50);
            this.bumpTextureBox.Margin = new System.Windows.Forms.Padding(0);
            this.bumpTextureBox.Name = "bumpTextureBox";
            this.bumpTextureBox.Size = new System.Drawing.Size(147, 50);
            this.bumpTextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bumpTextureBox.TabIndex = 2;
            this.bumpTextureBox.TabStop = false;
            // 
            // bumpTextureButton
            // 
            this.bumpTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bumpTextureButton.Location = new System.Drawing.Point(220, 50);
            this.bumpTextureButton.Margin = new System.Windows.Forms.Padding(0);
            this.bumpTextureButton.Name = "bumpTextureButton";
            this.bumpTextureButton.Size = new System.Drawing.Size(74, 30);
            this.bumpTextureButton.TabIndex = 4;
            this.bumpTextureButton.Text = "Zmień";
            this.bumpTextureButton.UseVisualStyleBackColor = true;
            // 
            // bumpNoneRadioButton
            // 
            this.bumpNoneRadioButton.AutoSize = true;
            this.bumpNoneRadioButton.Location = new System.Drawing.Point(3, 23);
            this.bumpNoneRadioButton.Name = "bumpNoneRadioButton";
            this.bumpNoneRadioButton.Size = new System.Drawing.Size(67, 17);
            this.bumpNoneRadioButton.TabIndex = 5;
            this.bumpNoneRadioButton.TabStop = true;
            this.bumpNoneRadioButton.Text = "Brak [0,0,0]";
            this.bumpNoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // bumpTextureRadioButton
            // 
            this.bumpTextureRadioButton.AutoSize = true;
            this.bumpTextureRadioButton.Location = new System.Drawing.Point(3, 53);
            this.bumpTextureRadioButton.Name = "bumpTextureRadioButton";
            this.bumpTextureRadioButton.Size = new System.Drawing.Size(67, 17);
            this.bumpTextureRadioButton.TabIndex = 6;
            this.bumpTextureRadioButton.TabStop = true;
            this.bumpTextureRadioButton.Text = "Tekstura";
            this.bumpTextureRadioButton.UseVisualStyleBackColor = true;
            // 
            // table
            // 
            this.table.ColumnCount = 3;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table.Controls.Add(this.label5, 0, 0);
            this.table.Controls.Add(this.vectorTextureBox, 1, 2);
            this.table.Controls.Add(this.vectorTextureButton, 2, 2);
            this.table.Controls.Add(this.vectorConstRadioButton, 0, 1);
            this.table.Controls.Add(this.vectorTextureRadioButton, 0, 2);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(3, 159);
            this.table.Name = "table";
            this.table.RowCount = 3;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(294, 100);
            this.table.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.table.SetColumnSpan(this.label5, 3);
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Wektor normalny";
            // 
            // vectorTextureBox
            // 
            this.vectorTextureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vectorTextureBox.Location = new System.Drawing.Point(73, 50);
            this.vectorTextureBox.Margin = new System.Windows.Forms.Padding(0);
            this.vectorTextureBox.Name = "vectorTextureBox";
            this.vectorTextureBox.Size = new System.Drawing.Size(147, 50);
            this.vectorTextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.vectorTextureBox.TabIndex = 2;
            this.vectorTextureBox.TabStop = false;
            // 
            // vectorTextureButton
            // 
            this.vectorTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vectorTextureButton.Location = new System.Drawing.Point(220, 50);
            this.vectorTextureButton.Margin = new System.Windows.Forms.Padding(0);
            this.vectorTextureButton.Name = "vectorTextureButton";
            this.vectorTextureButton.Size = new System.Drawing.Size(74, 30);
            this.vectorTextureButton.TabIndex = 4;
            this.vectorTextureButton.Text = "Zmień";
            this.vectorTextureButton.UseVisualStyleBackColor = true;
            // 
            // vectorConstRadioButton
            // 
            this.vectorConstRadioButton.AutoSize = true;
            this.table.SetColumnSpan(this.vectorConstRadioButton, 3);
            this.vectorConstRadioButton.Location = new System.Drawing.Point(3, 23);
            this.vectorConstRadioButton.Name = "vectorConstRadioButton";
            this.vectorConstRadioButton.Size = new System.Drawing.Size(83, 17);
            this.vectorConstRadioButton.TabIndex = 5;
            this.vectorConstRadioButton.TabStop = true;
            this.vectorConstRadioButton.Text = "Stały [0,0,1]";
            this.vectorConstRadioButton.UseVisualStyleBackColor = true;
            // 
            // vectorTextureRadioButton
            // 
            this.vectorTextureRadioButton.AutoSize = true;
            this.vectorTextureRadioButton.Location = new System.Drawing.Point(3, 53);
            this.vectorTextureRadioButton.Name = "vectorTextureRadioButton";
            this.vectorTextureRadioButton.Size = new System.Drawing.Size(67, 17);
            this.vectorTextureRadioButton.TabIndex = 6;
            this.vectorTextureRadioButton.TabStop = true;
            this.vectorTextureRadioButton.Text = "Tekstura";
            this.vectorTextureRadioButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.lightColorButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lightColorLabel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(294, 44);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lightColorButton
            // 
            this.lightColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightColorButton.Location = new System.Drawing.Point(220, 0);
            this.lightColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.lightColorButton.Name = "lightColorButton";
            this.lightColorButton.Size = new System.Drawing.Size(74, 30);
            this.lightColorButton.TabIndex = 4;
            this.lightColorButton.Text = "Zmień";
            this.lightColorButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kolor źródła światła";
            // 
            // lightColorLabel
            // 
            this.lightColorLabel.AutoSize = true;
            this.lightColorLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lightColorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lightColorLabel.Location = new System.Drawing.Point(73, 0);
            this.lightColorLabel.Margin = new System.Windows.Forms.Padding(0);
            this.lightColorLabel.Name = "lightColorLabel";
            this.lightColorLabel.Size = new System.Drawing.Size(147, 44);
            this.lightColorLabel.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.objectColorLabel, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.objectTextureBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.objectColorButton, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.objectTextureButton, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.objectColorRadioButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.objectTextureRadioButton, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(294, 100);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label3, 3);
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Kolor obiektu";
            // 
            // objectColorLabel
            // 
            this.objectColorLabel.AutoSize = true;
            this.objectColorLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.objectColorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectColorLabel.Location = new System.Drawing.Point(73, 20);
            this.objectColorLabel.Margin = new System.Windows.Forms.Padding(0);
            this.objectColorLabel.Name = "objectColorLabel";
            this.objectColorLabel.Size = new System.Drawing.Size(147, 30);
            this.objectColorLabel.TabIndex = 1;
            // 
            // objectTextureBox
            // 
            this.objectTextureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTextureBox.Location = new System.Drawing.Point(73, 50);
            this.objectTextureBox.Margin = new System.Windows.Forms.Padding(0);
            this.objectTextureBox.Name = "objectTextureBox";
            this.objectTextureBox.Size = new System.Drawing.Size(147, 50);
            this.objectTextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.objectTextureBox.TabIndex = 2;
            this.objectTextureBox.TabStop = false;
            // 
            // objectColorButton
            // 
            this.objectColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectColorButton.Location = new System.Drawing.Point(220, 20);
            this.objectColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.objectColorButton.Name = "objectColorButton";
            this.objectColorButton.Size = new System.Drawing.Size(74, 30);
            this.objectColorButton.TabIndex = 3;
            this.objectColorButton.Text = "Zmień";
            this.objectColorButton.UseVisualStyleBackColor = true;
            // 
            // objectTextureButton
            // 
            this.objectTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectTextureButton.Location = new System.Drawing.Point(220, 50);
            this.objectTextureButton.Margin = new System.Windows.Forms.Padding(0);
            this.objectTextureButton.Name = "objectTextureButton";
            this.objectTextureButton.Size = new System.Drawing.Size(74, 30);
            this.objectTextureButton.TabIndex = 4;
            this.objectTextureButton.Text = "Zmień";
            this.objectTextureButton.UseVisualStyleBackColor = true;
            // 
            // objectColorRadioButton
            // 
            this.objectColorRadioButton.AutoSize = true;
            this.objectColorRadioButton.Location = new System.Drawing.Point(3, 23);
            this.objectColorRadioButton.Name = "objectColorRadioButton";
            this.objectColorRadioButton.Size = new System.Drawing.Size(50, 17);
            this.objectColorRadioButton.TabIndex = 5;
            this.objectColorRadioButton.TabStop = true;
            this.objectColorRadioButton.Text = "Stały";
            this.objectColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // objectTextureRadioButton
            // 
            this.objectTextureRadioButton.AutoSize = true;
            this.objectTextureRadioButton.Location = new System.Drawing.Point(3, 53);
            this.objectTextureRadioButton.Name = "objectTextureRadioButton";
            this.objectTextureRadioButton.Size = new System.Drawing.Size(67, 17);
            this.objectTextureRadioButton.TabIndex = 6;
            this.objectTextureRadioButton.TabStop = true;
            this.objectTextureRadioButton.Text = "Tekstura";
            this.objectTextureRadioButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.bumbTrack, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 491);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(294, 34);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel7.Controls.Add(this.distributedTrack, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 531);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(294, 34);
            this.tableLayoutPanel7.TabIndex = 7;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel8.Controls.Add(this.mirrorTrack, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 571);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(294, 34);
            this.tableLayoutPanel8.TabIndex = 8;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel9.Controls.Add(this.cosinusTrack, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 611);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(294, 36);
            this.tableLayoutPanel9.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Zaburzenia";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "Składowa rozproszona";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 26);
            this.label8.TabIndex = 0;
            this.label8.Text = "Składowa zwierciadlana";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 26);
            this.label10.TabIndex = 0;
            this.label10.Text = "Wykładnik cosinusa";
            // 
            // bumbTrack
            // 
            this.bumbTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bumbTrack.Location = new System.Drawing.Point(91, 3);
            this.bumbTrack.Maximum = 100;
            this.bumbTrack.Name = "bumbTrack";
            this.bumbTrack.Size = new System.Drawing.Size(200, 28);
            this.bumbTrack.TabIndex = 1;
            // 
            // distributedTrack
            // 
            this.distributedTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.distributedTrack.Location = new System.Drawing.Point(91, 3);
            this.distributedTrack.Maximum = 100;
            this.distributedTrack.Name = "distributedTrack";
            this.distributedTrack.Size = new System.Drawing.Size(200, 28);
            this.distributedTrack.TabIndex = 2;
            // 
            // mirrorTrack
            // 
            this.mirrorTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mirrorTrack.Location = new System.Drawing.Point(91, 3);
            this.mirrorTrack.Maximum = 100;
            this.mirrorTrack.Name = "mirrorTrack";
            this.mirrorTrack.Size = new System.Drawing.Size(200, 28);
            this.mirrorTrack.TabIndex = 2;
            // 
            // cosinusTrack
            // 
            this.cosinusTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cosinusTrack.Location = new System.Drawing.Point(91, 3);
            this.cosinusTrack.Maximum = 100;
            this.cosinusTrack.Name = "cosinusTrack";
            this.cosinusTrack.Size = new System.Drawing.Size(200, 30);
            this.cosinusTrack.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1422, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.rightClickMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bumpTextureBox)).EndInit();
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vectorTextureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectTextureBox)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bumbTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distributedTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirrorTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cosinusTrack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox;
        private ContextMenuStrip rightClickMenu;
        private ToolStripMenuItem constantLengthRelationControl;
        private ToolStripMenuItem verticalRelationControl;
        private ToolStripMenuItem horizontalRelationControl;
        private ToolStripTextBox constLengthTextBox;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label lightColorLabel;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label3;
        private Label objectColorLabel;
        private PictureBox objectTextureBox;
        private Button objectColorButton;
        private Button objectTextureButton;
        private RadioButton objectColorRadioButton;
        private RadioButton objectTextureRadioButton;
        private Button lightColorButton;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label9;
        private RadioButton moveLightConstRadioButton;
        private RadioButton moveLightMovingRadioButton;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label7;
        private PictureBox bumpTextureBox;
        private Button bumpTextureButton;
        private RadioButton bumpNoneRadioButton;
        private RadioButton bumpTextureRadioButton;
        private TableLayoutPanel table;
        private Label label5;
        private PictureBox vectorTextureBox;
        private Button vectorTextureButton;
        private RadioButton vectorConstRadioButton;
        private RadioButton vectorTextureRadioButton;
        private TextBox moveLightTextBox;
        private Label label6;
        private TableLayoutPanel tableLayoutPanel9;
        private TrackBar cosinusTrack;
        private Label label10;
        private TableLayoutPanel tableLayoutPanel8;
        private TrackBar mirrorTrack;
        private Label label8;
        private TableLayoutPanel tableLayoutPanel7;
        private TrackBar distributedTrack;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label2;
        private TrackBar bumbTrack;
    }
}

