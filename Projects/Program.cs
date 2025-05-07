using Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Projects;
using Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

        var rabbitSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateProjectConsumer>();
            x.AddConsumer<GetProjectConsumer>();
            x.AddConsumer<GetAllProjectsConsumer>();
            x.AddConsumer<UpdateProjectConsumer>();
            x.AddConsumer<DeleteProjectConsumer>();

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

        builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        host.Run();
    }
}