using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeikoObfuscator
{
	class Logger
	{

		// Logger class Added for debugbuilds 
		// To get some better insight of the program

		public static void logger(string lines)
		{
			StreamWriter file = new StreamWriter("outputLog.log", true);
			string Stamp = string.Format("{0}:", DateTime.Now.ToUniversalTime());

			file.WriteLine("{0}{1}", Stamp, lines);

			file.Close();
		}
	}
}
