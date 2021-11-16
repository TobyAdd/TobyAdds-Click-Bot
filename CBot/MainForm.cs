using MemoryHacking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CBot

{

    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Keys vKeys);

        private bool mouseDown;
        private Point lastLocation;
        int DownCounter;
        int UpCounter;
        bool DownBool = true;
        bool UpBool = false;
        String RandomDown;
        String RandomUp;
        String InjectStatus;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen selPen = new Pen(Color.White);
            g.DrawRectangle(selPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CBot_CheckedChanged(object sender, EventArgs e)
        {
            if (CBot.Checked == true)
            {
                ClickGetter.Enabled = true;
            }
            else
            {
                ClickGetter.Enabled = false;
                DownLabel.Text = "Down: 0";
                UpLabel.Text = "Release: 0";
                DownCounter = 0;
                UpCounter = 0;
                
            }
        }



        private void ClickGetter_Tick(object sender, EventArgs e)
        {


            if ((GetAsyncKeyState(Keys.LButton) & 0x8000) != 0)
            {
            if (DownBool == true)
                {
                    RandomDown = Directory.EnumerateFiles(@"tcb_down", "*.wav").Random();
                    new Thread(playDownClicks).Start();
                    DownBool = false;
                    UpBool = true;
                    DownCounter = DownCounter + 1;
                    DownLabel.Text = "Down: " + DownCounter.ToString() + " (" + RandomDown + ")";
                }
            }
            else
            {
                if (UpBool == true)
                {
                    RandomUp = Directory.EnumerateFiles(@"tcb_release", "*.wav").Random();
                    new Thread(playReleaseClicks).Start();
                    DownBool = true;
                    UpBool = false;
                    UpCounter = UpCounter + 1;
                    UpLabel.Text = "Release: " + UpCounter.ToString() + " (" + RandomUp + ")";
                }

            }
        }

        private void playDownClicks()
        {
            SoundPlayer downSound = new SoundPlayer(@RandomDown);
            downSound.Play();
        }

        private void playReleaseClicks()
        {
            SoundPlayer upSound = new SoundPlayer(@RandomUp);
            upSound.Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Noclip [OFF]")
            {
                if (Process.GetProcessesByName("GeometryDash").Any())
                {
                    Process targetProcess = Process.GetProcessesByName("GeometryDash").First();
                    Memory processMemory = targetProcess.InitializeMemory();
                    IntPtr moduleBase = processMemory.GetModuleByName("GeometryDash.exe").BaseAddress;
                    processMemory.Write(moduleBase + 0x20A1DD, new byte[] { 0xEB, 0x37 });
                    button4.Text = "Noclip [ON]";
                }
            }
            else if (button4.Text == "Noclip [ON]")
            {
                if (Process.GetProcessesByName("GeometryDash").Any())
                {
                    Process targetProcess = Process.GetProcessesByName("GeometryDash").First();
                    Memory processMemory = targetProcess.InitializeMemory();
                    IntPtr moduleBase = processMemory.GetModuleByName("GeometryDash.exe").BaseAddress;
                    processMemory.Write(moduleBase + 0x20A1DD, new byte[] { 0x8A, 0x80 });
                    button4.Text = "Noclip [OFF]";
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Practice Music Hack [OFF]")
            {
                if (Process.GetProcessesByName("GeometryDash").Any())
                {
                    Process targetProcess = Process.GetProcessesByName("GeometryDash").First();
                    Memory processMemory = targetProcess.InitializeMemory();
                    IntPtr moduleBase = processMemory.GetModuleByName("GeometryDash.exe").BaseAddress;
                    processMemory.Write(moduleBase + 0x20C925, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
                    processMemory.Write(moduleBase + 0x20D143, new byte[] { 0x90, 0x90 });
                    processMemory.Write(moduleBase + 0x20A563, new byte[] { 0x90, 0x90 });
                    processMemory.Write(moduleBase + 0x20A595, new byte[] { 0x90, 0x90 });
                    button5.Text = "Practice Music Hack [ON]";
                }
            }
            else if (button5.Text == "Practice Music Hack [ON]")
            {
                if (Process.GetProcessesByName("GeometryDash").Any())
                {
                    Process targetProcess = Process.GetProcessesByName("GeometryDash").First();
                    Memory processMemory = targetProcess.InitializeMemory();
                    IntPtr moduleBase = processMemory.GetModuleByName("GeometryDash.exe").BaseAddress;
                    processMemory.Write(moduleBase + 0x20C925, new byte[] { 0x0F, 0x85, 0xF7, 0x00, 0x00, 0x00 });
                    processMemory.Write(moduleBase + 0x20D143, new byte[] { 0x75, 0x41 });
                    processMemory.Write(moduleBase + 0x20A563, new byte[] { 0x75, 0x3E });
                    processMemory.Write(moduleBase + 0x20A595, new byte[] { 0x75, 0x0C });
                    button5.Text = "Practice Music Hack [OFF]";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "Safe Mode [OFF]")
            {
                if (Process.GetProcessesByName("GeometryDash").Any())
                {
                    Process targetProcess = Process.GetProcessesByName("GeometryDash").First();
                    Memory processMemory = targetProcess.InitializeMemory();
                    IntPtr moduleBase = processMemory.GetModuleByName("GeometryDash.exe").BaseAddress;
                    processMemory.Write(moduleBase + 0x20A3B2, new byte[] { 0xE9, 0x9A, 0x01, 0x00, 0x00, 0x90, 0x90 });
                    processMemory.Write(moduleBase + 0x1FD40F, new byte[] { 0xE9, 0x13, 0x06, 0x00, 0x00, 0x90, 0x90 });
                    button6.Text = "Safe Mode [ON]";
                }
            }
            else if (button6.Text == "Safe Mode [ON]")
            {
                if (Process.GetProcessesByName("GeometryDash").Any())
                {
                    Process targetProcess = Process.GetProcessesByName("GeometryDash").First();
                    Memory processMemory = targetProcess.InitializeMemory();
                    IntPtr moduleBase = processMemory.GetModuleByName("GeometryDash.exe").BaseAddress;
                    processMemory.Write(moduleBase + 0x20A3B2, new byte[] { 0x80, 0xBB, 0x94, 0x04, 0x00, 0x00, 0x00 });
                    processMemory.Write(moduleBase + 0x1FD40F, new byte[] { 0x83, 0xB9, 0x64, 0x03, 0x00, 0x00, 0x01 });
                    button6.Text = "Safe Mode [OFF]";
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TobyAdd's Click Bot v0.1!" + Environment.NewLine + "Thanks to:" + Environment.NewLine + "TobyAdd - Main Developer" + Environment.NewLine + "Eimaen - Main Developer 2 " + Environment.NewLine + "And you!", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("GeometryDash").Any())
            {
                if (openDLLdialog.ShowDialog() == DialogResult.OK)
                {
                    Process process = Process.GetProcessesByName("GeometryDash").First();
                    process.InitializeMemory().Inject(openDLLdialog.FileName);
                    MessageBox.Show("Injected!", "DLL Injector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
            }
            else
            {
                MessageBox.Show("We cant inject dll, because gd is not running.", "Run GD lol", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    public static class EnumerableExtensions
    {
        private static Random random = new Random();
        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(random.Next(0, enumerable.Count()));
    }
}

