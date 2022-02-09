using Adai.Base.Utils;

namespace Adai.Core
{
	/// <summary>
	/// AccessorExample
	/// </summary>
	public class AccessorExample
	{
		/// <summary>
		/// Example
		/// </summary>
		public void Example()
		{
			var person = new Person()
			{
				Age = 4,
				Name = "Adai",
				Place = "Hefei"
			};

			var access = ReflectionHelper.BuildSetter((Person t) => t.Age);

			var accessor = new Accessor<Person>();
			var accessorAge = accessor.Get(x => x.Age);

			access(person, 10);

			accessorAge.Setter(person, 11);
			accessorAge.Getter(person);

			accessorAge[person] = 11;
			var id = accessorAge[person];//11
		}

		/// <summary>
		/// Person
		/// </summary>
		class Person
		{
			public int Age { get; set; }
			public string Name { get; set; }
			public string Place { get; set; }
		}
	}
}
