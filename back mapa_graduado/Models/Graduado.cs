﻿using System.Drawing;

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
        private dynamic foto;

        public Graduado(string cuil, string nombre, string mail, string numero, string carrera, string ciudad, string latitud, string longitud)
        {
            this.cuil = cuil;
            this.nombre = nombre;
            this.mail = mail;
            this.numero = numero;
            this.carrera = carrera;
            this.ciudad = ciudad;
            this.latitud = latitud;
            this.longitud = longitud;
        }

        public string Cuil { get => cuil; set => cuil = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Latitud { get => latitud; set => latitud = value; }
        public string Longitud { get => longitud; set => longitud = value; }
        public dynamic Foto { get => foto; set => foto = value; }

    }
}
