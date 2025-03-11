using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar() 
        { 
            List<Marca>lista =new List<Marca>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                return lista;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) " +
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
        public void modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("UPDATE MARCAS SET Descripcion=@Descripcion WHERE Id=@Id");

                // Validación de parámetros antes de asignarlos
                datos.setearparametro("@Descripcion", marca.Descripcion);
                datos.setearparametro("@Id", marca.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la marca en la base de datos.", ex);

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
                datos.setearConsulta("DELETE FROM MARCAS where id = @id");
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
