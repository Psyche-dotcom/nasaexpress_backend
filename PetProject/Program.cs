using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetProject;
using PetProject.Data.Context;
using PetProject.Data.Repository.Implementation;
using PetProject.Data.Repository.Interface;
using PetProject.Model.Entities;
using PetProject.Service.Implementation;
using PetProject.Service.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
    });

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLibrary(builder.Configuration);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<PetProjectDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<PetProjectDbContext>(dbContextOptions => dbContextOptions.UseNpgsql(builder.Configuration["ConnectionStrings:IdentityConnection"]));
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGenerateJwt, GenerateJwt>();
builder.Services.AddScoped<IEmailServices, EmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
/*}*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
