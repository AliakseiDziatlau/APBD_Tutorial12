using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tutorial12.Domain.Interfaces;
using Tutorial12.Infrastructure.DependencyInjection;
using Tutorial12.Infrastructure.Persistence;
using Tutorial12.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddDatabaseServices(builder.Configuration)
    .AddScopedServices()
    .AddSwagger()
    .AddControllers();

builder.Build()
    .AddSwagger()
    .AddControllerMapping()
    .Run();
