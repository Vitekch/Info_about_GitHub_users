using System;
using Gtk;

namespace MP_Laba_5
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Init();

            var app = new Application("App", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
           
           

            var win = new InputForm();
            
            app.AddWindow(win);

            win.Show();
            Application.Run();
                

        }
    }
}
