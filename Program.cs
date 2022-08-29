using System;
using System.IO;

namespace DeleteDirectoryV2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users/cavva/Desktop/sss";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            Console.WriteLine("Исходный размер папки: " + GetSize(dirInfo) + " байт"); 
            DeleteCatalog(dirInfo);
            Console.WriteLine("Текущий размер папки: " + GetSize(dirInfo) + " байт"); 
            Console.ReadKey();
        }
        static public void DeleteCatalog(DirectoryInfo dirInfo)
        {
            int count = 0;
            long cleanOutSize = 0;

            try
            {
                if (dirInfo.Exists)
                {
                    var files = dirInfo.GetFiles();
                    var directories = dirInfo.GetDirectories();

                    foreach (var directory in directories)
                    {
                        if (directory.LastAccessTime.AddMinutes(30) < DateTime.Now)
                        {
                            DeleteCatalog(directory);

                            if (directory.GetFiles().Length == 0)
                            {
                                directory.Delete(true);
                                count++;
                            }
                        }
                    }
                    foreach (var file in files)
                    {
                        if (file.LastAccessTime.AddMinutes(30) < DateTime.Now)
                        {
                            cleanOutSize += file.Length;
                            file.Delete();
                            count++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Папки по данному пути не существует, проверьте правильность ввода!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Удалено : {count} файлов, весом в {cleanOutSize} байт");
        }
        public static long GetSize(DirectoryInfo dirInfo)
        {
            long size = 0;

            try
            {
                if (dirInfo.Exists)
                {
                    var files = dirInfo.GetFiles();
                    var directories = dirInfo.GetDirectories();

                    foreach (var file in files)
                    {
                        size += file.Length;
                    }
                    foreach (var directory in directories)
                    {
                        size += GetSize(directory);
                    }
                }
                else
                {
                    Console.WriteLine("Папки по данному пути не найдено, проверьте правильность ввода");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return size;
        }
    }
}
