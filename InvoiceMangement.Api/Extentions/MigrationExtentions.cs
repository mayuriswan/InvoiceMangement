using InvoiceMangement.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoiceMangement.Api.Extentions
{
    public static class MigrationExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
            dbContext.Seed();

            // Test

        }
    }
}

