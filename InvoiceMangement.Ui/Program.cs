using InvoiceMangement.Api.Repository.Implementation;
using InvoiceMangement.Api.Repository.Interface;
using InvoiceMangement.Ui;
using InvoiceMangement.Ui.Services.Implementation;
using InvoiceMangement.Ui.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddMudPopoverService();
builder.Services.AddScoped(serviceProvider =>
{
    // Get base url from appsettings.json
    var baseURL = Environment.GetEnvironmentVariable("BASE_URL") ?? builder.Configuration["BaseURL"];

    // inject httpClient with baseURL
    return new HttpClient { BaseAddress = new Uri(baseURL) };
});
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceDetailsService, InvoiceDetailsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
