using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using moveo.Excpetions;
using Requests;
using Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IProjectsService, ProjectsService>();
        builder.Services.AddTransient<ITasksService, TasksService>();

        var app = builder.Build();

         app.UseSwagger();
         app.UseSwaggerUI();
       
        builder.Services.AddMassTransit(x =>
        {
            x.AddRequestClient<CreateProjectRequest>();
            x.AddRequestClient<UpdateProjectRequest>();
            x.AddRequestClient<DeleteProjectRequest>();
            x.AddRequestClient<GetProjectRequest>();
            x.AddRequestClient<GetAllProjectsRequest>();

            x.AddRequestClient<CreateTaskRequest>();
            x.AddRequestClient<UpdateTaskRequest>();
            x.AddRequestClient<DeleteTaskRequest>();
            x.AddRequestClient<GetTaskRequest>();
            x.AddRequestClient<GetAllTasksRequest>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        app.UseHttpsRedirection();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.Authority = "https://cognito-idp.eu-north-1.amazonaws.com/eu-north-1_uGc87AUgt";
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false,
                 RoleClaimType = "cognito:groups"
             };
         });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<ExcepctionMiddelware>();

        app.Run();
    }
}