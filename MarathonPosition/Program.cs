using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace MarathonPosition
{
    class Program
    {
        private static string url = "http://www.clasificacionmaratones.com/results21k/pag/{0}";

        static void Main(string[] args)
        {
            try
            {
                var regExpPersonas = new [] {
                    new Regex("sebastian.+lopez".ToLower()),
                    new Regex("humberto.+jara".ToLower()),
                    new Regex("mauro.+dimare".ToLower())
                };
                //var regExpPersona = new Regex("TORRES");
                int position = 0, pageIdx = 100;
                do
                {
                    var request = WebRequest.Create(string.Format(url, ++pageIdx));
                    request.Credentials = CredentialCache.DefaultCredentials;
                    var response = request.GetResponse();
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(dataStream);
                    var responseFromServer = reader.ReadToEnd();
                    foreach (var regExpPersona in regExpPersonas)
                    {
                        var matches = regExpPersona.Matches(responseFromServer.ToLower());
                        if (matches.Count > 0)
                        {
//                            var data = new Regex(string.Format("(?:[^<]+<td>([^<]+)</td>)+[\r|\n|\\s]*[^<]+<td>[\r|\n|\\s]*[^<]+<a\\s[^>]+>[\r|\n|\\s]*{0}[^<]+</a></td>(?:[^<]+<td>([^<]+)</td>)+", regExpPersona.ToString())).Matches(responseFromServer.ToLower());

                            Console.WriteLine("\r\nPersona:{0}, pageIdx{1}", regExpPersona.ToString(), pageIdx);
                        }
                    }
                    //Console.Write("{0}, ", pageIdx);
                    reader.Close();
                } while (pageIdx < 1200);
                Console.Write("\r\nListo");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
