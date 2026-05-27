using DemoCQRS.Application.Members.Commands.Validations;
using DemoCQRS.Domain.Abstractions;
using DemoCQRS.Infrastructure.Context;
using DemoCQRS.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DemoCQRS.CrossCutting.AppDependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
                 this IServiceCollection services,
                 IConfiguration configuration)
    {
        var dbConnection = configuration
                              .GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnection));

        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new NpgsqlConnection(dbConnection);
            connection.Open();
            return connection;
        });

        services.AddValidatorsFromAssembly(Assembly.Load("DemoCQRS.Application"));

        services.AddScoped<IMemberDapperRepository, MemberDapperRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var myHandlers = AppDomain.CurrentDomain.Load("DemoCQRS.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myHandlers);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}
