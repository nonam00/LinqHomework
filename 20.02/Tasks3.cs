using _20._02.Task2;
using System.Xml.Serialization;

namespace _20._02
{
	public static class Tasks3
	{
		private static List<Book> books = new List<Book>();
		private static List<Author> authors = new List<Author>();
		private static List<Publish> publishes = new List<Publish>();

		//Serialization
		public static void SerializeAll()
		{
			SerializeBooks();
			SerializeAuthors();
			SerializePublishes();
		}

		private static void SerializeBooks()
		{
			using (FileStream fs = new FileStream("..\\..\\..\\XmlFiles\\books.xml", FileMode.OpenOrCreate))
			{
				var serializer = new XmlSerializer(typeof(List<Book>));
				serializer.Serialize(fs, books);
			}
		}

		private static void SerializeAuthors()
		{
			using (FileStream fs = new FileStream("..\\..\\..\\XmlFiles\\authors.xml", FileMode.OpenOrCreate))
			{
				var serializer = new XmlSerializer(typeof(List<Author>));
				serializer.Serialize(fs, authors);
			}
		}

		private static void SerializePublishes()
		{
			using (FileStream fs = new FileStream("..\\..\\..\\XmlFiles\\publishes.xml", FileMode.OpenOrCreate))
			{
				var serializer = new XmlSerializer(typeof(List<Publish>));
				serializer.Serialize(fs, publishes);
			}
		}

		
		//Deserialization
		public static void DeserializeAll()
		{
			DeserializeBooks();
			DeserializeAuthors();
			DeserializePublishes();
		}

		private static void DeserializeBooks()
		{
			using (FileStream fs = new FileStream("..\\..\\..\\XmlFiles\\books.xml", FileMode.Open))
			{
				var deserializer = new XmlSerializer(typeof(List<Book>));
				books = (deserializer.Deserialize(fs) as List<Book>)!;
			}
		}

		private static void DeserializeAuthors()
		{
			using (FileStream fs = new FileStream("..\\..\\..\\XmlFiles\\authors.xml", FileMode.Open))
			{
				var deserializer = new XmlSerializer(typeof(List<Author>));
				authors = (deserializer.Deserialize(fs) as List<Author>)!;
			}
		}

		private static void DeserializePublishes()
		{
			using (FileStream fs = new FileStream("..\\..\\..\\XmlFiles\\publishes.xml", FileMode.Open))
			{
				var deserializer = new XmlSerializer(typeof(List<Publish>));
				publishes = (deserializer.Deserialize(fs) as List<Publish>)!;
			}
		}

		//Collection prints
		public static void ShowAllBooks()
		{
			var allBooks = books.Join(authors,
										book => book.IdAuthor,
										author => author.Id,
										(book, author) => new
										{
											book.Title,
											Author = $"{author.FirstName} {author.LastName}",
											book.IdPublish,
											book.Price
										})
								.Join(publishes,
										book => book.IdPublish,
										publish => publish.Id,
										(book, publish) => new
										{
											book.Title,
											book.Author,
											Publish = publish.Title,
											book.Price
										});

			foreach (var book in allBooks)
			{
				Console.WriteLine($"Title: {book.Title}\n" +
					$"Author: {book.Author}\n" +
					$"Publish: {book.Publish}\n" +
					$"Price: {book.Price}\n");
			}
		}

		public static void ShowByAuthor(string author)
		{
			var booksByAuthor = books.Join(authors,
										book => book.IdAuthor,
										author => author.Id,
										(book, author) => new
										{
											book.Title,
											Author = $"{author.FirstName} {author.LastName}",
											book.IdPublish,
											book.Price
										})
								.Join(publishes,
										book => book.IdPublish,
										publish => publish.Id,
										(book, publish) => new
										{
											book.Title,
											book.Author,
											Publish = publish.Title,
											book.Price
										})
								.Where(book => book.Author.Equals(author));

			if(booksByAuthor.Count() == 0)
			{
				Console.WriteLine("There are no such books");
				return;
			}

			Console.WriteLine($"{author} books:\n");

			foreach (var book in booksByAuthor)
			{
				Console.WriteLine($"Title: {book.Title}\n" +
					$"Publish: {book.Publish}\n" +
					$"Price: {book.Price}\n");
			}
			Console.WriteLine();
		}

