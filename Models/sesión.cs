namespace THERA.Models
{
    public class Sesión
    {
        public int Id {get; private set;}
        public int idTerapeuta {get; private set;}
        public int idPaciente {get; private set;}
        public string Anotaciones {get; private set;}
        public string Tarea {get; private set;}
        public string RespuestaPaciente {get; private set;}
        public DateTime Fecha {get; private set;}
        public int Modalidad {get; private set;}
    }
}