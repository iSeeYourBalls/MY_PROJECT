using AngleSharp.Parser.Html;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceWeather
{
    class Job : Quartz.IJob
    {
        public void CreateJob()
        {
            IniFile INI = new IniFile();
            int intervalInHours = int.Parse(INI.ReadINI("TriggerSetting", "intervalInHours"));
            int whatHour = int.Parse(INI.ReadINI("TriggerSetting", "whatHour"));
            int whatMinute = int.Parse(INI.ReadINI("TriggerSetting", "whatMinute"));
            bool isMinute = bool.Parse(INI.ReadINI("TriggerSetting", "everyMinute"));
            int intervalMinute = int.Parse(INI.ReadINI("TriggerSetting", "intervalMinute"));

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<Job>().Build();
            if (isMinute)
            {
                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInMinutes(intervalMinute)
                    )
                    .Build();
                scheduler.ScheduleJob(job, trigger);
            }
            else
            {
                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(intervalInHours)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(whatHour, whatMinute))
                    )
                    .Build();
                scheduler.ScheduleJob(job, trigger);
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                IniFile INI = new IniFile();
                string conn = INI.ReadINI("ConnectionToDB", "connectionString");

                Email send = new Email();

                //Connection
                var urlDnepr = INI.ReadINI("Connection", "urlDnepr");
                string ipProxy = INI.ReadINI("Connection", "proxy");
                int port = int.Parse(INI.ReadINI("Connection", "port"));
                string proxyUser = INI.ReadINI("Connection", "proxyUser");
                string proxyUserPass = INI.ReadINI("Connection", "proxyUserPass");

                //proxy
                WebClient wc = new WebClient();
                WebProxy proxy = new WebProxy(ipProxy, port);
                proxy.Credentials = new NetworkCredential(proxyUser, proxyUserPass);
                wc.Proxy = proxy;

                string depotAndHTTP = INI.ReadINI("Connection", "depotAndHTTP");
                string[] words = depotAndHTTP.Split(',');
                string[] depot = new string[words.Length * 2];
                string[] HTTP = new string[words.Length * 2];

                //для письма
                string aboutWeather = "";

                for (int i = 0; i < words.Length; i++)
                {
                    depot[i] = words[i].Split('|')[0];
                    HTTP[i] = words[i].Split('|')[1];

                    //Получаем URL
                    var page = wc.DownloadString(HTTP[i]);

                    //CSS Style
                    string CSS = INI.ReadINI("DataForParsing", "CSS");
                    int night = int.Parse(INI.ReadINI("DataForParsing", "night"));
                    int day = int.Parse(INI.ReadINI("DataForParsing", "day"));

                    //Константы
                    int maxDayTemp = int.Parse(INI.ReadINI("Constants", "maxDayTemp"));
                    int minDayTemp = int.Parse(INI.ReadINI("Constants", "minDayTemp"));
                    int maxMidDayTemp = int.Parse(INI.ReadINI("Constants", "maxMidDayTemp"));
                    int minMidDatTemp = int.Parse(INI.ReadINI("Constants", "minMidDatTemp"));

                    //Получение температуры
                    double tommorowNight = double.Parse(takeTemp(page, CSS)[night].ToString());
                    double tommorowAfternoon = double.Parse(takeTemp(page, CSS)[day].ToString());

                    //Логика определяющая нежуен режим или нет
                    bool result;
                    string date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

                    double middleTemp = (tommorowAfternoon + tommorowNight) / 2;

                    if (tommorowNight >= maxDayTemp || tommorowAfternoon >= maxDayTemp || tommorowNight <= minDayTemp || tommorowAfternoon <= minDayTemp)
                        result = true;
                    else if (middleTemp < minMidDatTemp || middleTemp > maxMidDayTemp)
                        result = true;
                    else
                        result = false;

                    insertToDB(depot[i], tommorowNight, tommorowAfternoon, result, middleTemp, date, conn);
                    //Console.WriteLine(depot[i] + " Ночь - " + tommorowNight + " День - " + tommorowAfternoon + " Результат - " + result + " Средняя температура - " + middleTemp + "\r\n" + date);

                    string resultText = " </br>согласно данных режим <b style='color:red'>НЕ включаем</b>";

                    if (result)
                        resultText = " </br>согласно данных режим <b style='color:red'>включаем</b>";

                    aboutWeather += "<p>" + depot[i] + " : ночь " + tommorowNight + ", день " + tommorowAfternoon + ", средняя " + middleTemp + resultText + "</p>";

                }

                string body = "<h2>Отработано успешно!</h2>" +
                              "<p>Погода на завтра следующая :</p>" +
                              aboutWeather;

                send.sendEmail(body, "Информация о необходимости включения дополнительного оборудования!");
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Email send = new Email();
                send.sendEmail("<h2>Информация для разработчика!</h2><p>" + ex + "</p>", "Внимание ошибка расчета включения оборудования!");
                Environment.Exit(0);
            }
        }

        public static string[] takeTemp(string page, string CSSStyle)
        {
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(page);

                var infoFromSite = document.QuerySelectorAll(CSSStyle);

                string[] tommorow = new string[infoFromSite.Length];

                for (int i = 0; i < infoFromSite.Length; i++)
                {
                    tommorow[i] = infoFromSite[i].TextContent.Replace("\u2212", "-");
                }

                return tommorow;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Email send = new Email();
                send.sendEmail("<h2>Информация для разработчика!</h2><p>" + ex + "</p>", "Внимание ошибка расчета включения оборудования!");
                return null;
            }
        }

        private static void insertToDB(string depot, double tommorowNight, double tommorowAfternoon, bool result, double middleTemp, string date, string conn)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conn))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        int res = 0;
                        if (result)
                            res = 1;

                        cmd.Connection = cn;
                        cmd.CommandText = "INSERT into testWeather (depotCode, nightTemp, dayTemp, middleTemp, result, dateTemp, dateInsert) VALUES (@depotCode, @nightTemp, @dayTemp, @middleTemp, @result, @dateTemp, CURRENT_TIMESTAMP)";
                        cmd.Parameters.AddWithValue("@depotCode", depot);
                        cmd.Parameters.AddWithValue("@nightTemp", tommorowNight);
                        cmd.Parameters.AddWithValue("@dayTemp", tommorowAfternoon);
                        cmd.Parameters.AddWithValue("@middleTemp", middleTemp);
                        cmd.Parameters.AddWithValue("@result", res);
                        cmd.Parameters.AddWithValue("@dateTemp", date);
                        cmd.ExecuteNonQuery();

                        SqlDataReader dr;
                        cmd.CommandText = "select * from weather where depot = @depotCode and datework = @dateTemp";
                        dr = cmd.ExecuteReader();
                        bool isHaveRows = dr.HasRows;
                        dr.Close();
                        if (isHaveRows)
                        {
                            cmd.CommandText = "UPDATE weather SET isWork = @result, userName='System', modifyDate = CURRENT_TIMESTAMP, nightTemp = @nightTemp, dayTemp = @dayTemp, middleTemp = @middleTemp WHERE depot = @depotCode and datework = @dateTemp";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "INSERT into weather (isWork,userName,depot,dateCreate,dateWork, nightTemp, dayTemp, middleTemp) VALUES (@result,'System',@depotCode,CURRENT_TIMESTAMP,@dateTemp,@nightTemp, @dayTemp, @middleTemp)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Email send = new Email();
                send.sendEmail("<h2>Информация для разработчика!</h2><p>" + ex + "</p>", "Внимание ошибка расчета включения оборудования!");
                Environment.Exit(0);
            }
        }
    }
}
