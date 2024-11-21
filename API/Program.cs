using BLL.Profile;
using BLL.Services.Impl;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

// Register services
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IMedicalOpinionService, MedicalOpinionService>();

builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IMedicalOpinionRepository, MedicalOpinionRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Register UniOfWork
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();

// Register IRepository
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();