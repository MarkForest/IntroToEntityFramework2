using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void AddPublisher(Publisher publisher)
        {
            using (LibraryDataEntities db = new LibraryDataEntities())
            {
                Publisher publisherCurr = db.Publisher.Where(p => p.PublisherName == publisher.PublisherName).FirstOrDefault();
                if(publisherCurr == null)
                {
                    db.Publisher.Add(publisher);
                    db.SaveChanges();
                    Console.WriteLine("New publisher added: "+publisher.PublisherName);
                }
            }
        }
        static void AddBooks(Book book)
        {
            using (LibraryDataEntities db = new LibraryDataEntities())
            {
                Book bookCurr = db.Book.Where(b => b.Title == book.Title).FirstOrDefault();
                if (bookCurr == null)
                {
                    db.Book.Add(book);
                    db.SaveChanges();
                    Console.WriteLine("New book added: " + book.Title);
                }
            }
        }
        static void Init()
        {
            Author author = new Author { FirstName="Ray",
                LastName ="Bradbury"};
            AddAuthor(author);

            author = new Author
            {
                FirstName = "Harry",
                LastName = "Harrison"
            };
            AddAuthor(author);
            author = new Author
            {
                FirstName = "Clifford",
                LastName = "Simak"
            };
            AddAuthor(author);

            Publisher publisher = new Publisher
            {
                PublisherName = "Raindow",
                Address = "Kyiv",
            };
            AddPublisher(publisher);
            publisher = new Publisher
            {
                PublisherName = "Exlibris",
                Address = "Kyiv",
            };
            AddPublisher(publisher);

            Book book = new Book {
                Title = "Way Station",
                IdAuthor = 4,
                IdPublisher = 2,
                Pages = 345,
                Price = 500,
            };
            AddBooks(book);
            book = new Book
            {
                Title = "The Martian Chronics",
                IdAuthor = 2,
                IdPublisher = 2,
                Pages = 345,
                Price = 500,
            };
            AddBooks(book);
            book = new Book
            {
                Title = "Ring Around the Sun",
                IdAuthor = 1,
                IdPublisher = 1,
                Pages = 345,
                Price = 500,
            };
            AddBooks(book);
        }
        static void GetAllBook()
        {
            using (LibraryDataEntities db = new LibraryDataEntities())
            {
                var books = db.Book.OrderBy(b => b.Title).ToList();
                foreach (var book in books)
                {
                    Console.WriteLine($"Book: {book.Title}, Price: {book.Price}, Author: {book.Author.LastName}, Publisher: {book.Publisher.PublisherName}");
                }
            }
            
        }
        static Author GetAuthorByName(string firstName)
        {
            using (LibraryDataEntities db = new LibraryDataEntities())
            {
                var author = (from a in db.Author
                              where a.FirstName == firstName
                              select a).FirstOrDefault();
                return author;
            }
        }
        static void AddAuthor(Author author)
        {
            using (LibraryDataEntities db = new LibraryDataEntities())
            {
                db.Author.Add(author);
                db.SaveChanges();
                Console.WriteLine("New author added: "+author.FirstName);
            }
        }
        static void GetAllAuthors()
        {
            using (LibraryDataEntities db = new LibraryDataEntities())
            {
                var authors = db.Author.Where(a=>a.LastName.StartsWith("A")).ToList();
                authors = (from author in db.Author
                          where author.LastName.StartsWith("A")
                          select author).ToList();
                foreach (var author in authors)
                {
                    Console.WriteLine(author.FirstName+" "+author.LastName);
                }
            }
        }
        static void Main(string[] args)
        {
            //Author author = new Author { FirstName = "Isaac", LastName = "Azimov" };
            //AddAuthor(author);
            //GetAllAuthors();
            //Author author = GetAuthorByName("Isaac");
            //if(author != null)
            //{
            //    Console.WriteLine(author.FirstName+" "+author.LastName);
            //}
            //else
            //{
            //    Console.WriteLine("Автора с таким именем нет!!!");
            //}
            //Init();
            GetAllBook();
            Console.ReadKey();

        }
    }
}
