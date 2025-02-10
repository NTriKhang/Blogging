using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.User.Infrastructure;
using Blogging.Common.Infrastructure;
using Blogging.Api.Extensions;
using Microsoft.OpenApi.Models;
using FluentValidation;
using Blogging.Modules.User.Application.Users.RegisterUser;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddInfrastructure(
            []
            ,builder.Configuration.GetConnectionString("Database")!
            , [Blogging.Modules.User.Application.AssemblyReference.Assembly]);

        builder.Services.AddUsersModule(builder.Configuration);

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

        var app = builder.Build();

        app.MapEndpoint([Blogging.Modules.User.Presentation.AssemblyReference.Assembly]);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.ApplyMigrations();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapControllers();

        app.Run();
    }
}