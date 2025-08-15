namespace Constants;

/// <summary>
/// مدیریت تگ های دیتابیس برای هر بخش
/// </summary>
public static class TagManager
{
	public static Dictionary<string, string> ListTagsToDictionary(this List<string> tagNames)
	{
		var result = new Dictionary<string, string>();

		foreach (var item in tagNames)
		{
			result.Add(item, string.Empty);
			Console.WriteLine(item);
		}
			
		return result;
	}
	
	/// <summary>
	/// دسته بندی ها
	/// </summary>
	public static class Category
	{
		/// <summary>
		/// نمایش در منو وب سایت
		/// </summary>
		public const string ShowInWebMenu = "show-in-web-menu";

		/// <summary>
		/// نمایش در منو نرم افزار موبایل
		/// </summary>
		public const string ShowInMobileMenu = "show-in-mobile-menu";

		/// <summary>
		/// در وب سایت پین باشد
		/// </summary>
		public const string PinWeb = "pin-web";

		/// <summary>
		/// در موبایل پین باشد
		/// </summary>
		public const string PinMobile = "pin-mobile";

		/// <summary>
		/// استفاده در بخش شبیه ساز
		/// </summary>
		public const string UseInVisualizer = "use-in-visualizer";

		public const string UseInVisualizerAllProduct = "use-in-visualizer-all-product";

		/// <summary>
		/// نمایش به عنوان بیشتر بازدید کننده
		/// </summary>
		public const string MostVisited = "most-visited";

		/// <summary>
		/// نمایش به عنوان پرفروش ترین ها
		/// </summary>
		public const string BestSelling = "best-selling";

		/// <summary>
		/// شگفت انگیزها
		/// </summary>
		public const string AmazingOffers = "amazing-offers";

		/// <summary>
		/// جدیدترین ها
		/// </summary>
		public const string Newest = "newest";

		/// <summary>
		/// با بالاترین امتیاز
		/// </summary>
		public const string TopRated = "top-rated";

		/// <summary>
		/// دریافت لیست همه تگ ها
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, string> GetList()
		{
			var result = new Dictionary<string, string>()
			{
				{ ShowInWebMenu, "نمایش در منو وب" },
				{ ShowInMobileMenu, "نمایش در منو موبایل" },
				{ PinWeb, "پین در وب" },
				{ PinMobile, "پین در موبایل" },
				{ UseInVisualizer, "استفاده در شبیه ساز" },
				{ UseInVisualizerAllProduct, "استفاده در شبیه ساز با تمام محصولات" },

				{ Newest, "جدید" },
				{ TopRated, "بیشترین امتیاز" },

				{ AmazingOffers, "شگفت انگیز" },
				{ MostVisited, "بیشترین بازدید" },
				{ BestSelling, "پرفروش" }
			};

			return result;
		}

		/// <summary>
		/// بررسی وجود یک تگ
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static bool CheckTag(string tag)
		{
			var list = GetList();

			return list.Select(x => x.Key).Any(x => x.Equals(tag));
		}

		public static Dictionary<string, string> FindTagField(string? tag)
		{
			if (string.IsNullOrEmpty(tag) == true)
			{
				return new Dictionary<string, string>();
			}

			Dictionary<string, string> tags = GetList();

			var dictSearch = new Dictionary<string, string>();

			foreach (string itemInTag in tag.Split(','))
			{
				string trimmedTag = itemInTag.Trim();
				if (tags.ContainsKey(trimmedTag) == true)
				{
					dictSearch[trimmedTag] = tags[trimmedTag];
				}
			}

			return dictSearch;
		}
	}

	public static class Product
	{
		/// <summary>
		/// استفاده در بخش شبیه ساز
		/// </summary>
		public const string UseInVisualizer = "use-in-visualizer";

		/// <summary>
		/// نمایش به عنوان بیشتر بازدید کننده
		/// </summary>
		public const string MostVisited = "most-visited";

		/// <summary>
		/// نمایش به عنوان پرفروش ترین ها
		/// </summary>
		public const string BestSelling = "best-selling";

		/// <summary>
		/// شگفت انگیزها
		/// </summary>
		public const string AmazingOffers = "amazing-offers";

		/// <summary>
		/// جدیدترین ها
		/// </summary>
		public const string Newest = "newest";

		/// <summary>
		/// با بالاترین امتیاز
		/// </summary>
		public const string TopRated = "top-rated";

		/// <summary>
		/// دریافت لیست همه تگ ها
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, string> GetList()
		{
			var result = new Dictionary<string, string>
			{
				{ UseInVisualizer, "استفاده در شبیه ساز" },

				{ Newest, "جدید" },
				{ TopRated, "بیشترین امتیاز" },

				{ AmazingOffers, "شگفت انگیز" },
				{ MostVisited, "بیشترین بازدید" },
				{ BestSelling, "پرفروش" }
			};

			return result;
		}

		/// <summary>
		/// بررسی وجود یک تگ
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static bool CheckTag(string tag)
		{
			var list = GetList();

			return list.Select(x => x.Key).Any(x => x.Equals(tag));
		}

		public static Dictionary<string, string> FindTagField(string? tag)
		{
			if (string.IsNullOrEmpty(tag) == true)
			{
				return new Dictionary<string, string>();
			}

			Dictionary<string, string> tags = GetList();

			var dictSearch = new Dictionary<string, string>();

			foreach (string itemInTag in tag.Split(','))
			{
				string trimmedTag = itemInTag.Trim();
				if (tags.ContainsKey(trimmedTag) == true)
				{
					dictSearch[trimmedTag] = tags[trimmedTag];
				}
			}

			return dictSearch;
		}
	}

	public static class Shop
	{
		/// <summary>
		/// دریافت لیست همه تگ ها
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, string> GetList()
		{
			var result = new Dictionary<string, string>
			{
				{ "Better", "بهترین ها" },
			};

			return result;
		}

		/// <summary>
		/// بررسی وجود یک تگ
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static bool CheckTag(string tag)
		{
			var list = GetList();

			return list.Select(x => x.Key).Any(x => x.Equals(tag));
		}

		public static Dictionary<string, string> FindTagField(string? tag)
		{
			if (string.IsNullOrEmpty(tag) == true)
			{
				return new Dictionary<string, string>();
			}

			Dictionary<string, string> tags = GetList();

			var dictSearch = new Dictionary<string, string>();

			foreach (string itemInTag in tag.Split(','))
			{
				string trimmedTag = itemInTag.Trim();
				if (tags.ContainsKey(trimmedTag) == true)
				{
					dictSearch[trimmedTag] = tags[trimmedTag];
				}
			}

			return dictSearch;
		}
	}
}