namespace THERA.Models
{
    public class Paciente
    {
        public int id {get; private set;}
        public int idTerapeuta {get; private set;}
        public string nombre {get; private set;}
        public string apellido {get; private set;}
        public DateTime fechaNacimiento {get; private set;}
        public string telefono {get; private set;}
        public string ubicacion {get; private set;}
        public DateTime inicioTratamiento {get; private set;}
        public int horarioSesiones{get; private set;}
        public string rasgosPersonalidad{get; private set;}
        public string modoVincularse{get; private set;}
        public string evaluacionGeneral{get; private set;}
        public string observaciones{get; private set;}
        public string correo{get; private set;}
        public string foto {get; private set;}
        public string ocupacion{get; private set;}
        public int idUsuario{get; private set;}
    }
}