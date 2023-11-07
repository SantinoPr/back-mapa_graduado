using Microsoft.AspNetCore.Mvc;

namespace back_mapa_graduado.Controllers
{
    [ApiController]
    [Route("graduado")]
    public class GraduadoController : ControllerBase
    {
        SortedList<string,Graduado > graduados = new SortedList<string,Graduado>();


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

        [HttpGet]
        [Route("listar")]
        public dynamic ListarGraduado(string cuil)
        {
           if(graduados.ContainsKey(cuil))
                return graduados[cuil];
            return new
            {
                succes = true,
                message = "Graduado no encontrado"
            };
        }
    }

   }

