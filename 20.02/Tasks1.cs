using _20._02.Task1;

namespace _20._02
{
    public static class Tasks1
	{
		private static List<Group> groups =
		[
			new Group
			{
				Title = "Group 1",
				Students =
				[
					 new Student
					 {
						 Name = "Alex",
						 Age = 20
					 },
					new Student
					{
						Name = "Joe",
						Age = 21
					}
				]
			},
			new Group
			{
				Title = "Group 2",
				Students =
				[
					new Student
					{
						Name = "Maxim",
						Age = 19
					},
					new Student
					{
						Name = "Ilya",
						Age = 20
					}
				]
			}
		];

		public static void First()
		{
			var anonStudents = groups.SelectMany(group => group.Students,
													(group, student) =>
													new
													{
														Name = student.Name,
														GroupName = group.Title
													});

			Console.WriteLine("\nAnonymous students (student name and group name)\n");

			foreach (var item in anonStudents)
			{
				Console.WriteLine($"Name: {item.Name}, Group name: {item.GroupName}");
			}
		}
		public static void Second()
		{
			var orderedStudents = groups.SelectMany(group => group.Students)
									.OrderBy(student => student.Name);

			Console.WriteLine("\nAll students ordered by name\n");

			foreach (var student in orderedStudents)
			{
				Console.WriteLine(student.Name);
			}
		}

		public static void Third()
		{
			var sortedStudents = groups.SelectMany(group => group.Students)
									   .Where(student => student.Age > 20);

			Console.WriteLine("\nAll students sorted by age\n");

			foreach (var student in sortedStudents)
			{
				Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
			}
		}
	}
}
