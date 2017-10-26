using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceWeather
{
    class Email
    {
        IniFile INI = new IniFile();

        public void sendEmail(string mess, string theme)
        {
            try
            {
                string fromEmailUser = INI.ReadINI("EmailSetting", "fromEmailUser");
                string emails = INI.ReadINI("EmailSetting", "emails");
                string smtpString = INI.ReadINI("EmailSetting", "smtpString");
                int smtpPort = int.Parse(INI.ReadINI("EmailSetting", "smtpPort"));
                string mailUser = INI.ReadINI("EmailSetting", "mailUser");
                string mailUserPassword = INI.ReadINI("EmailSetting", "mailUserPassword");

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(fromEmailUser, "Скрипт погоды");

                // создаем объект сообщения
                using (MailMessage m = new MailMessage())
                {
                    m.From = from;
                    string[] adrs = emails.Split(';');
                    foreach (string adr in adrs)
                    {
                        m.To.Add(adr);
                    }

                    // тема письма
                    m.Subject = theme;
                    // текст письма
                    m.Body = mess;
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    using (SmtpClient smtp = new SmtpClient(smtpString, smtpPort))
                    {
                        if (Scan(smtpPort, smtpString))
                        {
                            // логин и пароль
                            smtp.Credentials = new NetworkCredential(mailUser, mailUserPassword);
                            smtp.EnableSsl = true;
                            smtp.Send(m);
                        }
                        else
                        {
                            throw new Exception("Ошибка отправки письма, порт закрыт");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket SockClient = (Socket)ar.AsyncState;
                SockClient.EndConnect(ar);
            }
            catch
            {

            }
        }

        public bool Scan(int port, string serverString)
        {
            IPHostEntry host = Dns.GetHostEntry(serverString);

            //Создаем сокет
            IPEndPoint IpEndP = new IPEndPoint(host.AddressList[0], port);
            Socket MySoc = new Socket(AddressFamily.InterNetwork,
                                     SocketType.Stream, ProtocolType.Tcp);
            //Пробуем подключится к указанному хосту
            IAsyncResult asyncResult = MySoc.BeginConnect(IpEndP,
                             new AsyncCallback(ConnectCallback), MySoc);

            if (!asyncResult.AsyncWaitHandle.WaitOne(100, false))
            {
                MySoc.Close();
                return false;
            }

            MySoc.Close();
            return true;
        }
    }
}
