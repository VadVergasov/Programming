using Lab_153503_Verhasau.Models;
using Lab_153503_Verhasau.Services.CategoryService;
using Lab_153503_Verhasau.Services.SouvenirService;
using WEB_153505_Vlasenko.Services.CartService;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
//builder.Services.AddScoped<ISouvenirService, MemorySouvenirService>();
UriData uriData = builder.Configuration.GetSection("UriData").Get<UriData>()!;
builder.Services.AddHttpClient<ISouvenirService, ApiSouvenirService>(client =>
{
    client.BaseAddress = new Uri(uriData.ApiUri);
});

builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(client =>
{
    client.BaseAddress = new Uri(uriData.ApiUri);
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
})
.AddCookie("cookie")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority =
    builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
    options.ClientId =
    builder.Configuration["InteractiveServiceSettings:ClientId"];
    options.ClientSecret =
    builder.Configuration["InteractiveServiceSettings:ClientSecret"];
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ResponseType = "code";
    options.ResponseMode = "query";
    options.SaveTokens = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages().RequireAuthorization();

app.Run();
