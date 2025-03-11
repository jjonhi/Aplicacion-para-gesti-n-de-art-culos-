using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace negocio
{
    public class ArticuloNegocio
    {
        // Método para listar todos los artículos desde la base de datos
        public List<Articulo> Listar()
        {
            List<Articulo> listado = new List<Articulo>();// Lista donde se guardarán los artículos obtenidos de la BD

            SqlConnection conexion = new SqlConnection();// Objeto para la conexión a la base de dato
            SqlCommand comando = new SqlCommand();// Objeto para ejecutar comandos SQL
            SqlDataReader lector;// Objeto para leer los resultados de la consulta

            try
            {
                // Configuración de la cadena de conexión
                conexion.ConnectionString = "server=DESKTOP-78BNNHD ; database=CATALOGO_DB; integrated security=true";

                // Definimos el comando como texto SQL (un SELECT directo)
                comando.CommandType = System.Data.CommandType.Text;
                // Consulta SQL que trae artículos junto con la descripción de sus categorías y marcas
                comando.CommandText = "SELECT A.Id,A.Codigo, A.Nombre, A.Descripcion, C.Descripcion Categoria, M.Descripcion Marca, A.IdMarca, A.IdCategoria, A.ImagenUrl, A.Precio FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE C.Id = A.IdCategoria and M.Id = A.IdMarca";
                // Asociamos el comando a la conexión que definimos antes
                comando.Connection = conexion;

                conexion.Open();
                // Ejecutamos el comando y obtenemos un lector para recorrer los resultados
                lector = comando.ExecuteReader();


                // Recorremos cada fila de resultados
                while (lector.Read())
                {
                    Articulo aux = new Articulo();

                    // Asignamos las propiedades del artículo con los valores leídos de la base
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = lector["Codigo"] is DBNull ? "" : (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];


                    // Creamos y asignamos la categoría
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)lector["Categoria"];

                    // Creamos y asignamos la marca
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)lector["IdMarca"];
                    aux.Marca.Descripcion = (string)lector["Marca"];


                    // Validamos si la columna ImagenUrl es NULL en la base (para evitar errores)
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)lector["ImagenUrl"];
                    aux.Precio = lector["Precio"] is DBNull ? 0 : (decimal)lector["Precio"];

                    listado.Add(aux);
                }
                conexion.Close();
                return listado;
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
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) " +
                             "VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearparametro("@Codigo", nuevo.Codigo);
                datos.setearparametro("@Nombre", nuevo.Nombre);
                datos.setearparametro("@Descripcion", nuevo.Descripcion);
                datos.setearparametro("@IdMarca", nuevo.Marca?.Id ?? (object)DBNull.Value);
                datos.setearparametro("@IdCategoria", nuevo.Categoria?.Id ?? (object)DBNull.Value);
                datos.setearparametro("@ImagenUrl", nuevo.UrlImagen ?? (object)DBNull.Value);
                datos.setearparametro("@Precio", nuevo.Precio > 0 ? nuevo.Precio : (object)DBNull.Value);

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
                
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Descripcion, IdMarca=@IdMarca, IdCategoria=@IdCategoria, ImagenUrl=@ImagenUrl, Precio=@Precio WHERE Id=@Id");

                // Validación de parámetros antes de asignarlos
                datos.setearparametro("@Codigo", arti.Codigo);
                datos.setearparametro("@Nombre", arti.Nombre);
                datos.setearparametro("@Descripcion", arti.Descripcion);
                datos.setearparametro("@IdMarca", arti.Marca?.Id ?? (object)DBNull.Value);
                datos.setearparametro("@IdCategoria", arti.Categoria?.Id ?? (object)DBNull.Value);
                datos.setearparametro("@ImagenUrl", arti.UrlImagen ?? (object)DBNull.Value);
                datos.setearparametro("@Precio", arti.Precio > 0 ? arti.Precio : (object)DBNull.Value);
                datos.setearparametro("@Id", arti.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el artículo en la base de datos.", ex);

            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = null;
            try
            {
                datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM ARTICULOS where id = @id");
                datos.setearparametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (datos != null)
                    datos.cerrarConexion();

            }
        }




        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Empieza con WHERE 1=1
                string consulta = "SELECT A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria, A.Id FROM ARTICULOS A, MARCAS M, CATEGORIAS C WHERE M.Id = A.IdMarca AND C.Id = A.IdCategoria";

                if (campo == "Precio")
                {
                    // Forzar uso de punto decimal
                    if (!decimal.TryParse(filtro, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorDecimal))
                    {
                        throw new Exception("El valor ingresado no es un número válido para el campo Precio.");
                    }

                    consulta += " AND A.Precio ";
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "> @filtro";
                            break;
                        case "Menor a":
                            consulta += "< @filtro";
                            break;
                        default:
                            consulta += "= @filtro";
                            break;
                    }

                    

                    // Asignar el parámetro con el tipo correcto
                    datos.setearparametro("@filtro", valorDecimal); // O usar .ToString(CultureInfo.InvariantCulture) según lo requiera tu método
                }

                else if (campo == "Nombre" || campo == "Marca" || campo == "Categoria")
                {
                    string columna = campo == "Nombre" ? "A.Nombre"
                                   : campo == "Marca" ? "M.Descripcion"
                                                       : "C.Descripcion";

                    consulta += " AND " + columna + " LIKE @filtro";

                    switch (criterio)
                    {
                        case "Comienza con":
                            filtro += "%";
                            break;
                        case "Termina con":
                            filtro = "%" + filtro;
                            break;
                        default:
                            filtro = "%" + filtro + "%";
                            break;
                    }
                    datos.setearparametro("@filtro", filtro);
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Precio = (decimal)datos.Lector["Precio"],
                        UrlImagen = datos.Lector["ImagenUrl"] is DBNull ? null : (string)datos.Lector["ImagenUrl"],
                        Marca = new Marca
                        {
                            Id = (int)datos.Lector["IdMarca"],
                            Descripcion = (string)datos.Lector["Marca"]
                        },
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Descripcion = (string)datos.Lector["Categoria"]
                        }
                    };
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar artículos: " + ex.Message);
            }
        }    
    }
}
