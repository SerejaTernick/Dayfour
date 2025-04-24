using System;
using System.Linq;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;


namespace Dayfour
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
            MainWindow.Anima(btLogin, 15, 134); // анимация кнопки
        }
        public int ch = 0;

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            //ищу в контракте по айдишнику номер аккаунта и создаю новую запись в контракт со слуайным айди контракта
            bankEntities bankobj = new bankEntities();
            if (tbLogin.Text.Length > 3 && tbPassord.Text.Length > 3)
            {
                Double login = Convert.ToDouble(tbLogin.Text);
                var user = bankobj.User_.FirstOrDefault(oj => oj.Password ==
                tbPassord.Text && oj.Login == login);

                //Номер документа
                //Событи при нажатие кнопки

                Word.Document Doc = null;
                Word.Application app = new Word.Application();  //Для присваивания док-а

                string Path = Environment.CurrentDirectory.ToString() + @"\Шаблон договора.docx"; //Путь до док-а
                Doc = app.Documents.Add(Path); //Добавляем шаблон; () - новый док
                Doc.Activate();

                Word.Bookmarks BookMark = Doc.Bookmarks; //Все закладки
                Word.Range range; //Конкретная закладка

                ch++;
                //Серия и номер
                string number = user.Number_Series.ToString().Substring(0, 5);
                string series = user.Number_Series.ToString().Substring(6);
                Random rnd = new Random();
                DateTime today = DateTime.Now;

                var pasport = bankobj.Passport_.FirstOrDefault(u => u.Number_Series == user.Number_Series);
                double bancacc = (double)bankobj.Contract_.FirstOrDefault(u => u.IDContract == user.IDUser).NumberAccount;
                int idcont = rnd.Next(1000000, 9999999);

                //Массивы для заполнения Закладок в Документе
                string[] data = new string[18] {
                        user.E_Mail,
                        pasport.Adress,
                        pasport.Issued,
                        today.Year.ToString(),
                        today.AddDays(Culculation.period).ToShortDateString(),
                        ((DateTime)pasport.DateOfBirth).ToShortDateString(),
                        today.Day.ToString(),
                        pasport.PlaceOfBirth,
                        today.Month.ToString(),
                        number,
                        ""+ bancacc,
                        "" + rnd.Next(1000000, 9999999),
                        compare.selectedStavka.ToString(),
                        series,
                        Culculation.period + " дней",
                        Culculation.sum.ToString(),
                        user.Surname + " "+ user.Name + " " + user.Patronymic,
                        user.Surname + " "+ user.Name + " " + user.Patronymic,
                        };
                int i = 0;

                //Проходимся по всем Закладкам и выдаем из значения из массива
                foreach (Word.Bookmark mark in BookMark)
                {
                    range = mark.Range;
                    range.Text = data[i];
                    i++;
                }

                //Куда сохраняет
                Doc.SaveAs2(Environment.CurrentDirectory.ToString() + @"\Документ " + ch + ".docx");
                Doc.Close();
                Doc = null;
                //Закрываем, обнуляем

                //Заносим двынные в БД
                Contract_ contract = new Contract_()
                {
                    IDContract = idcont,
                    NumberAccount = bancacc,
                    IDUser = user.IDUser,
                    Amount = compare.lastSum,
                    Period = Culculation.period,
                    ExpirationDate = today.AddDays(Culculation.period),
                    Percet = Convert.ToDouble(compare.selectedStavka.ToString())
                };

                bankobj.Contract_.Add(contract);
                bankobj.SaveChanges();

                MessageBox.Show("Договор создан", "Сообщение"); 
            }
                else
                MessageBox.Show("Заполните Поля", "Предупреждение"); 
        }
    }
}
