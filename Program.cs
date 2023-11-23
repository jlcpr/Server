using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("/", () => "Hello World!");

app.MapPost("/Registrarse/Enviar", UsuariosRequestHandler.Registrarse);

app.MapPost("/inicio/IniciarSesion", UsuariosRequestHandler.Ingresar);

app.MapPost("/Recuperar/Enviar", UsuariosRequestHandler.Recuperar);

app.MapPost("/Crear", CategoriasResquestHandler.Crear);

app.MapGet("/Listar", CategoriasResquestHandler.Listar);

app.MapGet("/lenguaje/{idCategoria}", LenguajeRequestHandler.ListarRegistros);

app.MapPost("lenguaje", LenguajeRequestHandler.CrearRegistro);

app.MapDelete("/lenguaje/{id}", LenguajeRequestHandler.Eliminar);

app.MapGet("lenguaje/buscar", LenguajeRequestHandler.Buscar);
 
app.Run();
