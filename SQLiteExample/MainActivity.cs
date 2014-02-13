/*
Lucio A. Flores González.
Ejemplo con Licencia GPL.
*/
using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
namespace SQLiteExample
{
	[Activity (Label = "SQLiteExample", MainLauncher = true)]
	public class MainActivity : Activity
	{
		//int count = 1;
		private Android.App.AlertDialog.Builder builder;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			string dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal),"database.db3");
			var db = new SQLiteConnection (dbPath);
			db.CreateTable<Stock> ();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			builder = new AlertDialog.Builder(this);
			AlertDialog alert = builder.Create();
			alert.SetTitle (Resource.String.app_name);
			// Get our button from the layout resource,
			// and attach an event to it
			Button btnCreate = FindViewById<Button> (Resource.Id.btnCreate);
			Button btnShow = FindViewById<Button> (Resource.Id.btnShow);
			Button btnUpdate= FindViewById<Button> (Resource.Id.btnUpdate);
			Button btnDelete= FindViewById<Button> (Resource.Id.btnDelete);
			EditText txtMsj = FindViewById<EditText> (Resource.Id.txtMensaje);
			EditText txtID = FindViewById<EditText> (Resource.Id.txtID);
			
			btnCreate.Click += delegate {
				try{
					var newStock = new Stock();
					newStock.Symbol = txtMsj.Text;
					db.Insert (newStock);
					txtID.Text = string.Format("{0}",newStock.Id);
				}
				catch{
					alert.SetMessage("Error desconocido");
					alert.Show();
				}

			};

			btnShow.Click += delegate {
				try{
					var stock = db.Get<Stock>(System.Convert.ToInt16(txtID.Text));
					alert.SetMessage (string.Format("{0}:{1}",stock.Id, stock.Symbol));
					alert.Show();
				}
				catch{
					alert.SetMessage("Error desconocido");
					alert.Show();
				}
			};

			btnUpdate.Click += delegate {
				try{
					var updateStock = db.Get<Stock>(System.Convert.ToInt16(txtID.Text));
					updateStock.Symbol = txtMsj.Text;
					db.Update(updateStock);
				}
				catch{
					alert.SetMessage("Error desconocido");
					alert.Show();
				}
			};

			btnDelete.Click += delegate {
				try{
					var deleteStock = db.Get<Stock>(System.Convert.ToInt16(txtID.Text));
					deleteStock.Symbol = txtMsj.Text;
					db.Delete(deleteStock);
				}
				catch{
					alert.SetMessage("Error desconocido");
					alert.Show();
				}

			};

		}
		public class Stock
		{
			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }
			[MaxLength(100)]
			public string Symbol { get; set; }
		}
	
	}
}