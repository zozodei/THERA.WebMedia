namespace THERA.Models
{
    public class Nota
    {
        public int id{get;private set;}
        public int idPaciente{get;private set;}
        public int idDisparadora{get;private set;}
        public string titulo{get; private set;}
        public string descripción{get; private set;}
         public DateTime fecha{get; private set;}
        public bool favorito{get; private set;}
        public bool visibleParaTerapeuta{get; private set;}
    }
}