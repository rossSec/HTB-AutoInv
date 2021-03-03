using System;
using System.Net;


namespace HackTheBox_AutoInvite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "HackTheBox AutoInvite";
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("[+] HackTheBox Code Generator | Programmed by Ross#0005");

            //Downloads JSON string from my API Website
            WebClient wc = new WebClient();
            string data = wc.DownloadString("http://rossapi2920901.000webhostapp.com/ross.txt");

            CookieContainer cookies = new CookieContainer();
            bool result = HttpMethods.Post("https://api.reqbin.com/api/v1/requests", data, "https://reqbin.com/", cookies);
        }
    }
}
