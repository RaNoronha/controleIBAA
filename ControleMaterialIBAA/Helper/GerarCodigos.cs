using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Helper
{
    public class GerarCodigos
    {
        private static readonly Random _random = new Random();
        
        public static string GerarNumeroPatrimonial()
        {
            return _random.Next(100000, 1000000).ToString();
        }

        public static string GerarCodigoMaterial()
        {
            return _random.Next(1, 100000).ToString("D5");
        }
    }
}
