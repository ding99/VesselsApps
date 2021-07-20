using System.Threading.Tasks;

namespace ReaderAPI {
	public interface IReader {
		Task<string> GetBasics();
		Task<string> GetLocations();
	}
}
