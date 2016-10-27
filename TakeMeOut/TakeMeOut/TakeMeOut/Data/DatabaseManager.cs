using System;
using System.Collections.Generic;
using SQLite.Net;
using SQLite.Net.Attributes;
using Xamarin.Forms;
using XamarinForms.ViewModels;
using System.Linq;

namespace TakeMeOut
{
	public class DatabaseManager
	{
		static object locker = new object();

		SQLiteConnection database;


		public DatabaseManager()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			// create the tables
			database.CreateTable<Categories>();
		}

		public IEnumerable<Categories> GetCategories()
		{
			lock (locker)
			{
				return (from i in database.Table<Categories>() select i).ToList();
			}
		}

		public Categories GetCategory(string categoryId)
		{
			lock (locker)
			{
				return database.Table<Categories>().FirstOrDefault(x => x.CategoryId == categoryId);
			}
		}

		public IEnumerable<Categories> GetCategoriesByParentId(int parentId)
		{
			lock (locker)
			{
				return database.Query<Categories>("SELECT * FROM [Categories] WHERE [MainCategoryId] =  " + parentId);
			}
		}

		public Categories GetCategory(int id)
		{
			lock (locker)
			{
				return database.Table<Categories>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int SaveItem(Categories item)
		{
			lock (locker)
			{
				if (item.ID != 0)
				{
					database.Update(item);
					return item.ID;
				}
				else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker)
			{
				return database.Delete<Categories>(id);
			}
		}
	}

	public class Categories
	{
		public Categories()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public int Radius { get; set; }
		public string CategoryId { get; set; }
		// arts&entertainment = 1, Nature and parks = 2 and so on
		// You can also create separate table to hold the values  and reference it here
		public int MainCategoryId { get; set; }
	}

	public class MainCategories
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
	}

	// "{
	// "Entertainment" :
	// [
	//	{"Name":"gocart".
	//	"categoryidfoursqareapi":"abcdd",
	//	"radius":1000,
	//	"Image":"default.png",
	//	"ImageNumber":1
	//	},
	//	{"Name":"garden".
	//	"categoryidfoursqareapi":"abcdd",
	//	"radius":1000,
	//	"Image":"default.png",
	//	"ImageNumber":1
	//	},
	//],
	// "Sports":
	// [
	//	{"Name":"gocart".
	//	"categoryidfoursqareapi":"abcdd",
	//	"radius":1000,
	//	"Image":"default.png",
	//	"ImageNumber":1
	//	},
	// ]
	// }"
	//
}
