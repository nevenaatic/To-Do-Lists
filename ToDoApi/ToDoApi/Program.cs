using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using ToDoApi.Options;
using ToDoApi.Services;
using ToDoInfrastructure;



var builder = WebApplication.CreateBuilder(args);

// registration for Swagger generator
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<ToDoListContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.Configure<SendGridOptions>(
    builder.Configuration.GetSection(SendGridOptions.SendGrid));
//builder.Services.AddHostedService<ReminderService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
        });
});

builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console()
.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMvc();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "My API V1");
});

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ToDoListContext>();
    dataContext.Database.Migrate();
}

app.Run();

