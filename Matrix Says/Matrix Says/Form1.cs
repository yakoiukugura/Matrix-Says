using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrix_Says
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tiles[0] = label1;
            tiles[1] = label2;
            tiles[2] = label3;
            tiles[3] = label4;

            tiles[4] = label5;
            tiles[5] = label6;
            tiles[6] = label7;
            tiles[7] = label8;

            tiles[8] = label9;
            tiles[9] = label10;
            tiles[10] = label11;
            tiles[11] = label12;

            tiles[12] = label13;
            tiles[13] = label14;
            tiles[14] = label15;
            tiles[15] = label16;
            startGame();
        }

        Label[] tiles = new Label[16];
        Label[] map;
        int n, level = 0;

        private void deleteTile(int pos)
        {
            for (int i = pos; i < n - 1; i++)
                map[i] = map[i + 1];
            n--;
            if (n == 0)
            {
                if (level <= 7)
                    startGame();
                else
                {
                    MessageBox.Show("You Win!");
                    this.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.Tomato;
            bool ok = false;
            int i;
            for (i = 0; i < n && !ok; i++)
                if (map[i] == label)
                    ok = true;
            if (ok == true)
                deleteTile(i - 1);
            else
            {
                MessageBox.Show("Bad answer! Game Over");
                this.Close();
            }
        }

        public void wait(int milliseconds)
        {
            this.Enabled = false;
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                this.Enabled = true;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void startGame()
        {
            level++;
            instructions.Text = "Remember the tiles!";
            n = 0;
            map = new Label[level + 1];
            Random rand = new Random();
            for (int i = 0; i < tiles.Length; i++)
                tiles[i].BackColor = Color.White;
            for (int i = 1; i <= level; i++)
            {
                int nr = rand.Next(0, 16);
                while (tiles[nr].BackColor == Color.Tomato)
                    nr = rand.Next(0, 16);
                tiles[nr].BackColor = Color.Tomato;
                map[n++] = tiles[nr];
            }
            this.Show();
            wait(2000);
            instructions.Text = "Click the tiles that appeared.";
            for (int i = 0; i < tiles.Length; i++)
                tiles[i].BackColor = Color.White;
        }
    }
}
