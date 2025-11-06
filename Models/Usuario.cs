using Newtonsoft.Json;
namespace THERA.Models
{
    public class Usuario
    {
        [JsonProperty]
        public int id{get; private set;}
        [JsonProperty]
        public string username{get; private set;}
        public string contrasena{get; private set;}
        public int tipoUsuario{get; private set;}
    }
}