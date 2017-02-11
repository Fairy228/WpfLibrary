using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace WpfLibrary.Logic
{
    class Library : ILibrary
    {
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;

        public Library()
        {
            StreamReader streamReader = new StreamReader("connection.txt");
            string connectionString = streamReader.ReadLine();
            streamReader.Close();
            connection = new SqlConnection(connectionString);
            cmd.Connection = connection;
        }

        public void AddBook(Book newBook)
        {
            try
            {
                connection.Open();

                cmd.Parameters.AddWithValue("InventaryNumber", newBook.InventaryNumber);
                cmd.Parameters.AddWithValue("Name", newBook.Name);
                cmd.Parameters.AddWithValue("Author", newBook.Author);
                cmd.Parameters.AddWithValue("Section", newBook.Section);
                cmd.Parameters.AddWithValue("SectionNumber", newBook.SectionNumber);
                cmd.Parameters.AddWithValue("AdditionalInformation", newBook.AdditionalInformation);
                cmd.Parameters.AddWithValue("Annotation", newBook.Annotation);
                cmd.Parameters.AddWithValue("HavkinaNumber", newBook.HavkinaNumber);

                cmd.CommandText = @"INSERT [Books] ([InventaryNumber],
                    [Section], [SectionNumber], [Author], [Name],
                    [AdditionalInformation], [HavkinaNumber], [Annotation])
                    VALUES (@InventaryNumber,
                    @Section, @SectionNumber, @Author, @Name,
                    @AdditionalInformation, @HavkinaNumber, @Annotation)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteBook(Book book)
        {
            try
            {
                connection.Open();

                cmd.Parameters.AddWithValue("InventaryNumber", book.InventaryNumber);
                cmd.CommandText = "DELETE FROM [Books] WHERE InventaryNumber = @InventaryNumber";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Book GetBook(int InventaryNumber)
        {
            try
            {
                Book book = new Book();
                connection.Open();
                cmd.Parameters.AddWithValue("Number", InventaryNumber);
                cmd.CommandText = "SELECT * FROM [Books] WHERE InventaryNumber = @Number";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    book.InventaryNumber = int.Parse(reader[0].ToString());
                    book.Section = reader[1].ToString();
                    book.SectionNumber = reader[2].ToString();
                    book.Author = reader[3].ToString();
                    book.Name = (reader[4].ToString());
                    book.AdditionalInformation = reader[5].ToString();
                    book.HavkinaNumber = reader[6].ToString();
                    book.Annotation = reader[7].ToString();
                    book.ReaderId = reader[8].ToString();
                    book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                }
                cmd.Parameters.Clear();
                return book;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Book GetBook(string BookName)
        {
            try
            {
                Book book = new Book();
                connection.Open();
                cmd.Parameters.AddWithValue("Name", BookName);
                cmd.CommandText = "SELECT * FROM [Books] WHERE Name = @Name";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    book.InventaryNumber = int.Parse(reader[0].ToString());
                    book.Section = reader[1].ToString();
                    book.SectionNumber = reader[2].ToString();
                    book.Author = reader[3].ToString();
                    book.Name = (reader[4].ToString());
                    book.AdditionalInformation = reader[5].ToString();
                    book.HavkinaNumber = reader[6].ToString();
                    book.Annotation = reader[7].ToString();
                    book.ReaderId = reader[8].ToString();
                    book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                }
                cmd.Parameters.Clear();
                return book;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Book> GetBooks()
        {
            try
            {
                List<Book> books = new List<Book>();
                connection.Open();
                cmd.CommandText = "Select * From [Books]";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.InventaryNumber = int.Parse(reader[0].ToString());
                        book.Section = reader[1].ToString();
                        book.SectionNumber = reader[2].ToString();
                        book.Author = reader[3].ToString();
                        book.Name = (reader[4].ToString());
                        book.AdditionalInformation = reader[5].ToString();
                        book.HavkinaNumber = reader[6].ToString();
                        book.Annotation = reader[7].ToString();
                        book.ReaderId = reader[8].ToString();
                        book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                        books.Add(book);
                    }
                }
                cmd.Parameters.Clear();
                return books;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Book> GetBooks_Author(string Author)
        {
            try
            {
                List<Book> books = new List<Book>();
                connection.Open();
                cmd.Parameters.AddWithValue("Author", Author);
                cmd.CommandText = "SELECT * FROM [Books] WHERE Author = @Author";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.InventaryNumber = int.Parse(reader[0].ToString());
                        book.Section = reader[1].ToString();
                        book.SectionNumber = reader[2].ToString();
                        book.Author = reader[3].ToString();
                        book.Name = (reader[4].ToString());
                        book.AdditionalInformation = reader[5].ToString();
                        book.HavkinaNumber = reader[6].ToString();
                        book.Annotation = reader[7].ToString();
                        book.ReaderId = reader[8].ToString();
                        book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                        books.Add(book);
                    }
                }
                cmd.Parameters.Clear();
                return books;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Book> GetBooks(string Section)
        {
            try
            {
                List<Book> books = new List<Book>();
                connection.Open();
                cmd.Parameters.AddWithValue("Section", Section);
                cmd.CommandText = "SELECT * FROM [Books] WHERE Section = @Section";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.InventaryNumber = int.Parse(reader[0].ToString());
                        book.Section = reader[1].ToString();
                        book.SectionNumber = reader[2].ToString();
                        book.Author = reader[3].ToString();
                        book.Name = (reader[4].ToString());
                        book.AdditionalInformation = reader[5].ToString();
                        book.HavkinaNumber = reader[6].ToString();
                        book.Annotation = reader[7].ToString();
                        book.ReaderId = reader[8].ToString();
                        book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                        books.Add(book);
                    }
                }
                cmd.Parameters.Clear();
                return books;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Book> GetBooks(string Section, string SectionNumber)
        {
            try
            {
                List<Book> books = new List<Book>();
                connection.Open();
                cmd.Parameters.AddWithValue("Section", Section);
                cmd.Parameters.AddWithValue("SectionNumber", SectionNumber);
                cmd.CommandText = "SELECT * FROM [Books] WHERE Section = @Section AND SectionNumber = @SectionNumber";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.InventaryNumber = int.Parse(reader[0].ToString());
                        book.Section = reader[1].ToString();
                        book.SectionNumber = reader[2].ToString();
                        book.Author = reader[3].ToString();
                        book.Name = (reader[4].ToString());
                        book.AdditionalInformation = reader[5].ToString();
                        book.HavkinaNumber = reader[6].ToString();
                        book.Annotation = reader[7].ToString();
                        book.ReaderId = reader[8].ToString();
                        book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                        books.Add(book);
                    }
                }
                cmd.Parameters.Clear();
                return books;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddReader(Reader newReader)
        {
            try
            {
                connection.Open();

                cmd.Parameters.AddWithValue("Id", newReader.Id);
                cmd.Parameters.AddWithValue("Year", newReader.Year);
                cmd.Parameters.AddWithValue("Name", newReader.Name);
                cmd.Parameters.AddWithValue("Surname", newReader.Surname);
                cmd.Parameters.AddWithValue("Patronymic", newReader.Patronymic);
                cmd.Parameters.AddWithValue("YearOfBirth", newReader.YearOfBirth);
                cmd.Parameters.AddWithValue("Education", newReader.Education);
                cmd.Parameters.AddWithValue("Job", newReader.Job);
                cmd.Parameters.AddWithValue("Position", newReader.Position);
                cmd.Parameters.AddWithValue("WorkPhone", newReader.WorkPhone);
                cmd.Parameters.AddWithValue("Adress", newReader.Adress);
                cmd.Parameters.AddWithValue("Phone", newReader.Phone);
                cmd.Parameters.AddWithValue("PassportData", newReader.PassportData);
                cmd.Parameters.AddWithValue("RegistrationDate", newReader.RegistrationDate);

                cmd.CommandText = @"INSERT [Readers] ([Id], [Year], [Name],[Surname],
                    [Patronymic],[YearOfBirth],[Education],[Job],[Position],[WorkPhone],
                    [Adress],[Phone],[PassportData], [RegistrationDate]) 
                    VALUES (@Id, @Year, @Name, @Surname, @Patronymic, @YearOfBirth, @Education,
                    @Job,@Position,@WorkPhone,@Adress, @Phone, @PassportData, 
                    @RegistrationDate)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Reader GetReader(string name, string surname, string patronymic)
        {
            try
            {
                Reader newReader = new Reader();
                connection.Open();
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Surname", surname);
                cmd.Parameters.AddWithValue("Patronymic", patronymic);
                cmd.CommandText = @"SELECT * FROM [Readers] WHERE Name = @Name
                AND Surname = @Surname AND Patronymic = @Patronymic";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    newReader.Id = int.Parse(reader[0].ToString());
                    newReader.Year = int.Parse(reader[1].ToString());
                    newReader.Name = reader[2].ToString();
                    newReader.Surname = reader[3].ToString();
                    newReader.Patronymic = reader[4].ToString();
                    newReader.YearOfBirth = int.Parse(reader[5].ToString());
                    newReader.Education = reader[6].ToString();
                    newReader.Job = reader[7].ToString();
                    newReader.Position = reader[8].ToString();
                    newReader.WorkPhone = reader[9].ToString();
                    newReader.Adress = reader[10].ToString();
                    newReader.Phone = reader[11].ToString();
                    newReader.PassportData = reader[12].ToString();
                    newReader.RegistrationDate = DateTime.Parse(reader[13].ToString());
                }
                cmd.Parameters.Clear();
                return newReader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Reader GetReader(string name, string surname)
        {
            try
            {
                Reader newReader = new Reader();
                connection.Open();
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Surname", surname);
                cmd.CommandText = @"SELECT * FROM [Readers] WHERE Name = @Name
                AND Surname = @Surname";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    newReader.Id = int.Parse(reader[0].ToString());
                    newReader.Year = int.Parse(reader[1].ToString());
                    newReader.Name = reader[2].ToString();
                    newReader.Surname = reader[3].ToString();
                    newReader.Patronymic = reader[4].ToString();
                    newReader.YearOfBirth = int.Parse(reader[5].ToString());
                    newReader.Education = reader[6].ToString();
                    newReader.Job = reader[7].ToString();
                    newReader.Position = reader[8].ToString();
                    newReader.WorkPhone = reader[9].ToString();
                    newReader.Adress = reader[10].ToString();
                    newReader.Phone = reader[11].ToString();
                    newReader.PassportData = reader[12].ToString();
                    newReader.RegistrationDate = DateTime.Parse(reader[13].ToString());
                }
                cmd.Parameters.Clear();
                return newReader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Reader GetReader(string surname)
        {
            try
            {
                Reader newReader = new Reader();
                connection.Open();
                cmd.Parameters.AddWithValue("Surname", surname);
                cmd.CommandText = @"SELECT * FROM [Readers] WHERE Surname = @Surname";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    newReader.Id = int.Parse(reader[0].ToString());
                    newReader.Year = int.Parse(reader[1].ToString());
                    newReader.Name = reader[2].ToString();
                    newReader.Surname = reader[3].ToString();
                    newReader.Patronymic = reader[4].ToString();
                    newReader.YearOfBirth = int.Parse(reader[5].ToString());
                    newReader.Education = reader[6].ToString();
                    newReader.Job = reader[7].ToString();
                    newReader.Position = reader[8].ToString();
                    newReader.WorkPhone = reader[9].ToString();
                    newReader.Adress = reader[10].ToString();
                    newReader.Phone = reader[11].ToString();
                    newReader.PassportData = reader[12].ToString();
                    newReader.RegistrationDate = DateTime.Parse(reader[13].ToString());
                }
                cmd.Parameters.Clear();
                return newReader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Reader GetReader(int Id)
        {
            try
            {
                Reader newReader = new Reader();
                connection.Open();
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.CommandText = @"SELECT * FROM [Readers] WHERE Id = @Id";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    newReader.Id = int.Parse(reader[0].ToString());
                    newReader.Year = int.Parse(reader[1].ToString());
                    newReader.Name = reader[2].ToString();
                    newReader.Surname = reader[3].ToString();
                    newReader.Patronymic = reader[4].ToString();
                    newReader.YearOfBirth = int.Parse(reader[5].ToString());
                    newReader.Education = reader[6].ToString();
                    newReader.Job = reader[7].ToString();
                    newReader.Position = reader[8].ToString();
                    newReader.WorkPhone = reader[9].ToString();
                    newReader.Adress = reader[10].ToString();
                    newReader.Phone = reader[11].ToString();
                    newReader.PassportData = reader[12].ToString();
                    newReader.RegistrationDate = DateTime.Parse(reader[13].ToString());
                }
                return newReader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }
        }

        public void AddBookToReader(Book Book, Reader Reader)
        {
            try
            {
                connection.Open();
                cmd.Parameters.AddWithValue("BookId", Book.InventaryNumber);
                cmd.Parameters.AddWithValue("ReaderId", Reader.Id);
                cmd.Parameters.AddWithValue("DateOfIssue", DateTime.Now.Date);
                cmd.CommandText = @"UPDATE [Books]
                SET Reader = @ReaderId, DateOfIssue = @DateOfIssue WHERE InventaryNumber = @BookId";
                cmd.ExecuteNonQuery();
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }
        }

        public void DeleteBookFromReader(Book Book)
        {
            try
            {
                connection.Open();
                cmd.Parameters.AddWithValue("BookId", Book.InventaryNumber);
                cmd.Parameters.AddWithValue("DateOfIssue", new DateTime(1900, 01, 01));
                cmd.CommandText = @"UPDATE [Books]
                SET Reader = NULL, DateOfIssue = @DateOfIssue WHERE InventaryNumber = @BookId";
                cmd.ExecuteNonQuery();
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }
        }

        public List<Book> GetReadersBooks(Reader Reader)
        {
            try
            {
                List<Book> books = new List<Book>();
                connection.Open();
                cmd.Parameters.AddWithValue("ReaderId", Reader.Id);
                cmd.CommandText = "Select * From [Books] Where Reader = @ReaderId";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.InventaryNumber = int.Parse(reader[0].ToString());
                        book.Section = reader[1].ToString();
                        book.SectionNumber = reader[2].ToString();
                        book.Author = reader[3].ToString();
                        book.Name = (reader[4].ToString());
                        book.AdditionalInformation = reader[5].ToString();
                        book.HavkinaNumber = reader[6].ToString();
                        book.Annotation = reader[7].ToString();
                        book.ReaderId = reader[8].ToString();
                        book.DateOfIssue = DateTime.Parse(reader[9].ToString());
                        books.Add(book);
                    }
                }
                cmd.Parameters.Clear();
                return books;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
