using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReaderAPI {
	public class Reader : IReader {

		private string urlBase = "http://www.wsdot.wa.gov/Ferries/API/Vessels/rest/";
		private string usr = "?apiaccesscode=8a60a1e1-85d8-4126-8e21-bc606f121daa";

		public async Task<string> GetBasics() {
			StringBuilder url = new StringBuilder(urlBase);
			url.Append("vesselbasics").Append(usr);

			using (var client = new HttpClient()) {
				var response = await client.GetAsync(url.ToString());

				return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync(): "";
			}
		}

		public async Task<string> GetLocations() {
			StringBuilder url = new StringBuilder(urlBase);
			url.Append("vessellocations").Append(usr);

			using (var client = new HttpClient()) {
				var response = await client.GetAsync(url.ToString());
				return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : "";
			}
		}
	}
}
