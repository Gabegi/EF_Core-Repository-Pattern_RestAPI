using BrandApplication.Business.Mappings;
using BrandApplication.Business.Services.IServices;
using BrandApplication.Business.Services;
using BrandApplication.DataAccess;
using BrandApplication.DataAccess.Interfaces;
using BrandApplication.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using BrandApplication.Business.Services.IServices.IServiceMappings;
using BrandApplication.Business.Services.ServiceMappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


///// Data Base Configuration
builder.Services.AddScoped<BrandDbContext>();

builder.Services.AddDbContext<BrandDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("Plutus.ProductPricing.DataAccess");
        });
});

//// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

//// Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//// Generic Services
builder.Services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));
builder.Services.AddScoped(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));

//////////////////////////////////// Services ////////////////////////////////////

// Asset Mappings
builder.Services.AddScoped(typeof(IBrandMapping), typeof(BrandMapping));


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
