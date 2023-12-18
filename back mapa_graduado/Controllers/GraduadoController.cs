using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using back_mapa_graduado.Models;
using System.Security.Claims;
using Npgsql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using RestSharp;

namespace back_mapa_graduado.Controllers
{
    [ApiController]
    [Route("graduado")]
    public class GraduadoController : Controller
    {
        private readonly IGraduadoDataAcces _graduadoDataAcces;
        private List<Graduado> graduadoList = new List<Graduado>();
        private static string path = AppDomain.CurrentDomain.BaseDirectory + @"\graduados.json";

        public GraduadoController(IGraduadoDataAcces graduadoDataAcces)
        {
            _graduadoDataAcces = graduadoDataAcces;
        }
        [HttpGet]
        public IActionResult GetAllGraduados()
        {
            IEnumerable<Graduado> graduados = _graduadoDataAcces.GetAllGraduados().Result;
            return Ok(graduados);
        }
        [HttpPost]
        [Route("addgraduado")]
        public IActionResult AddGraduado(IFormCollection formCollection)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter txt = new StreamWriter(fs);
                Graduado g = JsonConvert.DeserializeObject<Graduado>(formCollection["datos"]);

                txt.WriteLine(formCollection["datos"]);

                //System.IO.File.WriteAllText(path, formCollection["datos"]);
                g.Foto = formCollection["imagen"];

                _graduadoDataAcces.AddGraduado(g);
                Console.WriteLine("Datos recibido con exito");
                txt.Close();
                fs.Close();
                return Ok();

            }
            catch (IOException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [HttpPost]
        [Route("guardar2")]
        public ActionResult GuardarGraduado2(IFormCollection formCollection)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter txt = new StreamWriter(fs);
                Graduado g = JsonConvert.DeserializeObject<Graduado>(formCollection["datos"]);

                txt.WriteLine(formCollection["datos"]);

                //System.IO.File.WriteAllText(path, formCollection["datos"]);
                g.Foto = formCollection["imagen"];
                Console.WriteLine("Datos recibido con exito");
                txt.Close();
                fs.Close();
            }
            catch (IOException e) {
                BadRequest(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());

            }

            return Ok(null);

        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string mail, string password) 
        {
            _graduadoDataAcces.AuthenticationGraduado(mail,password);
            return Ok();
        }
        //public async Task<ActionResult> Login(Graduado g)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection conex = new NpgsqlConnection())
        //        {
        //            _graduadoDataAcces.
        //            NpgsqlCommand cmd = new NpgsqlCommand("validate_graduado", conex);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = g.Mail;
        //            cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = g.Password;
        //            conex.Open();
        //            NpgsqlDataReader rd = cmd.ExecuteReader();
        //            while (rd.Read())
        //            {
        //                if (rd["email"] != null)
        //                {
        //                    List<Claim> c = new List<Claim>()
        //                    {
        //                        new Claim(ClaimTypes.NameIdentifier, g.Mail)
        //                    };
        //                    ClaimsIdentity ci = new ClaimsIdentity(c, CookieAuthenticationDefaults.AuthenticationScheme);
        //                    AuthenticationProperties p = new AuthenticationProperties();

        //                    p.AllowRefresh = true;
        //                    p.IsPersistent = true;

        //                    if (!p.IsPersistent)
        //                        p.ExpiresUtc = DateTime.UtcNow.AddMinutes(1);

        //                    else
        //                        p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

        //                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
        //                    return Ok();
        //                }
        //                else
        //                {
        //                    return BadRequest("Credenciales incorrectas o cuenta no registrada");
        //                }

        //            }
        //            conex.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        BadRequest(e.Message);
        //    }
        //    return null;
        //}


        [HttpGet]
        [Route("listarImg")]
        public ActionResult ListarGraduadoImg()
        {

            return BadRequest();

        }



        [HttpGet]
        [Route("listarGraduados")]
        public ActionResult ListarGraduados() 
        {
            List<string> jsonGraduados= new List<string>();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr=new StreamReader(fs);
            try 
            {
                while (!sr.EndOfStream)
                {
                    string json = sr.ReadLine();
                    jsonGraduados.Add(json);
                }

                return Ok(System.Text.Json.JsonSerializer.Serialize(jsonGraduados));
            }
            catch(IOException e) 
            {
                return BadRequest(e.ToString());
            }
        }

       
    }  
}

