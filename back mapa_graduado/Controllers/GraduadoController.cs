using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace back_mapa_graduado.Controllers
{
    [ApiController]
    [Route("graduado")]
    public class GraduadoController : ControllerBase
    {
        static SortedList<string,Graduado > graduados = new SortedList<string,Graduado>();


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
                Graduado g = JsonConvert.DeserializeObject<Graduado>(formCollection["datos"]);
                g.Foto = formCollection["imagen"];
                graduados.Add(g.Cuil, g);
                Console.WriteLine("Datos recibido con exito");
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());    
            
            }
           
            return Ok(null);

        }

        [HttpGet]
        [Route("listar")]
        public ActionResult ListarGraduado()
        {
           if(graduados.ContainsKey("20-45552194-8"))
                return  Ok(graduados["20-45552194-8"]) ;

           return BadRequest();
            
        }
    }

   }

