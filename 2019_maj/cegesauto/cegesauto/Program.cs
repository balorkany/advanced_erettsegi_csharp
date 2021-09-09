using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;

namespace cegesauto
{
    internal class Program
    {
        public static List<Jegyzek> adatok = new List<Jegyzek>();
        
        public static void Main(string[] args)
        {
            // 1. Feladat
            Feladat01();
            
            // 2. Feladat
            Console.WriteLine("2. Feladat");
            Feladat02();
            
            // 3. Feladat
            Console.WriteLine("3. Feladat");
            Feladat03();
            
            // 4. Feladat
            Console.WriteLine("4. Feladat");
            Feladat04();
            
            // 5. Feladat
            Console.WriteLine("5. Feladat");
            Feladat05();
            
            // 6. Feladat
            Console.WriteLine("6. Feladat");
            Feladat06();
            
            // 7. Feladat
            Console.WriteLine("7. Feladat");
            Feladat07();
            
        }

        private static void Feladat07()
        { 
            Console.Write("Rendszám: ");
            string rendszamBe = Console.ReadLine();
            
            List<Jegyzek> szurtAdatok = adatok.Where(a => a.Rendszam == rendszamBe).ToList();

            StreamWriter sw = new StreamWriter($"{rendszamBe}_menetlevel.txt");

            string sor = "";
            int i;
            for (i = 0; i < szurtAdatok.Count; i++)
            {
                var adat = szurtAdatok[i];
                if (i % 2 == 0)
                {
                    sor = $"{adat.SzemAz} \t {adat.Nap}. {adat.Ora}:{adat.Perc} \t {adat.Km} km \t";
                }
                else
                {
                    sor += $"{adat.Nap}. {adat.Ora}:{adat.Perc} \t {adat.Km} km";
                    sw.WriteLine(sor);
                }

            }

            if (i % 2 == 1)
            {
                sw.WriteLine(sor);
            }
            
            sw.Close();
            
            Console.WriteLine("Menetlevél kész.");
        }

        private static void Feladat06()
        {
            var groupingBySzemAz = adatok.GroupBy(a => a.SzemAz);

            int absMax = 0;
            string maxSzem = "";
            foreach (var szemGroup in groupingBySzemAz)
            {
                int szemMax = 0;
                int kezdo = 0, vegzo = 0;
                int megtettTav = 0;
                for (int i = 0; i < szemGroup.Count(); i++)
                {
                    var adat = szemGroup.ToList()[i];
                    if (i % 2 == 0)
                    {
                        kezdo = adat.Km;
                    }
                    else
                    {
                        vegzo = adat.Km;
                        megtettTav = vegzo - kezdo;
                        if (megtettTav > szemMax)
                        {
                            szemMax = megtettTav;
                        }
                        
                    }
                }

                if (szemMax > absMax)
                {
                    absMax = szemMax;
                    maxSzem = szemGroup.Key;
                }
                
            }
            
            Console.WriteLine($"Leghosszabb út: {absMax} km, személy: {maxSzem}");
            
        }

        static void Feladat05()
        {
            var groupingOfAutok = adatok.GroupBy(a => a.Rendszam);

            foreach (var group in groupingOfAutok)
            {
                int tav = group.Last().Km - group.First().Km;
                Console.WriteLine($"{group.Key} {tav} km");
            }
        }

        static void Feladat04()
        {
            int numOfKintAutok = adatok.GroupBy(a => a.Rendszam).Count(g =>g.Last().Ki);

            Console.WriteLine($"A hónap végén {numOfKintAutok} autót nem hoztak vissza.");
        }

        static void Feladat03()
        {
            Console.Write("Nap: ");
            int napBe = int.Parse(Console.ReadLine());

            List<Jegyzek> szurtAdatok = adatok.Where(a => a.Nap == napBe).ToList();
            
            Console.WriteLine($"Forgalom a(z) {napBe}. napon:");
            foreach (var sza in szurtAdatok)
            {
                string kibe = sza.Ki ? "ki" : "be";
                Console.WriteLine($"{sza.Ora}:{sza.Perc} {sza.Rendszam} {sza.SzemAz} {kibe}" );
            }
        }

        static void Feladat02()
        {
            Jegyzek utolsoAuto = adatok.Where(a => a.Ki).Last();
            Console.WriteLine($"{utolsoAuto.Nap}. nap rendszám: {utolsoAuto.Rendszam}");
        }

        static void Feladat01()
        {
            string[] fileIn = File.ReadAllLines("autok.txt");

            foreach (string line in fileIn)
            {
                string[] splittedLine = line.Split(' ');
                string[] oraPerc = splittedLine[1].Split(':');
                Jegyzek j = new Jegyzek();

                j.Nap = int.Parse(splittedLine[0]);
                j.Ora = int.Parse(oraPerc[0]);
                j.Perc = int.Parse(oraPerc[1]);
                j.Rendszam = splittedLine[2];
                j.SzemAz = splittedLine[3];
                j.Km = int.Parse(splittedLine[4]);
                j.Ki = splittedLine[5] == "0";
                
                adatok.Add(j);
            }
        }
    }

    class Jegyzek
    {
        public int Nap {get; set; }
        public int Ora { get; set; }
        public int Perc { get; set; }
        public string Rendszam { get; set; }
        public string SzemAz { get; set; }
        public int Km { get; set; }
        public bool Ki { get; set; }
    }
}