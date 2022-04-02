using System;

namespace FinalProject
{
    class Program
    {
        static void Main()
        {
            // Вызываем метод, который опрашивает пользователя
            var User = PollUser();
            // Очищаем консоль
            Console.Clear();
            // Выводим на экран данные опроса
            ShowUserInfo(User);
            // Как я понял, эту строчку надо добавить, чтобы консоль не закрылась после выполнения
            Console.ReadKey();
        }

        static (string name, string surname, int age, bool havePet, string[] nickPet, string[] favColors) PollUser()
        {
            (string name, string surname, int age, bool havePet, string[] nickPet, string[] favColors) User;
        
            Console.WriteLine("Введите Ваше имя:");
            User.name = Console.ReadLine();
        
            Console.WriteLine("\nВведите Вашу фамилию:");
            User.surname = Console.ReadLine();
            
            Console.WriteLine("\n{0}, сколько Вам лет?", User.name);
            User.age = GetAge();
        
            Console.WriteLine("\nЕсть ли у Вас домашние животные?\n" +
                "Если да, то перечислите их клички через запятую.\n" +
                "Если нет, то пропустите вопрос, нажав клавишу 'Enter'");
            User.havePet = InputHavePet(out string[] arrayNickPet);

            User.nickPet = arrayNickPet;

            Console.WriteLine("\nПеречислите через запятую Ваши любимые цвета:");
            User.favColors = GetFavColors();         

            return User;
        }

        static int GetAge()
        {
            bool numberEntered;
            bool numberCorrect;
            int outputInt;
            do
            {
                Console.WriteLine("Ведите число цифрами:");
                string input = Console.ReadLine();

                numberEntered = int.TryParse(input, out outputInt);
                numberCorrect = false;

                if (numberEntered == true)
                    if (outputInt > 0)
                    { numberCorrect = true; }
                    else
                    { Console.WriteLine("Возраст не может быть меньше или равен нулю!"); }

            } while (!(numberEntered && numberCorrect));

            return outputInt;
        }

        static bool InputHavePet(out string[] arrayNickPet)
        {
            string input = Console.ReadLine();
            if (input == "")
            {
                arrayNickPet = Array.Empty<string>();
                return false;
            }
            else
            {
                arrayNickPet = input.Split(", ");
                return true;
            }
        }

        static string[] GetFavColors()
        {
            string input = Console.ReadLine();
            if (input == "")
            {
                do
                {
                    Console.WriteLine("Обязательно укажите хотя бы один любимый цвет!");
                    input = Console.ReadLine();
                } while (input == "");
            }
            string[] arrayFavColors = input.Split(", ");
            return arrayFavColors;
        }

        static void ShowUserInfo
            ((string name, string surname, int age, bool havePet, string[] nickPet, string[] favColors)User)
        {
            Console.WriteLine($"\nИмя и Фамилия пользователя: {User.name} {User.surname}");

            // определяем, какое слово ("лет", "год", или "года) будет после значения возраста
            string years;
            if (User.age % 10 > 4 || User.age % 10 == 0 || User.age > 10 && User.age < 20)
                years = "лет";
            else if (User.age % 10 != 1)
                years = "года";
            else
                years = "год";
            Console.WriteLine($"Возраст: {User.age} {years}");

            // составляем информацию о питомцах, в зависимости от их наличия
            if (User.havePet)
            {
                Console.WriteLine($"Количество питомцев: {User.nickPet.Length}");
                Console.WriteLine("их клички:");
                foreach (string nick in User.nickPet)
                    Console.WriteLine("\t{0}",nick);
            }
            else
                Console.WriteLine("У пользователя нет питомцев.");

            // перечисляем любимые цвета
            Console.WriteLine("Любимые цвета:");
            foreach (string color in User.favColors)
                Console.WriteLine($"\t{color.ToLower()}");
        }
    }
}
