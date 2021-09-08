using System.Collections.Generic;
using System.Dynamic;
using System.IO;
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
        }

        private static void Feladat01()
        {
            string[] inputFile = File.ReadAllLines("tavirathu13.txt");

            foreach (var line in inputFile)
            {
                string[] splittedLine = line.Split(' ');
                Tavirat tavirat = new Tavirat();
                
                // ToDo:
                // - idő óó:pp formába
                // - szélerősség int-ként, nem kell szélirány (szélcsend?)
                // Többi értelem szerűen..

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