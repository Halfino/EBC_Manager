using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EBC_Manager
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new void Run()
        {
            try
            {
                //imports data from saved file if exists.
                Importer import = new Importer();
                import.parseCSV("export.csv");
            }
            catch
            {
                Console.Write("nic k importu!");
            }

            base.Run();
        }
    }
}
