using System;
namespace IAGInterna.Data
{
    public class ParametrosConfiguracion
    {

        const String file = "appsettings.json";
        public static IConfiguration Configuration;
        public ParametrosConfiguracion(string conectionString)
        {
            init();
        }

        private void init()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(file);

            Configuration = builder.Build();
        }
        public string getConexionString()
        {
            return Configuration["ConnectionStrings:MySqlConnection"];
        }
    }
}

