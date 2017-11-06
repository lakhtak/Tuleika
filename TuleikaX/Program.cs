﻿using System;

namespace TuleikaX
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TheGame game = new TheGame())
            {
                game.Run();
            }
        }
    }
#endif
}

