using _20._02.Task2;

List<Author> authors =
[
	new Author
	{
		FirstName = "Franz",
		LastName = "Kafka",
		Id = Guid.NewGuid()
	},
	new Author
	{
		FirstName = "Haruki",
		LastName = "Murakami",
		Id = Guid.NewGuid()
	},
	new Author
	{
		FirstName = "Albert",
		LastName = "Camus",
		Id = Guid.NewGuid()
	}
];

List<Publish> publishes =
[
	new Publish
	{
		Title = "Eksmo",
		City = "Moscow",
		Id = Guid.NewGuid()
	},
	new Publish
	{
		Title = "Hornbook",
		City = "Saint Petersburg",
		Id = Guid.NewGuid()
	},
	new Publish
	{
		Title = "Neoclassic",
		City = "Moscow",
		Id = Guid.NewGuid()
	}
];

IEnumerable<Book> books =
[
	new Book
	{
		Title = "Castle",
		Price = 250m,
		IdAuthor = authors[0].Id,
		IdPublish = publishes[1].Id
	},
	new Book
	{
		Title = "The Stranger",
		Price = 425m,
		IdAuthor = authors[2].Id,
		IdPublish = publishes[2].Id
	},
	new Book
	{
		Title = "Norwegian wood",
		Price = 500m,
		IdAuthor = authors[1].Id,
		IdPublish = publishes[0].Id
	},
	new Book
	{
		Title = "1q84",
		Price = 800,
		IdAuthor = authors[1].Id,
		IdPublish = publishes[0].Id
	},
	new Book
	{
		Title = "The Plague",
		Price = 240m,
		IdAuthor = authors[2].Id,
		IdPublish = publishes[2].Id
	},
	new Book
	{
		Title = "Metamorphosis",
		Price = 180,
		IdAuthor = authors[0].Id,
		IdPublish = publishes[1].Id
	}
];

// 1

var publishesWithBooks = publishes.GroupJoin(books,
							publish => publish.Id,
							book => book.IdPublish,
							(publish, books) => new
							{
								Title = publish.Title,
								City = publish.City,
								Books = books
							});

foreach (var publish in publishesWithBooks)
{
	var min = publish.Books.MinBy(book => book.Price);
	var max = publish.Books.MaxBy(book => book.Price);

	Console.WriteLine($"Title: {publish.Title}\n" +
		$"City: {publish.City}\n\n" +
		$"Book with minimal price:\n{min}\n" +
		$"Book with maximum price:\n{max}\n");
}

// 2

var booksByAuthor = books.Join(publishes,
									book => book.IdPublish,
									publish => publish.Id,
									(book, publish) => new
									{
										book.Title,
										book.IdAuthor,
										TitlePublish = publish.Title,
									})
							.Join(authors,
									book => book.IdAuthor,
									author => author.Id,
									(book, author) => new
									{
										FullNameAuthor = $"{author.FirstName} {author.LastName}",
										TitleBook = book.Title,
										book.TitlePublish
									})
							.GroupBy(book => book.FullNameAuthor);

Console.WriteLine("Second Task\n\n");

foreach (var item in booksByAuthor)
{
	Console.WriteLine($"Author full name: {item.Key}\n");
	foreach(var book in item)
	{
		Console.WriteLine($"Title: {book.TitleBook}\n" +
			$"Publish: {book.TitlePublish}\n");
	}
	Console.WriteLine();
}

Console.WriteLine();

var authorsByPublishes = books.Join(publishes,
									book => book.IdPublish,
									publish => publish.Id,
									(book, publish) => new
									{
										book.IdAuthor,
										Publish = publish,
									})
							.Join(authors,
									book => book.IdAuthor,
									author => author.Id,
									(book, author) => new
									{
										book.Publish,
										author
									})
							.GroupBy(x => x.Publish)
							.Select(publish => new
							{
								Publish = publish.Key,
								Authors = publish.Select(x => x.author)
													.Distinct()
							});

Console.WriteLine("The third task:\n\n");
foreach(var publish in authorsByPublishes)
{
	Console.WriteLine($"{publish.Publish}\n" +
		$"Count of publish authors: {publish.Authors.Count()}\n\n" +
		$"List of authors:\n");

	foreach(var author in publish.Authors)
	{
		Console.WriteLine(author);
	}
	Console.WriteLine();
}