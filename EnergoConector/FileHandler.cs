using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace EnergoConector
{
     static class FileHandler
    {

		private static string defaultFilePath="list_ble.csv";

		public static string DefaultFilePath
        {
			get { return defaultFilePath; }
			set {
				
				defaultFilePath = value; }
		}






	}
}
