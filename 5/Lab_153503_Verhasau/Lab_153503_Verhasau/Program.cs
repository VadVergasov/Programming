using Lab_153503_Verhasau.Services.CategoryService;
using Lab_153503_Verhasau.Services.SouvenirService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
builder.Services.AddScoped<ISouvenirService, MemorySouvenirService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
