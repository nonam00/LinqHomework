using _20._02;
using _20._02.Task2;

Tasks3.DeserializeAll(); //подгрузка данных из файла

Tasks3.ShowAllBooks();


Guid guid = new Guid("d69ca6ac-abee-4dc8-bf83-6545de685fe4"); // взял из xml-файла для примера

//нужно учитывать, что изменения в коллекциях authors или publishes затронут коллекцию book, так как join по id происходить не будет

Tasks3.RemoveAuthor(guid);
Tasks3.ShowAuthors();

var newAuth = new Author
{
	Id = Guid.NewGuid(),
	FirstName = "Franz",
	LastName = "Kafka"
};

Tasks3.AddAuthor(newAuth);
Tasks3.ShowAuthors();

Tasks3.UpdateAuthor(newAuth.Id, new Author { FirstName = "Mark", LastName = "Twain" });
Tasks3.ShowAuthors();

Tasks3.ShowAllBooks();

//Tasks3.SerializeAll(); // нужен для сохранения изменений в файлы