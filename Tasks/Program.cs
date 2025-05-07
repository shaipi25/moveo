using Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Requests;
using Tasks;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<ITasksRepository, TasksRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

var rabbitSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateTaskConsumer>();
    x.AddConsumer<GetTaskConsumer>();
    x.AddConsumer<UpdateTaskConsumer>();
    x.AddConsumer<DeleteTaskConsumer>();

    x.AddRequestClient<GetProjectRequest>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitSettings.HostName, rabbitSettings.VirtualHost, h =>
        {
            h.Username(rabbitSettings.UserName);
            h.Password(rabbitSettings.Password);
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
