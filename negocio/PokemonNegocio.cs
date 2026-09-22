using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar(string id = "") //Agregar el parametro con (= "") hace que sea opcional, entonces cualquier otra llamada al metodo de antes, no se va a romper ni nada por hacerte este agregado "opcional" 
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                //conexion.ConnectionString = "server=DESKTOP-HGTHACF\\SQLEXPRESS"; // Asi es con el nombre de la compu local
                //conexion.ConnectionString = "server=(local)\\SQLEXPRESS" // Asi tambien se puede poner "local" xq es la compu local
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true"; // Asi tambien, solo con PUNTO, que toma como la compu local
                                                                                                                   // .\\SQLEXPRESS;  Se conecta a la compu Local
                                                                                                                   // database=POKEDEX_DB;  Se conecta la BBDD que elegimos con la cual trabajar
                                                                                                                   //  integrate securiry=true  Tipo de auntenticacion de segurtidad, en este ejemplo usamos los de Windos de la BBDD

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Numero ,Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D Where P.IdTipo = E.Id AND P.IdDebilidad = D.Id \r\n";
                if(id != "")
                {
                    comando.CommandText += "and P.Id = " + id;
                }
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    // Validando si algun valor es null o no, para que no rompa la app (en caso que la BBDD no tenga ese valor como NotNull)
                    //if((!lector.IsDBNull(lector.GetOrdinal("UrlUImagen"))))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];
                    // Otra forma mas practica de hacerlo
                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    aux.Activo = bool.Parse(lector["Activo"].ToString());

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        
        public List<Pokemon> listarConSP()
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //string consulta = "Select Numero ,Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D Where P.IdTipo = E.Id AND P.IdDebilidad = D.Id AND P.Activo = 1";
                //datos.setearConsulta(consulta);

                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // Validando si algun valor el null o no, para que no rompa la app (en caso que la BBDD no tenga ese valor como NotNull)
                    //if((!lector.IsDBNull(lector.GetOrdinal("UrlUImagen"))))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];
                    // Otra forma mas practica de hacerlo
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {                                                                                                    
                //Tambien puede ser de esta forma el @Tipo y la @Debilidad agregando una funcion para que reconozca esos parametros en AccesoDatos                    
                datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, UrlImagen, IdTipo, IdDebilidad, Activo) Values (" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', '" + nuevo.UrlImagen + "', @idTipo, @idDebilidad, 1)");
                //datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, UrlImagen, IdTipo, IdDebilidad, Activo) Values (" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', '" + nuevo.UrlImagen + "', " + nuevo.Tipo.Id + ", " + nuevo.Debilidad.Id + ", 1)");
                datos.setearParametros("@idTipo", nuevo.Tipo.Id);
                datos.setearParametros("@idDebilidad", nuevo.Debilidad.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarConSP(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedAltaPokemon");

                datos.setearParametros("@numero", nuevo.Numero);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@urlImagen", nuevo.UrlImagen);
                datos.setearParametros("@idTipo", nuevo.Tipo.Id);
                datos.setearParametros("@idDebilidad", nuevo.Debilidad.Id);
                //datos.setearParametros("@idEvolucion", null);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @descripcion, UrlImagen = @imagen, IdTipo = @idTipo, IdDebilidad = @idDebilidad where id = @id ");

                datos.setearParametros("@numero", poke.Numero);
                datos.setearParametros("@nombre", poke.Nombre);
                datos.setearParametros("@descripcion", poke.Descripcion);
                datos.setearParametros("@imagen", poke.UrlImagen);
                datos.setearParametros("@idTipo", poke.Tipo.Id);
                datos.setearParametros("@idDebilidad", poke.Debilidad.Id);
                datos.setearParametros("@id", poke.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarConSP(Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedModificarPokemon");

                datos.setearParametros("@numero", poke.Numero);
                datos.setearParametros("@nombre", poke.Nombre);
                datos.setearParametros("@descripcion", poke.Descripcion);
                datos.setearParametros("@urlImagen", poke.UrlImagen);
                datos.setearParametros("@idTipo", poke.Tipo.Id);
                datos.setearParametros("@idDebilidad", poke.Debilidad.Id);
                datos.setearParametros("@id", poke.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from pokemons where id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarLogico(int id, bool activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update pokemons set Activo = @activo where id = @id");
                datos.setearParametros("@id", id);
                datos.setearParametros("@activo", activo);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select Numero ,Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D Where P.IdTipo = E.Id AND P.IdDebilidad = D.Id AND ";


                if (campo == "Número")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero <" + filtro;
                            break;
                        default:
                            consulta += "Numero =" + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                if(estado == "Activo")
                    consulta += " and P.Activo = 1";
                else if(estado == "Inactivo")
                    consulta += " and P.Activo = 0";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // Validando si algun valor el null o no, para que no rompa la app (en caso que la BBDD no tenga ese valor como NotNull)
                    //if((!lector.IsDBNull(lector.GetOrdinal("UrlUImagen"))))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];
                    // Otra forma mas practica de hacerlo
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
