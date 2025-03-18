using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250317
{
    class HelloWorld
    {
        public HelloWorld(int inGold = 100, int inHP = 100, int inMP = 100)
        {
            Gold = inGold;
            MP = inMP;
            HP = inHP;
        }

        public int Gold;
        public int MP;
        public int HP;
    }

    class Program
    {

        static void Main(string[] args)
        {
            HelloWorld h = new HelloWorld(10, 20, 30);
            StreamWriter sw = new StreamWriter("./data.dat");
            sw.WriteLine(h.Gold);
            sw.WriteLine(h.HP);
            sw.WriteLine(h.MP);
            sw.Close();


            StreamReader sr = new StreamReader("./data.dat");
            String DataGold = sr.ReadLine();
            String DataHP = sr.ReadLine();
            String DataMP = sr.ReadLine();

            HelloWorld h2 = new HelloWorld(int.Parse(DataGold), int.Parse(DataHP), int.Parse(DataMP));


            sr.Close();
        }
    }
}