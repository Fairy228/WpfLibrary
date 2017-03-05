using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfLibrary.Logic
{
    public interface ILibrary
    {
        void AddBook(Book newBook);

        void DeleteBook(Book book);

        Book GetBook(int Number);

        List<Book> GetBooks();

        List<Book> GetBooks_Author(string Author);


        void AddReader(Reader newReader);

        //void DeleteReader(Reader reader);

        Reader GetReader(string surname);
    }
}
