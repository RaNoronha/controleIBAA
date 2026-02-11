using ControleMaterialIBAA.Infra;
using ControleMaterialIBAA.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.ViewModels
{
    public class ViewModelLogin : ViewModelBase
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }

        private readonly ServicoUsuarios _service = new();

        public async Task EntrarAsync()
        {
            var usuarioModel = await _service.LoginAsync(Usuario, Senha);

            if (usuarioModel != null)
            {
                var usuarioVM = ViewModelUsuario.FromModel(usuarioModel);
                Sessao.Login(usuarioVM);
            }
        }
    }
}
