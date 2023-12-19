using back_mapa_graduado.Controllers;
using Npgsql;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace back_mapa_graduado.Models
{
    public class GraduadoDataAcces : IGraduadoDataAcces
    {
        private PostgreSQLConfiguration _connectionString;

        public GraduadoDataAcces(PostgreSQLConfiguration conecctionString)
        {
            _connectionString = conecctionString;
        }

        protected NpgsqlConnection DbConnection() 
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Graduado>> GetAllGraduados()
        {
            List<Graduado> graduados = new List<Graduado>();

            using (NpgsqlConnection db = DbConnection())
            {
                await db.OpenAsync();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from getallgraduados()", db);
                cmd.CommandType = System.Data.CommandType.Text;

                using (NpgsqlDataReader rd = await cmd.ExecuteReaderAsync()) 
                {
                    while (rd.Read())
                    {
                        Graduado graduado = new Graduado
                        {
                            Dni = rd["dni"].ToString(),
                            Nombre = rd["nombre"].ToString(),
                            Numero = rd["numero"].ToString(),
                            Mail = rd["email"].ToString(),
                            Password = rd["password"].ToString(),
                            Carrera = rd["carrera"].ToString(),
                            Pais = rd["pais"].ToString(),
                            Provincia= rd["provincia"].ToString(),
                            Ciudad = rd["ciudad"].ToString(),
                            Latitud = rd["latitud"].ToString(),
                            Longitud = rd["longitud"].ToString()
                        };
                        graduados.Add(graduado);

                    }

                }
                   
            }

          
            return graduados;

        }
        public async Task<bool> AddGraduado(Graduado g)
        {
            using (NpgsqlConnection db = DbConnection()) 
            {
                try
                {
                    await db.OpenAsync();

                    NpgsqlCommand cmd = new NpgsqlCommand("addgraduado", db);
                    cmd.Parameters.AddWithValue(@"dni", g.Dni);
                    cmd.Parameters.AddWithValue(@"nombre", g.Nombre);
                    cmd.Parameters.AddWithValue(@"numero", g.Numero);
                    cmd.Parameters.AddWithValue(@"email", g.Mail);
                    cmd.Parameters.AddWithValue(@"password", g.Password);
                    cmd.Parameters.AddWithValue(@"carrera", g.Carrera);
                    cmd.Parameters.AddWithValue(@"pais", g.Pais);
                    cmd.Parameters.AddWithValue(@"provincia", g.Provincia);
                    cmd.Parameters.AddWithValue(@"ciudad", g.Ciudad);
                    cmd.Parameters.AddWithValue(@"latitud", g.Latitud);
                    cmd.Parameters.AddWithValue(@"longitud", g.Longitud);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    
                    Console.WriteLine("Registro exitoso");
                    return true;

                }
                catch (NpgsqlException e) 
                {
                    Console.WriteLine(e.Message);
                }           
            }
            return false;
        }

        public Task<bool> DeleteGraduado(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Graduado> GetGraduado(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Graduado> AuthenticationGraduado(string mail, string password)
        {
            Graduado g=null;

            using (NpgsqlConnection db = DbConnection())
            {
                await db.OpenAsync();

                
                //pongo la consulta en ves del nombre de la funcion por un error desconocido
                NpgsqlCommand cmd = new NpgsqlCommand("select * from login(@pass, @mail)", db);

                cmd.Parameters.Add("@pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value=password;
                cmd.Parameters.Add("@mail", NpgsqlTypes.NpgsqlDbType.Varchar).Value=mail;

                cmd.CommandType = System.Data.CommandType.Text;

                var rd= cmd.ExecuteReader();

                if (rd!=null) 
                {
                    while (rd.Read())
                    {
                        g = new Graduado
                        {
                            Dni = rd["dni"].ToString(),
                            Nombre = rd["nombre"].ToString(),
                            Numero = rd["numero"].ToString(),
                            Mail = rd["email"].ToString(),
                            Password = rd["password"].ToString(),
                            Carrera = rd["carrera"].ToString(),
                            Pais = rd["pais"].ToString(),
                            Provincia = rd["provincia"].ToString(),
                            Ciudad = rd["ciudad"].ToString(),
                            Latitud = rd["latitud"].ToString(),
                            Longitud = rd["longitud"].ToString()
                        };                       
                    }
                }
            }
            return g;
        }
        public Task<bool> UpdateGraduado(int id)
        {
            throw new NotImplementedException();
        }
    }
}
