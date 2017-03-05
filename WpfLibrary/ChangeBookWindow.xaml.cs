using System;
using WpfLibrary.Logic;
using System.Windows;
using System.Data.SqlClient;


namespace WpfLibrary
{
    public partial class ChangeBookWindow : Window
    {
        Book book;
        Library lib;
        public ChangeBookWindow(Library lib,Book book)
        {
            InitializeComponent();
            this.book = book;
            this.lib = lib;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeBook_NameTBX.Text = book.Name;
            ChangeBook_InventaryNumberTBX.Text = book.InventaryNumber.ToString();
            ChangeBook_SectionTBX.Text = book.Section;
            ChangeBook_AuthorTBX.Text = book.Author;
            ChangeBook_AdditionalInformationTBX.Text = book.AdditionalInformation;
            ChangeBook_HavkinaNumberTBX.Text = book.HavkinaNumber;
            ChangeBook_AnnotationTBX.Text = book.Annotation;
            lib = new Library();
        }

        private void ChangeBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book newBook = new Book();
                newBook.InventaryNumber = int.Parse(ChangeBook_InventaryNumberTBX.Text);
                newBook.Section = ChangeBook_SectionTBX.Text;
                newBook.Author = ChangeBook_AuthorTBX.Text;
                newBook.Name = ChangeBook_NameTBX.Text;
                newBook.AdditionalInformation = ChangeBook_AdditionalInformationTBX.Text;
                newBook.HavkinaNumber = ChangeBook_HavkinaNumberTBX.Text;
                newBook.Annotation = ChangeBook_AnnotationTBX.Text;
                lib.UpdateBook(book.InventaryNumber, newBook);
                MessageBoxResult rs = MessageBox.Show("Успех!", "Обновление книги", MessageBoxButton.OK, MessageBoxImage.Information);
                if (rs == MessageBoxResult.OK)
                {
                    Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult rs = MessageBox.Show("Вы действительно хотите удалить книгу?", "Внимание!", 
                    MessageBoxButton.OKCancel, 
                    MessageBoxImage.Exclamation);
                if (rs == MessageBoxResult.OK)
                {
                    lib.DeleteBook(book);
                    rs = MessageBox.Show("Успех!", "Книга удалена", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (rs == MessageBoxResult.OK)
                    {
                        Close();
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
