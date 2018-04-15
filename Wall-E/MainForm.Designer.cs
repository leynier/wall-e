namespace WallE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectProyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.closeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugOpcionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildSimuladorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildRobotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.nextInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeOfTheTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sintimeLanguajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerParent = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.panelNotepad = new System.Windows.Forms.Panel();
            this.notepad = new CodeStrange.Controls.Notepad();
            this.panelLeftDown = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.columnHeaderFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExplication = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageWatch = new System.Windows.Forms.TabPage();
            this.treeViewWatch = new System.Windows.Forms.TreeView();
            this.panelRigth = new System.Windows.Forms.Panel();
            this.panelDisplay = new System.Windows.Forms.Panel();
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerParent)).BeginInit();
            this.splitContainerParent.Panel1.SuspendLayout();
            this.splitContainerParent.Panel2.SuspendLayout();
            this.splitContainerParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.panelNotepad.SuspendLayout();
            this.panelLeftDown.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageMessages.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            this.tabPageWatch.SuspendLayout();
            this.panelRigth.SuspendLayout();
            this.panelDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.DodgerBlue;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.debugOpcionsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.toolStripSeparator5,
            this.saveFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveProjectProyectoToolStripMenuItem,
            this.toolStripSeparator6,
            this.closeFileToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.newFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.openFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.openProjectToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(225, 6);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.saveFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.saveAsToolStripMenuItem.Text = "Save File As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveProjectProyectoToolStripMenuItem
            // 
            this.saveProjectProyectoToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.saveProjectProyectoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveProjectProyectoToolStripMenuItem.Name = "saveProjectProyectoToolStripMenuItem";
            this.saveProjectProyectoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveProjectProyectoToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.saveProjectProyectoToolStripMenuItem.Text = "Save Project";
            this.saveProjectProyectoToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(225, 6);
            // 
            // closeFileToolStripMenuItem
            // 
            this.closeFileToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.closeFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.closeFileToolStripMenuItem.Name = "closeFileToolStripMenuItem";
            this.closeFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.closeFileToolStripMenuItem.Text = "Close File";
            this.closeFileToolStripMenuItem.Click += new System.EventHandler(this.closeFileToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.closeProjectToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // debugOpcionsToolStripMenuItem
            // 
            this.debugOpcionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildSimuladorToolStripMenuItem,
            this.buildRobotToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.toolStripSeparator2,
            this.startToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator3,
            this.nextInstructionToolStripMenuItem,
            this.nextActionToolStripMenuItem});
            this.debugOpcionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.debugOpcionsToolStripMenuItem.Name = "debugOpcionsToolStripMenuItem";
            this.debugOpcionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.debugOpcionsToolStripMenuItem.Text = "Debug";
            // 
            // buildSimuladorToolStripMenuItem
            // 
            this.buildSimuladorToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.buildSimuladorToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.buildSimuladorToolStripMenuItem.Name = "buildSimuladorToolStripMenuItem";
            this.buildSimuladorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.buildSimuladorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.buildSimuladorToolStripMenuItem.Text = "Build Simulator";
            this.buildSimuladorToolStripMenuItem.Click += new System.EventHandler(this.buildSimulatorToolStripMenuItem_Click);
            // 
            // buildRobotToolStripMenuItem
            // 
            this.buildRobotToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.buildRobotToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.buildRobotToolStripMenuItem.Name = "buildRobotToolStripMenuItem";
            this.buildRobotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
            this.buildRobotToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.buildRobotToolStripMenuItem.Text = "Build Robot";
            this.buildRobotToolStripMenuItem.Click += new System.EventHandler(this.buildRobotToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.debugToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(214, 6);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.startToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.continueToolStripMenuItem.Enabled = false;
            this.continueToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.continueToolStripMenuItem.Text = "Continue";
            this.continueToolStripMenuItem.Click += new System.EventHandler(this.continueToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.pauseToolStripMenuItem.Enabled = false;
            this.pauseToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.pauseToolStripMenuItem.Text = "Pause ";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(214, 6);
            // 
            // nextInstructionToolStripMenuItem
            // 
            this.nextInstructionToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.nextInstructionToolStripMenuItem.Enabled = false;
            this.nextInstructionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.nextInstructionToolStripMenuItem.Name = "nextInstructionToolStripMenuItem";
            this.nextInstructionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.nextInstructionToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.nextInstructionToolStripMenuItem.Text = "Next Instruction";
            this.nextInstructionToolStripMenuItem.Click += new System.EventHandler(this.nextInstructionToolStripMenuItem_Click);
            // 
            // nextActionToolStripMenuItem
            // 
            this.nextActionToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.nextActionToolStripMenuItem.Enabled = false;
            this.nextActionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.nextActionToolStripMenuItem.Name = "nextActionToolStripMenuItem";
            this.nextActionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.nextActionToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.nextActionToolStripMenuItem.Text = "Next Action";
            this.nextActionToolStripMenuItem.Click += new System.EventHandler(this.nextActionToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeOfTheTilesToolStripMenuItem,
            this.updatesSpeedToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // sizeOfTheTilesToolStripMenuItem
            // 
            this.sizeOfTheTilesToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.sizeOfTheTilesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.sizeOfTheTilesToolStripMenuItem.Name = "sizeOfTheTilesToolStripMenuItem";
            this.sizeOfTheTilesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sizeOfTheTilesToolStripMenuItem.Text = "Size of the Tiles";
            this.sizeOfTheTilesToolStripMenuItem.Click += new System.EventHandler(this.sizeOfTheTilesToolStripMenuItem_Click);
            // 
            // updatesSpeedToolStripMenuItem
            // 
            this.updatesSpeedToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.updatesSpeedToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.updatesSpeedToolStripMenuItem.Name = "updatesSpeedToolStripMenuItem";
            this.updatesSpeedToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.updatesSpeedToolStripMenuItem.Text = "Update\'s Speed";
            this.updatesSpeedToolStripMenuItem.Click += new System.EventHandler(this.updatesSpeedToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sintimeLanguajeToolStripMenuItem,
            this.toolStripSeparator1,
            this.informationToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // sintimeLanguajeToolStripMenuItem
            // 
            this.sintimeLanguajeToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.sintimeLanguajeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.sintimeLanguajeToolStripMenuItem.Name = "sintimeLanguajeToolStripMenuItem";
            this.sintimeLanguajeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.sintimeLanguajeToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.sintimeLanguajeToolStripMenuItem.Text = "SINTIME Languaje";
            this.sintimeLanguajeToolStripMenuItem.Visible = false;
            this.sintimeLanguajeToolStripMenuItem.Click += new System.EventHandler(this.sintimeLanguajeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.informationToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.informationToolStripMenuItem.Text = "Information";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // splitContainerParent
            // 
            this.splitContainerParent.BackColor = System.Drawing.Color.DodgerBlue;
            this.splitContainerParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerParent.Location = new System.Drawing.Point(0, 24);
            this.splitContainerParent.Name = "splitContainerParent";
            // 
            // splitContainerParent.Panel1
            // 
            this.splitContainerParent.Panel1.Controls.Add(this.splitContainerLeft);
            this.splitContainerParent.Panel1MinSize = 10;
            // 
            // splitContainerParent.Panel2
            // 
            this.splitContainerParent.Panel2.Controls.Add(this.panelRigth);
            this.splitContainerParent.Panel2MinSize = 10;
            this.splitContainerParent.Size = new System.Drawing.Size(1264, 657);
            this.splitContainerParent.SplitterDistance = 399;
            this.splitContainerParent.TabIndex = 1;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.panelNotepad);
            this.splitContainerLeft.Panel1MinSize = 10;
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.panelLeftDown);
            this.splitContainerLeft.Panel2MinSize = 10;
            this.splitContainerLeft.Size = new System.Drawing.Size(399, 657);
            this.splitContainerLeft.SplitterDistance = 449;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // panelNotepad
            // 
            this.panelNotepad.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelNotepad.Controls.Add(this.notepad);
            this.panelNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNotepad.Location = new System.Drawing.Point(0, 0);
            this.panelNotepad.Name = "panelNotepad";
            this.panelNotepad.Size = new System.Drawing.Size(399, 449);
            this.panelNotepad.TabIndex = 0;
            // 
            // notepad
            // 
            this.notepad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notepad.BackColor = System.Drawing.Color.DodgerBlue;
            this.notepad.Extension = "stm";
            this.notepad.Languaje = "SINTIME";
            this.notepad.Location = new System.Drawing.Point(12, 3);
            this.notepad.Name = "notepad";
            this.notepad.Size = new System.Drawing.Size(384, 443);
            this.notepad.TabIndex = 0;
            // 
            // panelLeftDown
            // 
            this.panelLeftDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeftDown.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelLeftDown.Controls.Add(this.tabControl);
            this.panelLeftDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLeftDown.ForeColor = System.Drawing.Color.DodgerBlue;
            this.panelLeftDown.Location = new System.Drawing.Point(12, 0);
            this.panelLeftDown.Name = "panelLeftDown";
            this.panelLeftDown.Size = new System.Drawing.Size(387, 192);
            this.panelLeftDown.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageMessages);
            this.tabControl.Controls.Add(this.tabPageErrors);
            this.tabControl.Controls.Add(this.tabPageWatch);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(381, 189);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.Controls.Add(this.richTextBoxMessages);
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessages.Size = new System.Drawing.Size(373, 163);
            this.tabPageMessages.TabIndex = 0;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.UseVisualStyleBackColor = true;
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.BackColor = System.Drawing.Color.White;
            this.richTextBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxMessages.ForeColor = System.Drawing.Color.Black;
            this.richTextBoxMessages.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.ReadOnly = true;
            this.richTextBoxMessages.Size = new System.Drawing.Size(367, 157);
            this.richTextBoxMessages.TabIndex = 0;
            this.richTextBoxMessages.Text = "";
            // 
            // tabPageErrors
            // 
            this.tabPageErrors.Controls.Add(this.listViewErrors);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 22);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(373, 163);
            this.tabPageErrors.TabIndex = 1;
            this.tabPageErrors.Text = "Errors";
            this.tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // listViewErrors
            // 
            this.listViewErrors.BackColor = System.Drawing.Color.White;
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFile,
            this.columnHeaderLine,
            this.columnHeaderExplication});
            this.listViewErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewErrors.ForeColor = System.Drawing.Color.Black;
            this.listViewErrors.FullRowSelect = true;
            this.listViewErrors.Location = new System.Drawing.Point(3, 3);
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(367, 157);
            this.listViewErrors.TabIndex = 0;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            this.listViewErrors.SelectedIndexChanged += new System.EventHandler(this.listViewErrors_SelectedIndexChanged);
            // 
            // columnHeaderFile
            // 
            this.columnHeaderFile.Text = "File";
            // 
            // columnHeaderLine
            // 
            this.columnHeaderLine.Text = "Line";
            this.columnHeaderLine.Width = 50;
            // 
            // columnHeaderExplication
            // 
            this.columnHeaderExplication.Text = "Explication";
            this.columnHeaderExplication.Width = 255;
            // 
            // tabPageWatch
            // 
            this.tabPageWatch.Controls.Add(this.treeViewWatch);
            this.tabPageWatch.Location = new System.Drawing.Point(4, 22);
            this.tabPageWatch.Name = "tabPageWatch";
            this.tabPageWatch.Size = new System.Drawing.Size(373, 163);
            this.tabPageWatch.TabIndex = 2;
            this.tabPageWatch.Text = "Watch";
            this.tabPageWatch.UseVisualStyleBackColor = true;
            // 
            // treeViewWatch
            // 
            this.treeViewWatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewWatch.Location = new System.Drawing.Point(0, 0);
            this.treeViewWatch.Name = "treeViewWatch";
            this.treeViewWatch.Size = new System.Drawing.Size(373, 163);
            this.treeViewWatch.TabIndex = 0;
            // 
            // panelRigth
            // 
            this.panelRigth.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelRigth.Controls.Add(this.panelDisplay);
            this.panelRigth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRigth.Location = new System.Drawing.Point(0, 0);
            this.panelRigth.Name = "panelRigth";
            this.panelRigth.Size = new System.Drawing.Size(861, 657);
            this.panelRigth.TabIndex = 0;
            // 
            // panelDisplay
            // 
            this.panelDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDisplay.AutoScroll = true;
            this.panelDisplay.Controls.Add(this.pictureBoxDisplay);
            this.panelDisplay.Location = new System.Drawing.Point(3, 26);
            this.panelDisplay.Name = "panelDisplay";
            this.panelDisplay.Size = new System.Drawing.Size(846, 619);
            this.panelDisplay.TabIndex = 1;
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.BackColor = System.Drawing.Color.White;
            this.pictureBoxDisplay.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(1920, 1080);
            this.pictureBoxDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxDisplay.TabIndex = 0;
            this.pictureBoxDisplay.TabStop = false;
            this.pictureBoxDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxDisplay_Paint);
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitContainerParent);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wall-E Simulator - Sintime Compiler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainerParent.Panel1.ResumeLayout(false);
            this.splitContainerParent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerParent)).EndInit();
            this.splitContainerParent.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            this.panelNotepad.ResumeLayout(false);
            this.panelLeftDown.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageMessages.ResumeLayout(false);
            this.tabPageErrors.ResumeLayout(false);
            this.tabPageWatch.ResumeLayout(false);
            this.panelRigth.ResumeLayout(false);
            this.panelDisplay.ResumeLayout(false);
            this.panelDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugOpcionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sintimeLanguajeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildSimuladorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem nextInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerParent;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.Panel panelNotepad;
        private System.Windows.Forms.Panel panelLeftDown;
        private System.Windows.Forms.Panel panelRigth;
        private CodeStrange.Controls.Notepad notepad;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectProyectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem closeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildRobotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.TabPage tabPageErrors;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.ListView listViewErrors;
        private System.Windows.Forms.ColumnHeader columnHeaderFile;
        private System.Windows.Forms.ColumnHeader columnHeaderLine;
        private System.Windows.Forms.ColumnHeader columnHeaderExplication;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageWatch;
        private System.Windows.Forms.TreeView treeViewWatch;
        private System.Windows.Forms.ToolStripMenuItem sizeOfTheTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatesSpeedToolStripMenuItem;
    }
}

