using System;
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
            FindReader_PassportDataTBC.Text = "Пасспортные данные: " + reader.PassportData;
            FindReader_RegistrationDateTBC.Text = "Дата регистрации: " + reader.RegistrationDate.ToString();
            if (reader.Surname != "")
            {
                BooksDG.ItemsSource = lib.GetReadersBooks(reader);
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
                    Book book1 = new Book();
                    string Idstr = ""; ;
                    for (int i = 7; i < AddReader_IdTBX.Text.Length; i++)
                        Idstr += AddReader_IdTBX.Text[i];
                    int Id = int.Parse(Idstr);
                    MessageBoxResult MSBX = MessageBox.Show("Вы действительно хотите удалить книгу " + book.Name + " из карты текущего читателя?", "Внимание!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (MSBX == MessageBoxResult.Yes)
                    {
                        Reader reader = lib.GetReader(Id);
                        book = (Book)ItemsDG.SelectedItem;
                        book1 = lib.GetBook(book.InventaryNumber);
                        lib.DeleteBookFromReader(book1);
                        BooksDG.ItemsSource = lib.GetReadersBooks(reader);
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBookToReaderButton1_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDG.Items != null && ItemsDG.SelectedIndex != -1 && AddReader_IdTBX.Text.Length > 7)
            {
                Book book = new Book();
                Book tempBook;
                string Idstr = ""; ;
                for (int i = 7; i < AddReader_IdTBX.Text.Length; i++)
                    Idstr += AddReader_IdTBX.Text[i];
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

        private void SearchBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GetBook_InventaryNumberTBX.Text == "" && GetBook_NameTBX.Text == "" &&
                    GetBook_AuthorTBX.Text == "" && GetBook_SectionTBX.Text == "" &&
                    GetBook_SectionNumberTBX.Text == "")
                {
                    ItemsDG.ItemsSource = lib.GetBooks();
                }
                else if (GetBook_InventaryNumberTBX.Text != "")
                {
                    List<Book> books = new List<Book>();
                    books.Add(lib.GetBook(int.Parse(GetBook_InventaryNumberTBX.Text)));
                    ItemsDG.ItemsSource = books;
                }
                else if (GetBook_NameTBX.Text != "")
                {
                    List<Book> books = new List<Book>();
                    books.Add(lib.GetBook(GetBook_NameTBX.Text));
                    ItemsDG.ItemsSource = books;
                }
                else if (GetBook_NameTBX.Text == "" && GetBook_InventaryNumberTBX.Text == "" &&
                    GetBook_AuthorTBX.Text != "")
                {
                    List<Book> books = lib.GetBooks_Author(GetBook_AuthorTBX.Text);
                    ItemsDG.ItemsSource = books;
                }
                else if (GetBook_NameTBX.Text == "" && GetBook_InventaryNumberTBX.Text == "" &&
                    GetBook_AuthorTBX.Text == "" && GetBook_SectionTBX.Text != "" &&
                    GetBook_SectionNumberTBX.Text == "")
                {
                    List<Book> books = lib.GetBooks(GetBook_SectionTBX.Text);
                    ItemsDG.ItemsSource = books;
                }
                else if (GetBook_NameTBX.Text == "" && GetBook_InventaryNumberTBX.Text == "" &&
                    GetBook_AuthorTBX.Text == "" && GetBook_SectionTBX.Text != "" &&
                    GetBook_SectionNumberTBX.Text != "")
                {
                    List<Book> books = lib.GetBooks(GetBook_SectionTBX.Text, GetBook_SectionNumberTBX.Text);
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
                    book.SectionNumber = AddBook_SectionNumberTBX.Text;
                    book.Author = AddBook_AuthorTBX.Text;
                    book.Name = AddBook_NameTBX.Text;
                    book.AdditionalInformation = AddBook_AdditionalInformationTBX.Text;
                    book.HavkinaNumber = AddBook_HavkinaNumberTBX.Text;
                    book.Annotation = AddBook_AnnotationTBX.Text;

                    if (lib.GetBook(book.InventaryNumber).InventaryNumber == book.InventaryNumber)
                        MessageBox.Show("Книга с таким номером существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        lib.AddBook(book);
                        MessageBox.Show("Книга добавлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        AddBook_InventaryNumberTBX.Text = "";
                        AddBook_SectionTBX.Text = "";
                        AddBook_SectionNumberTBX.Text = "";
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
                    Reader reader = lib.GetReader(book.ReaderId);
                    inStock = "Книга выдана читателю " + reader.Surname + " " +
                        reader.Name + " под номером " + reader.Id + "/n" +
                        book.DateOfIssue.Date + ".";
                }
                else
                {
                    inStock = "Книга находится в библиотеке.";
                }
                string header = "'" + book.Name + "'";
                string message = "Аннотация: " + book.Annotation + "\n" + inStock;
                MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddReader_DatePicker_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AddReader_DatePicker.IsDropDownOpen = true;
        }

        private void BooksDG_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
