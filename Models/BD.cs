using Microsoft.Data.SqlClient;
using Dapper;
namespace THERA.WEBMEDIA
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=NOMBRE ACÁ;IntegratedSecurity=True;TrustServerCertificate=True;"; //AÑADIR NOMBRE DE BASE DE DATOS
        public static List<Nota> levantarNotas(int idPaciente)
        {
            List<Nota> notas = new List<Nota>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notas WHERE idPaciente = @pidPaciente";
                notas = connection.Query<Nota>(query, new {pidPaciente = idPaciente}).ToList();
            }
            return notas;
        }
        public static int Login(string Username, string Contraseña)
        {
            int idUsuario = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id FROM Usuario WHERE username = @pUsername AND contraseña = @pContraseña";
                idUsuario = connection.QueryFirstOrDefault<int>(query, new { pUsername = Username, pContraseña = Contraseña });
            }
            return idUsuario;
        }
        public static void CompartirTarea(Tarea tarea, string usernameCompartir)
        {
             user = ObtenerUsuarioPorUsername(usernameCompartir);
            string query = "INSERT INTO Tarea (Titulo, Descripcion, FechaTarea, Finalizado, IdUsuario) VALUES (@pTitulo, @pDescripcion, @pFechaTarea, @pFinalizado, @pIdUsuario)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { pTitulo = tarea.Titulo, pDescripcion = tarea.Descripcion, pFechaTarea = tarea.FechaTarea, pFinalizado = false, pIdUsuario = user.IdUsuario });
            }
        }
    }
}