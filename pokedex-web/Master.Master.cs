using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pokedex_web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Page : "this" para ver todos las propiedades del objeto en el que estamos (en este caso Master) pero no es necesario poner this para que funcione.
            imgAvatar.ImageUrl = "https://media.istockphoto.com/id/1495088043/es/vector/icono-de-perfil-de-usuario-avatar-o-icono-de-persona-foto-de-perfil-s%C3%ADmbolo-de-retrato.jpg?s=612x612&w=0&k=20&c=mY3gnj2lU7khgLhV6dQBNqomEGj3ayWH-xtpYuCXrzk=";

            //if (!(Page is Default || Page is Login || Page is Registro || Page is Error))
            //{
            //    if (!Seguridad.sesionActiva(Session["trainee"]))
            //        Response.Redirect("Login.aspx", false);
            //    else
            //    {
            //        Trainee user = (Trainee)Session["trainee"];
            //        lblUser.Text = user.Email;
            //        if(!string.IsNullOrEmpty(user.ImagenPerfil))
            //            imgAvatar.ImageUrl = "~/Images/" + user.ImagenPerfil;
            //    }
            //}

            if (!(Page is Default || Page is Login || Page is Registro || Page is Error))
            {
                if (!Seguridad.sesionActiva(Session["trainee"]))
                    Response.Redirect("Login.aspx", false);
            }

            if (Seguridad.sesionActiva(Session["trainee"]))
            {
                Trainee user = (Trainee)Session["trainee"];
                lblUser.Text = user.Email;
                if (!string.IsNullOrEmpty(user.ImagenPerfil))
                    imgAvatar.ImageUrl = "~/Images/" + user.ImagenPerfil;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            //Session.Remove("trainee");
            //Reemplazo remove por Clear porque quiero eliminar todos los datos que haya cargado el usuario en su sesion
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}