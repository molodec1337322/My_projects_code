using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace fileSaver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileWorker.WriteToFile("test.txt", "Hello World!");

            string textToRead = await FileWorker.ReadFromFile("test.txt");
            Console.WriteLine(textToRead);

            Console.ReadKey();
        }
    }

    static class FileWorker
    {
        /// <summary>
        /// Записывает текст в файл
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="textToWrite"></param>
        public static async void WriteToFile(string filename, string textToWrite)
        {
            string path = Environment.CurrentDirectory;

            using (FileStream fstream = new FileStream($"{path}\\{filename}", FileMode.OpenOrCreate))
            {
                byte[] arrayToWrite = Encoding.Default.GetBytes(textToWrite);
                await fstream.WriteAsync(arrayToWrite, 0, arrayToWrite.Length);
            }
        }

        /// <summary>
        /// Возвращает текст из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static async Task<string> ReadFromFile(string filename)
        {
            return await Task.Run(() => Read(filename)); 
        }

        private static string Read(string filename)
        {
            string path = Environment.CurrentDirectory;

            using (FileStream fstream = new FileStream($"{path}\\{filename}", FileMode.Open))
            {
                try
                {
                    byte[] dataArray = new byte[fstream.Length];
                    fstream.ReadAsync(dataArray, 0, dataArray.Length);
                    return Encoding.Default.GetString(dataArray);
                }
                catch(DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    return "";
                }
            }
        }
    }
}
