using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;


namespace Repaso
{
    class Program
    {
        static void Main(string[] args)
        {
            String pal;
            int importe;
            int resdebe, reshaber;
            int debe, haber;
            NamedPipeServerStream pipa = new NamedPipeServerStream("pipazo", PipeDirection.InOut);
            pipa.WaitForConnection();
            StreamWriter sw = new StreamWriter(pipa);
            sw.AutoFlush = true;
            StreamReader sr = new StreamReader(pipa);
            
            Console.WriteLine("Debe: ");
            debe = int.Parse(Console.ReadLine());
            Console.WriteLine("Haber: ");
            haber = int.Parse(Console.ReadLine());
            sw.WriteLine(debe);
            sw.WriteLine(haber);
            
            pal = pideyval();

            while (pal.CompareTo("fin") != 0)
            {
                Console.WriteLine("Importe: ");
                importe = int.Parse(Console.ReadLine());
                sw.WriteLine(pal);
                sw.WriteLine(importe);
                pal = pideyval();
            }
            sw.WriteLine("fin");
            resdebe = int.Parse(sr.ReadLine());
            reshaber = int.Parse(sr.ReadLine());
            Console.WriteLine("Saldo = {0}", reshaber - resdebe);



            Console.ReadLine();
            pipa.Close();
        }

        private static string pideyval()
        {
            String pal;
            do
            {
                Console.WriteLine("Escribe: ingreso, reintegro o fin");
                pal = Console.ReadLine().ToLower();
            } while (pal.CompareTo("ingreso") != 0 && pal.CompareTo("reintegro") != 0 && pal.CompareTo("fin") != 0);
            return pal;
        }
    }
}
