using MongoDB.Driver;
public static class UsuariosRequestHandler {

    public static IResult Registrarse(Registro datos) {

        if (string.IsNullOrWhiteSpace(datos.CorreoElectronico)){
            return Results.BadRequest("El correo es requerido");
        }

        if (string.IsNullOrWhiteSpace(datos.Nombre)){
            return Results.BadRequest("El nombre es requerido");
        }

        if (string.IsNullOrWhiteSpace(datos.Contrasena)){
            return Results.BadRequest("La contraseña es requerida");
        }

        //validar los otros dos articulos

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColection<Registro>("Usuarios");
        if (coleccion == null){
            throw new Exception("No existe la coleccioón Usuarios");
        }

        FilterDefinitionBuilder<Registro> filterbuilder = new FilterDefinitionBuilder<Registro>();
        var filter = filterbuilder.Eq(X => X.CorreoElectronico, datos.CorreoElectronico);

        Registro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
        if (usuarioExistente != null){
            return Results.BadRequest($"Ya existe un usuario con el correo {datos.CorreoElectronico}");
        }
        
        coleccion.InsertOne(datos);

        return Results.Ok();
    }

    public static IResult Ingresar(inicio datos) {

        if (string.IsNullOrWhiteSpace(datos.CorreoElectronico)){
            return Results.BadRequest("El correo es requerido");
        }

        if (string.IsNullOrWhiteSpace(datos.Contrasena)){
            return Results.BadRequest("La contraseña es requerida");
        }

        //validar los otros dos articulos

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColection<Registro>("Usuarios");
        if (coleccion == null){
            throw new Exception("No existe la colección Usuarios");
        }

        FilterDefinitionBuilder<Registro> filterbuilder = new FilterDefinitionBuilder<Registro>();
        var filter = filterbuilder.Eq(X => X.CorreoElectronico, datos.CorreoElectronico);

        Registro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
        if (usuarioExistente == null){
            return Results.BadRequest($"No existe un usuario con el correo {datos.CorreoElectronico}");
        }

        if (usuarioExistente.Contrasena != datos.Contrasena){
            return Results.BadRequest("Usuario o contraseña incorrectas");
        }

        return Results.Ok();
    } 

        public static IResult Recuperar(RecuperarContrasena datos) {

        if (string.IsNullOrWhiteSpace(datos.CorreoElectronico)){
            return Results.BadRequest("El correo es requerido");
        }

        //validar los otros dos articulos

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColection<Registro>("Usuarios");
        if (coleccion == null){
            throw new Exception("No existe la colección Usuarios");
        }

        FilterDefinitionBuilder<Registro> filterbuilder = new FilterDefinitionBuilder<Registro>();
        var filter = filterbuilder.Eq(X => X.CorreoElectronico, datos.CorreoElectronico);

        Registro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
        if (usuarioExistente == null){
            return Results.BadRequest($"No existe un usuario con el correo {datos.CorreoElectronico}");
        }else {
            Correos c = new Correos();
            c.Destinatario = usuarioExistente.CorreoElectronico;
            c.Asunto = "Recuperar Contraseña";
            c.Mensaje = $"Tu contraseña es \"{usuarioExistente.Contrasena}\" 😸";
            c.Enviar();
        }

        return Results.Ok();
    }   

}