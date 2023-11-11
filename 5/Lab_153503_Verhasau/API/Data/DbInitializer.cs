using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            var categories = new List<Category> {
                new Category { Id = 1, Name = "Магнит", NormalizedName = "magneet"},
                new Category { Id = 2, Name = "Открытка", NormalizedName = "otkritka"}
            };
            var _souvenirList = new List<Souvenir> {
                new Souvenir {
                    Id = 1,
                    Name = "Ван Гог - Звездная ночь",
                    Category = categories.Single(x=>x.Name == "Магнит"),
                    Description = "Магнит на холодильник с кассной картиной",
                    Image = "images/night.jpeg",
                    Price = 10M,
                },
                new Souvenir {
                    Id = 2,
                    Name = "Микелянджело - Давид",
                    Category = categories.Single(x => x.Name == "Открытка"),
                    Description = "Открытка с изображением статуи “Давид” работы Микеланджело. На открытке также может быть надпись “Поздравляем с праздником!” или “С наилучшими пожеланиями!”.",
                    Image = "images/david.jpeg",
                    Price = 5.15M,
                },
                new Souvenir
                {
                    Id = 3,
                    Name = "Леонардо да Винчи - Мона Лиза",
                    Category = categories.Single(x => x.Name == "Открытка"),
                    Description = "Открытка со знаменитой улыбкой Джоконды. Может быть использована как подарок к любому празднику.",
                    Image = "images/monalisa.jpeg",
                    Price = 20M,
                },
                new Souvenir
                {
                    Id = 4,
                    Name = "Винсент Ван Гог - Подсолнухи",
                    Category = categories.Single(x => x.Name == "Открытка"),
                    Description = "Картина с изображением подсолнухов известного художника Винсента Ван Гога. Размер картины: 50x50 см. Картина может быть использована как элемент декора или как подарок для ценителей искусства.",
                    Image = "images/sunflower.jpeg",
                    Price = 12.00M,
                },
                new Souvenir
                {
                    Id = 5,
                    Name = "Леонардо да Винчи - Мадонна с младенцем",
                    Category = categories.Single(x => x.Name == "Открытка"),
                    Description = "Открытка с репродукцией знаменитого произведения Леонардо да Винчи “Мадонна с младенцем”. Размеры открытки: 16,5x23,5 см. Идеальный подарок для ценителей искусства и всех, кто интересуется творчеством великого мастера.",
                    Image = "images/madonna.jpeg",
                    Price = 7.5M,
                },
            };

            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            if (!context.Category.Any())
            {
                await context.Category.AddRangeAsync(categories!);
                await context.SaveChangesAsync();
            }

            var imageBaseUrl = app.Configuration.GetValue<string>("ImageUrl");
            if (!context.Souvenir.Any())
            {

                foreach (var souvenir in _souvenirList)
                {
                    souvenir.Image = $"{imageBaseUrl}/{souvenir.Image}";
                }
                await context.Souvenir.AddRangeAsync(_souvenirList);
                await context.SaveChangesAsync();
            }
        }
    }
}
