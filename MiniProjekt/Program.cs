using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    class Program
    {
        static Dictionary<int, int> Values = new Dictionary<int, int>() { { 1,1 },{ 13,140 },{ 30,300 } };
        static void Main(string[] args)
        {
            string input = Console.ReadLine().Trim();
            int.TryParse(input, out int inputNumber);
            //Console.WriteLine(RekCutChain(inputNumber, 0));
            Console.WriteLine(IteCutChain(inputNumber));
            foreach (var v in IteCutChainCuts(inputNumber))
            {
                Console.WriteLine(v.Value);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


        static int RekCutChain(int length, int money)
        {
            int highestvalue = -1;
            foreach (var val in Values)
            {
                if(length >= val.Key)
                {
                    highestvalue = Math.Max(highestvalue,RekCutChain(length  - val.Key-1, money + val.Value+1));
                }
            }
            if(highestvalue == -1)
            {
                return money + length;
            }
            return highestvalue;
        }

        static int IteCutChain(int l)
        {
            int[] chain = new int[l+1];
            for (int i = 1; i < chain.Length; i++)
            {
                int max = -1;
                foreach (var v in Values)
                {
                    if(i > v.Key)
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


        static Dictionary<int,int> IteCutChainCuts(int l)
        {
            Dictionary<int, int> owo = new Dictionary<int, int>() {  };
            foreach(var v in Values)
            {
                owo.Add(v.Key,0);
            }
            int[] chain = new int[l + 1];
            for (int i = 1; i < chain.Length; i++)
            {
                int max = -1;
                int maxCut = -1;
                foreach (var v in Values)
                {
                    if (i > v.Key)
                    {
                        if (chain[i - v.Key-1] + v.Value+1 > max)
                        {
                            maxCut = v.Key;
                        }
                    }
                    else if (i >= v.Key)
                    {
                        if(chain[i - v.Key] + v.Value > max)
                        {
                            maxCut = v.Key;
                        }
                    }
                }
                owo[maxCut]++;
                chain[i] = max;
            }
            return owo;
        }
    }
}
