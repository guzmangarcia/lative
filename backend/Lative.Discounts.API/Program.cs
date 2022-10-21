using Lative.Discounts.API.Profiles;
using Lative.Discounts.Domain.Domain;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Models;
using Lative.Discounts.Domain.Utils;
using Lative.Discounts.Infrastructure;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperProfile));

AddTransitiens(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy => { policy.WithOrigins("http://localhost:3000"); });
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
}


app.MapFallback(() => Results.Redirect("/swagger"));

app.MapGet("/get-discounts", () => { return Results.Ok(CreateDiscountsMock()); });


app.UseCors(MyAllowSpecificOrigins);

app.Run();

static void AddTransitiens(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IDb, Db>();
    builder.Services.AddTransient<IEmployeeDiscountQueryService, EmployeeDiscountQueryService>();
    builder.Services.AddTransient<IEmployeeQueryService, EmployeeQueryService>();
    builder.Services.AddTransient<IDiscountQueryService, DiscountQueryService>();
    builder.Services.AddTransient<IEmployeeCompanyStatusQueryService, EmployeeCompanyStatusQueryService>();
    //DI-Container registration
    builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
}


static List<EmployeeDiscount> CreateDiscountsMock()
{
    return new List<EmployeeDiscount>
    {
        new()
        {
            FirstName = "James",
            LastName = "Smith",
            Discount = 10,
            Type = "Permanent"
        },
        new()
        {
            FirstName = "Robert",
            LastName = "Jones",
            Discount = 10,
            Type = "Permanent"
        },
        new()
        {
            FirstName = "Michael",
            LastName = "Brown",
            Discount = 15,
            Type = "Permanent"
        },
        new()
        {
            FirstName = "David",
            LastName = "Wilson",
            Discount = 15,
            Type = "Permanent"
        },
        new()
        {
            FirstName = "William",
            LastName = "Taylor",
            Discount = 15,
            Type = "Permanent"
        },
        new()
        {
            FirstName = "Richard",
            LastName = "Morton",
            Discount = 5,
            Type = "Part-time"
        },
        new()
        {
            FirstName = "Joseph",
            LastName = "White",
            Discount = 5,
            Type = "Part-time"
        },

        new()
        {
            FirstName = "Charles",
            LastName = "Anderson",
            Discount = 5,
            Type = "Part-time"
        },
        new()
        {
            FirstName = "Christopher",
            LastName = "Anderson",
            Discount = 8,
            Type = "Part-time"
        },
        new()
        {
            FirstName = "Daniel",
            LastName = "Wang",
            Discount = 8,
            Type = "Part-time"
        },
        new()
        {
            FirstName = "Matthew",
            LastName = "Li",
            Discount = 5,
            Type = "Intern"
        },
        new()
        {
            FirstName = "Anthony",
            LastName = "Rodriguez",
            Discount = 5,
            Type = "Intern"
        },
        new()
        {
            FirstName = "Mark",
            LastName = "Ryan",
            Discount = 5,
            Type = "Intern"
        },
        new()
        {
            FirstName = "Donald",
            LastName = "Gelbero",
            Discount = 5,
            Type = "Intern"
        },
        new()
        {
            FirstName = "Steven",
            LastName = "Tremblay",
            Discount = 5,
            Type = "Intern"
        },
        new()
        {
            FirstName = "Paul",
            LastName = "Gagnon",
            Discount = 0,
            Type = "Contractor"
        },
        new()
        {
            FirstName = "Andrew",
            LastName = "Evans",
            Discount = 0,
            Type = "Contractor"
        },
        new()
        {
            FirstName = "Joshua",
            LastName = "Davies",
            Discount = 0,
            Type = "Contractor"
        },
        new()
        {
            FirstName = "Kenneth",
            LastName = "Sullivan",
            Discount = 0,
            Type = "Contractor"
        },
        new()
        {
            FirstName = "Kevin",
            LastName = "Rodriguez",
            Discount = 0,
            Type = "Contractor"
        }
    };
}