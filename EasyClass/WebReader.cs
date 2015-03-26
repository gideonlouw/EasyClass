﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyClass
{
    public class WebReader
    {
        public bool AddHeader { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }

        public WebReader()
        {
            AddHeader = false;
        }

        public string BuildClassFromJson(string inputurl, string namespacename, string classname, [Optional] string classoutputfolder)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(inputurl);

            if (AddHeader == true)
            {
                webRequest.Headers.Add(HeaderKey, HeaderValue);
            }

            string responseData = string.Empty;
            HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader responseReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
            }

            var workData = responseData;
            var path = "";

            if (classoutputfolder == null)
            {
                path = Directory.GetCurrentDirectory() + "/" + classname + ".cs";
            }
            else
            {
                path = classoutputfolder + "/" + classname + ".cs";
            }

            //Create class file
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("using System;");
            sw.WriteLine("namespace " + namespacename);
            sw.WriteLine("{");
            sw.WriteLine("public class " + classname);
            sw.WriteLine("{");

            JObject jObject = JObject.Parse(workData);
            var voxzalData = jObject["query_result"][0];
            foreach (var vData in voxzalData)
            {
                var counter = vData.Count();

                var entry = vData.First().ToString();
                var actualdata = entry.Split(':');
                sw.WriteLine("public string " + actualdata[0].ToString().Replace("\"", "") + " { get; set; }");

                for (int i = 1; i < counter; i++)
                {
                    entry = vData.Skip(i).First().ToString();
                    actualdata = entry.Split(':');

                    sw.WriteLine("public string " + actualdata[0].ToString().Replace("\"", "") + " { get; set; }");
                }

            }
            sw.WriteLine("}");
            sw.WriteLine("}");
            sw.Close();
            return "done";
        }
    }
}
