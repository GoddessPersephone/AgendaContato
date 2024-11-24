using AgendaContatoApi.Data;
using AgendaContatoApi.Interface;
using AgendaContatoApi.Repositorio;
using Microsoft.EntityFrameworkCore;
using AgendaContatoApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlite("Data Source=agenda.db")); // Configuração do SQLite
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAgendaContatoRepository, AgendaContatoRepository>();
builder.Services.AddScoped<IAgendaContatoService, AgendaContaoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AgendaContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();