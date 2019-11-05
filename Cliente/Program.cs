using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            int debe, haber, importe;
            String pal;

            NamedPipeClientStream pipa = new NamedPipeClientStream(".","pipazo", PipeDirection.InOut);
            pipa.Connect();
            StreamWriter sw = new StreamWriter(pipa);
            sw.AutoFlush = true;
            StreamReader sr = new StreamReader(pipa);

            debe=int.Parse(sr.ReadLine());
            haber = int.Parse(sr.ReadLine());
            pal = sr.ReadLine();
            while (pal.CompareTo("fin")!=0)
            {
                importe = int.Parse(sr.ReadLine());
                if (pal.CompareTo("ingreso")==0)
                {
                    haber += importe;
                }
                else
                {
                    debe += importe;
                }
                pal = sr.ReadLine();
            }
            sw.WriteLine(debe);
            sw.WriteLine(haber);
            pipa.WaitForPipeDrain();
            pipa.Close();
        }
    }
}
