namespace Tarefas.Endpoints;

using Microsoft.EntityFrameworkCore;
using Tarefas.Data;

public static class TarefasEndpoints
{
    public static void MapTarefasEndpoints(this WebApplication app)
    {
        app.MapGet("/tarefas", async (TarefaDbContext db) =>
            await db.Tarefas.ToListAsync())
            .WithName("GetTarefas");

        app.MapGet("/tarefas/{id}", async (int id, TarefaDbContext db) =>
            await db.Tarefas.FindAsync(id)
                is Tarefa tarefa
                    ? Results.Ok(tarefa)
                    : Results.NotFound())
            .WithName("GetTarefaById")
            .Produces<Tarefa>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        app.MapPost("/tarefas", async (Tarefa tarefa, TarefaDbContext db) =>
        {
            if (tarefa != null)
            {
                db.Tarefas.Add(tarefa);
                await db.SaveChangesAsync();

                return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
            }
            else
            {
                return Results.BadRequest("Request inválido");
            }
        }
        ).WithName("CreateTarefa")
         .ProducesValidationProblem()
         .Produces<Tarefa>(StatusCodes.Status201Created);

        app.MapPut("/tarefas/{id}", async (int id, Tarefa inputTarefa, TarefaDbContext db) =>
        {
            var tarefa = await db.Tarefas.FindAsync(id);

            if (tarefa is null) return Results.NotFound();

            tarefa.Titulo = inputTarefa.Titulo;
            tarefa.IsCompleta = inputTarefa.IsCompleta;

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
            .WithName("UpdateTarefa")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        app.MapDelete("/tarefas/{id}", async (int id, TarefaDbContext db) =>
        {
            if (await db.Tarefas.FindAsync(id) is Tarefa tarefa)
            {
                db.Tarefas.Remove(tarefa);
                await db.SaveChangesAsync();
                return Results.Ok(tarefa);
            }

            return Results.NotFound();
        })
        .WithName("DeleteTarefa")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        app.MapDelete("/tarefas/delete-tarefas", async (TarefaDbContext db) =>
          Results.Ok(await db.Database.ExecuteSqlRawAsync("DELETE FROM Tarefas")))
          .WithName("DeleteTarefas")
          .Produces<int>(StatusCodes.Status200OK);
       }
  }
