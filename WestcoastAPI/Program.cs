using Microsoft.EntityFrameworkCore;
using WestcoastAPI.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<WestcoastContext>(Options => {
    Options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// builder.Services.AddCors();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try{
    var context = services.GetRequiredService<WestcoastContext>();
    await context.Database.MigrateAsync();

    await SeedData.LoadTeachersData(context);
    await SeedData.LoadCoursesData(context);
    await SeedData.LoadStudentsData(context);
    await SeedData.LoadStudentCourseData(context);
}
catch(Exception ex){
    Console.WriteLine("{0} - {1}", ex.Message, "Något gick fel");
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

    // app.UseCors(c => c.AllowAnyHeader()
    //     .AllowAnyMethod()
    //     .WithOrigins("http://127.0.0.1:5500"));//Min port för live server, kan behöva ändra den att matcha rätt origin

app.MapControllers();

app.Run();
