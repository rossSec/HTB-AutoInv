using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;

namespace HackTheBox_AutoInvite
{
    class HttpMethods
    {
        public static bool Post(string url, string postData, string referer, CookieContainer cookies)
        {
            //Error Handling
            string key = "Invalid";

            //Sends POST Request to API Website
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.CookieContainer = cookies;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:86.0) Gecko/20100101 Firefox/86.0";
            request.Accept = "*/*";
            request.Headers.Add("Accept-Language", "en-GB,en;q=0.5");
            request.ContentType = "application/json";
            request.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            request.Headers.Add("Pragma", "no-cache");
            request.Headers.Add("Expires", "0");
            request.Headers.Add("Origin", "https://reqbin.com/");
            request.Headers.Add("DNT", "1");
            request.Referer = "https://reqbin.com/";
            request.Headers.Add("Sec-GPC", "1");

            //POSTS JSON Data to Website
            using (var streamWrite = new StreamWriter(request.GetRequestStream()))
            {
                string json = postData;
                streamWrite.Write(json);
                streamWrite.Flush();
            }

            //Captures response
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            cookies.Add(resp.Cookies);
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string pageSrc = sr.ReadToEnd();

            //Regex to capture Base64 from HttpResponse
            int pFrom = pageSrc.IndexOf(@":\") + @":\".Length;
            int pTo = pageSrc.LastIndexOf(@"=\");
            string EncCode = pageSrc.Substring(pFrom, pTo - pFrom);
            string Code2 = EncCode.Substring(1, 40);

            //Console Text
            Console.WriteLine("[+] Base64 String Found: " + Code2);
            Console.Write("[+] Decrypting Base64.");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");

            //Decoding previously found Base64 String
            var decodedCode = Convert.FromBase64String(Code2);
            Console.WriteLine("");

            Console.WriteLine("");
            Console.Write("[+] Invite code found: ");
            Console.Write(Encoding.UTF8.GetString(decodedCode));
            

            Console.WriteLine("");

            Console.Write("[+] Press any key to exit: ");
            Console.ReadKey();
            Environment.Exit(0);
            


            sr.Dispose();
            return (!pageSrc.Contains(key));


        }
    }
}
