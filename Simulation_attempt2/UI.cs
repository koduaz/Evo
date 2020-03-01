using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Simulation_attempt2
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
            RunADay.Enabled = false;
            Day.Enabled = false;
            Grid.Enabled = false;
            Status.Enabled = false;
            BulbsDied.Enabled = false;
            BulbsWith1.Enabled = false;
            BulbsWith2.Enabled = false;
            EnergyAtStart.Enabled = false;
            EnergyLeft.Enabled = false;
            FoodAtStart.Enabled = false;
            FoodLeft.Enabled = false;
            BulbsAtStart.Enabled = false;
            BulbsSurvived.Enabled = false;
            FieldSize.Focus();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if
            (
                   int.TryParse(this.Food.Text, out int r1) == true
                && int.TryParse(this.FieldSize.Text, out int r2) == true
                && int.TryParse(this.Energy.Text, out int r3) == true
                && int.TryParse(this.InitialPopulation.Text, out int r4) == true
            )
            {
                if
                (
                   int.Parse(Food.Text) > 0
                && int.Parse(FieldSize.Text) > 0
                && int.Parse(Energy.Text) > 0
                && int.Parse(InitialPopulation.Text) > 0
                )
                {
                    if
                    (
                       int.Parse(Food.Text) < (int.Parse(FieldSize.Text) - 1) * (int.Parse(FieldSize.Text) - 1)
                    && int.Parse(FieldSize.Text) <= 20
                    && int.Parse(Energy.Text) <= 100
                    && int.Parse(InitialPopulation.Text) < (int.Parse(FieldSize.Text) - 2) * 4
                    )
                    {
                        await init();

                    }
                    else
                    {
                        MessageBox.Show("Input error\nDude\nI can't create a simulation with those numbers\nThat's literally uncreatable\nMake them more realistic", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Input error\nDude\nPut something not negative", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Input error\nDude\nPut an integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task init()
        {
            Status.Text = "initializing...";

            button1.Enabled = false;

            Food.Enabled = false;
            FieldSize.Enabled = false;
            Energy.Enabled = false;
            InitialPopulation.Enabled = false;

            await Task.Delay(3000);

            Day.Text = "0";
            Grid.Text = $"{FieldSize.Text} x {FieldSize.Text}";
            FoodAtStart.Text = Food.Text;
            BulbsSurvived.Text = InitialPopulation.Text;
            BulbsDied.Text = "-";
            BulbsAtStart.Text = "-";
            EnergyAtStart.Text = Energy.Text;
            BulbsWith1.Text = "-";
            BulbsWith2.Text = "-";
            EnergyLeft.Text = "-";
            FoodLeft.Text = "-";
            

            RunADay.Enabled = true;
            Status.Text = "initialized";
        }

        private async void RunADay_Click(object sender, EventArgs e)
        {
            BulbsAtStart.Text = BulbsSurvived.Text;
            Status.Text = "day has started";
            Random random = new Random();
            Day.Text = (int.Parse(Day.Text) + 1).ToString();
            EnergyLeft.Text = EnergyAtStart.Text;
            Bulb[] bulb = new Bulb[int.Parse(this.BulbsAtStart.Text)];



            //create an array
            string[,] grid = new string[int.Parse(this.FieldSize.Text), int.Parse(this.FieldSize.Text)];
            for (int y = 0; y < int.Parse(FieldSize.Text); y++)
            {
                for (int x = 0; x < int.Parse(FieldSize.Text); x++)
                {
                    grid[x, y] = " ";
                }
            }
           

            //spawn bulbs
            for (int b = 0; b < int.Parse(BulbsAtStart.Text); b++)
            {
                await spawnBulb(grid, bulb, b);
            }


            //show(bulb);


            //spawn food
            for (int i = 0; i < int.Parse(FoodAtStart.Text); i++)
            {
                spawnFood(random, grid);
            }


            //run their moves
            for (int i = int.Parse(EnergyAtStart.Text); i > 0; i--)
            {
                for (int b = 0; b < int.Parse(BulbsAtStart.Text); b++)
                {
                    bulb[b].move();
                }
                await Task.Delay(3000);
                EnergyLeft.Text = (i - 1).ToString();        
            }

            show(bulb);
            show(grid);


            {
                //reproduction();
            }
            Status.Text = "day is over..";
        }

        private static async Task spawnBulb(string[,] grid, Bulb[] bulb, int b)
        {
            bulb[b] = new Bulb(-1, -1);
            bulb[b].spawn(grid.GetUpperBound(0));
            //Console.WriteLine($"bulb {b}: {bulb[b].x},{bulb[b].y}");
            await Task.Delay(10);
            grid[bulb[b].x, bulb[b].y] = "b";
        }

        private void spawnFood(Random random, string[,] grid)
        {
            bool isPlaced = false;
            do
            {
                int checkX = random.Next(1, int.Parse(FieldSize.Text) - 1);
                int checkY = random.Next(1, int.Parse(FieldSize.Text) - 1);
                if (grid[checkX, checkY] != "f")
                {
                    grid[checkX, checkY] = "f";
                    isPlaced = true;
                }

            } while (isPlaced != true);
        }

        //для отладки
        public void show(string[,] grid)
        {
            
            for (int y = 0; y < int.Parse(FieldSize.Text); y++)
            {
                for (int x = 0; x < int.Parse(FieldSize.Text); x++)
                {
                    Console.Write($"[{grid[x, y]}]");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void show(Bulb[] bulb)
        {
            for (int b = 0; b < bulb.Length; b++)
            {
                Console.WriteLine($"bulb {b}: {bulb[b].x},{bulb[b].y}");
            }
        }
    }
}

