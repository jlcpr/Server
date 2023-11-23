using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;

public static class LenguajeRequestHandler {
    public static IResult ListarRegistros(string idCategoria){
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x=> x.IdCategoria, idCategoria);

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColection<LenguajeDbMap>("Lenguaje");
        var lista = coleccion .Find(filter).ToList();

        return Results.Ok(lista.Select(x => new {
            Id = x.Id.ToString(),
            idCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.EsVideo,
            Url = x.Url
        }).ToList());
    }

    public static IResult CrearRegistro(LenguajeDTO datos){

        if(string.IsNullOrWhiteSpace(datos.IdCategoria)){
            return Results.BadRequest("Se necesita el Id de la categoria");
        }

        if(string.IsNullOrWhiteSpace(datos.Descripcion)){
            return Results.BadRequest("Se necesita la descripcion");
        }

        if(string.IsNullOrWhiteSpace(datos.Titulo)){
            return Results.BadRequest("Se necesita el titulo");
        }

        if(string.IsNullOrWhiteSpace(datos.Url)){
            return Results.BadRequest("Se necesita el Url");
        }

        if(!ObjectId.TryParse(datos.IdCategoria, out ObjectId idCategoria)){
            return Results.BadRequest($"El Id de la categoria ({datos.IdCategoria}) no es valido");
        }
        BaseDatos bd = new BaseDatos();

        //Validar que exista la categoria
        var filterBuilderCategorias = new FilterDefinitionBuilder<CategoriaDbMap>();
        var filterCategoria = filterBuilderCategorias.Eq(x => x.Id, idCategoria);
        var collecionCategoria = bd.ObtenerColection<CategoriaDbMap>("Categorias");
        var categoria = collecionCategoria.Find(filterCategoria).FirstOrDefault();

        if (categoria == null){
            return Results.NotFound($"No existe una categoria con ID = '{datos.IdCategoria}'");
        }

        LenguajeDbMap registro = new LenguajeDbMap();
        registro.Titulo = datos.Titulo;
        registro.EsVideo = datos.EsVideo;
        registro.Descripcion = datos.Descripcion;
        registro.Url = datos.Url;
        registro.IdCategoria = datos.IdCategoria;

        var coleccionLenguaje = bd.ObtenerColection<LenguajeDbMap>("Lenguaje");
        coleccionLenguaje!.InsertOne(registro);

        return Results.Ok(registro.Id.ToString());
    }


    public static IResult Buscar(string texto){
        var queryExpr = new BsonRegularExpression(new Regex(texto, RegexOptions.IgnoreCase));
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Regex("Titulo", queryExpr) |
            filterBuilder.Regex("Descripcion", queryExpr);

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColection<LenguajeDbMap>("Lenguaje");
        var lista = coleccion. Find (filter).ToList();

        return Results.Ok(lista.Select(x => new {
            Id = x.Id.ToString(),
            IdCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.EsVideo,
            Url = x.Url
        }).ToList());
    }
    
    public static IResult Eliminar(string id){
        if (!ObjectId.TryParse(id, out ObjectId idLenguaje)){
            return Results.BadRequest($"El Id proporcionado ({id}) no es válido");
        }

        BaseDatos bd = new BaseDatos();
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x => x.Id, idLenguaje);
        var coleccion = bd.ObtenerColection<LenguajeDbMap>("Lenguaje");
        coleccion!.DeleteOne(filter);

        return Results.NoContent();
    }
}