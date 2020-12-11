using Memory;
using OsuSongPathSearcher;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace osu__Space_Saver
{
    public partial class Form1 : Form
    {
        private OsuSongPathSearcher.Class1 class1 = new Class1();
        public string TempPath;
        public int id;
        private Memory.Mem m = new Mem();
        private Process proc = new Process();
        private ProcessStartInfo procS = new ProcessStartInfo();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            class1.GetOsuSongPath();
            if (class1.OsuSongPath == "")
            {
                MessageBox.Show(class1.Status, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                textBox1.Text = class1.OsuSongPath;
                MessageBox.Show(class1.Status + Environment.NewLine + class1.OsuSongPath, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } // Tries to find osu! song path

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "";
                TempPath = textBox1.Text;
                if (checkBox1.Checked == true) { file = file + "del /s *.avi" + Environment.NewLine; }
                if (checkBox2.Checked == true) { file = file + "del /s *.mp4" + Environment.NewLine; }
                if (checkBox3.Checked == true) { file = file + "del /s *.flv" + Environment.NewLine; }
                if (checkBox4.Checked == true) { file = file + "del /s *.wav" + Environment.NewLine; }
                if (checkBox5.Checked == true) { file = file + "del /s *.png" + Environment.NewLine; }
                if (checkBox6.Checked == true) { file = file + "del /s *.jpg" + Environment.NewLine; }
                if (checkBox7.Checked == true) { file = file + "del /s *.mp3" + Environment.NewLine; }

                System.IO.File.WriteAllText(TempPath + "\\run.bat", file);

                Exec();
            }
            catch (Exception ex) { MessageBox.Show(ex + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.Source, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        } // Create batch file

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void Exec()
        {
            try
            {
                string s = "run.bat";
                procS.CreateNoWindow = false;
                procS.FileName = s;
                procS.UseShellExecute = true;
                procS.WorkingDirectory = TempPath;
                Process proc = Process.Start(procS);

                label6.Text = "Status: Running";
            }
            catch (Exception ex) { MessageBox.Show(ex + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.Source, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        } // Tries to execute the batch file

        private void StatusCheck()
        {
            int procID = m.GetProcIdFromName("cmd");
            if (procID > 0)
            {
                label6.Text = "Status: Running";
            }
            else
            {
                label6.Text = "Status: Not running";
            }

            int procID2 = m.GetProcIdFromName("osu!");
            if (procID2 > 0)
            {
                label7.Text = "osu!: Running";
            }
            else
            {
                label7.Text = "osu!: Not running";
            }
        } //Check Running Status

        private void timer1_Tick(object sender, EventArgs e)
        {
            StatusCheck();
        }
    }
}