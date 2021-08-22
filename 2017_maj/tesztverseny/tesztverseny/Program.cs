using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

namespace tesztverseny
{
    internal class Program
    {
        public static List<Valasz> valaszok = new List<Valasz>();
        public static string joValasz;

        public static string feladat3_azon;
        public static string feladat3_tipp;
        public static void Main(string[] args)
        {
            // 1. Feladat
            Console.WriteLine("1. Feladat: Az adatok beolvasása");
            Feladat01();
            
            
            
            // 2. Feladat
            Console.WriteLine($"2. Feladat: a vetélkedőn {valaszok.Count} versenyző indult.");
            
            // 3. Feladat
            Console.Write("3. Feladat: ");
            Feladat03();
            
            // 4. Feladat
            Console.WriteLine("4. Feladat:");
            Feladat04_v1();
            
            // 5. Feladat
            Console.Write("5. Feladat: ");
            Feladat05();
            
            // 6. Feladat
            Console.WriteLine("6. Feladat: A versenyzők pontszámának meghatározása");
            Feladat06();
            
            // 7. Feladat
            Console.WriteLine("7. Feladat: A verseny legjobbjai:");
            Feladat07();
            
        }

        private static int Pontszam(Valasz valasz, string joValasz)
        {
            int pontszam = 0;

            for (int i = 0; i < 5; i++)
            {
                if (valasz.tipp[i] == joValasz[i])
                {
                    pontszam += 3;
                }
            }
                
            for (int i = 5; i < 10; i++)
            {
                if (valasz.tipp[i] == joValasz[i])
                {
                    pontszam += 4;
                }
            }
                
            for (int i = 10; i < 13; i++)
            {
                if (valasz.tipp[i] == joValasz[i])
                {
                    pontszam += 5;
                }
            }
                
            if (valasz.tipp[13] == joValasz[13])
            {
                pontszam += 6;
            }

            return pontszam;
        }
        
        private static void Feladat07()
        {
            List<int> only_pontok = new List<int>();
            
            foreach (var v in valaszok)
            {
                int pont = Pontszam(v, joValasz);
                if (!only_pontok.Contains(pont))
                {
                    only_pontok.Add(pont);
                }
            }

            only_pontok.Sort();
            only_pontok.Reverse();
      
            for (int i = 0; i < 3; i++)
            {
                if (only_pontok.Count > i)
                {
                    int kov_pont = only_pontok[i];
                    List<string> kovik = new List<string>();
                    foreach (var v in valaszok)
                    {
                        if (Pontszam(v, joValasz) == kov_pont)
                        {
                            kovik.Add(v.azon);
                        }
                    }

                    foreach (var versenyzo in kovik)
                    {
                        Console.WriteLine($"{i+1}. díj ({kov_pont}) : {versenyzo}");
                    }
                }
            }
        }

        private static void Feladat06()
        {
            StreamWriter sw = new StreamWriter("pontok.txt");
            
            foreach (var v in valaszok)
            {
                int pontszam = Pontszam(v, joValasz);
                sw.WriteLine($"{v.azon} {pontszam}");
            }
            
            sw.Close();
        }

        private static void Feladat05()
        {
            Console.Write("A feladat sorszáma = ");
            int feladatSorszam = int.Parse(Console.ReadLine()) - 1;  //Fontos! 10-ik kérdés a 9-es indexű!!

            int joValaszokSzama = 0;
            foreach (var v in valaszok)
            {
                if (v.tipp[feladatSorszam] == joValasz[feladatSorszam])
                {
                    joValaszokSzama++;
                }
            }
            
            Console.WriteLine($"A feladatra {joValaszokSzama} fő, a versenyzők {((double)joValaszokSzama / valaszok.Count) * 100:N2}%-a adott helyes választ.");
        }

        private static void Feladat04_v1()
        {
            string megoldasLenyomat = "";
            for (int ind = 0; ind <  joValasz.Length; ind++)
            {
                if (joValasz[ind] == feladat3_tipp[ind])
                {
                    megoldasLenyomat += '+';
                }
                else
                {
                    megoldasLenyomat += ' ';
                }
            }
            
            Console.WriteLine($"{joValasz}    (a helyes megoldás)");
            Console.WriteLine($"{megoldasLenyomat}    (a versenyző helyes válaszai)");
        }

        private static void Feladat04_v2()
        {
            StringBuilder megoldasLenyomat = new StringBuilder("              ");
            for (int ind = 0; ind <  joValasz.Length; ind++)
            {
                if (joValasz[ind] == feladat3_tipp[ind])
                {
                    megoldasLenyomat[ind] = '+';
                }
            }
            
            Console.WriteLine($"{joValasz}    (a helyes megoldás)");
            Console.WriteLine($"{megoldasLenyomat.ToString()}    (a versenyző helyes válaszai)");
        }
        
        private static void Feladat03()
        {
            Console.Write("A versenyző azonosítója = ");
            feladat3_azon = Console.ReadLine(); 
            
            foreach (var v in valaszok)
            {
                if (v.azon == feladat3_azon)
                {
                    feladat3_tipp = v.tipp;
                }
            }
            
            Console.WriteLine($"{feladat3_tipp}    (a versenyző válasza)");
        }

        private static void Feladat01()
        {
            string[] adatok = File.ReadAllLines("valaszok.txt");

            bool joMegoldasBeolvasva = false;
            
            foreach (string line in adatok)
            {
                if (!joMegoldasBeolvasva)
                {
                    joValasz = line;
                    joMegoldasBeolvasva = true;
                }
                else
                {
                    string[] splittedLine = line.Split(' ');
                    Valasz v = new Valasz();
                    v.azon = splittedLine[0];
                    v.tipp = splittedLine[1];
                
                    valaszok.Add(v);
                }
            }
        }
    }
    
    class Valasz {
        public string azon {get; set;}
        public string tipp {get; set;}
    }
}