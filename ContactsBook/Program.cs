using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ContactsBook.Domain.Core;
using ContactsBook.Domain.Interfaces;
using ContactsBook.Domain.Services;
using ContactsBook.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var host = builder.Configuration.GetValue<string>("postgres-host");
    var user = builder.Configuration.GetValue<string>("postgres-user");
    var pass = builder.Configuration.GetValue<string>("postgres-pass");
    var db = builder.Configuration.GetValue<string>("postgres-contacts-db");
    var connstr = $"Host={host};Database={db};Username={user};Password={pass};SSL Mode=Prefer";

    options.UseNpgsql(connstr);
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IContactDomainService, ContactDomainService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                if (context.HostingEnvironment.IsProduction())
                {
                    var builtConfig = config.Build();

                    var secretClient = new SecretClient(new Uri($"https://{builtConfig["keyVaultName"]}.vault.azure.net/"),
                                      new DefaultAzureCredential());

                    config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                }
            });

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();