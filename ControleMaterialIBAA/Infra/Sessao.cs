using ControleMaterialIBAA.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Infra
{
    public static class Sessao
    {
        public static ViewModelUsuario? UsuarioLogado { get; private set; }

        public static bool EstaLogado => UsuarioLogado != null;

        public static void Login(ViewModelUsuario usuario)
        {
            UsuarioLogado = usuario;
        }

        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}
