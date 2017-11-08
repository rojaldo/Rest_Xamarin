using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            ProcessRequest();
        }

        private async void ProcessRequest()
        {
            var data = await GetRequest("https://opentdb.com/api.php?amount=10");
            Console.WriteLine(data.response_code);
        }

        private async Task<dynamic> GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            dynamic data = null;
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        data = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                        label.Text = content;
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
