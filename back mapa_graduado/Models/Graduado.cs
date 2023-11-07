using System.Drawing;

namespace back_mapa_graduado.Controllers
{
    public class Graduado
    {
        private string cuil;
        private string nombre;
        private string mail;
        private string numero;
        private string carrera;
        private string ciudad;
        private string latitud;
        private string longitud;
        //private Bitmap foto;

        public string Cuil { get => cuil; set => cuil = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Latitud { get => latitud; set => latitud = value; }
        public string Longitud { get => longitud; set => longitud = value; }
        //public Bitmap Foto { get => foto; set => foto = value; }

    }
}
