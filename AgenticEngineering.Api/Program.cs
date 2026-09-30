using AgenticEngineering.Application.Ai;
using AgenticEngineering.Application.Config;
using AgenticEngineering.Application.Workspace;
using AgenticEngineering.Domain.Workspace;
using AgenticEngineering.Infrustructure.Ai;
using AgenticEngineering.Infrustructure.Workspace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Read the AI endpoint from appsettings.json (with a safe fallback)
var aiEndpoint = builder.Configuration["Ai:Endpoint"] ?? "http://localhost:11434/";
var workSpaceName = builder.Configuration["Workspace:ProjectName"];
var workSpaceDirectory = builder.Configuration["Workspace:ProjectPath"];

builder.Services.AddHttpClient<IAiHealthService, OllamaHealthService>(client =>
{
    client.BaseAddress = new Uri(aiEndpoint);
});

builder.Services.AddHttpClient<IOllamaTaskService, OllamaTaskService>(client =>
{
    client.BaseAddress = new Uri(aiEndpoint);
});

builder.Services.Configure<WorkspaceOptions>(
    builder.Configuration.GetSection("Workspace"));


builder.Services.AddScoped<AiSettings>();
builder.Services.Configure<AiSettings>(builder.Configuration.GetSection("Ai"));
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
    app.MapSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();