using InvoiceMangement.Api.Data;
using InvoiceMangement.Api.Extentions;
using InvoiceMangement.Api.Repository.Implementation;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
builder.Services.AddScoped<IInvoiceLineItemRepository, InvoiceLineItemRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();