using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;

namespace metjelentes
{
    
    internal class Program
    {
        public static List<Tavirat> taviratok = new List<Tavirat>();
        
        public static void Main(string[] args)
        {
            //Feladat 1
            Feladat01();
            
            //Feladat 2
            Console.WriteLine("2. Feladat");
            Feladat02();
            
            //Feladat 3
            Console.WriteLine("3. Feladat");
            Feladat03();
       
            //Feladat 4
            Console.WriteLine("4. Feladat");
            Feladat04();
            
            //Feladat 5
            Console.WriteLine("5. Feladat");
            Feladat05();
            
            //Feladat 6
            Console.WriteLine("6. Feladat");
            Feladat06();
        }

        private static void Feladat06()
        {
            var grouppedByTelepules = taviratok.GroupBy(t => t.telepules);

            foreach (var group in grouppedByTelepules)
            {
                string telepules = group.Key;
                List<Tavirat> telepulesList = group.ToList();
                StreamWriter fileKi = new StreamWriter($"{telepules}.txt");

                fileKi.WriteLine(telepules);
                foreach (var t in telepulesList)
                {
                    string szelerossegKettoskeresztek = "".PadLeft(t.szelerosseg, '#');
                    fileKi.WriteLine($"{t.ido} {szelerossegKettoskeresztek}");
                }
                
                fileKi.Close();
            }
            
            Console.WriteLine("A fájlok elkészültek.");
        }
        
        private static void Feladat05()
        {
            Console.WriteLine("ToDo..");
        }

        private static void Feladat04()
        {
            List<Tavirat> szelcsendes = taviratok.Where(t => t.szelerosseg == 0).ToList();

            if (szelcsendes.Count > 0)
            {
                foreach (var t in szelcsendes)
                {
                    Console.WriteLine($"{t.telepules} {t.ido}");
                
                }    
            }
            else
            {
                Console.WriteLine("Nem volt szélcsend a mérések idején.");
            }
        }

        private static void Feladat03()
        {
            Tavirat legalacsonyabbHom = taviratok.OrderBy(t => t.homerseklet).First();
            Tavirat legmagasabbHom = taviratok.OrderBy(t => t.homerseklet).Last();
            
            Console.WriteLine($"A legalacsonyabb hőmérséklet: {legalacsonyabbHom.telepules} {legalacsonyabbHom.ido} {legalacsonyabbHom.homerseklet} fok.");
            Console.WriteLine($"A legmagasabb hőmérséklet: {legmagasabbHom.telepules} {legmagasabbHom.ido} {legmagasabbHom.homerseklet} fok.");
        }

        private static void Feladat02()
        {
            Console.Write("Adja meg a település kódját! Település: ");
            string telepulesBe = Console.ReadLine();

            string utolsoIdo = taviratok.Where(t => t.telepules == telepulesBe).Last().ido;
            
            Console.WriteLine($"Az utolsó mérési adat a megadott telepükésről {utolsoIdo}-kor érkezett.");
        }

        private static void Feladat01()
        {
            string[] inputFile = File.ReadAllLines("tavirathu13.txt");

            foreach (var line in inputFile)
            {
                string[] splittedLine = line.Split(' ');
                Tavirat tavirat = new Tavirat();

                tavirat.telepules = splittedLine[0];
                tavirat.ido = splittedLine[1].Insert(2, ":");
                //vagy másként: tavirat.ido = $"{splittedLine[1].Substring(0, 2)}:{splittedLine[1].Substring(2,2)}";
                tavirat.homerseklet = Int32.Parse(splittedLine[3]);
                tavirat.szelerosseg = Int32.Parse(splittedLine[2].Substring(3,2));
                
                taviratok.Add(tavirat);
            }
        }
    }

    public class Tavirat
    {
        public string telepules;
        public string ido; 
        public int szelerosseg;
        public int homerseklet;
    }
    
}