using Library.Books;
using Library.Members;
using Library.Operations;

namespace Library.Books
{
    public enum Genre
    {
        Fiction,
        NonFiction,
        Science,
        History,
        Fantasy,
        Mystery
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Book(string title, string author, Genre genre)
        {
            Title = title;
            Author = author;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} — {Author} ({Genre}) - {(IsAvailable ? "Available" : "issued")}";
        }
    }
}


namespace Library.Members
{
    public class Member
    {
        public string Name { get; set; }
        public int TicketNumber { get; set; }

        public Member(string name, int ticketNumber)
        {
            Name = name;
            TicketNumber = ticketNumber;
        }

        public override string ToString()
        {
            return $"{Name} (Ticket №{TicketNumber})";
        }
    }
}

namespace Library.Operations
{
    public class LibraryManager
    {
        private List<Book> books = new List<Book>();
        private List<Member> members = new List<Member>();

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void AddMember(Member member)
        {
            members.Add(member);
        }

        public void DisplayBooks()
        {
            Console.WriteLine("\nList of books:");
            foreach (var book in books)
                Console.WriteLine(book);
        }

        public void DisplayMembers()
        {
            Console.WriteLine("\n/List of library members:");
            foreach (var member in members)
                Console.WriteLine(member);
        }

        public void BorrowBook(string title, int ticketNumber)
        {
            var book = books.Find(b => b.Title == title);
            var member = members.Find(m => m.TicketNumber == ticketNumber);

            if (book == null)
                Console.WriteLine("Book not found.");
            else if (member == null)
                Console.WriteLine("No member with this ticket found.");
            else if (!book.IsAvailable)
                Console.WriteLine("The book has already been published.");
            else
            {
                book.IsAvailable = false;
                Console.WriteLine($"{member.Name} took the book \"{book.Title}\".");
            }
        }

        public void ReturnBook(string title)
        {
            var book = books.Find(b => b.Title == title);
            if (book == null)
                Console.WriteLine("Book not found.");
            else if (book.IsAvailable)
                Console.WriteLine("The book has already been returned..");
            else
            {
                book.IsAvailable = true;
                Console.WriteLine($"Book \"{book.Title}\" returned.");
            }
        }
    }
}


class Program
{
    static void Main()
    {
        var manager = new LibraryManager();

        manager.AddBook(new Book("1984", "George Orwell", Genre.Fiction));
        manager.AddBook(new Book("A brief history of time", "Hoking", Genre.Science));

        manager.AddMember(new Member("Daryna", 101));
        manager.AddMember(new Member("Tolik", 102));

        manager.DisplayBooks();
        manager.DisplayMembers();

        Console.WriteLine("\nOperation:");
        manager.BorrowBook("1984", 101);
        manager.BorrowBook("1984", 102);  

        manager.ReturnBook("1984");
        manager.BorrowBook("1984", 102);  

        manager.DisplayBooks();
    }
}