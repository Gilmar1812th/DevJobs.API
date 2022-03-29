using devjobs.api.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DevJobsCs");

builder.Services.AddDbContext<DevJobsContext>(options => 
    // usar banco de dados em memória - usar em nuvem
    //options.UseInMemoryDatabase("DevJobs"));
    // usar sql server banco de dados
    options.UseSqlServer(connectionString));

// como esta utilizando o repository - compartilhar o repositório dentro de cada requisição
// Parâmetros Interface e implementação da interface
builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();    

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Habilitar documentação XML do Swagger
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "DevJobs.API",
        Version = "v1 ",
        Contact = new OpenApiContact {
            Name = "GilmarDev",
            Email = "gilmar1812th@gmail.com",
            Url = new Uri("https://gilmardev.com.br")
        }
    });

    var xmlFile = "DevJobs.API.xml";

    // diretório base
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

// Para usar na nuvem - comente
// configurar Log - contexto de log para o Serilog
builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        // Escrever para o SqlServer
        .WriteTo.MSSqlServer(connectionString, 
            sinkOptions: new MSSqlServerSinkOptions() {
                // criar tabela sql quando for inicializado os logs
                AutoCreateSqlTable = true,
                TableName = "Logs"
            })
//            // se quiser escrever no console
            .WriteTo.Console()
.CreateLogger();
}).UseSerilog(); // vai parar de escrever no console

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
