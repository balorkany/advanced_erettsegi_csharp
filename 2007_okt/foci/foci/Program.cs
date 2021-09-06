using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace foci
{
    internal class Program
    {
        public static List<FociJegyzek> meccsek = new List<FociJegyzek>();
        public static string csapatFeladat4;
        
        
        public static void Main(string[] args)
        {
            // Feladat 1
            Feladat01();
            
            //Feladat 2
            Console.WriteLine("Feladat 2.");
            Feladat02();
            
            //Felladat 3
            Console.WriteLine("Feladat 3");
            Feladat03();
            
            //Feladat 4 
            Console.WriteLine("Csapat:");
            csapatFeladat4 = Console.ReadLine();
            
            //Feladat 5
            Console.WriteLine("Feladat 5");
            Feladat05();
            
            // Feladat 6
            Console.WriteLine("Feladat 6");
            //Feladat06_NoLINQ();
            Feladat06_WithLINQ();
            
            // Feladat 7
            Feladat07();
        }

        private static void Feladat07()
        {
            List<string> eredmenyek = new List<string>(); // Ebbe jegyezzük fel a különböző eredményeket
            List<int> hanyatTalaltunk = new List<int>(); // Ebbe, hogy a különböző eredményekből hány darabot találtunk
            // Az index köti össze őket
            
            for (int i = 0; i < meccsek.Count; i++)
            {
                // A különböző eredményeket '4-2' formájú kulccsá alakítjuk 
                FociJegyzek aktMeccs = meccsek[i];
                string kulcs = "";
                if (aktMeccs.hazaiGolok > aktMeccs.vendegGolok)
                {
                    kulcs = $"{aktMeccs.hazaiGolok}-{aktMeccs.vendegGolok}";
                }
                else
                {
                    kulcs = $"{aktMeccs.vendegGolok}-{aktMeccs.hazaiGolok}";
                }
                
                if (!eredmenyek.Contains(kulcs)) 
                {
                    // Ha egy adott eredményt még nem láttunk, létrehozzuk és feljegyezzük, hogy eddig 1-et láttunk 
                    eredmenyek.Add(kulcs);
                    hanyatTalaltunk.Add(1);
                }
                else
                {
                    // Ha már láttuk, megkeressük az indexét és ennek segítségével a találatok számát egyel növeljük
                    int kulcsIndex = eredmenyek.IndexOf(kulcs);
                    hanyatTalaltunk[kulcsIndex]++;
                }
            }
            
            // Végül kiírjuk
            StreamWriter kiFile = new StreamWriter("stat.txt");

            for (int i = 0; i < eredmenyek.Count; i++)
            {
                kiFile.WriteLine("{0}: {1} darab", eredmenyek[i], hanyatTalaltunk[i]);
            }
            
            kiFile.Close();
        }
        

        private static void Feladat06_NoLINQ()
        {
            // Ötlet:
            // - Rendezzük a meccseket a forduló szerinti növekvő sorrendbe
            // - Lépegetünk a listán, amíg megtaláljuk az első meccset, ahol a kiválasztott csapat otthonában
            // játszottak és kikapott. (while-ban megvalósítva: addig lépkedünk, amíg nem találunk megfelelőt vagy
            // elfogy a lista)
            
            List<FociJegyzek> rendezettMeccsek = meccsek.OrderBy(m => m.fordulo).ToList();
            
            int ind = 0;
            while (ind < rendezettMeccsek.Count && (rendezettMeccsek[ind].hazai != csapatFeladat4 || rendezettMeccsek[ind].hazaiGolok > rendezettMeccsek[ind].vendegGolok))
            {
              ind++;
            }

            if (ind == rendezettMeccsek.Count)
            {
              Console.WriteLine("veretlen");
            }
            else
            {
              Console.WriteLine("{0} {1}", rendezettMeccsek[ind].fordulo, rendezettMeccsek[ind].vendeg);                  
            }
        }
        
        private static void Feladat06_WithLINQ()
        {
            // Ötlet: 
            // - Kiválogatjuk azokat a meccseket, ahol a kiválasztott csapat otthonában játszottak és a csapat vesztett (Where)
            // - Forduló szerint rendezzük (OrderBy)
            
            List<FociJegyzek> kikapottAdatok = meccsek.Where(m => 
                    m.hazai == csapatFeladat4 && m.hazaiGolok < m.vendegGolok)
                .OrderBy(m => m.fordulo).ToList();

            if (kikapottAdatok.Count > 0)
            {
                FociJegyzek elsoKikapasOtthon = kikapottAdatok[0];
                Console.WriteLine($"fordulo: {elsoKikapasOtthon.fordulo}, {elsoKikapasOtthon.vendeg}");
            }
            else
            {
                Console.WriteLine("A csapat otthon még nem kapoot ki");
            }

        }

        private static void Feladat05()
        {
            List<FociJegyzek> csapatHazaiAatok = meccsek.Where(m => m.hazai == csapatFeladat4).ToList();
            List<FociJegyzek> csapatVendegAdatok = meccsek.Where(m => m.vendeg == csapatFeladat4).ToList();

            int kapott = 0, lott = 0;

            foreach (var meccs in csapatHazaiAatok)
            {
                kapott += meccs.vendegGolok;
                lott += meccs.hazaiGolok;
            }
            
            foreach (var meccs in csapatVendegAdatok)
            {
                lott += meccs.vendegGolok;
                kapott += meccs.hazaiGolok;
            }
            
            Console.WriteLine($"lőtt: {lott}, kapott: {kapott}");
        }

        private static void Feladat03()
        {
            List<FociJegyzek> forditottAdatok = meccsek.Where(m => (
                                                                       m.vendegGolok > m.hazaiGolok &&
                                                                       m.vendegGolokFelido < m.hazaiGolokFelido) ||
                                                                   (m.vendegGolok > m.hazaiGolok &&
                                                                    m.vendegGolokFelido < m.hazaiGolokFelido)
            ).ToList();

            foreach (var meccs in forditottAdatok)
            {
                if (meccs.hazaiGolok > meccs.vendegGolok)
                {
                    Console.WriteLine($"{meccs.fordulo}: {meccs.hazai}");
                }
                else
                {
                    Console.WriteLine($"{meccs.fordulo}: {meccs.vendeg}");
                }
            }
        }

        private static void Feladat02()
        {
            Console.WriteLine("Forduló:");

            int fordBe = int.Parse(Console.ReadLine());

            List<FociJegyzek> forduloAdatok = meccsek.Where(m => m.fordulo == fordBe).ToList();

            foreach (var meccs in forduloAdatok)
            {
                Console.WriteLine($"{meccs.hazai}-{meccs.vendeg}: {meccs.hazaiGolok}-{meccs.vendegGolok} ({meccs.hazaiGolokFelido}-{meccs.vendegGolokFelido})");
            }
        }

        static void Feladat01()
        {
            string[] fileIn = File.ReadAllLines("meccs.txt");

            Boolean firstLine = true;
            
            foreach (string line in fileIn)
            {
                if (firstLine)
                {
                    firstLine = false;
                }
                else
                {
                    string[] splittedLine = line.Split(' ');
                    FociJegyzek j = new FociJegyzek();

                    j.fordulo = int.Parse(splittedLine[0]);
                    j.hazaiGolok = int.Parse(splittedLine[1]);
                    j.vendegGolok = int.Parse(splittedLine[2]);
                    j.hazaiGolokFelido = int.Parse(splittedLine[3]);
                    j.vendegGolokFelido = int.Parse(splittedLine[4]);

                    j.hazai = splittedLine[5];
                    j.vendeg = splittedLine[6];
                    
                    meccsek.Add(j);
                }
            }
        }
        
    }

    class FociJegyzek
    {
        public int fordulo { get; set; }
        public string hazai { get; set; }
        public string vendeg { get; set; }
        
        public int hazaiGolok { get; set; }
        public int hazaiGolokFelido { get; set; }
        public int vendegGolok { get; set; }
        public int vendegGolokFelido { get; set; }
    }
}