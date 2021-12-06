using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_6
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static List<int> ReadInput()
        {
            var line = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\LanternfishesInternalClocks.txt");

            List<int> lanternfish = line.Split(",").Select(fish =>
            {
                return Convert.ToInt32(fish);

            }).ToList();
            
            return lanternfish;
        }
        static void FirstStar()
        {
            List<int> lanternfish = ReadInput();

            for(int a = 0; a < 80; a++)
            {
                lanternfish = NewDay(lanternfish);
            }
            Console.WriteLine(lanternfish.Count);

        }

        static List<int> NewDay(List<int> lanternfish)
        {
            var newFish = 0;

            lanternfish = lanternfish.Select(fish =>
            {
                if (fish == 0)
                {
                    newFish++;
                    return 7;
                }
                return fish;
            }).ToList();

            lanternfish = lanternfish.Select(fish => --fish).ToList();

            for (int i = 0; i < newFish; i++)
            {
                lanternfish.Add(8);
            }

            return lanternfish;
        }

        static void SecondStar()
        {
            List<int> lanternfish = ReadInput();

            long[] fishAtDifferentAges = new long[9];

            foreach (int fish in lanternfish)
            {
                fishAtDifferentAges[fish]++;
            }

            for(int i = 0;i < 256; i++)
            {
                fishAtDifferentAges = NewDayOptimized(fishAtDifferentAges);
            }

            long numberOfFish = 0;

             foreach(long fish in fishAtDifferentAges)
            {
                numberOfFish += fish;
            }

            Console.WriteLine(numberOfFish);
        }

        static long[] NewDayOptimized(long[] lanternfish)
        {
            var newFish = lanternfish[0];

            for (int i = 0; i < lanternfish.Length - 1 ; i++)
            {
                lanternfish[i] = lanternfish[i + 1];
              
            }

            lanternfish[6] += newFish;
            lanternfish[8] = newFish;


            return lanternfish;
        }

    }
}