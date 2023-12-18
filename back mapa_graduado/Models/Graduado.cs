using Microsoft.JSInterop.Infrastructure;
using System.Drawing;

namespace back_mapa_graduado.Controllers
{
    public class Graduado
    {

        public string Dni { get ; set ; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Numero { get; set; }
        public string Carrera { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Ciudad { get; set; }      
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public dynamic Foto { get; set; }
        public string Password { get; set; }

    }
}
