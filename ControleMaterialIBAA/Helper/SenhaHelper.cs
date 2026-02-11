using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ControleMaterialIBAA.Helper
{
    public class SenhaHelper
    {
        public static string GerarHash(string senha)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }

        public static bool Verificar(string senhaDigitada, string hashBanco)
        {
            var hashSenha = GerarHash(senhaDigitada);
            return hashSenha == hashBanco;
        }
    }
}
