﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine ge = Runner.SetupGame();
            Runner.RunGame(ge);
        }
    }
}
