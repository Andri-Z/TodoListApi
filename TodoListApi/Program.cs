using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoListApi.Context;
using TodoListApi.Interface;
using TodoListApi.Services;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*Se registra el contexto TodoListContext para la base de datos
    utilizando la cadena de conexion que se encuentra en appssetings.json.*/
builder.Services.AddDbContext<TodoListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*Se crea un enlace entre Itasks y TaskServices para que se inyecte
    automaticamente en todos los servicios o controladores que lo implementen.*/
builder.Services.AddScoped<ITasks, TasksServices>();

//Configura Serilog como el encargado de los Logs.
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .Filter.ByExcluding(LogEvent => LogEvent.Exception != null);
});

/*Aqui configuro el comportamiento por defecto de los controladores
    para que no se validen automaticamente y poder mostrar un mensaje personalizado.*/
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

//Registrar in Middleware global para mostrar un mensaje con las excepciones no controladas.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            mensaje = "Ha ocurrido un error inesperado, intentelo mas tarde."
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();