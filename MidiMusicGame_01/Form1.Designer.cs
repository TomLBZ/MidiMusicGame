namespace MidiMusicGame_01
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mIDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.positionHScrollBar = new System.Windows.Forms.HScrollBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tracksToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.allTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tickToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pointsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pianoControl1 = new Sanford.Multimedia.Midi.UI.PianoControl();
            this.sequence1 = new Sanford.Multimedia.Midi.Sequence();
            this.sequencer1 = new Sanford.Multimedia.Midi.Sequencer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openMidiFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCombo = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mIDIToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.gameModeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mIDIToolStripMenuItem
            // 
            this.mIDIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputDeviceToolStripMenuItem});
            this.mIDIToolStripMenuItem.Name = "mIDIToolStripMenuItem";
            this.mIDIToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.mIDIToolStripMenuItem.Text = "&MIDI";
            // 
            // outputDeviceToolStripMenuItem
            // 
            this.outputDeviceToolStripMenuItem.Name = "outputDeviceToolStripMenuItem";
            this.outputDeviceToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.outputDeviceToolStripMenuItem.Text = "Output Device...";
            this.outputDeviceToolStripMenuItem.Click += new System.EventHandler(this.outputDeviceToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gameModeToolStripMenuItem
            // 
            this.gameModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.hardToolStripMenuItem,
            this.automaticToolStripMenuItem});
            this.gameModeToolStripMenuItem.Name = "gameModeToolStripMenuItem";
            this.gameModeToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.gameModeToolStripMenuItem.Text = "&Game Mode";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.easyToolStripMenuItem.Text = "Easy(Rhythm Only)";
            this.easyToolStripMenuItem.CheckedChanged += new System.EventHandler(this.easyToolStripMenuItem_CheckedChanged);
            this.easyToolStripMenuItem.Click += new System.EventHandler(this.easyToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Checked = true;
            this.normalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.CheckedChanged += new System.EventHandler(this.normalToolStripMenuItem_CheckedChanged);
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            this.hardToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.hardToolStripMenuItem.Text = "Hard";
            this.hardToolStripMenuItem.CheckedChanged += new System.EventHandler(this.hardToolStripMenuItem_CheckedChanged);
            this.hardToolStripMenuItem.Click += new System.EventHandler(this.hardToolStripMenuItem_Click);
            // 
            // automaticToolStripMenuItem
            // 
            this.automaticToolStripMenuItem.Name = "automaticToolStripMenuItem";
            this.automaticToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.automaticToolStripMenuItem.Text = "Automatic";
            this.automaticToolStripMenuItem.CheckedChanged += new System.EventHandler(this.automaticToolStripMenuItem_CheckedChanged);
            this.automaticToolStripMenuItem.Click += new System.EventHandler(this.automaticToolStripMenuItem_Click);
            // 
            // positionHScrollBar
            // 
            this.positionHScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.positionHScrollBar.Location = new System.Drawing.Point(3, 16);
            this.positionHScrollBar.Name = "positionHScrollBar";
            this.positionHScrollBar.Size = new System.Drawing.Size(563, 25);
            this.positionHScrollBar.TabIndex = 3;
            this.positionHScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.positionHScrollBar_Scroll);
            this.positionHScrollBar.ValueChanged += new System.EventHandler(this.positionHScrollBar_ValueChanged);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(646, 12);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(71, 32);
            this.stopButton.TabIndex = 0;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(569, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(71, 32);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(723, 12);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(71, 32);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.tracksToolStripDropDownButton,
            this.tickToolStripStatusLabel,
            this.pointsToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // tracksToolStripDropDownButton
            // 
            this.tracksToolStripDropDownButton.BackColor = System.Drawing.SystemColors.Info;
            this.tracksToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tracksToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allTrackToolStripMenuItem,
            this.toolStripSeparator2});
            this.tracksToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("tracksToolStripDropDownButton.Image")));
            this.tracksToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tracksToolStripDropDownButton.Name = "tracksToolStripDropDownButton";
            this.tracksToolStripDropDownButton.Size = new System.Drawing.Size(96, 20);
            this.tracksToolStripDropDownButton.Text = "Choose Tracks";
            // 
            // allTrackToolStripMenuItem
            // 
            this.allTrackToolStripMenuItem.Checked = true;
            this.allTrackToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allTrackToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.allTrackToolStripMenuItem.Name = "allTrackToolStripMenuItem";
            this.allTrackToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.allTrackToolStripMenuItem.Text = "&All Tracks";
            this.allTrackToolStripMenuItem.CheckedChanged += new System.EventHandler(this.allTrackToolStripMenuItem_CheckedChanged);
            this.allTrackToolStripMenuItem.Click += new System.EventHandler(this.allTrackToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // tickToolStripStatusLabel
            // 
            this.tickToolStripStatusLabel.Name = "tickToolStripStatusLabel";
            this.tickToolStripStatusLabel.Size = new System.Drawing.Size(46, 17);
            this.tickToolStripStatusLabel.Text = "Ticks: 0";
            // 
            // pointsToolStripStatusLabel
            // 
            this.pointsToolStripStatusLabel.Name = "pointsToolStripStatusLabel";
            this.pointsToolStripStatusLabel.Size = new System.Drawing.Size(110, 17);
            this.pointsToolStripStatusLabel.Text = "Points: 0; Combo: 0";
            // 
            // pianoControl1
            // 
            this.pianoControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pianoControl1.HighNoteID = 127;
            this.pianoControl1.Location = new System.Drawing.Point(0, 475);
            this.pianoControl1.LowNoteID = 0;
            this.pianoControl1.Name = "pianoControl1";
            this.pianoControl1.NoteOnColor = System.Drawing.Color.SkyBlue;
            this.pianoControl1.Size = new System.Drawing.Size(800, 53);
            this.pianoControl1.TabIndex = 5;
            this.pianoControl1.Text = "pianoControl1";
            this.pianoControl1.PianoKeyDown += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.pianoControl1_PianoKeyDown);
            this.pianoControl1.PianoKeyUp += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.pianoControl1_PianoKeyUp);
            // 
            // sequence1
            // 
            this.sequence1.Format = 1;
            // 
            // sequencer1
            // 
            this.sequencer1.Position = 0;
            this.sequencer1.Sequence = this.sequence1;
            this.sequencer1.PlayingCompleted += new System.EventHandler(this.HandlePlayingCompleted);
            this.sequencer1.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.HandleChannelMessagePlayed);
            this.sequencer1.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.HandleSysExMessagePlayed);
            this.sequencer1.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.HandleChased);
            this.sequencer1.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.HandleStopped);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openMidiFileDialog
            // 
            this.openMidiFileDialog.DefaultExt = "mid";
            this.openMidiFileDialog.FileName = "town";
            this.openMidiFileDialog.Filter = "MIDI files|*.mid|All files|*.*";
            this.openMidiFileDialog.InitialDirectory = "C:\\Windows\\media";
            this.openMidiFileDialog.Title = "Open MIDI file";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stopButton);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Controls.Add(this.continueButton);
            this.groupBox1.Controls.Add(this.positionHScrollBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 50);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Music Control";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 401);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // lblCombo
            // 
            this.lblCombo.AutoSize = true;
            this.lblCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCombo.Location = new System.Drawing.Point(198, 0);
            this.lblCombo.Name = "lblCombo";
            this.lblCombo.Size = new System.Drawing.Size(386, 108);
            this.lblCombo.TabIndex = 9;
            this.lblCombo.Text = "Combo!";
            this.lblCombo.VisibleChanged += new System.EventHandler(this.lblCombo_VisibleChanged);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.lblCombo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pianoControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(816, 589);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIDI Music Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mIDIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.HScrollBar positionHScrollBar;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private Sanford.Multimedia.Midi.UI.PianoControl pianoControl1;
        private Sanford.Multimedia.Midi.Sequence sequence1;
        private Sanford.Multimedia.Midi.Sequencer sequencer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openMidiFileDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel tickToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem gameModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tracksToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem allTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem automaticToolStripMenuItem;
        private System.Windows.Forms.Label lblCombo;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripStatusLabel pointsToolStripStatusLabel;
    }
}

