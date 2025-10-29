using ProvaAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Aqui criamos o banco em memória (InMemory)
builder.Services.AddDbContext<ProvaApiDbContext>(options => options.UseInMemoryDatabase("ProvaDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Definindo as informações básicas do doc Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ProvaAPI", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    //Ativando no pipeline
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("swagger/v1/swagger.json", "ProvaAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
