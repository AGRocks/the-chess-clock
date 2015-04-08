using System;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Xml;
using System.Reflection;
using PCLStorage;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency (typeof(the_chess_clock.Droid.AppSettings))]
namespace the_chess_clock.Droid
{
	public class AppSettings : ISetttingsProvider
	{
		private string configFile;
		private XDocument document;
		IFile file;

		/// <summary>
		/// Constructor that gets the application settings.
		/// </summary>
		public AppSettings (string configFile)
		{
			//appData = ApplicationData.Current.LocalSettings;
			this.configFile = configFile; //"the_chess_clock.Resources.app.config";
			InitStorage ().RunSynchronously ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="the_chess_clock.AppSettings"/> class.
		/// </summary>
		public AppSettings ()
			: this ("app.config")
		{
		}

		public async Task InitStorage ()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			// create a folder, if one does not exist already
			IFolder folder = await rootFolder.CreateFolderAsync ("app-settings", CreationCollisionOption.OpenIfExists);

			// create a file, overwriting any existing file
			this.file = await folder.CreateFileAsync (configFile, CreationCollisionOption.OpenIfExists);
			var xml = await file.ReadAllTextAsync ();
			this.document = string.IsNullOrEmpty(xml) ? new XDocument() : XDocument.Parse (xml);
		}

		public void SetValue (string key, object value)
		{
			this [key] = value.ToString ();
		}

		public T GetValueOrDefault<T> (string key, T defaultValue)
		{
			T value;

			if (!string.IsNullOrEmpty (this [key])) {
				value = (T)(object)this [key];
			} else {
				value = defaultValue;
			}

			return value;
		}

		private string this [string key] {
			get {
				var values = from n in document.Root.Elements ()
				              where n.Name == key
				              select n.Value;
				if (values.Any ()) {
					return values.First ();
				}

				return null;
			}
			set {
				var values = from n in document.Root.Elements ()
				              where n.Name == key
				              select n;
				if (values.Any ()) {
					values.First ().Value = value;
				} else {
					document.Root.Add (new XElement (key, value));
				}

				this.Save ().RunSynchronously ();
			}
		}

		private async Task Save ()
		{
			await file.WriteAllTextAsync (document.ToString ());
		}
	}
}

