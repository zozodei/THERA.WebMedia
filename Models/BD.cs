using Microsoft.Data.SqlClient;
using Dapper;
namespace THERA.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=Thera;IntegratedSecurity=True;TrustServerCertificate=True;";
        public static List<Nota> levantarDiario(int idPaciente)
        {
            List<Nota> diario = new List<Nota>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notas WHERE idPaciente = @pidPaciente";
                diario = connection.Query<Nota>(query, new {pidPaciente = idPaciente}).ToList();
            }
            return diario;
        }
        public static List<Audio> levantarAudios()
        {
            List<Audio> audios = new List<Audio>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Audios";
                audios = connection.Query<Audio>(query).ToList();
            }
            return audios;
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
        // public static void CompartirTarea(Tarea tarea, string usernameCompartir)
        // {
        //     user = ObtenerUsuarioPorUsername(usernameCompartir);
        //     string query = "INSERT INTO Tarea (Titulo, Descripcion, FechaTarea, Finalizado, IdUsuario) VALUES (@pTitulo, @pDescripcion, @pFechaTarea, @pFinalizado, @pIdUsuario)";
        //     using (SqlConnection connection = new SqlConnection(_connectionString))
        //     {
        //         connection.Execute(query, new { pTitulo = tarea.Titulo, pDescripcion = tarea.Descripcion, pFechaTarea = tarea.FechaTarea, pFinalizado = false, pIdUsuario = user.IdUsuario });
        //     }
        // }
        public static int Registro(string username, string contraseña, int tipoDeUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuario (username, contraseña, tipoDeUsuario) VALUES (@pusername, @pcontraseña, @ptipoDeUsuario)";
                connection.Execute(query, new {pusername = username, pcontraseña = contraseña, ptipoDeUsuario = tipoDeUsuario});
            }

            int idUsuario = Login(username, contraseña);

            return (idUsuario);

        }
        public static Nota levantarNota(int idNota)
        {
            Nota nota = new Nota();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notas WHERE id = @pidNota";
                nota = connection.QueryFirstOrDefault<Nota>(query, new {pidNota = idNota});
            }
            return nota;
        }
        public static List<Terapeuta> levantarTerapeutas()
        {
            List<Terapeuta> terapeutas = new List<Terapeuta>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Terapeuta";
                terapeutas = connection.Query<Terapeuta>(query).ToList();
            }
            return terapeutas;
        }
    }
}