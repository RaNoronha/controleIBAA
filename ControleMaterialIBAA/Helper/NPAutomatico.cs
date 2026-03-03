using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Helper
{
    public class NPAutomatico
    {       
        private static readonly Random _random = new Random();
        public static string Gerar()
        {
            return _random.Next(100000, 1000000).ToString();
        }
    }
}
