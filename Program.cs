using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input1 = System.IO.File.ReadAllLines("../../../Day1Input.txt");
            Console.WriteLine("Day 1: " + GetFuelRequirements(input1));

            string[] input3 = System.IO.File.ReadAllLines("../../../Day3Input.txt");
            Console.WriteLine("Day 3: " + GetManhattanDistance(input3));

            string[] input6 = System.IO.File.ReadAllLines("../../../Day6Input.txt");
            Console.WriteLine("Day 6: " + GetOrbitCount(input6));

        }

        //advent day 1
        public static int GetFuelRequirements(string[] masses)
        {
            int sum = 0;

            foreach (string mass in masses)
            {
                int fuel = int.Parse(mass) / 3 - 2;
                sum += fuel;
            }

            return sum;
        }

        //advent day 3
        public static int GetManhattanDistance(string[] wires)
        {
            List<string> lWire = new List<string>();
            int closestDistance = int.MaxValue;

            int x = 0;
            int y = 0;

            //add first wire coordinates to lWire
            String[] input1 = wires[0].Split(',');

            for (int i = 0; i < input1.Length; i++)
            {
                int[] dir = GetDir(input1[i][0]);
                int len = int.Parse(input1[i].Substring(1));
                for (int j = 0; j < len; j++)
                {
                    int newX = x + dir[0];
                    int newY = y + dir[1];
                    if (!lWire.Contains(newX + "_" + newY))
                    {
                        lWire.Add(newX + "_" + newY);
                    }
                    x = newX;
                    y = newY;
                }
            }

            //get all second wire coordinates 
            //If any are in first wire coordinates list and the absolute value of the coordinates 
            //added together is less than closest distance, set to closest distance

            String[] input2 = wires[1].Split(',');
            x = 0;
            y = 0;

            for (int i = 0; i < input2.Length; i++)
            {
                int[] dir = GetDir(input2[i][0]);
                int len = int.Parse(input2[i].Substring(1));
                for (int j = 0; j < len; j++)
                {

                    int newX = x + dir[0];
                    int newY = y + dir[1];

                    if (lWire.Contains(newX + "_" + newY))
                    {
                        closestDistance = Math.Min(closestDistance, (int)Math.Abs(newX) + (int)Math.Abs(newY));
                    }
                    x = newX;
                    y = newY;
                }
            }
            return closestDistance;
        }


        public static int[] GetDir(char c)
        {
            switch (c)
            {
                case 'U': return new int[] { 0, 1 };
                case 'D': return new int[] { 0, -1 };
                case 'L': return new int[] { -1, 0 };
                case 'R': return new int[] { 1, 0 };
            }
            return null;
        }

        //advent day 6
        public static int GetOrbitCount(string[] orbits)
        {
            Hashtable htOrbits = new Hashtable();
            int sum = 0;

            foreach (string orbit in orbits)
            {
                string[] splitOrbit = orbit.Split(')');

                int numOrbits = 1;

                //if first string is a key in hashtable, add it's value to numOrbits
                if (htOrbits.ContainsKey(splitOrbit[0]))
                {
                    numOrbits += int.Parse(htOrbits[splitOrbit[0]].ToString());
                }

                htOrbits.Add(splitOrbit[1], numOrbits);
            }

            //add all values in hashtable to get sum of orbits
            foreach (DictionaryEntry entry in htOrbits)
            {
                sum += int.Parse(entry.Value.ToString());
            }

            return sum;
        }

    }
}
