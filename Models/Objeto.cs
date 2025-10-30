namespace THERA.Models{

using Newtonsoft.Json;

public static class Objeto 
{
    public static string ObjetoATexto<T> (T? objeto) 
    {
        return JsonConvert.SerializeObject(objeto);
    }

    public static T? TextoAObjeto<T> (string texto)
    {
        if(string.IsNullOrEmpty(texto))
        {
            return default;
        } 
        else 
        {
            return JsonConvert.DeserializeObject<T> (texto);

        }

    }

    public static string ListaATexto<T> (List<T> lista) 
    {
        return JsonConvert.SerializeObject(lista);
    }

    public static List<T>? TextoALista<T> (string json)
    {
        if(string.IsNullOrEmpty(json))
        {
            return default;
        } 
        else 
        {
            return JsonConvert.DeserializeObject <List<T>> (json);
        }

    }


}
}