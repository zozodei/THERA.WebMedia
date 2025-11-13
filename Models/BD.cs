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
        public static int levantarIdChat(int idPaciente, int? idTerapeuta)
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
                string query = "SELECT * FROM Paciente WHERE IdUsuario = @pIdUsuario";
                Paciente = connection.QueryFirstOrDefault<Paciente>(query, new { pIdUsuario = idUsuario });
            }
            return Paciente;
        }
        public static Terapeuta levantarTerapeutaConIdTerapeuta(int idTerapeuta)
        {
            Terapeuta terapeuta = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Terapeuta WHERE Id = @pIdTerapeuta";
                terapeuta = connection.QueryFirstOrDefault<Terapeuta>(query, new {pIdTerapeuta = idTerapeuta});
            }
            return terapeuta;
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
        public static void CrearNuevoChat(int idPaciente, int idTerapeuta){
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Chat (IdTerapeuta, IdPaciente) VALUES (@pIdTerapeuta, @pIdPaciente)";
                connection.Execute(query, new {pIdPaciente = idPaciente, pIdTerapeuta = idTerapeuta});
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
            try{
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "exec Registrarse @pusername, @pcontraseña, @ptipoDeUsuario";
                    connection.Execute(query, new {pusername = username, pcontraseña = contraseña, ptipoDeUsuario = tipoDeUsuario});
                    int idUsuario = Login(username, contraseña);
                    return (idUsuario);
                }
            }catch (SqlException ex){
                if (ex.Message.Contains("ya está en uso"))
                {
                    return -1;
                }
                else
                {
                    return -2;
                }
            }


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
        public static List<Paciente> levantarPacientes(int idTerapeuta)
        {
            List<Paciente> pacientes = new List<Paciente>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Paciente WHERE IdTerapeuta = @pidTerapeuta";
                pacientes = connection.Query<Paciente>(query, new {pidTerapeuta = idTerapeuta}).ToList();
            }
            return pacientes;
        }
        public static void modificarNota(int idNota, string titulo, string descripcion, bool visibleTerapeuta)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Notas SET Titulo = @ptitulo, Descripción = @pdescripcion, Fecha = GETDATE(), VisibleParaTerapeuta = @pvisibleTerapeuta WHERE Id = @pidNota";
                connection.Execute(query, new {ptitulo = titulo, pdescripcion = descripcion, pvisibleTerapeuta = visibleTerapeuta, pidNota = idNota});
            }
        }
            public static void modificarNota(int idNota, string titulo, string descripcion, bool visibleTerapeuta, int idDisparadora)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Notas SET IdDisparadora = @pidDisparadora, Titulo = @ptitulo, Descripción = @pdescripcion, Fecha = GETDATE(), VisibleParaTerapeuta = @pvisibleParaTerapeuta WHERE Id = @pidNota";
                    connection.Execute(query, new {pidDisparadora = idDisparadora, ptitulo = titulo, pdescripcion = descripcion, pvisibleParaTerapeuta = visibleTerapeuta, pidNota = idNota});
                }
            }
        public static void modificarDatosPaciente(int id, string nombre, string apellido, string correo, string ubicacion, DateTime fechaNacimiento, int telefono, string ocupacion, int idObraSocial)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Paciente 
                                SET Nombre = @pnombre, 
                                    Apellido = @papellido, 
                                    Correo = @pcorreo, 
                                    FechaNacimiento = @pfechaNacimiento, 
                                    Telefono = @ptelefono, 
                                    Ocupacion = @pocupacion,
                                    Ubicacion = @pUbicacion,
                                    IdObraSocial = @pidObraSocial
                                WHERE Id = @pid";

                connection.Execute(query, new
                {
                    pnombre = nombre,
                    papellido = apellido,
                    pcorreo = correo,
                    pfechaNacimiento = fechaNacimiento,
                    ptelefono = telefono,
                    pubicacion = ubicacion,
                    pocupacion = ocupacion,
                    pid = id,
                    pidObraSocial = idObraSocial
                });
            }
        }
        public static List<Disparadora> levantarDisparadoras()
        {
            List<Disparadora> disparadoras = new List<Disparadora>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM DisparadorasNotas";
                disparadoras = connection.Query<Disparadora>(query).ToList();
            }
            return disparadoras;
        }
        public static List<ObraSocial> levantarObrasSociales()
        {
            List<ObraSocial> obras = new List<ObraSocial>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Obra Social]";
                obras = connection.Query<ObraSocial>(query).ToList();
            }
            return obras;
        }
        public static void agregarNota(int idPaciente, string titulo, string descripcion, bool visibleTerapeuta, int idDisparadora)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Notas (idPaciente, idDisparadora, titulo, descripción, fecha, favorito, visibleParaTerapeuta) VALUES (@pidPaciente, @pidDisparadora, @ptitulo, @pdescripcion, GETDATE(), 0, @pvisibleParaTerapeuta)";
                connection.Execute(query, new { pidPaciente = idPaciente, pidDisparadora = idDisparadora, ptitulo = titulo, pdescripcion = descripcion, pvisibleParaTerapeuta = visibleTerapeuta });
            }
        }
        

        public static void eliminarNota(int idNota)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Notas WHERE Id = @pidNota";
                connection.Execute(query, new { pidNota = idNota });
            }
        }
        public static List<string> levantarEspecialidadesXTerapeuta(int idTerapeuta)
        {
            List<string> lista = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec ObtenerEspecialidadesPorTerapeuta @pIdTerapeuta";
                lista = connection.Query<string>(query, new { pidTerapeuta = idTerapeuta }).ToList();
            }
            return lista;
        }
        public static List<Frase> levantarFrasesFavTerapeuta(int idTerapeuta)
        {
            List<Frase> lista = new List<Frase>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec ObtenerFrasesFavTerapeuta @pIdTerapeuta";
                lista = connection.Query<Frase>(query, new { pidTerapeuta = idTerapeuta }).ToList();
            }
            return lista;
        }
        public static List<ObraSocial> levantarObrasSocialesXTerapeuta(int idTerapeuta)
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec obtenerObrasSocialesXTerapeuta @pIdTerapeuta";
                lista = connection.Query<ObraSocial>(query, new { pidTerapeuta = idTerapeuta }).ToList();
            }
            return lista;
        }
        public static decimal levantarPromedioRatingPorTerapeuta(int idTerapeuta)
        {
            decimal promedio = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec ObtenerPromedioRatingPorTerapeuta @pIdTerapeuta";
                promedio = connection.QueryFirstOrDefault<decimal>(query, new { pIdTerapeuta = idTerapeuta });
            }
            return promedio;
        }
        public static List<Resenas> levantarResenasPorTerapeuta(int idTerapeuta)
        {
            List<Resenas> lista = new List<Resenas>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec ObtenerResenasXTerapeuta @pIdTerapeuta";
                lista = connection.Query<Resenas>(query, new { pidTerapeuta = idTerapeuta }).ToList();
            }
            return lista;
        }
        public static Sesión levantarUltimaTareaYRespuesta(int idPaciente)
        {
            Sesión ultimaSesion = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec ObtenerUltimaTareaYRespuesta @pIdPaciente";
                ultimaSesion = connection.QueryFirstOrDefault<Sesión>(query, new {pIdPaciente = idPaciente});
            }
            return ultimaSesion;
        }

    }
}