using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Drawing.Imaging;

namespace Digital_Assistant
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();
        System.Media.SoundPlayer music = new System.Media.SoundPlayer();

        public Form1()
        {
            InitializeComponent();

            Choices choices = new Choices();
            string[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammer.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);

            speech.SelectVoiceByHints(VoiceGender.Female);
        }

        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;

            if (result == "Hello")
            {
                result = "Hello, I am Zain how can I help you?";
            }

            if (result == "Hey Zain What time is it now")
            {
                result = "It is  Currently " + DateTime.Now.ToLongTimeString();
            }

            if (result == "Hey Zain Open Google")
            {
                System.Diagnostics.Process.Start("https://www.google.com/");
                result = "Opening Google";
            }

            if (result == "Hey Zain Open Gmail")
            {
                System.Diagnostics.Process.Start("https://mail.google.com/");
                result = "Opening G-Mail";
            }

            if (result == "Hey Zain Shut Down")
            {
                Application.Exit();
            }

           

            speech.SpeakAsync(result);
            txtlabel.Text = result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
