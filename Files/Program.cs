using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\dm\Desktop";           

            //CreateDirFile(path);
            Exmpl05(path);
        }

        public static void CreateDirFile(string path)
        {            
            DirectoryInfo dir1 = new DirectoryInfo(Path.Combine(path, "K1"));
            DirectoryInfo dir2 = new DirectoryInfo(Path.Combine(path, "K2"));
            dir1.Create();
            dir2.Create();

            FileInfo file1 = new FileInfo(Path.Combine(path, "K1", "t1.txt"));
            FileInfo file2 = new FileInfo(Path.Combine(path, "K1", "t2.txt"));
            file1.Create();
            file2.Create();
        }

        public static void Exmpl01()
        {
            //1.	В файле записана непустая последовательность целых чисел, являющихся числами Фибоначчи. Приписать еще столько же чисел этой последовательности.

            List<int> seqFibo = new List<int>();
            try
            {
                using (StreamReader sr=new StreamReader("fibo.txt"))
                {
                    string tmp = sr.ReadToEnd();
                    string[] str = tmp.Split(' ');
                    foreach (string i in str)
                    {
                        seqFibo.Add(Int32.Parse(i));
                    }
                }

                int k = seqFibo.Count;
                for (int i = k; i < 2*k-2; i++)
                {
                    seqFibo.Add(seqFibo[i - 2] + seqFibo[i - 1]);
                }

                using (StreamWriter sw=new StreamWriter("fibo.txt"))
                {
                    foreach (int i in seqFibo)
                    {
                        sw.Write(i + " ");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Exmpl02()
        {
            //2.	Сложить два целых числа А и В.
            //Входные данные. В единственной строке входного файла INPUT.TXT записано два натуральных числа через пробел.
            //Выходные данные. В единственную строку выходного файла OUTPUT.TXT нужно вывести одно целое число — сумму чисел А и В.

            List<int> numbers = new List<int>();
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    string tmp = sr.ReadToEnd();
                    string[] str = tmp.Split(' ');
                    foreach (string i in str)
                    {
                        numbers.Add(Int32.Parse(i));
                    }
                }

                using (StreamWriter sw=new StreamWriter("output.txt"))
                {
                    sw.Write(numbers.Sum());
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Exmpl03()
        {
            //1.	Написать программу, читающую побайтно заданный файл и подсчитывающую число появлений каждого из 256 возможных знаков.

            Dictionary<char, int> dic = new Dictionary<char, int>();
            try
            {
                using (StreamReader sr=new StreamReader("text.txt", Encoding.Default))
                {
                    string tmp = sr.ReadToEnd();
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (!dic.ContainsKey(tmp[i]))
                            dic.Add(tmp[i], 1);
                        else
                            dic[tmp[i]]++;
                    }
                }
                foreach (var i in dic)
                {
                    Console.WriteLine(i.Key+" "+i.Value);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Exmpl04()
        {
            //2.	С помощью класса StreamWriter записать в текстовый файл свое имя, фамилию и возраст. Каждая запись должна начинаться с новой строки.

            try
            {
                using (StreamWriter sw=new StreamWriter("name.txt"))
                {
                    Console.Write("Ваше имя: ");
                    sw.WriteLine(Console.ReadLine());
                    Console.Write("Ваша фамилия: ");
                    sw.WriteLine(Console.ReadLine());
                    Console.Write("Ваш возраст: ");
                    sw.WriteLine(Console.ReadLine());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Exmpl05(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(path, "K1", "t1.txt")))
                {
                    sw.WriteLine("Иванов Иван Иванович, 1965 года рождения, место жительства г. Саратов");
                }

                using (StreamWriter sw = new StreamWriter(Path.Combine(path, "K1", "t2.txt")))
                {
                    sw.WriteLine("Петров Сергей Федорович, 1966 года рождения, место жительства г.Энгельс");
                }

                using (StreamReader sr = new StreamReader(Path.Combine(path, "K1", "t1.txt")))
                {
                    string str = sr.ReadLine();
                    using (StreamWriter sw = new StreamWriter(Path.Combine(path, "K2", "t3.txt")))
                    {
                        sw.WriteLine(str);
                    }
                }

                using (StreamReader sr = new StreamReader(Path.Combine(path, "K1", "t2.txt")))
                {
                    string str = sr.ReadLine();
                    using (StreamWriter sw = new StreamWriter(Path.Combine(path, "K2", "t3.txt"), true))
                    {
                        sw.WriteLine(str);
                    }
                }

                FileInfo file1 = new FileInfo(Path.Combine(path, "K1", "t1.txt"));
                FileInfo file2 = new FileInfo(Path.Combine(path, "K1", "t2.txt"));
                FileInfo file3 = new FileInfo(Path.Combine(path, "K2", "t3.txt"));

                FileInfo[] files = new FileInfo[] { file1, file2, file3 };
                foreach (FileInfo i in files)
                {
                    Console.WriteLine("Полный путь: {0}", i.FullName);
                    Console.WriteLine("Название: {0}", i.Name);
                    Console.WriteLine("Время создания: {0}", i.CreationTime);
                    Console.WriteLine("Атрибуты: {0}", i.Attributes);
                    Console.WriteLine("--------------------------------\n");
                }

                file1.CopyTo(Path.Combine(path, "K2", "t1.txt"), true);
                file2.MoveTo(Path.Combine(path, "K2", "t2.txt"));

                DirectoryInfo dir1 = new DirectoryInfo(Path.Combine(path, "K1"));
                DirectoryInfo dir2 = new DirectoryInfo(Path.Combine(path, "K2"));
                dir1.Delete(true);

                FileInfo[] files2 = dir2.GetFiles();
                foreach (FileInfo i in files2)
                {
                    Console.WriteLine("Полный путь: {0}", i.FullName);
                    Console.WriteLine("Название: {0}", i.Name);
                    Console.WriteLine("Время создания: {0}", i.CreationTime);
                    Console.WriteLine("Атрибуты: {0}", i.Attributes);
                    Console.WriteLine("--------------------------------\n");
                }
            }
            catch 
            {
                throw new Exception("Запустите сначала метод CreateDirFile(string path)!");
            }
            
        }
    }
}
