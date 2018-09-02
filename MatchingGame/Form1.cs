using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        private SoundPlayer Music;
        private SoundPlayer Music1;
        private SoundPlayer Music2;
        Label firstClicked = null;
        Label secondClicked = null;
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!","!","N","N","Y","Y","K","K","b","b","v","v","^","^","z","z","o","o","@","@"
        };
        int timeLeft;
        int minutes;
        int seconds;
        
        private void AssignIconsToSquare()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label; 
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }

        }   
        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquare();

            Music = new SoundPlayer("coin-04.wav");
            Music1 = new SoundPlayer("beep-05.wav");
            Music2 = new SoundPlayer("beep-07.wav");

            timeLeft = 0;
            label17.Text = "0 : 00 minutes !!";
        }

        private void label_Click(object sender, EventArgs e)
        {
            timer2.Start();
            if (timer1.Enabled == true)
                return;

            
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.DarkGreen)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.DarkGreen;

                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.DarkGreen;
                
                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    Music.Play();
                    return;
                }
                
                Music1.Play();
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            Music2.Play();
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                    

                    
                }
            }

            timer2.Stop();
            MessageBox.Show("You matched all the icons!", "Congratulations!");
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft+1;
            minutes = timeLeft / 60;
            seconds = timeLeft - minutes * 60;
            if (seconds <10)
            {
                label17.Text = minutes + " : 0" + seconds + " minutes !!";
            }
            else
                label17.Text = minutes + " : " + seconds + " minutes !!";


        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
