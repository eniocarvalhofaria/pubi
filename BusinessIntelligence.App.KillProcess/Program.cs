using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace BusinessIntelligence.App.KillProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o nome do processo.");
            Process[] a = Process.GetProcessesByName(Console.ReadLine());
            Console.WriteLine("Encontrados " + a.Length.ToString());
            foreach (var p in a)
            {
                Console.WriteLine("Matando processo " + p.Id.ToString());
                p.Kill();
            }
            Console.WriteLine("Fim do processo.");
            Console.ReadLine();
        }
    }
}
