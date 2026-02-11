using ControleMaterialIBAA.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.ViewModels
{
    public class ViewModelUsuario : ViewModelBase
    {
        public Guid Id { get; set; }
        public string Usuario { get; set; }

        public static ViewModelUsuario FromModel (ModelosUsuarios model)
        {
            ViewModelUsuario usuario = new ViewModelUsuario ();

            usuario.Id = model.id;
            usuario.Usuario = model.usuario;

            return usuario;

        }
    }
}
