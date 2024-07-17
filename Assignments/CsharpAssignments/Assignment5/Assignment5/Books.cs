using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment5
{
    public class Books
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }

        public Books(string bookName, string authorName)
        {
            BookName = bookName;
            AuthorName = authorName;
        }

        public void Display()
        {
            Console.WriteLine("Book: " + BookName + ", Author: " + AuthorName);
            Console.ReadLine();
        }
    }

    public class BookShelf
    {
        private Books[] books = new Books[5];

        public Books this[int index]
        {
            get { return books[index]; }
            set { books[index] = value; }
        }
    }

    class program
    {
        static void Main(string[] args)
        {
            BookShelf shelf = new BookShelf();
            shelf[0] = new Books("harrypotter", "j.k.rowling");
            shelf[1] = new Books("the Hobbit", "cao Xueqin");
            shelf[2] = new Books("alice in wonderland", "lewis carrol");
            shelf[3] = new Books("arms and the man", "jules verne");
            shelf[4] = new Books("time machine", "h.g.wells");

            for (int i = 0; i < 5; i++)
            {
                shelf[i].Display();
            }
        }
    }
}