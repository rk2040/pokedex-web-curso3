using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace pokedex_web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Trainee user = new Trainee();
                TraineeNegocio traineeNegocio = new TraineeNegocio();
                EmailService emailService = new EmailService();
                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;

                user.Id = traineeNegocio.insertarNuevo(user);

                Session.Add("trainee", user); //Con esto dejo abierta la sesión al usuario despues de que se registre, asi no tiene que registrarse y despues tenga que loguearse

                //Una vez que se registra un nuevo usuario, podemos enviarle un email de bienvenida o algo asi
                emailService.armarCorreo(user.Email, "Bienvenido Trainee", "Hola, te damos la bienvenida como nuevo trainee!!!");
                emailService.enviarEmail();

                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
            }
        }
    }
}