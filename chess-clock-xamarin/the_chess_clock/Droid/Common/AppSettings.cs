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
				IFolder folder = rootFolder.CreateFolderAsync ("app-settings", CreationCollisionOption.OpenIfExists).Result;

				// following call maade sync to aviod deadlocks
				this.file = folder.CreateFileAsync (configFile, CreationCollisionOption.OpenIfExists).Result;

				this.isInitialized = true;
			} catch (Exception ex) {
				Debug.WriteLine (ex.Message);
				throw;
			}
		}

		private XDocument Document {
			get {
				if (this.document == null) {
					var xml = ReadFile(this.file).Result;
					this.document = string.IsNullOrEmpty (xml) ? new XDocument (new XElement ("Properties")) : XDocument.Parse (xml);
				}

				return this.document;
			}
		}

		public async Task<string> ReadFile(IFile f){
			return await Task.Run(() => f.ReadAllTextAsync ()).ConfigureAwait(false);
		}

		public string this [string key] {
			get {
				var values = from n in this.Document.Root.Elements ()
				             where n.Name == key
				             select n.Value;
				if (values.Any ()) {
					return values.First ();
				}

				return string.Empty;
			}
			set {
				var values = from n in this.Document.Root.Elements ()
				             where n.Name == key
				             select n;
				if (values.Any ()) {
					values.First ().Value = value.ToString ();
				} else {
					document.Root.Add (new XElement (key, value));
				}
			}
		}

		public async Task Save ()
		{
			await file.WriteAllTextAsync (this.Document.ToString ());
		}


		public bool IsInitialized {
			get { 
				return this.isInitialized;
			}
		}
	}
}

