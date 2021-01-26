using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{   
    // Ученик - Андрей Марачковский
    class Program
    {
        #region Task 01
        static void Task01()
        {
            int[] massiv = new int[20];
            Random random = new Random();
            int count = 0;

            for (int i = 0; i < massiv.Length; i++)
            {
                massiv[i] = random.Next(-10000, 10000);
                if (i == 9)
                {
                    Console.WriteLine("{0} ", massiv[i]);
                }
                else
                {
                    Console.Write("{0} ", massiv[i]);
                }
            }

            for (int i = 0; i < massiv.Length - 1; i++)
            {
                var a1 = massiv[i];
                var a2 = massiv[i + 1];
                if (a1 % 3 == 0 && a2 % 3 == 0)
                {
                    count++;
                }
            }
            Console.WriteLine($"Количество пар: {count}");

            Console.ReadKey();
        }
        #endregion

        #region Task 02
        class Massiv
        {
            private int[] _a;
            private int _sum;
            private int _maxCount;
            private string _filePath = AppDomain.CurrentDomain.BaseDirectory + "ArrayList.txt";
            private int _length;
            /// <summary>
            /// инициализация массива
            /// </summary>
            /// <param name="n">размерность массива</param>
            /// <param name="step">шаг значения в массиве</param>
            /// <param name="nz">начальное значение</param>
            public Massiv(int n, int step, int nz)
            {
                int[] a = new int[n];
                a[0] = nz;
                for (int i = 1; i < a.Length; i++)
                {                    
                    a[i] = a[i - 1] + step;
                }
                _a = a;
                _length = a.Length;
                Sum();
                MaximumCount();
            }
            public Massiv()
            {                
                LoadArrayFromFile();
                Sum();
                MaximumCount();
                _length = _a.Length;
                Console.ReadKey();
            }
            private void LoadArrayFromFile()
            {
                if (File.Exists(_filePath))
                {
                    StreamReader reader = new StreamReader(_filePath);

                    var str = reader.ReadLine();
                    var len = int.Parse(str);
                    var a = new int[len];
                    for (int i = 0; i < len; i++)
                    {
                        a[i] = int.Parse(reader.ReadLine());
                    }
                    reader.Close();
                    _a = a;
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            private void SaveArrayToFile()
            {
                if (File.Exists(_filePath))
                {                    
                    StreamWriter writer = new StreamWriter(_filePath);
                    writer.WriteLine(_length); // длина массива
                    for (int i = 0; i < _a.Length; i++)
                    {
                        writer.WriteLine(_a[i]);
                    }
                    writer.Close();
                }
                else
                {
                    throw new FieldAccessException();
                }
            }
            public void Sum()
            {
                int summa = 0;
                for (int i = 0; i < _a.Length; i++)
                {
                    summa += _a[i];
                }
                _sum = summa;
            }
            public void Inverse(bool isFile)
            {
                for (int i = 0; i < _a.Length; i++)
                {
                    _a[i] = -_a[i];
                }
                if (isFile)
                {
                    SaveArrayToFile();
                }
            }
            public void Multi(int n, bool isFile)
            {
                for (int i = 0; i < _a.Length; i++)
                {
                    _a[i] = _a[i] * n;
                }
                if (isFile)
                {
                    SaveArrayToFile();
                }
            }
            public void MaximumCount()
            {
                var array = _a;
                int count = 0;
                Array.Sort(array);
                var znach = array[array.Length - 1];
                for (int i = 0; i < _a.Length; i++)
                {
                    if (array[i] == znach)
                    {
                        count++;
                    }
                }
                _maxCount = count;
            }

            /// <summary>
            /// Индексное свойство
            /// </summary>
            /// <param name="i">Индекс</param>
            /// <returns></returns>
            public int this[int i]
            {
                get
                {
                    return _a[i];
                }
                set
                {
                    _a[i] = value;
                }
            }
            public int sum
            {
                get
                {
                    return _sum;
                }
                set
                {
                    _sum = value;
                }
            }
            public int maxCount
            {
                get
                {
                    return _maxCount;
                }
                set
                {
                    _maxCount = value;
                }
            }
            public int lengthMassive
            {
                get
                {
                    return _length;
                }
                set
                {
                    _length = value;
                }
            }
        }

        static void Task02()
        {
            Console.Write("Введите размерность массива: ");
            string strRaz = Console.ReadLine();
            int raz = Convert.ToInt32(strRaz);

            Console.Write("Введите шаг значения в массиве: ");
            string strStep = Console.ReadLine();
            int step = Convert.ToInt32(strStep);

            Console.Write("Введите начальное значение: ");
            string strZn = Console.ReadLine();
            int zn = Convert.ToInt32(strZn);

            var mas = new Massiv(raz, step, zn);

            for (int i = 0; i < raz; i++)
            {
                Console.Write("{0} ", mas[i]);
            }
            Console.WriteLine("");
            Console.WriteLine($"Сумма элементов массива: {mas.sum}");
            mas.Inverse(false);
            Console.WriteLine($"Инвентированный массив!!!");
            for (int i = 0; i < raz; i++)
            {
                Console.Write("{0} ", mas[i]);
            }
            Console.WriteLine("");

            Console.Write($"Введите целое число: ");
            string strNumber = Console.ReadLine();
            int number = Convert.ToInt32(strNumber);
            mas.Multi(number, false);
            Console.WriteLine($"Элементы массива умноженные на число {number}:");
            for (int i = 0; i < raz; i++)
            {
                Console.Write("{0} ", mas[i]);
            }
            Console.WriteLine("");

            Console.WriteLine($"Количество максимальных элементов: {mas.maxCount}");

            // работа с массива через файл
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Массив загруженный из файла!!!");
            var massive = new Massiv();
            for (int i = 0; i < massive.lengthMassive; i++)
            {
                Console.Write("{0} ", massive[i]);
            }
            Console.WriteLine("");

            Console.WriteLine($"Сумма элементов массива: {massive.sum}");
            massive.Inverse(true);
            Console.WriteLine($"Инвентированный массив!!!");
            for (int i = 0; i < massive.lengthMassive; i++)
            {
                Console.Write("{0} ", massive[i]);
            }
            Console.WriteLine("");

            Console.Write($"Введите целое число: ");
            string strNumber2 = Console.ReadLine();
            int number2 = Convert.ToInt32(strNumber2);
            massive.Multi(number2, true);
            Console.WriteLine($"Элементы массива умноженные на число {number}:");
            for (int i = 0; i < massive.lengthMassive; i++)
            {
                Console.Write("{0} ", massive[i]);
            }
            Console.WriteLine("");

            Console.WriteLine($"Количество максимальных элементов: {massive.maxCount}");

            Console.ReadKey();
        }
        #endregion

        #region Task03
        struct Account
        {
            public string _login;
            public string _password;
        }
        static string[] LoadArrayFromFile()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "Account.txt";
            string login = "";
            string password = "";
            string[] account = new string[2];
            if (File.Exists(filePath))
            {
                StreamReader reader = new StreamReader(filePath);

                var str = reader.ReadLine();
                if (str != "account")
                {
                    throw new Exception("Файл не содержит логина и пароля!!!");
                }
                login = reader.ReadLine();
                password = reader.ReadLine();
                reader.Close();
            }
            else
            {
                throw new FileNotFoundException();
            }
            account[0] = login;
            account[1] = password;
            return account;
        }
        static void Task03()
        {
            Account account;
            var mas = LoadArrayFromFile();
            account._login = mas[0];
            account._password = mas[1];
            int k = 3;
            bool result = false;
            do
            {
                Console.Clear();
                Console.WriteLine($"Осталось попыток: {k}");
                Console.Write("Введите логин: ");
                string strLogin = Console.ReadLine();
                Console.Write("Введите пароль: ");
                string strPassword = Console.ReadLine();

                if (strLogin == account._login && strPassword == account._password)
                {
                    result = true;
                    break;
                }
                k--;
            }
            while (k > 0);
            if (result)
                Console.WriteLine("Доступ разрешен!");
            else
                Console.WriteLine("Доступ запрешен!");
            Console.ReadKey();
        }
        #endregion

        static void Main(string[] args)
        {
            bool isExit = false;
            do
            {
                int number;
                Console.Clear();
                Console.Write("Введите номер задания (1-3), либо число 0 для выхода: ");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    number = 0;
                }
                switch (number)
                {
                    case 0:
                        isExit = true;
                        break;
                    case 1:
                        Console.Clear();
                        Task01();
                        break;
                    case 2:
                        Console.Clear();
                        Task02();
                        break;
                    case 3:
                        Console.Clear();
                        Task03();
                        break;
                }
            }
            while (!isExit);
        }
    }
}
