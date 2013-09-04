﻿using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Geta.EPi.Cms.Extensions
{
	public static class CategoryListExtensions
	{
		public static string GetCommaSeparatedCategories(this CategoryList categoryList, bool requireAvailable = false, bool requireSelectable = false)
		{
			var categories = categoryList.GetFullCategories();

			if (requireAvailable)
			{
				categories = categories.Where(x => x.Available);
			}

			if (requireSelectable) 
			{
				categories = categories.Where(x => x.Selectable);
			}

			return categories.Any() 
					? string.Join(", ", categories.OrderBy(c => c.LocalizedDescription).Select(c => c.LocalizedDescription)) 
					: string.Empty;
		}

		public static IEnumerable<Category> GetFullCategories(this CategoryList categoryList)
		{
			return categoryList == null 
				? Enumerable.Empty<Category>() 
				: categoryList.Select(Category.Find).Where(category => category != null).ToList();
		}
	}
}