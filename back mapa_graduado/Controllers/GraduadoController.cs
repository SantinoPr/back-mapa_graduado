using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace back_mapa_graduado.Controllers
{
    [ApiController]
    [Route("graduado")]
    public class GraduadoController : ControllerBase
    {
        static SortedList<string,Graduado > graduados = new SortedList<string,Graduado>();
        static string path=AppDomain.CurrentDomain.BaseDirectory + @"\graduados.json";

        [HttpPost]
        [Route("guardar")]
        public dynamic GuardarGraduado([FromBody]Graduado graduado)
        {
            graduados.Add(graduado.Cuil,graduado);
            Console.WriteLine("Datos recibido con exito");
            return new
            {
                succes = true,
                message = "Graduado guardado con exito",
            };

        }

        [HttpPost]
        [Route("guardar2")]
        public ActionResult GuardarGraduado2(IFormCollection formCollection)
        {       
            try
            {
                FileStream fs=new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter txt=new StreamWriter(fs);
                Graduado g = JsonConvert.DeserializeObject<Graduado>(formCollection["datos"]);

                txt.WriteLine(formCollection["datos"]);

                //System.IO.File.WriteAllText(path, formCollection["datos"]);
                g.Foto = formCollection["imagen"];
                graduados.Add(g.Cuil, g);
                Console.WriteLine("Datos recibido con exito");
                txt.Close();
                fs.Close();
            }
            catch(IOException e){
                BadRequest(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());    
            
            }
           
            return Ok(null);

        }

        [HttpGet]
        [Route("listarJson")]
        public ActionResult ListarGraduadoJson()
        {
           if(graduados.ContainsKey("20-45552194-8"))
                return  Ok(graduados["20-45552194-8"]) ;

           return BadRequest();
            
        }
        [HttpGet]
        [Route("listarImg")]
        public ActionResult ListarGraduadoImg()
        {
            if (graduados.ContainsKey("20-45552194-8"))
                return Ok(graduados["20-45552194-8"].Foto);

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

