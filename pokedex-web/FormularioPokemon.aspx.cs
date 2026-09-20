using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace pokedex_web
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;

            try
            {
                //Configuración inicial de la pantalla Formulario
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();

                    List<Elemento> lista = negocio.listar();

                    ddlTipo.DataSource = lista;
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }

                //Configuración si estamos Modificando
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"] : ""; //Para saber si encontro un id en la url que trajo. Si trajo guardamos el id, sino guardamos "" vacio.
                if (id != "" && !IsPostBack) //Si trajo un id, osea es distinto a vacio, cargamos los datos para modificar
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    //List<Pokemon> lista = negocio.listar(id);
                    //Pokemon seleccionado = lista[0];

                    Pokemon seleccionado = (negocio.listar(id))[0]; // es lo mismo que las 2 lineas anteriores, pero en una sola linea. No creo la variable lista y me ahorro esa linea

                    //Pre cargar todos los datos al formulario...
                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtUrlImagen.Text = seleccionado.UrlImagen;
                    txtNumero.Text = seleccionado.Numero.ToString();

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();

                    // Esta forma de forzarlo es bastante fea, tendría que ver creando un metodo o algo.
                    txtUrlImagen_TextChanged(sender, e); // Forzamos el llamado a la funcion para que cargue la imagen, ya que no se precargaba al cargar los datos del seleccionado en el formulario

                }

            }
            catch (Exception ex)
            {
                Session.Add("Error ", ex);
                throw ;
                //redireccion pantalla error
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtUrlImagen.Text;

                nuevo.Tipo = new Elemento();
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);

                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text); // o podría ser: = int.Parse(Request.QueryString["id"]);
                    negocio.modificarConSP(nuevo);
                } 
                else
                    negocio.agregarConSP(nuevo);

                Response.Redirect("PokemonLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error ", ex);
                throw;
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtUrlImagen.Text;
        }
    }
}