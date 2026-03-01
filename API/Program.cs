using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.Infrastructure.Data;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); // extension method to add application services like db context, repositories, automapper, swagger, etc.


// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// //builder.Services.AddOpenApi();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();



// builder.Services.AddDbContext<StoreContext>(options =>
// {
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
// builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// builder.Services.Configure<ApiBehaviorOptions>(options =>
// {
//     options.InvalidModelStateResponseFactory = actionContext =>
//     {
//         var errors = actionContext.ModelState
//         .Where(e => e.Value.Errors.Count > 0)
//         .SelectMany(x => x.Value.Errors)
//         .Select(x => x.ErrorMessage).ToArray();

//         var errorResponse = new ApiValidationErrorResponse
//         {
//             Errors = errors
//         };

//         return new BadRequestObjectResult(errorResponse); 
//     };
// });


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); // to handle exceptions globally and return custom error responses

// Configure the HTTP request pipeline.
app.UseStatusCodePagesWithReExecute("/error/{0}"); // to handle status code pages and redirect to error controller

// if (app.Environment.IsDevelopment())
// {
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection(); // when we use https redirection  

app.UseStaticFiles(); // to serve static files like images, css, js from wwwroot folder
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during migration or seeding the database.");
}

app.Run();
