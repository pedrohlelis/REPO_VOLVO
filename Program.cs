using TRABALHO_VOLVO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

// Adicionar meu Exception Handler na Build
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
<<<<<<< HEAD
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
=======
>>>>>>> cb1c4a445598c4c102b50ad09dadd4a7e27b4277
    app.UseHsts();
}
// Adicionar meus controllers
app.MapControllers();
// Adicionar meu Exception Handler em meu app
app.UseExceptionHandler( _ => { });

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
