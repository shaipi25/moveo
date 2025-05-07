using FluentValidation;
using Gateway;
using Gateway.Model.Queries;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middelwares;
using Requests;
using Services;
using Validators;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddTransient<IProjectsService, ProjectsService>();
        builder.Services.AddTransient<ITasksService, TasksService>();

        builder.Services.AddScoped<IValidator<CreateProjectRequestDto>, CreateProjectRequestValidator>();
        builder.Services.AddScoped<IValidator<GetAllProjectsQuery>, GetAllProjectsQueryValidator>();
        builder.Services.AddScoped<IValidator<UpdateProjectRequestDto>, UpdateProjectRequestValidator>();

        builder.Services.AddScoped<IValidator<CreateTaskRequestDto>, CreateTaskRequestValidator>();
        builder.Services.AddScoped<IValidator<GetAllTasksQuery>, GetAllTasksQueryValidator>();
        builder.Services.AddScoped<IValidator<UpdateTaskRequestDto>, UpdateTaskRequestValidator>();

        var rabbitSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // Add JWT Bearer support
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter 'Bearer' [space] and then your valid token.\nExample: Bearer eyJhbGciOiJIUzI1...",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });

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
                cfg.Host(rabbitSettings.HostName, rabbitSettings.VirtualHost, h =>
                {
                    h.Username(rabbitSettings.UserName);
                    h.Password(rabbitSettings.Password);
                });
            });
        });

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

        builder.Services.AddAuthorization(options =>
        {
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<ExcepctionMiddelware>();

        app.Run();
    }
}