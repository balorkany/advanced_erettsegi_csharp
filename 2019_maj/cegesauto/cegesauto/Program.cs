using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;

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
            //Feladat04();
            
            
            
            //Console.WriteLine(adatok.Count);
        }

        private static void Feladat04()
        {
            throw new NotImplementedException();
        }

        static void Feladat03()
        {
            Console.Write("Nap: ");
            int NapBe = int.Parse(Console.ReadLine());

            List<Jegyzek> szurtAdatok = adatok.Where(a => a.Nap == NapBe).ToList();
            
            Console.WriteLine($"Forgalom a(z) {NapBe}. napon:");
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