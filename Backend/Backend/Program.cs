using Business.Classes;
using Business.DataAccess;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:WebApiDatabase"];

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddScoped<IPurchaseOrderBusiness, PurchaseOrderBusiness>();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseCors(c => c
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

var appIp = "0.0.0.0";
var appPort = "6010";
app.Run($"http://{appIp}:{appPort}");