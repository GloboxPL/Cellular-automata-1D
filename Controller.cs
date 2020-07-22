using System;
using System.Drawing;

namespace ZCA1
{
    class Controller
    {
        private int bc;
        private bool[] rule;
        private int iterations;
        private int dimSize;
        private bool[,] array; //true - black, false - white

        public Controller(int ruleNumber, int iterations, int dimSize, int bc)
        {
            this.iterations = iterations;
            this.dimSize = dimSize;
            this.bc = bc;
            CalculateRule(ruleNumber);
            array = new bool[dimSize, iterations];
            array[dimSize / 2, 0] = true;
        }

        private void CalculateRule(int ruleNumber)
        {
            string ruleNumberBinary = Convert.ToString(ruleNumber, 2);
            while (ruleNumberBinary.Length != 8)
            {
                ruleNumberBinary = '0' + ruleNumberBinary;
            }
            rule = new bool[8];
            for (int i = 0; i < ruleNumberBinary.Length; i++)
            {
                if (ruleNumberBinary[i] == '1')
                {
                    rule[i] = true;
                }
            }
        }

        private bool Transition(bool a, bool b, bool c)
        {
            //111
            if (a && b && c)
            {
                return rule[0];
            }
            //110
            if (a && b && !c)
            {
                return rule[1];
            }
            //101
            if (a && !b && c)
            {
                return rule[2];
            }
            //100
            if (a && !b && !c)
            {
                return rule[3];
            }
            //011
            if (!a && b && c)
            {
                return rule[4];
            }
            //010
            if (!a && b && !c)
            {
                return rule[5];
            }
            //001
            if (!a && !b && c)
            {
                return rule[6];
            }
            //000
            if (!a && !b && !c)
            {
                return rule[7];
            }
            return false;
        }

        private void Generate()
        {
            for (int i = 0; i < iterations - 1; i++)
            {
                switch (bc)
                {
                    case 1: //stale warunki brzegowe
                        array[0, i + 1] = Transition(false, array[0, i], array[1, i]);
                        array[dimSize - 1, i + 1] = Transition(array[dimSize - 2, i], array[dimSize - 1, i], false);
                        break;
                    case 2: //zawijane warunki brzegowe
                        array[0, i + 1] = Transition(array[dimSize - 1, i], array[0, i], array[1, i]);
                        array[dimSize - 1, i + 1] = Transition(array[dimSize - 2, i], array[dimSize - 1, i], array[0, i]);
                        break;
                    case 3: //odbijane warunki brzegowe
                        array[0, i + 1] = Transition(true, array[0, i], array[1, i]);
                        array[dimSize - 1, i + 1] = Transition(array[dimSize - 2, i], array[dimSize - 1, i], true);
                        break;
                }
                for (int j = 1; j < dimSize - 1; j++)
                {
                    array[j, i + 1] = Transition(array[j - 1, i], array[j, i], array[j + 1, i]);
                }
            }
        }

        public Bitmap GenerateBitmap()
        {
            Generate();
            Bitmap bitmap = new Bitmap(dimSize, iterations);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (array[i, j])
                    {
                        bitmap.SetPixel(i, j, Color.Green);
                    }
                }
            }
            return bitmap;
        }
    }
}
