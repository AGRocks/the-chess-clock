using System;
using System.Xml.Linq;
using System.Linq;
using PCLStorage;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency (typeof(the_chess_clock.Droid.AppSettings))]
namespace the_chess_clock.Droid
{
	public class AppSettings : ISetttingsProvider
	{
		private string configFile;
		private XDocument document;
		IFile file;
		private bool isInitialized;

		/// <summary>
		/// Constructor that gets the application settings.
		/// </summary>
		public AppSettings (string configFile)
		{
			this.configFile = configFile;
			this.Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="the_chess_clock.AppSettings"/> class.
		/// </summary>
		public AppSettings ()
			: this ("app.config")
		{
		}

		public async Task Initialize ()
		{
			try {
				IFolder rootFolder = FileSystem.Current.LocalStorage;

				// create a folder, if one does not exist already
				IFolder folder = await rootFolder.CreateFolderAsync ("app-settings", CreationCollisionOption.OpenIfExists);
				//await folder.DeleteAsync();
				// create a file, overwriting any existing file
				this.file = await folder.CreateFileAsync (configFile, CreationCollisionOption.OpenIfExists);
				var xml = await file.ReadAllTextAsync ();
				this.document = string.IsNullOrEmpty (xml) ? new XDocument(new XElement("Properties")) : XDocument.Parse (xml);
				this.isInitialized = true;
			} catch (Exception ex) {
				Debug.WriteLine (ex.Message);
				throw;
			}
		}

		public string this [string key] {
			get {
				var values = from n in document.Root.Elements ()
				             where n.Name == key
				             select n.Value;
				if (values.Any ()) {
					return values.First ();
				}

				return string.Empty;
			}
			set {
				var values = from n in document.Root.Elements ()
				             where n.Name == key
				             select n;
				if (values.Any ()) {
					values.First ().Value = value.ToString();
				} else {
					document.Root.Add (new XElement (key, value));
				}
			}
		}

		public async Task Save ()
		{
			await file.WriteAllTextAsync (document.ToString ());
		}


		public bool IsInitialized {
			get { 
				return this.isInitialized;
			}
		}
	}
}

