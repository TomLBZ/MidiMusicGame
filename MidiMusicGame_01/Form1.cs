using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace MidiMusicGame_01
{
    public partial class Form1 : Form
    {
        private const int _EASY = 0;
        private const int _NORMAL = 1;
        private const int _HARD = 2;
        private const int _AUTOMATIC = 3;
        private const int _PIANO = 224;
        private const int _PIANO_ADJUSTED = 222;
        private const int _TICK_PER_PIXEL = 10;
        private const int _PIXELFALLSPEED = 100;
        private const int _KEY_ALPHA = 180;
        private int outDeviceID = 0;
        private int _GAMEMODE = 1;
        private int _MAXHEIGHT = 0;
        private int _TICKS_FALLEN = 0;
        private int _POINTS = 0;
        private int _COMBO = 0;
        private bool isCombo = false;
        private bool scrolling = false;
        private bool playing = false;
        private bool closing = false;
        private bool IS_GARDED = false;
        private bool[] selectedTracks;
        private OutputDevice outDevice;
        private OutputDeviceDialog outDialog = new OutputDeviceDialog();
        private List<MidiEvent[]> loadedTracks;
        private List<Rectangle[]> midiNotes;
        private List<Rectangle> touchingNotes = new List<Rectangle>();
        private Bitmap _IMAGE;

        public Form1()
        {
            InitializeComponent();
        }
 
        /// <summary>
        /// h[0-360],s[0-1f],v[0-1f]
        /// </summary>
        public struct HSV
        {
            public float h;
            public float s;
            public float v;
        }

        // the Color Converter
        static public Color ColorFromHSL(HSV hsl, int alpha)
        {
            if (hsl.s == 0)
            {
                int L = (int)hsl.v; return Color.FromArgb(_KEY_ALPHA, L, L, L);
            }

            double min, max, h;
            h = hsl.h / 360d;

            max = hsl.v < 0.5d ? hsl.v * (1 + hsl.s) : (hsl.v + hsl.s) - (hsl.v * hsl.s);
            min = (hsl.v * 2d) - max;

            Color c = Color.FromArgb(_KEY_ALPHA, (int)(255 * RGBChannelFromHue(min, max, h + 1 / 3d)),
                                          (int)(255 * RGBChannelFromHue(min, max, h)),
                                          (int)(255 * RGBChannelFromHue(min, max, h - 1 / 3d)));
            return c;
        }

        static double RGBChannelFromHue(double m1, double m2, double h)
        {
            h = (h + 1d) % 1d;
            if (h < 0) h += 1;
            if (h * 6 < 1) return m1 + (m2 - m1) * 6 * h;
            else if (h * 2 < 1) return m2;
            else if (h * 3 < 2) return m1 + (m2 - m1) * 6 * (2d / 3d - h);
            else return m1;

        }

        // color brightness as perceived:
        private float getBrightness(Color c)
        {
            return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount==0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);
                    sequence1.LoadProgressChanged += HandleLoadProgressChanged;
                    sequence1.LoadCompleted += HandleLoadCompleted;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Close();
                }
            }
            base.OnLoad(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pianoControl1.PressPianoKey(e.KeyCode);

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            pianoControl1.ReleasePianoKey(e.KeyCode);

            base.OnKeyUp(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            closing = true;

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            sequence1.Dispose();

            if (outDevice != null)
            {
                outDevice.Dispose();
            }

            outDialog.Dispose();

            base.OnClosed(e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openMidiFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openMidiFileDialog.FileName;
                Open(fileName);
                startButton.Enabled = true;
            }
        }

        public void Open(string fileName)
        {
            try
            {
                sequencer1.Stop();
                playing = false;
                sequence1.LoadAsync(fileName);
                this.Cursor = Cursors.WaitCursor;
                startButton.Enabled = false;
                continueButton.Enabled = false;
                stopButton.Enabled = false;
                openToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void outputDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();

            dlg.ShowDialog();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                playing = false;
                sequencer1.Stop();
                timer1.Stop();
                stopButton.Enabled = false;
                startButton.Enabled = true;
                continueButton.Enabled = true;
            }
            catch (Exception ex)
            {
                stopButton.Enabled = true;
                startButton.Enabled = false;
                continueButton.Enabled = false;
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                //List<MidiEvent> events = LoadSelectedTracks(selectedTracks);
                //Visualise(events);
                _POINTS = 0;
                _COMBO = 0;
                VisualiseRectangles(midiNotes, out touchingNotes);
                playing = true;
                sequencer1.Start();
                timer1.Start();
                startButton.Enabled = false;
                stopButton.Enabled = true;
                continueButton.Enabled = false;
            }
            catch (Exception ex)
            {
                startButton.Enabled = true;
                stopButton.Enabled = false;
                continueButton.Enabled = true;
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private List<MidiEvent> LoadSelectedTracks(bool[] selectedTracks)
        {
            int length = 0;
            for (int i = 0; i < loadedTracks.Count; i++)
            {
                if (selectedTracks[i])
                {
                    length += loadedTracks[i].Length;
                }
            }
            List<MidiEvent> midiEvents = new List<MidiEvent>(length);
            for (int j = 0; j < loadedTracks.Count; j++)
            {
                if (selectedTracks[j])
                {
                    for (int k = 0; k < loadedTracks[j].Length; k++)
                    {
                        midiEvents.Add(loadedTracks[j][k]);
                    }
                }
            }
            return midiEvents.OrderBy(u => u.AbsoluteTicks).ToList();
        }

        private int[,] GetIntMapFromEvents(List<MidiEvent> midiEvents, out int width, out int height)
        {
            height = midiEvents.Last().AbsoluteTicks + 1;
            width = 128;
            int[,] intMap = new int[width, height];
            foreach (MidiEvent midiEvent in midiEvents)
            {
                ChannelMessage cm = (ChannelMessage)midiEvent.MidiMessage;
                switch (cm.Command)
                {
                    case ChannelCommand.NoteOff:
                        intMap[cm.Data1, midiEvent.AbsoluteTicks] = -1;
                        break;
                    case ChannelCommand.NoteOn:
                        if (cm.Data2 > 0)
                        {
                            intMap[cm.Data1, midiEvent.AbsoluteTicks] = 1;
                        }
                        else
                        {
                            intMap[cm.Data1, midiEvent.AbsoluteTicks] = -1;
                        }
                        break;
                    case ChannelCommand.PolyPressure:
                        break;
                    case ChannelCommand.Controller:
                        break;
                    case ChannelCommand.ProgramChange:
                        break;
                    case ChannelCommand.ChannelPressure:
                        break;
                    case ChannelCommand.PitchWheel:
                        break;
                    default:
                        break;
                }
            }
            return intMap;
        }

        private void Visualise(List<MidiEvent> loadedEvents)
        {
            int width = 0;
            int height = 0;
            int[,] intMap = GetIntMapFromEvents(loadedEvents, out width, out height);
            tickToolStripStatusLabel.Text = "Ticks: " + height.ToString();
            int ticksPerScreen = 5000;
            int minimalPianoWidth = 224;
            Bitmap bmp = new Bitmap(minimalPianoWidth, height);
            for (int w = 0; w < width; w++)
            {
                int status = 0;
                int[] targetPixelWs = PianoMap(w);
                for (int h = 0; h < height; h++)
                {
                    status += intMap[w, h];
                    Color color = Color.Black;
                    switch (status)
                    {
                        case 1:
                            color = Color.White;
                            for (int x = 0; x < targetPixelWs.Length; x++)
                            {
                                bmp.SetPixel(targetPixelWs[x], h, color);
                            }
                            break;
                        case 2:
                            color=Color.Green;
                            for (int x = 0; x < targetPixelWs.Length; x++)
                            {
                                bmp.SetPixel(targetPixelWs[x], h, color);
                            }
                            break;
                        case 3:
                            color=Color.Blue;
                            for (int x = 0; x < targetPixelWs.Length; x++)
                            {
                                bmp.SetPixel(targetPixelWs[x], h, color);
                            }
                            break;
                        case 4: case 5: case 6: case 7: case 8: case 9:
                            color=Color.Red;
                            for (int x = 0; x < targetPixelWs.Length; x++)
                            {
                                bmp.SetPixel(targetPixelWs[x], h, color);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            Bitmap targetBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(targetBitmap);
            g.DrawImage(bmp, new Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height), new Rectangle(0, 0, minimalPianoWidth, ticksPerScreen), GraphicsUnit.Pixel);           
            g.Dispose();
            targetBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            pictureBox1.Image = targetBitmap;
        }

        private List<Rectangle[]> GetRectangles(List<MidiEvent[]> midiTracks)
        {
            for (int i = 0; i < midiTracks.Count; i++)
            {
                midiTracks[i].OrderBy(u => u.AbsoluteTicks);
            }
            List<Rectangle[]> midiRectangles = new List<Rectangle[]>(midiTracks.Count);
            for (int j = 0; j < midiTracks.Count; j++)
            {
                MidiEvent[] midiEvents = midiTracks[j];
                Rectangle[] rectangles = new Rectangle[0];
                List<Point[]> notePoints = new List<Point[]>(128);
                for (int m = 0; m < 128; m++)
                {
                    notePoints.Add(new Point[0]);
                }
                for (int k = 0; k < midiEvents.Length; k++)
                {
                    ChannelMessage channelMessage = (ChannelMessage)midiEvents[k].MidiMessage;
                    _MAXHEIGHT = Math.Max(_MAXHEIGHT, midiEvents[k].AbsoluteTicks);
                    if (channelMessage.Command == ChannelCommand.NoteOn)
                    {
                        if (channelMessage.Data2 != 0)
                        {
                            notePoints[channelMessage.Data1] = 
                                notePoints[channelMessage.Data1].Append(new Point(midiEvents[k].AbsoluteTicks, 0)).ToArray();
                        }
                        else
                        {
                            notePoints[channelMessage.Data1][notePoints[channelMessage.Data1].Length - 1].Y = midiEvents[k].AbsoluteTicks;
                            int rectHeight = notePoints[channelMessage.Data1].Last().Y - notePoints[channelMessage.Data1].Last().X;
                            int[] pianos = PianoMap(channelMessage.Data1);
                            int rectWidth = pianos.Length * pictureBox1.Width / _PIANO_ADJUSTED;
                            int rectX = pianos[0] * pictureBox1.Width / _PIANO_ADJUSTED;
                            int rectY = notePoints[channelMessage.Data1].Last().X / _TICK_PER_PIXEL;
                            rectHeight /= _TICK_PER_PIXEL;
                            rectangles = rectangles.Append(new Rectangle(rectX, rectY, rectWidth, rectHeight)).ToArray();
                            List<Point> lst = notePoints[channelMessage.Data1].ToList();
                            lst.RemoveAt(lst.Count - 1);
                            notePoints[channelMessage.Data1] = lst.ToArray();
                        }
                    }
                    else if (channelMessage.Command == ChannelCommand.NoteOff)
                    {
                        notePoints[channelMessage.Data1][notePoints[channelMessage.Data1].Length - 1].Y = midiEvents[k].AbsoluteTicks;
                        int rectHeight = notePoints[channelMessage.Data1].Last().Y - notePoints[channelMessage.Data1].Last().X;
                        int[] pianos = PianoMap(channelMessage.Data1);
                        int rectWidth = pianos.Length * pictureBox1.Width / _PIANO_ADJUSTED;
                        int rectX = pianos[0] * pictureBox1.Width / _PIANO_ADJUSTED;
                        int rectY = notePoints[channelMessage.Data1].Last().X / _TICK_PER_PIXEL;
                        rectHeight /= _TICK_PER_PIXEL;
                        rectangles = rectangles.Append(new Rectangle(rectX, rectY, rectWidth, rectHeight)).ToArray();
                        List<Point> lst = notePoints[channelMessage.Data1].ToList();
                        lst.RemoveAt(lst.Count - 1);
                        notePoints[channelMessage.Data1] = lst.ToArray();
                    }
                }
                midiRectangles.Add(rectangles);
            }
            _MAXHEIGHT += 1;
            return midiRectangles;
        }

        private void VisualiseRectangles(List<Rectangle[]> rectanglesByTrack, out List<Rectangle> touchingRects)
        {
            int intTracks = selectedTracks.Where(u => u.ToString() == "True").ToList().Count;
            List<Rectangle[]> actualList = new List<Rectangle[]>(intTracks);
            for (int m = 0; m < selectedTracks.Length; m++)
            {
                if (selectedTracks[m])
                {
                    actualList.Add(rectanglesByTrack[m]);
                }
            }
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Color[] colors = GenerateColors(actualList.Count);
            List<Rectangle> output = new List<Rectangle>();
            for (int i = 0; i < actualList.Count; i++)
            {
                Rectangle[] rects = actualList[i];
                List<Rectangle> rectDrawn = new List<Rectangle>();
                for (int j = 0; j < rects.Length; j++)
                {
                    int measure = rects[j].Y - _TICKS_FALLEN / _TICK_PER_PIXEL;
                    if (measure < bmp.Height)
                    {
                        rectDrawn.Add(rects[j]);
                    }
                    if (measure < 5 && measure + rects[j].Height > 0)
                    {
                        output.Add(rects[j]);
                    }
                }
                if (rectDrawn.Count > 0)
                {
                    RectangleF[] finalRects = new RectangleF[rectDrawn.Count];
                    for (int k = 0; k < rectDrawn.Count; k++)
                    {
                        finalRects[k] = new RectangleF(rectDrawn[k].X,
                            rectDrawn[k].Y - _TICKS_FALLEN / _TICK_PER_PIXEL, 
                            rectDrawn[k].Width, rectDrawn[k].Height);
                    }
                    g.FillRectangles(new SolidBrush(colors[i]), finalRects);
                }
            }
            g.Dispose();
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            //pictureBox1.Image = bmp;
            pictureBox1.BackgroundImage = bmp;
            touchingRects = output;
        }

        private Color[] GenerateColors(int number)
        {
            if (number == 0)
            {
                number += 1;
            }
            int step = 360 / number;
            HSV hsv = new HSV();
            Color[] color = new Color[number];
            for (int i = 0; i < number; i++)
            {
                hsv.h = i * step;
                hsv.s = 0.5f;
                hsv.v = 0.5f;
                color[i] = ColorFromHSL(hsv, _KEY_ALPHA);
            }
            return color;
        }

        private int InvPianoMap(int location)
        {
            location += 1;  //from [0-223] to [1-224]
            const int Octet = 21;
            int multiplier = location / Octet;
            int remainder = location - Octet * multiplier;
            int octetPosition = 0;
            switch (remainder)
            {
                case 1: case 2:
                    octetPosition = 1;
                    break;
                case 5:
                    octetPosition = 3;
                    break;
                case 8: case 9:
                    octetPosition = 5;
                    break;
                case 10: case 11:
                    octetPosition = 6;
                    break;
                case 14:
                    octetPosition = 8;
                    break;
                case 17:
                    octetPosition = 10;
                    break;
                case 20:
                    octetPosition = 12;
                    break;
                case 0:
                    octetPosition = 0;
                    break;

                case 3: case 4:
                    octetPosition = 2;
                    break;
                case 6: case 7:
                    octetPosition = 4;
                    break;
                case 12: case 13:
                    octetPosition = 7;
                    break;
                case 15: case 16:
                    octetPosition = 9;
                    break;
                case 18: case 19:
                    octetPosition = 11;
                    break;
                default:
                    octetPosition = 0;
                    break;
            }
            return multiplier * 12 + octetPosition - 1; //from [1,128] to [0,127]
        }

        private int[] PianoMap(int key)
        {
            key += 1;   //from [0,127] to [1,128]
            const int Octet = 12;
            int multiplier = key / Octet;
            int remainder = key - Octet * multiplier;
            int[] octetPosition;
            switch (remainder)
            {
                case 1:
                    octetPosition = new int[2] { 1, 2 };
                    break;
                case 2:
                    octetPosition = new int[2] { 3, 4 };
                    break;
                case 3:
                    octetPosition = new int[1] { 5 };
                    break;
                case 4:
                    octetPosition = new int[2] { 6, 7 };
                    break;
                case 5:
                    octetPosition = new int[2] { 8, 9 };
                    break;
                case 6:
                    octetPosition = new int[2] { 10, 11 };
                    break;
                case 7:
                    octetPosition = new int[2] { 12, 13 };
                    break;
                case 8:
                    octetPosition = new int[1] { 14 };
                    break;
                case 9:
                    octetPosition = new int[2] { 15, 16 };
                    break;
                case 10:
                    octetPosition = new int[1] { 17 };
                    break;
                case 11:
                    octetPosition = new int[2] { 18, 19 };
                    break;
                case 0:
                    octetPosition = new int[2] { -1, 0 };
                    break;
                default:
                    octetPosition = new int[2] { 20, 21 };
                    break;
            }
            for (int i = 0; i < octetPosition.Length; i++)
            {
                octetPosition[i] += multiplier * 21;
                octetPosition[i] -= 1;  //from [1-224] to [0-223]
            }
            return octetPosition;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            try
            {
                playing = true;
                sequencer1.Continue();
                timer1.Start();
                startButton.Enabled = false;
                continueButton.Enabled = false;
                stopButton.Enabled = true;
            }
            catch (Exception ex)
            {
                startButton.Enabled = true;
                continueButton.Enabled = true;
                stopButton.Enabled = false;
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void positionHScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll)
            {
                sequencer1.Position = e.NewValue;

                scrolling = false;
            }
            else
            {
                scrolling = true;
            }
        }

        private void HandleLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            startButton.Enabled = true;
            continueButton.Enabled = false;
            stopButton.Enabled = false;
            openToolStripMenuItem.Enabled = true;
            toolStripProgressBar1.Value = 0;
            
            Track[] tracks = sequence1.ToArray();
            List<MidiEvent[]> midiTracks = new List<MidiEvent[]>(tracks.Length);
            int totalevents = 0;
            for (int k = 0; k < tracks.Length; k++)
            {
                totalevents += tracks[k].Count;
            }
            for (int i = 0; i < tracks.Length; i++)
            {
                int c = tracks[i].Count;
                MidiEvent[] trackEvents = new MidiEvent[c];
                for (int j = 0; j < c; j++)
                {
                    MidiEvent mEvent = tracks[i].GetMidiEvent(j);
                    trackEvents[j] = mEvent;
                }
                MidiEvent[] midis = trackEvents.Where(x => x.MidiMessage.MessageType == MessageType.Channel).ToArray();
                if (midis.Length > 0)
                {
                    midiTracks.Add(midis);
                }
            }
            foreach (MidiEvent[] item in midiTracks)
            {
                item.OrderBy(k => k.AbsoluteTicks);
            }
            loadedTracks = midiTracks;          

            int tracksCount = loadedTracks.Count;
            selectedTracks = new bool[tracksCount];
            int dropdownLength = tracksToolStripDropDownButton.DropDownItems.Count;
            for (int i = 2; i < dropdownLength; i++)
            {
                tracksToolStripDropDownButton.DropDownItems.RemoveAt(2);
            }
            for (int m = 0; m < tracksCount; m++)
            {
                tracksToolStripDropDownButton.DropDownItems.Add("Track " + m.ToString());
                ToolStripMenuItem menuItem = (ToolStripMenuItem)tracksToolStripDropDownButton.DropDownItems[m + 2];
                menuItem.Click += new EventHandler(track0ToolStripMenuItem_Click);
                menuItem.CheckedChanged += new EventHandler(track0ToolStripMenuItem_CheckedChanged);
                if (allTrackToolStripMenuItem.Checked)
                {
                    menuItem.Checked = true;
                }
            }

            _MAXHEIGHT = 0;
            _POINTS = 0;
            _TICKS_FALLEN = 0;
            _COMBO = 0;
            positionHScrollBar.Value = 0;
            tickToolStripStatusLabel.Text = "Ticks: 0";
            pointsToolStripStatusLabel.Text = "Points: 0; Combo: 0";
            midiNotes = GetRectangles(loadedTracks);

            if (e.Error == null)
            {
                positionHScrollBar.Value = 0;
                positionHScrollBar.Maximum = sequence1.GetLength();
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if (closing)
            {
                return;
            }
            switch (_GAMEMODE)
            {
                case _EASY:
                    IS_GARDED = false;
                    outDevice.Send(e.Message);
                    break;
                case _NORMAL:
                    IS_GARDED = false;
                    outDevice.Send(e.Message);
                    break;
                case _HARD:
                    IS_GARDED = false;
                    outDevice.Send(e.Message);
                    break;
                case _AUTOMATIC:
                    IS_GARDED = false;
                    if (e.Message.Command == ChannelCommand.NoteOn)
                    {
                        if (e.Message.Data2 > 0)
                        {
                            PianoKeyEventArgs pianoKeyEventArgs = new PianoKeyEventArgs(e.Message.Data1);
                            pianoControl1_PianoKeyDown(sender, pianoKeyEventArgs);
                        }
                        else
                        {
                            PianoKeyEventArgs pianoKeyEventArgs = new PianoKeyEventArgs(e.Message.Data1);
                            pianoControl1_PianoKeyUp(sender, pianoKeyEventArgs);
                        }
                    }
                    else if (e.Message.Command == ChannelCommand.NoteOff)
                    {
                        PianoKeyEventArgs pianoKeyEventArgs = new PianoKeyEventArgs(e.Message.Data1);
                        pianoControl1_PianoKeyUp(sender, pianoKeyEventArgs);
                    }
                    else
                    {
                        outDevice.Send(e.Message);
                    }
                    IS_GARDED = true;
                    pianoControl1.Send(e.Message);
                    break;
                default:
                    IS_GARDED = false;
                    outDevice.Send(e.Message);
                    break;
            }
        }

        private void HandleChased(object sender, ChasedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                switch (_GAMEMODE)
                {
                    case _EASY:
                        IS_GARDED = false;
                        outDevice.Send(message);
                        break;
                    case _NORMAL:
                        IS_GARDED = false;
                        outDevice.Send(message);
                        break;
                    case _HARD:
                        IS_GARDED = false;
                        outDevice.Send(message);
                        break;
                    case _AUTOMATIC:
                        IS_GARDED = false;
                        if ((message.Command == ChannelCommand.NoteOn && message.Data2 == 0)
                            || message.Command == ChannelCommand.NoteOff)
                        {
                            PianoKeyEventArgs pianoKeyEventArgs = new PianoKeyEventArgs(message.Data1);
                            pianoControl1_PianoKeyUp(sender, pianoKeyEventArgs);
                        }
                        else
                        {
                            outDevice.Send(message);
                        }
                        IS_GARDED = true;
                        //outDevice.Send(message);
                        pianoControl1.Send(message);
                        break;
                    default:
                        IS_GARDED = false;
                        outDevice.Send(message);
                        break;
                }
            }
        }

        private void HandlePlayingCompleted(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void pianoControl1_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            #region Guard

            if (playing && IS_GARDED)
            {
                return;
            }

            #endregion

            int[] pianos = PianoMap(e.NoteID);
            int w = pianos.Length * pictureBox1.Width / _PIANO_ADJUSTED;
            int x = pianos[0] * pictureBox1.Width / _PIANO_ADJUSTED;
            try
            {
                Graphics g = Graphics.FromImage(_IMAGE);
                Rectangle[] shootingRects = new Rectangle[3];
                shootingRects[0] = new Rectangle(x, 0, w, pictureBox1.Height);
                shootingRects[1] = new Rectangle(x - 1, pictureBox1.Height - 5, w + 2, 5);
                shootingRects[2] = new Rectangle(x - 2, pictureBox1.Height - 3, w + 4, 3);
                g.FillRectangles(new SolidBrush(Color.FromArgb(128, 255, 255, 255)), shootingRects);

                int hit = 0;
                for (int i = 0; i < touchingNotes.Count; i++)
                {
                    if (touchingNotes[i].X >= x && touchingNotes[i].X < x + w)
                    {
                        hit += 1;
                        _POINTS += 10;
                        if (isCombo)
                        {
                            _COMBO += 1;
                        }
                        isCombo = true;
                    }
                }
                if (hit == 0)
                {
                    isCombo = false;
                }
                else
                {
                    if (lblCombo.Visible == false)
                    {
                        lblCombo.Visible = true;
                    }
                    else
                    {
                        timer2.Enabled = true;
                    }
                }
                g.Dispose();
                pictureBox1.Image = _IMAGE;
                pointsToolStripStatusLabel.Text = "Points: " + _POINTS.ToString() + "; Combo: " + _COMBO.ToString();
            }
            catch (InvalidOperationException)
            {
                //throw;
            }

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }

        private void pianoControl1_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            #region Guard

            if (playing && IS_GARDED)
            {
                return;
            }

            #endregion
            try
            {
                Graphics g = Graphics.FromImage(_IMAGE);
                Rectangle[] shootingRects = new Rectangle[3];
                int[] pianos = PianoMap(e.NoteID);
                int w = pianos.Length * pictureBox1.Width / _PIANO_ADJUSTED;
                int x = pianos[0] * pictureBox1.Width / _PIANO_ADJUSTED;
                shootingRects[0] = new Rectangle(x, 0, w, pictureBox1.Height);
                shootingRects[1] = new Rectangle(x - 1, pictureBox1.Height - 5, w + 2, 5);
                shootingRects[2] = new Rectangle(x - 2, pictureBox1.Height - 3, w + 4, 3);
                g.FillRectangles(new SolidBrush(Color.Red), shootingRects);
                _IMAGE.MakeTransparent(Color.Red);
                _IMAGE.MakeTransparent();
                g.Dispose();
                pictureBox1.Image = _IMAGE;
            }
            catch (InvalidOperationException)
            {
                //throw;
            }

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!scrolling)
            {
                positionHScrollBar.Value = Math.Min(sequencer1.Position, positionHScrollBar.Maximum);
            }
        }

        private void allTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allTrackToolStripMenuItem.Checked = !allTrackToolStripMenuItem.Checked;
        }

        private void track0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = !item.Checked;
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = !easyToolStripMenuItem.Checked;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = !normalToolStripMenuItem.Checked;
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hardToolStripMenuItem.Checked = !hardToolStripMenuItem.Checked;
        }

        private void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            automaticToolStripMenuItem.Checked = !automaticToolStripMenuItem.Checked;
        }

        private void easyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (easyToolStripMenuItem.Checked)
            {
                normalToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = false;
                automaticToolStripMenuItem.Checked = false;
                _GAMEMODE = _EASY;
            }
            else
            {
                if (!normalToolStripMenuItem.Checked && !hardToolStripMenuItem.Checked && !automaticToolStripMenuItem.Checked)
                {
                    easyToolStripMenuItem.Checked = true;
                }
            }
        }

        private void normalToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (normalToolStripMenuItem.Checked)
            {
                easyToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = false;
                automaticToolStripMenuItem.Checked = false;
                _GAMEMODE = _NORMAL;
            }
            else
            {
                if (!easyToolStripMenuItem.Checked && !hardToolStripMenuItem.Checked && !automaticToolStripMenuItem.Checked)
                {
                    normalToolStripMenuItem.Checked = true;
                }
            }
        }

        private void hardToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (hardToolStripMenuItem.Checked)
            {
                easyToolStripMenuItem.Checked = false;
                normalToolStripMenuItem.Checked = false;
                automaticToolStripMenuItem.Checked = false;
                _GAMEMODE = _HARD;
            }
            else
            {
                if (!easyToolStripMenuItem.Checked && !normalToolStripMenuItem.Checked && !automaticToolStripMenuItem.Checked)
                {
                    hardToolStripMenuItem.Checked = true;
                }
            }
        }

        private void automaticToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (automaticToolStripMenuItem.Checked)
            {
                easyToolStripMenuItem.Checked = false;
                normalToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = false;
                _GAMEMODE = _AUTOMATIC;
            }
            else
            {
                if (!easyToolStripMenuItem.Checked && !normalToolStripMenuItem.Checked && !hardToolStripMenuItem.Checked)
                {
                    automaticToolStripMenuItem.Checked = true;
                }
            }
        }

        private void allTrackToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tracksToolStripDropDownButton.DropDownItems.Count; i++)
            {
                if (tracksToolStripDropDownButton.DropDownItems[i].GetType() == allTrackToolStripMenuItem.GetType())
                {
                    if (tracksToolStripDropDownButton.DropDownItems[i].Text != allTrackToolStripMenuItem.Text)
                    {
                        if (!allTrackToolStripMenuItem.Checked && i == tracksToolStripDropDownButton.DropDownItems.Count - 1)
                        {
                            return;
                        }
                        ((ToolStripMenuItem)tracksToolStripDropDownButton.DropDownItems[i]).Checked = allTrackToolStripMenuItem.Checked;
                    }
                }
            }
        }

        private void track0ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            int index = int.Parse(item.Text.Split(' ')[1]);
            if (item.Checked)
            {
                selectedTracks[index] = true;
            }
            else
            {
                selectedTracks[index] = false;
            }
        }

        private void positionHScrollBar_ValueChanged(object sender, EventArgs e)
        {
            tickToolStripStatusLabel.Text = "Fallen: " + positionHScrollBar.Value.ToString() + " / " + _MAXHEIGHT.ToString() + " Ticks.";
            _TICKS_FALLEN = positionHScrollBar.Value;
            VisualiseRectangles(midiNotes, out touchingNotes);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _IMAGE = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            lblCombo.Visible = false;
            lblCombo.ForeColor = Color.Gold;
            lblCombo.BackColor = Color.FromArgb(0, 0, 0, 0);
            Point newLocation = new Point((pictureBox1.Width - lblCombo.Width) / 2, (pictureBox1.Height - lblCombo.Height) / 2);
            lblCombo.Location = newLocation;
            //_GRAPHICS = Graphics.FromImage(_IMAGE);
        }

        private void lblCombo_VisibleChanged(object sender, EventArgs e)
        {
            timer2.Enabled = lblCombo.Visible;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblCombo.Visible = false;
            if (lblCombo.Visible == false)
            {
                timer2.Enabled = false;
            }
        }
    }
}
