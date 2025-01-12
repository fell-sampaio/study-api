using Microsoft.EntityFrameworkCore;
using StudyApi.Data.Context;
using AutoMapper.EquivalencyExpression;
using StudyApi.Api.Configuration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddCollectionMappers();
}, typeof(Program));

builder.Services.AddWebApiConfig();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseAuthentication();
//app.UseAuthorization();

app.UseWebApiConfig();

app.Run();
