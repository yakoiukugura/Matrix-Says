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
        }


        Label[] tiles;
        Label[] map;
        // Declararea variabilelor pentru dimensiunea si nivelul jocului
        int n, level = 0;

        // Evenimentul declansat la apasarea butonului "Start"
        private void start_button_Click(object sender, EventArgs e)
        {
            // Dezactivam si ascundem meniul
            menu.Enabled = false;
            menu.Visible = false;

            // Dezactivam si ascundem titlul jocului
            title.Enabled = false;
            title.Visible = false;

            // Dezactivam si ascundem butonul de start
            start_button.Enabled = false;
            start_button.Visible = false;

            // Dezactivam si ascundem butonul de iesire
            quit_button.Enabled = false;
            quit_button.Visible = false;

            // Initializam vectorul de etichete
            tiles = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8,
                              label9, label10, label11, label12, label13, label14, label15, label16};

            // Incrementam nivelul jocului
            level++;

            // Pornim jocul
            startGame();
        }

        // Evenimentul declansat la apasarea butonului "Quit"
        private void quit_button_Click(object sender, EventArgs e)
        {
            // Daca utilizatorul confirma ca doreste sa inchida aplicatia, o inchidem
            if (MessageBox.Show("Are you sure you want to quit?", "Quit Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        // Functia pentru pornirea jocului
        private void startGame()
        {
            // Setam instructiunile pentru joc
            instructions.Text = "Remember the tiles!";

            // Initializam numarul de elemente din harta jocului
            n = 0;
            map = new Label[level + 1];

            Random rand = new Random();

            // Resetam culorile de pe etichete la alb
            for (int i = 0; i < tiles.Length; i++)
                tiles[i].BackColor = Color.White;

            // Generam harta jocului pentru nivelul curent
            for (int i = 1; i <= level; i++)
            {
                int nr = rand.Next(0, 16);

                // Verificam daca o eticheta a mai fost aleasa inainte si o alegem pe alta in cazul acesta
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

        // verifică dacă jucătorul a apăsat corect pe piesă și actualizează vectorul cu combinația de piese
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
                gameOver(false);
            }
        }

        // sterge elementul de pe pozitia pos din vector
        // daca nu mai sunt elemente in vector atunci jocul a fost castigat
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
                    gameOver(true);
                }
            }
        }

        private void gameOver(bool win)
        {
            // Reactiveaza meniul, titlul si butoanele
            menu.Enabled = true;
            menu.Visible = true;

            if (win)
                title.Text = "You Win!";
            else
                title.Text = "Bad answer!";
            title.Enabled = true;
            title.Visible = true;

            start_button.Enabled = true;
            start_button.Visible = true;
            start_button.Text = "Play Again";

            quit_button.Enabled = true;
            quit_button.Visible = true;
        }

        // functia wait() face o scurta pauza in program
        // https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
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
    }
}
