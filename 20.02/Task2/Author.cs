namespace _20._02.Task2
{
    public class Author
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

		public override string ToString() =>
            $"First name: {FirstName}\n" +
            $"Last name: {LastName}\n";
	}
}
