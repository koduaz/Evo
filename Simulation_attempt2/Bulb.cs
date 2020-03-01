using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_attempt2
{
    public class Bulb
    {
        public int x, y;
        public int food;
        public int energy;
        public bool isDead;
        public Bulb(int x, int y)
        {
            this.x = x;
            this.y = y;         
        }

        public void move()
        {
            //if nrg != 0

            //if not stuck

            //

            //nrg --

        }
        public void spawn(int s)
        {
            Random random = new Random();
            //уебищный способ
            int pos;
            isDead = false;
            energy = 20;
            bool isPlaced = false;
            do
            {
                int side = random.Next(0, 4);
                pos = random.Next(1, s);
                switch (side)
                {
                    case 0:
                        this.y = 0;
                        this.x = pos;
                        break;
                    case 1:
                        this.x = s;
                        this.y = pos;
                        break;
                    case 2:
                        this.x = pos;
                        this.y = s;
                        break;
                    case 3:
                        this.x = 0;
                        this.y = pos;
                        break;              
                }
                //grid[x, y] = "b";
                isPlaced = true;
                

            } while (isPlaced != true);
            //неуебищный способ
            /*делать я конечно же его не буду*/
        }
    }
}
