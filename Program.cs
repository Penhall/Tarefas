using Tarefas.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();
builder.Services.AddCors();
builder.AddPersistence();

var app = builder.Build();

app.MapTarefasEndpoints();

var environment = app.Environment;

app
    .UseExceptionHandling(environment)
    .UseSwaggerEndpoints()
    .UseAppCors();

app.Run();