		public static void ShowByPublish(string publish)
		{
			var booksByAuthor = books.Join(authors,
										book => book.IdAuthor,
										author => author.Id,
										(book, author) => new
										{
											book.Title,
											Author = $"{author.FirstName} {author.LastName}",
											book.IdPublish,
											book.Price
										})
								.Join(publishes,
										book => book.IdPublish,
										publish => publish.Id,
										(book, publish) => new
										{
											book.Title,
											book.Author,
											Publish = publish.Title,
											book.Price
										})
								.Where(book => book.Publish.Equals(publish));

			if (booksByAuthor.Count() == 0)
			{
				Console.WriteLine("There are no such books");
				return;
			}

			Console.WriteLine($"\"{publish}\" publish books:\n");
			foreach (var book in booksByAuthor)
			{
				Console.WriteLine($"Title: {book.Title}\n" +
					$"Author: {book.Author}\n" +
					$"Price: {book.Price}\n");
			}
		}

		public static void ShowByAuthorAndPublish(string author, string publish)
		{
			var booksByAuthor = books.Join(authors,
										book => book.IdAuthor,
										author => author.Id,
										(book, author) => new
										{
											book.Title,
											Author = $"{author.FirstName} {author.LastName}",
											book.IdPublish,
											book.Price
										})
								.Join(publishes,
										book => book.IdPublish,
										publish => publish.Id,
										(book, publish) => new
										{
											book.Title,
											book.Author,
											Publish = publish.Title,
											book.Price
										})
								.Where(book => book.Author.Equals(author) && book.Publish.Equals(publish));

			if (booksByAuthor.Count() == 0)
			{
				Console.WriteLine("There are no such books");
				return;
			}

			Console.WriteLine($"{author} books by \"{publish}\" publishment:\n");
			foreach (var book in booksByAuthor)
			{
				Console.WriteLine($"Title: {book.Title}\n" +
					$"Price: {book.Price}\n");
			}
		}

		// for examples
		public static void ShowAuthors()
		{
			foreach(var author in authors)
			{
				Console.WriteLine(author);
			}
			Console.WriteLine();
		}

		//Collections editing

		//Authors
		public static void AddAuthor(Author author)
		{
			authors.Add(author);
		}

		public static void RemoveAuthor(Guid authorId)
		{
			var author = authors.FirstOrDefault(a => a.Id.Equals(authorId));

			if (author is not null)
			{
				authors.Remove(author);
			}
		}

		public static void UpdateAuthor(Guid authorId, Author updatedAuthor)
		{
			var author = authors.FirstOrDefault(a => a.Id.Equals(authorId));

			if (author is not null)
			{
				author.FirstName = updatedAuthor.FirstName;
				author.LastName = updatedAuthor.LastName;
			}
		}

		// Publishes
		public static void AddPublish(Publish publish)
		{
			publishes.Add(publish);
		}

		public static void RemovePublish(Guid publishId)
		{
			var publish = publishes.FirstOrDefault(p => p.Id.Equals(publishId));

			if (publish is not null)
			{
				publishes.Remove(publish);
			}
		}

		public static void UpdatePublish(Guid publishId, Publish updatedPublish)
		{
			var publish = publishes.FirstOrDefault(p => p.Id.Equals(publishId));

			if (publish is not null)
			{
				publish.Title = updatedPublish.Title;
				publish.City = updatedPublish.City;
			}
		}


		//Books
		public static void AddBook(Book book)
		{
			books.Add(book);
		}

		public static void RemoveBook(string bookTitle)
		{
			var book = books.FirstOrDefault(b => b.Title.Equals(bookTitle));

			if (book is not null)
			{
				books.Remove(book);
			}
		}

		public static void UpdateBook(string oldTitle, Book updatedBook)
		{
			var book = books.FirstOrDefault(b => b.Title.Equals(oldTitle));

			if (book is not null)
			{
				book.IdAuthor = updatedBook.IdAuthor;
				book.IdPublish = updatedBook.IdPublish;
				book.Price = updatedBook.Price;
			}
		}
	}
}
