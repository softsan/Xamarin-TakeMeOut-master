using System;
using TakeMeOut.Droid;
using Xamarin.Forms;
using XamarinForms.ViewModels;
using System.IO;
[assembly: Dependency(typeof(SqliteAndroid))]
namespace TakeMeOut.Droid
{
	public class SqliteAndroid: ISQLite
	{
		public SqliteAndroid()
		{
		}

		#region ISQLite implementation

		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var fileName = "MyDatabase.db3";
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, fileName);

			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var connection = new SQLite.Net.SQLiteConnection(platform, path);

			return connection;
		}

		#endregion
	}
}
