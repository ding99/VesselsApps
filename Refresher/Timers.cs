using System;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace Refresher {
	public class Timers {

		private string baseUrl = "http://localhost:42224/api/basic/";

		public void Start() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Timers");

			this.Init();

			Timer basic = new Timer(3600000);
			basic.Elapsed += OnBasic;
			basic.AutoReset = true;
			basic.Enabled = true;

			Timer location = new Timer(300000);
			location.Elapsed += OnLocation;
			location.AutoReset = true;
			location.Enabled = true;

			Console.WriteLine("Press Enter key to stop the timer service");
			Console.ReadLine();

			basic.Stop();
			basic.Dispose();

			location.Stop();
			location.Dispose();

			Console.ResetColor();
		}

		private void Init() {
			StringBuilder url = new StringBuilder(baseUrl);
			url.Append("refresh");

			using (var client = new HttpClient()) {
				var response = client.GetAsync(url.ToString()).GetAwaiter().GetResult();
				Console.WriteLine($"Updated DB [{url}], Success Status: [{response.IsSuccessStatusCode}]");
			}

			url = new StringBuilder(baseUrl);
			url.Append("cache");

			using (var client = new HttpClient()) {
				var response = client.GetAsync(url.ToString()).GetAwaiter().GetResult();
				Console.WriteLine($"Set Cache [{url}], Success Status: [{response.IsSuccessStatusCode}]");
			}
		}

		private void OnBasic(object source, ElapsedEventArgs e) {
			StringBuilder url = new StringBuilder(baseUrl);
			url.Append("refresh");

			using (var client = new HttpClient()) {
				var response = client.GetAsync(url.ToString()).GetAwaiter().GetResult();
				Console.WriteLine($"{e.SignalTime} Refresh DB [{url}], Success Status: [{response.IsSuccessStatusCode}]");
			}
		}

		private void OnLocation(object source, ElapsedEventArgs e) {
			StringBuilder url = new StringBuilder(baseUrl);
			url.Append("cache");

			using (var client = new HttpClient()) {
				var response = client.GetAsync(url.ToString()).GetAwaiter().GetResult();
				Console.WriteLine($"{e.SignalTime} Refresh Cache [{url}], Success Status: [{response.IsSuccessStatusCode}]");
			}
		}
	}
}
