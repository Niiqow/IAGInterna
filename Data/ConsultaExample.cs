using IAGInterna.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace IAGInterna.Data
{
    public class ConsultaExample
    {
        private ParametrosConfiguracion ParametrosConfiguracion = new ParametrosConfiguracion("MySqlConnection");

    



        public object Example()
        {
            try
            {
                Success success = new Success();
                var procedure = "sp_example";
                using (MySqlConnection conexion = new MySqlConnection(ParametrosConfiguracion.getConexionString()))
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand(procedure, conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        success.Numero = 200;
                        success.Resultado = reader["JsonResponse"].ToString();
                    }
                    return success;
                }
            }
            catch (Exception e)
            {
                Errors error = new Errors();
                error.Message = e.Message;
                return error;
            }
        }
       
    }

}