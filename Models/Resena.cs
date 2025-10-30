namespace THERA.Models
{
    public class Resenas
    {
        public int Id {get; private set;}
        public int idTerapeuta {get; private set;}
        public int idPaciente {get; private set;}
        public string Titulo {get; private set;}
        public string Contenido {get; private set;}
        public float rating {get; private set;}
        
    }
}