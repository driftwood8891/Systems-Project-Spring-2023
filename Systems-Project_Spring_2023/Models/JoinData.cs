namespace Systems_Project_Spring_2023.Models
{
	public class JoinData
	{
		public List<Kit> Kits { get; set; }
		public Kit SelectedKit { get; set; }

		public List<Student> Students { get; set; }

		public Student SelectedStudent { get; set; }

		public List<Item> Items { get; set; }
		public Item SelectedItem { get; set; }

		public List<Location> Locations { get; set; }
	}
}
