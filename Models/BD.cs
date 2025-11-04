using Microsoft.Data.SqlClient;
using Dapper;
namespace THERA.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=Thera;Integrated Security=True;TrustServerCertificate=True;";
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
        public static List<Frase> levantarFrases()
        {
            List<Frase> frases = new List<Frase>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Frases";
                frases = connection.Query<Frase>(query).ToList();
            }
            return frases;
        }

        public static int Login(string Username, string Contraseña)
        {
            int idUsuario = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id FROM Usuario WHERE username = @pUsername AND Contrasena = @pContraseña";
                idUsuario = connection.QueryFirstOrDefault<int>(query, new { pUsername = Username, pContraseña = Contraseña });
            }
            return idUsuario;
        }
        public static int levantarIdChat(int idPaciente, int idTerapeuta)
        {
            int idChat = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id FROM Chat WHERE IdTerapeuta = @pIdTerapeuta AND IdPaciente = @pIdPaciente";
                idChat = connection.QueryFirstOrDefault<int>(query, new {pIdTerapeuta = idTerapeuta, pIdPaciente = idPaciente});
            }
            return idChat;
        }
        public static List<Mensaje> levantarMensajes(int idChat)
        {
            List<Mensaje> mensajes = new List<Mensaje>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP(15) * FROM Mensaje WHERE IdChat = @pIdChat";
                mensajes = connection.Query<Mensaje>(query, new {pIdChat = idChat}).ToList();
            }
            return mensajes;
        }
        public static Usuario levantarUsuario(int idUsuario)
        {
            Usuario usuario = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuario WHERE Id = @pIdUsuario";
                usuario = connection.QueryFirstOrDefault<Usuario>(query, new {pIdUsuario = idUsuario});
            }
            return usuario;
        }
        public static Paciente levantarPaciente(int idUsuario)
        {
            Paciente Paciente = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Paciente WHERE Id = @pIdUsuario";
                Paciente = connection.QueryFirstOrDefault<Paciente>(query, new {pIdUsuario = idUsuario});
            }
            return Paciente;
        }
        public static void enviarMensaje(string mensaje, int idUsuario, int idChat, bool tipoUsuario)
        {
            if (!tipoUsuario)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Mensaje (IdChat, IdPaciente, Mensaje, Hora) VALUES (@pIdChat, @pIdUsuario, @pMensaje, GETDATE())";
                    connection.Execute(query, new {pIdChat = idChat, pIdUsuario = idUsuario, pMensaje = mensaje});
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Mensaje (IdChat, IdTerapeuta, Mensaje, Hora) VALUES (@pIdChat, @pIdUsuario, @pMensaje, GETDATE())";
                    connection.Execute(query, new {pIdChat = idChat, pIdUsuario = idUsuario, pMensaje = mensaje});
                }
            }
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
                string query = "INSERT INTO Usuario (Username, Contrasena, TipoUsuario) VALUES (@pusername, @pcontraseña, @ptipoDeUsuario)";
                connection.Execute(query, new {pusername = username, pcontraseña = contraseña, ptipoDeUsuario = tipoDeUsuario});
            }
            if(tipoDeUsuario==0){
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Usuario (username, contraseña, tipoDeUsuario) VALUES (@pusername, @pcontraseña, @ptipoDeUsuario)";
                    connection.Execute(query, new {pusername = username, pcontraseña = contraseña, ptipoDeUsuario = tipoDeUsuario});
                }
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
    public static List<int> levantarCantidadResenas()
        {
            List<int> cantidadResenas = new List<int>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT ISNULL(COUNT(r.Id), 0) AS CantidadResenas
                    FROM Terapeuta t
                    LEFT JOIN Resenas r ON t.Id = r.IdTerapeuta
                    GROUP BY t.Id
                    ORDER BY t.Id";
                
                cantidadResenas = connection.Query<int>(query).ToList();
            }
            return cantidadResenas;
        }


        public static bool levantarTipoUsuario(int idUsuario)
        {
            bool tipoDeUsuario;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TipoUsuario FROM Usuario WHERE Id = @pId";
                tipoDeUsuario = connection.QueryFirstOrDefault<bool>(query, new {pId = idUsuario});
            }
            return tipoDeUsuario;   
        }
    
    }
}