namespace THERA.WEBMEDIA
{
    public class Mensaje
    {
        public int id{get; private set;}
        public int idChat{get; private set;}
        public int idTerapeuta{get; private set;}
        public int idPaciente{get; private set;}
        public string mensaje{get; private set;}
        public DateTime hora{get; private set;}
    }
}