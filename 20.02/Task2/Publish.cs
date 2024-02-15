namespace _20._02.Task2
{
    public class Publish
    {
        public Guid Id { get; init; }
		public string Title { get; set; }
        public string City { get; set; }

		public override string ToString() =>
            $"Title: {Title}\n" +
            $"City: {City}\n";
	}
}
