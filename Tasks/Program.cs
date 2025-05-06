using Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Repositories;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<ITasksRepository, TasksRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateTaskConsumer>();
    x.AddConsumer<GetTaskConsumer>();
    x.AddConsumer<UpdateTaskConsumer>();
    x.AddConsumer<DeleteTaskConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

host.Run();
