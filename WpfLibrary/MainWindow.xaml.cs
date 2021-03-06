﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using WpfLibrary.Logic;

namespace WpfLibrary
{
    public partial class MainWindow : Window
    {
        Library lib;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lib = new Library();
        }

        private void AddReaderButton_Click(object sender, RoutedEventArgs e)    
        {
            try
            {
                if (AddReader_IdTBX.Text != "" && AddReader_YearTBX.Text != ""
                && AddReader_NameTBX.Text != "" && AddReader_SurnameTBX.Text != "" &&
                AddReader_PatronymicTBX.Text != "" && AddReader_YearOfBirthTBX.Text != "" &&
                AddReader_JobTBX.Text != "" && AddReader_AdressTBX.Text != "" &&
                AddReader_PhoneTBX.Text != "" &&
                AddReader_PassportDataTBX.Text != "")
                {
                    Reader reader = new Reader();
                    reader.Id = int.Parse(AddReader_IdTBX.Text);
                    if (lib.GetReader(reader.Id).Id == reader.Id)
                    {
                        MessageBox.Show("Читатель с таким номером уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        reader.Year = int.Parse(AddReader_YearTBX.Text);
                        reader.Name = AddReader_NameTBX.Text;
                        reader.Surname = AddReader_SurnameTBX.Text;
                        reader.Patronymic = AddReader_PatronymicTBX.Text;
                        reader.YearOfBirth = int.Parse(AddReader_YearOfBirthTBX.Text);
                        reader.Education = AddReader_EducationTBX.Text;
                        reader.Job = AddReader_JobTBX.Text;
                        reader.Position = AddReader_PositionTBX.Text;
                        reader.WorkPhone = AddReader_WorkPhoneTBX.Text;
                        reader.Adress = AddReader_AdressTBX.Text;
                        reader.Phone = AddReader_PhoneTBX.Text;
                        reader.PassportData = AddReader_PassportDataTBX.Text;
                        reader.RegistrationDate = AddReader_DatePicker.DisplayDate;
                        lib.AddReader(reader);
                        MessageBox.Show("Читатель успешно добавлен!", "Успех!",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        AddReader_IdTBX.Text = "";
                        AddReader_YearTBX.Text = "";
                        AddReader_NameTBX.Text = "";
                        AddReader_SurnameTBX.Text = "";
                        AddReader_PatronymicTBX.Text = "";
                        AddReader_YearOfBirthTBX.Text = "";
                        AddReader_EducationTBX.Text = "";
                        AddReader_JobTBX.Text = "";
                        AddReader_PositionTBX.Text = "";
                        AddReader_WorkPhoneTBX.Text = "";
                        AddReader_AdressTBX.Text = "";
                        AddReader_PhoneTBX.Text = "";
                        AddReader_PassportDataTBX.Text = "";
                    }
                }
                else MessageBox.Show("Заполните обязательные поля!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Недопустимые данные!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindReaderButton_Click(object sender, RoutedEventArgs e)
        {
            string content = FindReaderButton.Content.ToString();
            if (content == "Найти")
            {
                Reader reader = new Reader();
                if (FindReader_NameTBX.Text != "Имя: " && FindReader_SurnameTBX.Text != "Фамилия: " &&
                    FindReader_PatronymicTBX.Text != "Отчество: " &&
                    FindReader_NameTBX.Text != "" && FindReader_SurnameTBX.Text != "" && FindReader_PatronymicTBX.Text != "")
                    reader = lib.GetReader(FindReader_NameTBX.Text, FindReader_SurnameTBX.Text, FindReader_PatronymicTBX.Text);
                else if (FindReader_NameTBX.Text != "Имя: " && FindReader_SurnameTBX.Text != "Фамилия: " &&
                    FindReader_NameTBX.Text != "" && FindReader_SurnameTBX.Text != "")
                    reader = lib.GetReader(FindReader_NameTBX.Text, FindReader_SurnameTBX.Text);
                else if (FindReader_SurnameTBX.Text != "Фамилия: " && FindReader_SurnameTBX.Text != "")
                    reader = lib.GetReader(FindReader_SurnameTBX.Text);

                FindReader_IdTBXC.Text = "Номер: " + reader.Id;
                FindReader_YearTBC.Text = "Год: " + reader.Year.ToString();
                FindReader_NameTBX.Text = "Имя: " + reader.Name;
                FindReader_SurnameTBX.Text = "Фамилия: " + reader.Surname;
                FindReader_PatronymicTBX.Text = "Отчество: " + reader.Patronymic;
                FindReader_YearOfBirthTBC.Text = "Год рождения: " + reader.YearOfBirth.ToString();
                FindReader_EducationTBC.Text = "Образование: " + reader.Education;
                FindReader_JobTBC.Text = "Место работы: " + reader.Job;
                FindReader_PositionTBC.Text = "Должность: " + reader.Position;
                FindReader_WorkPhoneTBC.Text = "Рабочий телефон: " + reader.WorkPhone;
                FindReader_AdressTBC.Text = "Адрес: " + reader.Adress;
                FindReader_PhoneTBC.Text = "Телефон: " + reader.Phone;
                FindReader_PassportDataTBC.Text = "Паспортные данные: " + reader.PassportData;
                FindReader_RegistrationDateTBC.Text = "Дата регистрации: " + reader.RegistrationDate.ToString();
                NameOfReaderTBC.Text = reader.Surname + " " + reader.Name + " " + reader.Patronymic;
                if (reader.Surname != "")
                {
                    BooksDG.ItemsSource = lib.GetReadersBooks(reader);
                }
                FindReaderButton.Content = "Сбросить";
                FindReader_NameTBX.IsReadOnly = false;
                FindReader_SurnameTBX.IsReadOnly = true;
                FindReader_PatronymicTBX.IsReadOnly = true;
            }
            else
            {
                FindReaderButton.Content = "Найти";
                BooksDG.ItemsSource = null;
                FindReader_NameTBX.IsReadOnly = false;
                FindReader_SurnameTBX.IsReadOnly = false;
                FindReader_PatronymicTBX.IsReadOnly = false;
                FindReader_IdTBXC.Text = "Номер: ";
                FindReader_YearTBC.Text = "Год: ";
                FindReader_NameTBX.Text = "Имя: ";
                FindReader_SurnameTBX.Text = "Фамилия: ";
                FindReader_PatronymicTBX.Text = "Отчество: ";
                FindReader_YearOfBirthTBC.Text = "Год рождения: ";
                FindReader_EducationTBC.Text = "Образование: ";
                FindReader_JobTBC.Text = "Место работы: ";
                FindReader_PositionTBC.Text = "Должность: ";
                FindReader_WorkPhoneTBC.Text = "Рабочий телефон: ";
                FindReader_AdressTBC.Text = "Адрес: ";
                FindReader_PhoneTBC.Text = "Телефон: ";
                FindReader_PassportDataTBC.Text = "Паспортные данные: ";
                FindReader_RegistrationDateTBC.Text = "Дата регистрации: ";
                NameOfReaderTBC.Text = "";
            }
        }

        private void AddBookToReaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (FindReader_IdTBXC.Text != "Номер: ")
            {
                MainTabControl.SelectedItem = FindBooksTabItem;
                ReadersTabItem.IsEnabled = false;
                AddBookTabItem.IsEnabled = false;
                AddBookToReader1Button.IsEnabled = true;
                AddBookToReader1Button.Visibility = Visibility.Visible;
                CanselAddBookToReader1Button.IsEnabled = true;
                CanselAddBookToReader1Button.Visibility = Visibility.Visible;
            }
        }

        private void DeleteBookFromReaderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksDG.Items != null && BooksDG.SelectedIndex != -1 && BooksDG.SelectedItem != null)
                {
                    Book book = new Book();
                    Book tempBook;
                    string Idstr = ""; ;
                    for (int i = 7; i < FindReader_IdTBXC.Text.Length; i++)
                        Idstr += FindReader_IdTBXC.Text[i];
                    int Id = int.Parse(Idstr);
                    MessageBoxResult MSBX = MessageBox.Show("Вы действительно хотите удалить книгу " + book.Name + " из карты текущего читателя?", "Внимание!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (MSBX == MessageBoxResult.Yes)
                    {
                        Reader reader = lib.GetReader(Id);
                        tempBook = (Book)BooksDG.SelectedItem;
                        book = lib.GetBook(tempBook.InventaryNumber);
                        lib.DeleteBookFromReader(book);
                        BooksDG.ItemsSource = lib.GetReadersBooks(reader);
                        MessageBox.Show("Читатель сдал книгу!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBookToReaderButton1_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDG.Items != null && ItemsDG.SelectedIndex != -1)
            {
                Book book = new Book();
                Book tempBook;
                string Idstr = ""; ;
                for (int i = 7; i < FindReader_IdTBXC.Text.Length; i++)
                    Idstr += FindReader_IdTBXC.Text[i];
                int Id = int.Parse(Idstr);
                MessageBoxResult MSBX = MessageBox.Show("Вы действительно хотите записать книгу " + book.Name + " на текущего читателя?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (MSBX == MessageBoxResult.Yes)
                {
                    Reader reader = lib.GetReader(Id);
                    tempBook = (Book)ItemsDG.SelectedItem;
                    book = lib.GetBook(tempBook.InventaryNumber);
                    if (book.ReaderId != "")
                        MessageBox.Show("Книга уже выдана!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        lib.AddBookToReader(book, reader);
                        BooksDG.ItemsSource = lib.GetReadersBooks(reader);
                        ReadersTabItem.IsEnabled = true;
                        AddBookTabItem.IsEnabled = true;
                        AddBookToReader1Button.IsEnabled = false;
                        AddBookToReader1Button.Visibility = Visibility.Hidden;
                        CanselAddBookToReader1Button.IsEnabled = false;
                        CanselAddBookToReader1Button.Visibility = Visibility.Hidden;
                        MainTabControl.SelectedItem = ReadersTabItem;

                    }
                }
            }
        }

        private void CanselAddBookToReaderButton1_Click(object sender, RoutedEventArgs e)
        {
            ReadersTabItem.IsEnabled = true;
            AddBookTabItem.IsEnabled = true;
            AddBookToReader1Button.IsEnabled = false;
            AddBookToReader1Button.Visibility = Visibility.Hidden;
            CanselAddBookToReader1Button.IsEnabled = false;
            CanselAddBookToReader1Button.Visibility = Visibility.Hidden;
            MainTabControl.SelectedItem = ReadersTabItem;
        }

        public void SearchBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    if (GetBook_InventaryNumberTBX.Text == "" && GetBook_NameTBX.Text == "" &&
                        GetBook_AuthorTBX.Text == "")
                    {
                        ItemsDG.ItemsSource = lib.GetBooks();
                    }
                    else if (GetBook_InventaryNumberTBX.Text != "")
                    {
                        List<Book> books = new List<Book>();
                    if (lib.GetBook(int.Parse(GetBook_InventaryNumberTBX.Text)).Name != null) 
                            books.Add(lib.GetBook(int.Parse(GetBook_InventaryNumberTBX.Text)));
                        ItemsDG.ItemsSource = books;
                    }
                    else if (GetBook_NameTBX.Text != "")
                    {
                        List<Book> books = new List<Book>();
                    if (lib.GetBook(GetBook_NameTBX.Text).Name != null) 
                            books.Add(lib.GetBook(GetBook_NameTBX.Text));
                        ItemsDG.ItemsSource = books;
                    }
                    else if (GetBook_NameTBX.Text == "" && GetBook_InventaryNumberTBX.Text == "" &&
                        GetBook_AuthorTBX.Text != "")
                    {
                        List<Book> books = lib.GetBooks_Author(GetBook_AuthorTBX.Text);
                        ItemsDG.ItemsSource = books;
                    }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddBook_InventaryNumberTBX.Text == "" || AddBook_NameTBX.Text == "")
                {
                    MessageBox.Show("Заполните обязательные поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Book book = new Book();

                    book.InventaryNumber = int.Parse(AddBook_InventaryNumberTBX.Text);
                    book.Section = AddBook_SectionTBX.Text;
                    book.Author = AddBook_AuthorTBX.Text;
                    book.Name = AddBook_NameTBX.Text;
                    book.AdditionalInformation = AddBook_AdditionalInformationTBX.Text;
                    book.HavkinaNumber = AddBook_HavkinaNumberTBX.Text;
                    book.Annotation = AddBook_AnnotationTBX.Text;
                    book.DateOfIssue = new DateTime(1900, 01, 01);

                    if (lib.GetBook(book.InventaryNumber).InventaryNumber == book.InventaryNumber)
                        MessageBox.Show("Книга с таким номером существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        lib.AddBook(book);
                        MessageBox.Show("Книга добавлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        AddBook_InventaryNumberTBX.Text = "";
                        AddBook_SectionTBX.Text = "";
                        AddBook_AuthorTBX.Text = "";
                        AddBook_NameTBX.Text = "";
                        AddBook_AdditionalInformationTBX.Text = "";
                        AddBook_HavkinaNumberTBX.Text = "";
                        AddBook_AnnotationTBX.Text = "";
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ItemsDG_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ItemsDG.SelectedIndex != -1)
            {
                Book tempBook = (Book)ItemsDG.SelectedItem;
                Book book = lib.GetBook(tempBook.InventaryNumber);
                string inStock;
                if (book.DateOfIssue != new DateTime(1900, 01, 01))  //проверяю в библиотеке книга или выдана
                {                                                    //для формирования сообщения
                    Reader reader = lib.GetReader(int.Parse(book.ReaderId));
                    inStock = "Книга выдана читателю " + reader.Surname + " " +
                        reader.Name + " под номером " + reader.Id + "\n" +
                        book.DateOfIssue.Date + ".";
                }
                else
                {
                    inStock = "Книга находится в библиотеке.";
                }
                string header = "'" + book.Name + "'";
                string message = "Аннотация: " + book.Annotation + "\n" +
                    "Дополнительная информация: " + book.AdditionalInformation + "\n" + inStock;
                MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddReader_DatePicker_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AddReader_DatePicker.IsDropDownOpen = true;
        }

        private void BooksDG_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (BooksDG.SelectedIndex != -1)
            {
                Book tempBook = (Book)BooksDG.SelectedItem;
                Book book = lib.GetBook(tempBook.InventaryNumber);
                string message;                                                //для формирования сообщения
                Reader reader = lib.GetReader(book.ReaderId);
                TimeSpan ts = DateTime.Now - book.DateOfIssue;
                if (ts.Days <= 30)
                {
                    message = "Аннотация: " + book.Annotation + "\n" + "Дополнительная информация: " +
                        book.AdditionalInformation + "\n" +
                        "Выдана " + book.DateOfIssue + "\n" +
                        "Дней до просрочки: " + (30 - ts.Days);
                }
                else
                {
                    message = "Аннотация: " + book.Annotation + "\n" + "Дополнительная информация: " +
                        book.AdditionalInformation + "\n" +
                        "Выдана " + book.DateOfIssue + "\n" +
                        "Книга просрочена. Дней просрочки: " + (ts.Days - 30);
                }
                string header = "'" + book.Name + "'";
                MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChangeBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDG.SelectedIndex != -1)
            {
                Book tempBook = (Book)ItemsDG.SelectedItem;
                Book book = lib.GetBook(tempBook.InventaryNumber);
                ChangeBookWindow wnd = new ChangeBookWindow(lib, book);
                wnd.ShowDialog();
            }
        }

        private void FindReader_SurnameTBX_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FindReader_SurnameTBX.Text = "";
        }

        private void FindReader_NameTBX_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FindReader_NameTBX.Text = "";
        }

        private void FindReader_PatronymicTBX_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FindReader_PatronymicTBX.Text = "";
        }
    }
}
