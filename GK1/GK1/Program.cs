﻿using System;
using System.Windows.Forms;

namespace GK1
{
    internal static class Program
    {
        //private static void Main()
        //{
        //    PolygonFiller.test();
        //}


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
