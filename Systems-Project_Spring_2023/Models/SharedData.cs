using System.Collections;

namespace Systems_Project_Spring_2023.Models
{
	public class SharedData : IEnumerable
	{
		// Class that combines data from multiple tables (Read-Only data)
		public IEnumerable<Student> studentdetails { get; set; }
		public IEnumerable<Item> itemdetails { get; set; }
		public IEnumerable<Kit> kitdetails { get; set; }
		public IEnumerator GetEnumerator()
		{
			throw new System.NotImplementedException();
		}
	}
}
