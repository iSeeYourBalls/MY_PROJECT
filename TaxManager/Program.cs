using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxManager
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            Login form2 = new Login();
            if (form2.status == true)
            {
                Application.Run(new Main());
            }
        }
    }
    class GlobalVariable
    {
        public static string userName { get; set; }
        public static string registrNumber { get; set; }
        public static bool statusLogin { get; set; }
        public static string matrixDate { get; set; }
        public static int id_user { get; set; }
        public static string resourceName { get; set; }
        public static bool editTariff { get; set; }
        public static int idTariff { get; set; }
        public static int sh { get; set; }
        public static double maxHourInTrip { get; set; }
        public static double tAndTinCity { get; set; }
        public static double tInCity { get; set; }
        public static double tAndTOutCity { get; set; }
        public static double tOutCity { get; set; }
        public static double addPerForPoint { get; set; }
        public static double maxKmToFirstPoint { get; set; }
        public static bool cityOrNot { set; get; }
        public static int[] acts {set; get;}
        public static bool isPrintManyActs { set; get; }
        public static string connectionString = "Data Source=qwerty;Integrated Security=False;Initial Catalog=qwerty;Persist Security Info=True;User ID=qwerty;Password=qwerty";
    }
    public class Translit
    {
            // объявляем и заполняем словарь с заменами
            Dictionary<string, string> dictionaryChar = new Dictionary<string, string>()
            {
                {"А","A"},
                {"Б","B"},
                {"В","B"},
                {"Г","G"},
                {"Д","D"},
                {"Е","E"},
                {"Ё","E"},
                {"Ж","ZH"},
                {"З","Z"},
                {"И","I"},
                {"Й","Y"},
                {"К","K"},
                {"Л","L"},
                {"М","M"},
                {"Н","H"},
                {"О","O"},
                {"П","P"},
                {"Р","P"},
                {"С","C"},
                {"Т","T"},
                {"У","Y"},
                {"Ф","F"},
                {"Х","X"},
                {"Ц","TS"},
                {"Ч","CH"},
                {"Ш","SH"},
                {"Щ","SCH"},
                {"Ъ","'"},
                {"Ы","YI"},
                {"Ь",""},
                {"Э","E"},
                {"Ю","YU"},
                {"Я","YA"}
            };
            /// <summary>
            /// метод делает транслит на латиницу
            /// </summary>
            /// <param name="source"> это входная строка для транслитерации </param>
            /// <returns>получаем строку после транслитерации</returns>
        public string TranslitFileName(string source)
        {
            var result = "";
            // проход по строке для поиска символов подлежащих замене которые находятся в словаре dictionaryChar
            foreach (var ch in source)
            {
                var ss = "";
                // берём каждый символ строки и проверяем его на нахождение его в словаре для замены,
                // если в словаре есть ключ с таким значением то получаем true 
                // и добавляем значение из словаря соответствующее ключу
                if (dictionaryChar.TryGetValue(ch.ToString().ToUpper(), out ss))
                {
                    result += ss;
                }
                // иначе добавляем тот же символ
                else result += ch.ToString().ToUpper();
            }
            return result;
        }
    }

    public class months
    {
        public string name {get; set;}
        public int value {get; set;}
    }
}
