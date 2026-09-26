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

            if (!(Page is Default || Page is Login || Page is Registro))
            {
                if (!(Seguridad.sesionActiva(Session["trainee"])))
                    Response.Redirect("Login.aspx");
            }

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            //Session.Remove("trainee");
            //Reemplazo remove por Clear porque quiero eliminar todos los datos que haya cargado el usuario en su sesion
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}