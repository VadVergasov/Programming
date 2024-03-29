﻿using Domain.Entities;
using Domain.Models;
using Lab_153503_Verhasau.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Lab_153503_Verhasau.Services.SouvenirService
{
	public class MemorySouvenirService : ISouvenirService
	{
		private readonly ICategoryService _categoryService;
		private readonly List<Souvenir> _souvenirList;
		private readonly int _pageSize;
		public MemorySouvenirService([FromServices] IConfiguration config, ICategoryService categoryService)
		{
			_categoryService = categoryService;
			_pageSize = Convert.ToInt32(config["ItemsPerPage"]);
			_souvenirList = new List<Souvenir> {
				new Souvenir {
					Id = 1,
					Name = "Ван Гог - Звездная ночь",
					Category = _categoryService.GetCategoryAsync().Result.Data.Single(x=>x.Name == "Магнит"),
					Description = "Магнит на холодильник с кассной картиной",
					Image = "images/night.jpeg",
					Price = 10M,
				},
				new Souvenir {
					Id = 2,
					Name = "Микелянджело - Давид",
					Category = _categoryService.GetCategoryAsync().Result.Data.Single(x => x.Name == "Открытка"),
					Description = "Открытка с изображением статуи “Давид” работы Микеланджело. На открытке также может быть надпись “Поздравляем с праздником!” или “С наилучшими пожеланиями!”.",
					Image = "images/david.jpeg",
					Price = 5.15M,
				},
				new Souvenir
				{
					Id = 3,
					Name = "Леонардо да Винчи - Мона Лиза",
					Category = _categoryService.GetCategoryAsync().Result.Data.Single(x => x.Name == "Открытка"),
					Description = "Открытка со знаменитой улыбкой Джоконды. Может быть использована как подарок к любому празднику.",
					Image = "images/monalisa.jpeg",
					Price = 20M,
				},
				new Souvenir
				{
					Id = 4,
					Name = "Винсент Ван Гог - Подсолнухи",
					Category = _categoryService.GetCategoryAsync().Result.Data.Single(x => x.Name == "Открытка"),
					Description = "Картина с изображением подсолнухов известного художника Винсента Ван Гога. Размер картины: 50x50 см. Картина может быть использована как элемент декора или как подарок для ценителей искусства.",
					Image = "images/sunflower.jpeg",
					Price = 12.00M,
				},
				new Souvenir
				{
					Id = 5,
					Name = "Леонардо да Винчи - Мадонна с младенцем",
					Category = _categoryService.GetCategoryAsync().Result.Data.Single(x => x.Name == "Открытка"),
					Description = "Открытка с репродукцией знаменитого произведения Леонардо да Винчи “Мадонна с младенцем”. Размеры открытки: 16,5x23,5 см. Идеальный подарок для ценителей искусства и всех, кто интересуется творчеством великого мастера.",
					Image = "images/madonna.jpeg",
					Price = 7.5M,
				},
			};
		}

		public Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		public Task DeleteSouvenirAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<Souvenir>> GetSouvenirByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<ListModel<Souvenir>>> GetSouvenirListAsync(string? categoryNormalizedName, int pageNo = 0)
		{
			ResponseData<ListModel<Souvenir>> result = new() { Data = new ListModel<Souvenir>() };
			List<Souvenir> list;
			if (categoryNormalizedName == null)
			{
				list = _souvenirList.ToList();
			}
			else
			{
				list = _souvenirList.Where(x => x.Category.NormalizedName == categoryNormalizedName).ToList();
			}
			result.Data.Items = list.Skip(_pageSize * pageNo).Take(_pageSize).ToList();
			result.Data.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(list.Count) / Convert.ToDouble(_pageSize)));
			result.Data.CurrentPage = pageNo;
			return Task.FromResult(result);
		}

		public Task UpdateSouvenirAsync(int id, Souvenir souvenir, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}
	}
}
