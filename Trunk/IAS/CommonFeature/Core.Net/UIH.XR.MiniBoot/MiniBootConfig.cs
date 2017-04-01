/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XR.MiniBoot
{
    public class MiniBootConfig
    {
        public string ViewName { get; set; }

        public double WindowWidth { get; set; }

        public double WindowHeight { get; set; }

        public List<string> AssemblyList { get; set; }

        public MiniBootConfig()
        {
            AssemblyList = new List<string>();
        }

        public bool SetConfig(string[] args)
        {
            if (args.Length < 4)
                return false;

            try
            {
                ViewName = args[0];
                WindowWidth = double.Parse(args[1]);
                WindowHeight = double.Parse(args[2]);
                for (int i = 3; i < args.Length; i++)
                {
                    AssemblyList.Add(args[i]);
                }
                return true;
            }
            catch { }

            return false;
        }
    }
}
