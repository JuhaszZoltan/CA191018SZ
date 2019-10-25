using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA191018SZ
{
    class Program
    {
        static Dictionary<string, List<Jatekos>> csapatok;
        static void Main(string[] args)
        {
            Beolvas();
            //Teszt();
            OsszJovedelem();
            LegjobbFizu();
            FizuCsapatonkent();
            MaxMinDif();
            LoegjobbAtlag();
            Console.ReadKey();
        }

        private static void LoegjobbAtlag()
        {
            float lszaf = float.MaxValue;
            string lszafcsn = "";

            foreach (var cs in csapatok)
            {
                int sum = 0;

                foreach (var j in cs.Value)
                {
                    sum += j.EvesFizu;
                }
                float atlag = sum / (float)cs.Value.Count;
                if (lszaf > atlag)
                {
                    lszaf = atlag;
                    lszafcsn = cs.Key;
                }
            }

            Console.WriteLine($"Legrosszabb átlagfizetés a {lszafcsn}-ne van (${lszaf})");
        }

        private static void MaxMinDif()
        {
            int maxDif = 0;
            string mdcsn = "";

            foreach (var cs in csapatok)
            {
                int mini = 0;
                int maxi = 0;

                for (int i = 1; i < cs.Value.Count; i++)
                {
                    if (cs.Value[i].EvesFizu < cs.Value[mini].EvesFizu) mini = i;
                    if (cs.Value[i].EvesFizu > cs.Value[maxi].EvesFizu) maxi = i;
                }

                int dif = cs.Value[maxi].EvesFizu - cs.Value[mini].EvesFizu;
                if (dif > maxDif)
                {
                    maxDif = dif;
                    mdcsn = cs.Key;
                }
            }
            Console.WriteLine($"\nA legnagyobb különbség a {mdcsn}-ben van a min és max fizu között\n");
        }

        private static void FizuCsapatonkent()
        {
            foreach (var cs in csapatok)
            {
                int sum = 0;
                foreach (var j in cs.Value)
                {
                    sum += j.EvesFizu;
                }
                Console.WriteLine("{0, -23} {1:N0} USD", cs.Key + ":", sum);
            }
        }

        private static void LegjobbFizu()
        {
            var max = new Jatekos("", -1, 0);

            foreach (var cs in csapatok)
            {
                foreach (var j in cs.Value)
                {
                    if (j.EvesFizu > max.EvesFizu) max = j;
                }
            }
            Console.WriteLine($"\nlegjobban kereső játékos:\n{max.Nev} (${max.EvesFizu})\n");
        }

        private static void OsszJovedelem()
        {
            foreach (var cs in csapatok)
            {
                foreach (var j in cs.Value)
                {
                    Console.WriteLine("{0, -23} {1,11:N0} USD", j.Nev + ":", j.EvesFizu * j.Evek);
                }
            }
        }

        private static void Teszt()
        {
            foreach (var cs in csapatok.Keys)
            {
                Console.WriteLine(cs);
            }
        }

        private static void Beolvas()
        {
            csapatok = new Dictionary<string, List<Jatekos>>();
            var sr = new StreamReader($"NBA2003.csv", Encoding.UTF8);
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                var tmp = sr.ReadLine().Split(';');
                if(!csapatok.ContainsKey(tmp[0].Trim('"')))
                {
                    csapatok.Add(tmp[0].Trim('"'), new List<Jatekos>());
                }
                csapatok[tmp[0].Trim('"')].Add(
                    new Jatekos(
                        tmp[1].Trim('"'),
                        int.Parse(tmp[2]),
                        int.Parse(tmp[3])));
            }

            sr.Close();
        }
    }
}
