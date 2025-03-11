using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

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

        public void agregar(Categoria nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) " +
                                     "VALUES (@Descripcion)");

                datos.setearparametro("@Descripcion", nuevo.Descripcion);
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
        public void modificar(Categoria cate)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("UPDATE CATEGORIAS SET Descripcion=@Descripcion WHERE Id=@Id");

                // Validación de parámetros antes de asignarlos
                datos.setearparametro("@Descripcion", cate.Descripcion);
                datos.setearparametro("@Id", cate.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la categoría en la base de datos.", ex);

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
                datos.setearConsulta("DELETE FROM CATEGORIAS where id = @id");
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
    }
}
