// See https://aka.ms/new-console-template for more information

using System.IO;
using System.Collections.Generic;
using System.Data;

using System;
using System.Text;
using System.Data.SqlClient;   // System.Data.dll
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
//using System.Data;           // For:  SqlDbType , ParameterDirection

namespace csharp_db_test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<CSV_Result> result = new List<CSV_Result>();
                
                //Read CSV file
                string filePath;
                filePath = @"C:\Test\Test.csv";
                var reader = new StreamReader(File.OpenRead(filePath));
                int num = 1;
                string[] values = null;

                var line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    values = line.Split(",");

                    result.Add(new CSV_Result { num = num.ToString(), Name = values[0], age = values[1], school = values[2] });
                    
                    num++;
                }
                reader.Close();

                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
                HttpClient client = new HttpClient(handler);
                string strAPIHost = "https://localhost:7050/swagger/v1/swagger.json";

                client.BaseAddress = new Uri(strAPIHost);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string test = "CSV_Result";
                //POST
                string json = JsonConvert.SerializeObject(result);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var P_response = client.PostAsync(test, stringContent).Result;
                if (P_response.IsSuccessStatusCode) 
                {
                    Console.WriteLine(P_response.Content.ReadAsStringAsync().Result);
                }

                //GET
                //string strAPIHost = "https://localhost:7050/Data";

                //client.BaseAddress = new Uri(strAPIHost);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var G_response = client.GetAsync(test).Result;
                if (G_response.IsSuccessStatusCode)
                {
                    //result = JsonConvert.DeserializeObject<List<Student>>(response.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(G_response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public partial class CSV_Result
        {
            public string num { get; set; }
            public string Name { get; set; }
            public string age { get; set; }
            public string school { get; set; }
        }
        
        
    } // EndOfClass
}