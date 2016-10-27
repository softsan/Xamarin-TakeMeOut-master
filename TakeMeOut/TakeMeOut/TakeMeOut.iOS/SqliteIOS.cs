using System;
using System.IO;
using TakeMeOut.iOS;
using Xamarin.Forms;
using XamarinForms.ViewModels;

[assembly: Dependency(typeof(SqliteiOS))]
namespace TakeMeOut.iOS
{
	public class SqliteiOS : ISQLite
	{
		public SqliteiOS()
		{
		}

		#region ISQLite implementation

		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var fileName = "MyDatabase.db3";
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(documentsPath, "..", "Library");
			var path = Path.Combine(libraryPath, fileName);

			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var connection = new SQLite.Net.SQLiteConnection(platform, path);

			return connection;
		}

		#endregion
	}
}
