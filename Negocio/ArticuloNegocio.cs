using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using Negocio;
using System.Configuration;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Xml.Linq;
using System.Collections;

namespace Negocio
{
    public class ArticuloNegocio
    {


        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion AS Descripcion, ImagenUrl AS Imagen, C.Descripcion AS Categoria, M.Descripcion AS Marca, A.Precio AS Precio, A.IdMarca, A.IdCategoria FROM ARTICULOS A INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria INNER JOIN MARCAS M ON M.Id = A.IdMarca");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Codigo = datos.Lector["Codigo"] == DBNull.Value ? string.Empty : (string)datos.Lector["Codigo"];
                    aux.Nombre = datos.Lector["Nombre"] == DBNull.Value ? string.Empty : (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] == DBNull.Value ? string.Empty : (string)datos.Lector["Descripcion"];
                    aux.Imagen = datos.Lector["Imagen"] == DBNull.Value ? string.Empty : (string)datos.Lector["Imagen"];
                    aux.DescripcionC = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        DescripcionC = datos.Lector["Categoria"] == DBNull.Value ? string.Empty : (string)datos.Lector["Categoria"]
                    };
                    aux.DescripcionM = new Marca
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        DescripcionM = datos.Lector["Marca"] == DBNull.Value ? string.Empty : (string)datos.Lector["Marca"]
                    };
                    aux.Precio = datos.Lector["Precio"] == DBNull.Value ? 0 : (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
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
        public List<Articulo> listarId(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = ConfigurationManager.AppSettings["cadenaConexion"];
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT A.Id, Codigo, Nombre, A.Descripcion AS Descripcion, ImagenUrl AS Imagen, C.Descripcion AS Categoria, M.Descripcion AS Marca, A.Precio AS Precio, A.IdMarca, A.IdCategoria FROM ARTICULOS A INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria INNER JOIN MARCAS M ON M.Id = A.IdMarca";
                if (id != "")
                    comando.CommandText += " and A.Id = " + id;

                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.Precio = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Codigo= (string)lector["Codigo"];
                    //if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];
                    if (!(lector["Imagen"] is DBNull))
                        aux.Imagen = (string)lector["Imagen"];

                    aux.DescripcionC = new Categoria();
                    aux.DescripcionC.Id = (int)lector["IdCategoria"];
                    aux.DescripcionC.DescripcionC = (string)lector["Categoria"];
                    aux.DescripcionM = new Marca();
                    aux.DescripcionM.Id = (int)lector["IdMarca"];
                    aux.DescripcionM.DescripcionM = (string)lector["Marca"];

                    

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
        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //string consulta = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo And D.Id = P.IdDebilidad And P.Activo = 1";
                //datos.setearConsulta(consulta);
                datos.setearProcedimiento("storedListar");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.DescripcionC = new Categoria();
                    aux.DescripcionC.DescripcionC = (string)datos.Lector["Categoria"];
                    aux.DescripcionC.Id = (int)datos.Lector["IdCategoria"];
                    aux.DescripcionM = new Marca();
                    aux.DescripcionM.Id = (int)datos.Lector["IdMarca"];
                    aux.DescripcionM.DescripcionM = (string)datos.Lector["Marca"];
                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS(Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values ('" + nuevo.Codigo + "','" + nuevo.Nombre + "', '" + nuevo.Descripcion + "',@IdMarca,@IdCategoria,@ImagenUrl," + nuevo.Precio + " )");
                datos.setearParametro("@idMarca", nuevo.DescripcionM.Id);
                datos.setearParametro("@idCategoria", nuevo.DescripcionC.Id);
                datos.setearParametro("@ImagenUrl", nuevo.Imagen);
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
        public void agregarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedAltaArticulo");
                datos.setearParametro("@precio", nuevo.Precio);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@imagen", nuevo.Imagen);
                datos.setearParametro("@descripcionC", nuevo.DescripcionC.Id);
                datos.setearParametro("@descripcionM", nuevo.DescripcionM.Id);
                //datos.setearParametro("@idEvolucion", null);
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

        public void modificar(Articulo arti)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, ImagenUrl = @img, IdMarca = @idmarca, IdCategoria = @idcategoria, Precio=@precio Where Id = @id");
                datos.setearParametro("@codigo", arti.Codigo);
                datos.setearParametro("@nombre", arti.Nombre);
                datos.setearParametro("@desc", arti.Descripcion);
                datos.setearParametro("@img", arti.Imagen);
                datos.setearParametro("@idmarca", arti.DescripcionM.Id);
                datos.setearParametro("@idCategoria", arti.DescripcionC.Id);
                datos.setearParametro("@id", arti.Id);
                datos.setearParametro("@precio", arti.Precio);

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
        public void modificarConSP(Articulo arti)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarArticulo");
                datos.setearParametro("@precio", arti.Precio);
                datos.setearParametro("@nombre", arti.Nombre);
                datos.setearParametro("@desc", arti.Descripcion);
                datos.setearParametro("@img", arti.Imagen);
                datos.setearParametro("@idMarca", arti.DescripcionM.Id);
                datos.setearParametro("@idCategoria", arti.DescripcionC.Id);
                datos.setearParametro("@id", arti.Id);

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
                datos.setearConsulta("delete from articulos where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion Descripcion, ImagenUrl Imagen, C.Descripcion Categoria, M.Descripcion Marca, A.ImagenUrl Imagen, A.Precio Precio,A.IdMarca, A.IdCategoria From ARTICULOS A, CATEGORIAS C, MARCAS M Where C.Id = A.IdCategoria And M.Id=A.IdMarca And ";
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + @filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + @filtro;
                            break;
                        default:
                            consulta += "Precio = " + @filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + @filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + @filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + @filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + @filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + @filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + @filtro + "%'";
                            break;

                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion like '" + @filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + @filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + @filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.DescripcionC = new Categoria();
                    aux.DescripcionC.DescripcionC = (string)datos.Lector["Categoria"];
                    aux.DescripcionC.Id = (int)datos.Lector["IdCategoria"];
                    aux.DescripcionM = new Marca();
                    aux.DescripcionM.Id = (int)datos.Lector["IdMarca"];
                    aux.DescripcionM.DescripcionM = (string)datos.Lector["Marca"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];

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
        /*  public void eliminarLogico(int id)
       {
           try
           {
               AccesoDatos datos = new AccesoDatos();
               datos.setearConsulta("update ARTICULOS set Activo = 0 Where id = @id");
               datos.setearParametro("@id", id);
               datos.ejecutarAccion();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }  
        */
    




