using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //temp func
            Map map = new Map("../../Map.csv");
            map.Print();
        }
    }
}
