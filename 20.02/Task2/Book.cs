namespace _20._02.Task2
{
    public class Book
    {
        public string Title { get; set; }
        public Guid IdAuthor { get; set; }
        public Guid IdPublish { get; set; }
        public decimal Price { get; set; }

        public override string ToString() =>
            $"Title: {Title}\n" +
            $"Price: {Price}\n";

    }
}
