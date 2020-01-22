using System;
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
            HashSet<string> hsWire = new HashSet<string>();
            int closestDistance = int.MaxValue;

            int x = 0;
            int y = 0;

            //add first wire coordinates to hsWire
            String[] input1 = wires[0].Split(',');

            foreach (string s1 in input1)
            {
                int[] dir = GetDir(s1[0]);
                int len = int.Parse(s1.Substring(1));
                for (int j = 0; j < len; j++)
                {
                    int newX = x + dir[0];
                    int newY = y + dir[1];
                    if (!hsWire.Contains(newX + "_" + newY))
                    {
                        hsWire.Add(newX + "_" + newY);
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

            foreach (string s2 in input2)
            {
                int[] dir = GetDir(s2[0]);
                int len = int.Parse(s2.Substring(1));
                for (int j = 0; j < len; j++)
                {

                    int newX = x + dir[0];
                    int newY = y + dir[1];

                    if (hsWire.Contains(newX + "_" + newY))
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
            Dictionary<String, Node> planets = new Dictionary<String, Node>();

            foreach (string orbit in orbits)
            {
                string[] splitOrbit = orbit.Split(')');

                //if planets doesn't contain left side of ')', add with no parent
                if (!planets.ContainsKey(splitOrbit[0]))
                {
                    planets[splitOrbit[0]] = new Node(splitOrbit[0], null);
                }

                //if planets doesn't contain right side of ')', add with left side as parent
                //else, update parent of right side to be left side
                if (!planets.ContainsKey(splitOrbit[1]))
                {
                    planets[splitOrbit[1]] = new Node(splitOrbit[1], planets[splitOrbit[0]]);
                }
                else
                {
                    planets[splitOrbit[1]].parent = planets[splitOrbit[0]];
                }
            }

            //count orbits for all planets
            int orbitCount = 0;
            foreach (var item in planets)
            {
                orbitCount += item.Value.orbitCount;
            }

            return orbitCount;
        }
    }
    class Node
    {
        public string name { get; }
        private Node _parent;
        public Node parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null && _parent != value)
                {
                    throw new ArgumentException($"Node[{name}] already has a parent.");
                }
                _parent = value;
            }
        }

        public int orbitCount => (parent == null) ? 0 : parent.orbitCount + 1;

        public Node(string name, Node parent)
        {
            this.name = name;
            this.parent = parent;
        }
    }
}
