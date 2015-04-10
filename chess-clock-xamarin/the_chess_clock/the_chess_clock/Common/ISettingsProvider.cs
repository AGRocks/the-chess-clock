using System;
using System.Threading.Tasks;

namespace the_chess_clock
{
	public interface ISetttingsProvider
	{
		Task Initialize ();
		string this [string param]{ get; set; }
		Task Save();
	}
}