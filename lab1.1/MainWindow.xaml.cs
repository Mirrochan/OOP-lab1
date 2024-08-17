
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Specialized;


namespace lab1._1
{
    class Book
    {
       public string Title{ get; set; }
        public string Author {  get; set; }  
        public string Year { get; set; }
        public string ISBN { get; set; }

    }
    class Library
    {
        public static List<Book> ShowAll()/// метод для показу всіх книг
        {
            var file = @"D:\Desktop\lab1.1\list.txt"; ///створюється зміна, в якій міститься шлях до файлу,
                                                          ///де зберігаюються всі дані про книги

            List<Book> books = new List<Book>(); ///створюється список із книгами з властивостями класу Book

            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    string[] properties = line.Split(',');
                    /* у нас файл містить таку стуркуру для кожної книги "Назва, автор, рік, код книги"
                     * за допомогою split ми ділимо кожен рядок за допомогою коми і вносимо кожні властивості книги 
                     * в масив
                    */


                    Book book = new Book  //добавляються властивості книги списка
                    {
                        Title = properties[0].Trim(), //Trim - видалення всіх пробілів, щоб не було помилок 
                        Author = properties[1].Trim(),
                        Year = properties[2].Trim(),
                        ISBN = properties[3].Trim()
                    };

                    books.Add(book); //добавляємо книгу в список

                }
            }
            return books;

        }
        public static void Add(string title, string author, string year, string isbn) {
           var file = @"D:\Desktop\lab1.1\list.txt";

            using (StreamWriter writer = new StreamWriter(file, append: true))
            {
                writer.WriteLine(title + "," + author + "," + year + "," + isbn);
            }
            //просто добавляємо в файл інформацію, яку ми внесли
        }

        public void Delete(string book) {
          var file = @"D:\Desktop\lab1.1\list.txt";
            List<string> result = new List<string>();
           foreach (var item in File.ReadAllLines(file)) {
                if (!item.Contains(book)) { result.Add(item); }
            }
          // якщо назва,яку ми внесли, не є книгою, яка зараз проходить в циклі, то ми добавляємо його
          //в список, який потім передамо в таблицю. і так видалиться елемент який ми зазначили
            File.WriteAllLines(file, result);
        }

        public static List<Book> Search(string value) {

            List<Book> books = new List<Book>();
            var file = @"D:\Desktop\lab1.1\list.txt";
          
            foreach (var item in File.ReadAllLines(file))
            {
               
                    string[] properties = item.Split(',');


                    Book book = new Book
                    {
                        Title = properties[0].Trim(),
                        Author = properties[1].Trim(),
                        Year = properties[2].Trim(),
                        ISBN = properties[3].Trim()
                    };
                      if (book.Title ==value || book.Author==value) {
                    books.Add(book); //якщо назва співпадає із тим що ми написали - добавляється в масив
                    // так в результаті покажеться в таблиці лише те що ми шукали
                       
                }
            }

            return books;
           
        }
       
          
       
        
    }
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
             
       
           
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGrid1.ItemsSource = null;//чистимо список на екрані
           var data= Library.ShowAll(); //запускаємо метод для показу книг
            DataGrid1.ItemsSource = data;// показуємо результат в таблиці
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string value = TextBoxDelete.Text;// вносиму інформацію для пошуку
            DataGrid1.ItemsSource = null;//чистимо список на екрані
            Library library = new Library();
            library.Delete(value);//запускаємо метод видалення
            var data = Library.ShowAll(); // запускаємо метод для показу всіх книг
            DataGrid1.ItemsSource = data;// показуємо весь список
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {   
            string value=TextBoxToSearch.Text; // вносиму інформацію для пошуку
            DataGrid1.ItemsSource = null;//чистимо список на екрані
            var data =Library.Search(value);//запускаємо метод пошуку 
            DataGrid1.ItemsSource = data;// показуємо результат на екрані

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string title = TextBoxTitle.Text;
            string author = TextBoxAuthor.Text;
            string year = TextBoxYear.Text;
            string isbn = TextBoxisbn.Text;
            Library.Add(title, author, year, isbn);
            var data = Library.ShowAll(); // запускаємо метод для показу всіх книг
            DataGrid1.ItemsSource = data;// показуємо результат на екрані
            ///викликаємо метод і вставляється туди всю інформацію із TextBox
        }
    }
    
}