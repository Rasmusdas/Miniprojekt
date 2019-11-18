using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniProjekt
{
    class RekDynMiniProjekt
    {
        /// <summary>
        /// The main function.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Dictionary<int,int>> values = LoadValuesFromFile("values");
            string input = Console.ReadLine().Trim();
            int.TryParse(input, out int inputNumber);
            foreach (var v in values)
            {
                Console.WriteLine(RekCutChain(inputNumber,0 , v));
                Console.WriteLine(IteCutChain(inputNumber, v));
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Cuts the chain in to smaller pieces to determine the value.
        /// Works using recursion (very slow)
        /// </summary>
        /// <param name="length"></param>
        /// <param name="money"></param>
        /// <param name="values"></param>
        /// <returns> The value of the chain </returns>
        static int RekCutChain(int length, int money, Dictionary<int,int> values)
        {
            int highestvalue = -1;
            if (length <= 0)
            {
                return money + length;
            }
            foreach (var val in values)
            {
                if(length >= val.Key)
                {
                    highestvalue = Math.Max(highestvalue,RekCutChain(length  - val.Key-1, money + val.Value+1, values));
                }
            }
            return highestvalue;
        }

        /// <summary>
        /// Cuts the chain into smaller pieces to determine the value
        /// Works using iteration (fast)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="values"></param>
        /// <returns> The value of the chain </returns>
        static int IteCutChain(int l, Dictionary<int,int> values)
        {
            int[] chain = new int[l+1];
            for (int i = 1; i < chain.Length; i++)
            {
                int max = -1;
                foreach (var v in values)
                {
                    if(i >= v.Key)
                    {
                        max = Math.Max(max, chain[i-1-v.Key]+v.Value+1);
                    }
                    else if (i >= v.Key)
                    {
                        max = Math.Max(max, chain[i - v.Key] + v.Value);
                    }
                }
                chain[i] = max;
            }
            return chain[l];
        }

        /// <summary>
        /// Loads the file 'values' from the same directory as the program is ran from and formats it into values.
        /// </summary>
        /// <param name="path"></param>
        /// <returns> The values from the file </returns>
        static List<Dictionary<int,int>> LoadValuesFromFile(string path)
        {
            List<Dictionary<int, int>> vals = new List<Dictionary<int, int>>();
            string[] fileContent = File.ReadAllLines(path);
            foreach(string s in fileContent)
            {
                string[] sA = s.Split('|');
                Dictionary<int, int> value = new Dictionary<int, int>();
                foreach (string ss in sA)
                {
                    Console.WriteLine("uwu");
                    string[] ssA = ss.Split(',');
                    
                    int.TryParse(ssA[0], out int key);
                    int.TryParse(ssA[1], out int val);
                    value.Add(key,val);
                }
                vals.Add(value);
            }
            return vals;
        }
    }
}
