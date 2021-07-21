using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VesselsApi.Models;
using Xunit;

namespace VesselsApiTest {
	public class BasicDbContextTest {

		public BasicDbContextTest() {
		}

		[Fact]
		public async Task GetAll_ReturnsAllItems() {

			var options = new DbContextOptionsBuilder<BasicDbContext>()
				.UseInMemoryDatabase(databaseName: "VesselDb")
				.Options;

			using (var context = new BasicDbContext(options)) {
				context.basic.Add(new Basic { Id = 1, VesselID = 66, VesselName = "Vessel1", Status = 1 });
				context.basic.Add(new Basic { Id = 8, VesselID = 67, VesselName = "Vessel2", Status = 2 });
				context.SaveChanges();
			}

			using (var context = new BasicDbContext(options)) {
				List<Basic> basics = context.basic.ToListAsync().GetAwaiter().GetResult();
				Assert.Equal(2, basics.Count);
			}
		}
	}
}
