﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading;

namespace UIH.XA.MiniBoot
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread appThread = new Thread(new ThreadStart(() =>
            {
                MiniApp app = new MiniApp();
                app.Run();
            }));
            appThread.SetApartmentState(ApartmentState.STA);
            appThread.Start();

        }
    }
}
