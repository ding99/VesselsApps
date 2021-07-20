namespace VesselsApi.Models {
	public class Location {
		public int Id { get; set; }
		public int VesselID { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double Speed { get; set; }
		public int Heading { get; set; }
	}
}
