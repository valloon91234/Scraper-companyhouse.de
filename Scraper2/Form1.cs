using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Scraper2
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
            //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/xml, text/xml, */*; q=0.01");
            //httpClient.DefaultRequestHeaders.Host = "vahan.parivahan.gov.in";
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            //httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            //httpClient.DefaultRequestHeaders.Referrer = new Uri("https://ant.aliceblueonline.com/");
            //httpClient.DefaultRequestHeaders.Add("X-Device-Type", "web");
            //httpClient.DefaultRequestHeaders.Add("Faces-Request", "partial/ajax");
        }

        private async Task<String> asyncGet(String url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            {
                String headers = @"Faces-Request: partial/ajax
                Origin: https://vahan.parivahan.gov.in
                Sec-Fetch-Mode: cors
                Sec-Fetch-Site: same-origin";
                String[] lines = headers.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String line in lines)
                {
                    String[] array = line.Split(new char[] { ':' }, 2);
                    String fieldName = array[0].Trim();
                    String fieldValue = array[1].Trim();
                    requestMessage.Headers.Add(fieldName, fieldValue);
                }
                //String cookie = textBox_Cookie.Text.Trim();
                //requestMessage.Headers.Add("Cookie", cookie);
            }
            var response = await httpClient.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        private async Task<String> asyncPost(String url, String data)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            {
                String headers = @"Faces-Request: partial/ajax
                Origin: https://vahan.parivahan.gov.in
                Sec-Fetch-Mode: cors
                Sec-Fetch-Site: same-origin";
                String[] lines = headers.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String line in lines)
                {
                    String[] array = line.Split(new char[] { ':' }, 2);
                    String fieldName = array[0].Trim();
                    String fieldValue = array[1].Trim();
                    requestMessage.Headers.Add(fieldName, fieldValue);
                }
                //String cookie = textBox_Cookie.Text.Trim();
                //requestMessage.Headers.Add("Cookie", cookie);
            }
            var values = new Dictionary<string, string>();
            {
                String[] lines = data.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String line in lines)
                {
                    int lineLength = line.Length;
                    int k = line.LastIndexOf(':');
                    if (k < 1)
                    {
                        MessageBox.Show("Invalid data");
                        continue;
                    }
                    String fieldName = line.Substring(0, k);
                    String fieldValue = k < lineLength - 1 ? line.Substring(k + 1) : "";
                    values.Add(fieldName, fieldValue);
                }
                requestMessage.Content = new FormUrlEncodedContent(values);
            }
            var response = await httpClient.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        private String HttpGet(String url)
        {
            var result = Task.Run(() => asyncGet(url)).Result;
            return result;
        }

        private String HttpPost(String url, String data)
        {
            var result = Task.Run(() => asyncPost(url, data)).Result;
            return result;
        }


        private String requestGet(String url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            //httpWebRequest.ContentType = "application/json; charset=UTF-8";
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36";
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            String headers = @"Accept-Language: en-US,en;q=0.9
cookie: __cfduid=da5a345aaa3d3792d506d3b74f081b7191576246199; muuid=631d2fef47b97f00d7178f154ca3f102d517139393b9cdc20eef9ec1ed3b0ed3a%3A2%3A%7Bi%3A0%3Bs%3A5%3A%22muuid%22%3Bi%3A1%3Bs%3A36%3A%22cb34023f-a35f-4676-882e-b1815f8305f2%22%3B%7D; _ga=GA1.2.1685951065.1576246170; __gads=ID=b70d423d81554ad8:T=1576246212:S=ALNI_MbAw92D9TJ9JvyhP_7JpAX552ji7w; __qca=P0-2120231003-1576246179204; _hjid=3c484e60-9c1c-43d1-bd2d-1c930d529700; _gid=GA1.2.1456444986.1577336415; PHPSESSID=jd4vie3b0umd9glbbuvi7lfa2j; _csrf=758a0a2735adf3bcb41053f290eb9d44c663b33bfbb3e4de35cd6af49a032fd0a%3A2%3A%7Bi%3A0%3Bs%3A5%3A%22_csrf%22%3Bi%3A1%3Bs%3A32%3A%22crzzyC2hr_FFdV1VBmgJE9iv9Rw3Dyx2%22%3B%7D; cf_clearance=a362b72d02874dc1e89c786b81b13c70ae6c7af5-1577352010-0-250; _dc_gtm_UA-52755337-1=1; _hjIncludedInSample=1
";
            List<string> listOfHeaders = new List<string>();
            listOfHeaders = headers.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var header in listOfHeaders)
                httpWebRequest.Headers.Add(header);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream receiveStream = httpWebResponse.GetResponseStream();
            Boolean encodingInfoNotFound = string.IsNullOrEmpty(httpWebResponse.CharacterSet) || !Encoding.GetEncodings().Any(e => e.Name == httpWebResponse.CharacterSet);
            StreamReader streamReader = new StreamReader(receiveStream, encodingInfoNotFound ? Encoding.UTF8 : Encoding.GetEncoding(httpWebResponse.CharacterSet));
            String response = streamReader.ReadToEnd();
            streamReader.Close();
            receiveStream.Close();
            httpWebResponse.Close();
            return response;
        }

        private void processPage(String page, Item item)
        {
            String retrivedCompanyName = null, address = null, name1 = null, name2 = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            {
                var nodes = doc.DocumentNode.CssSelect("h1.companyName");
                if (nodes.Count() > 0)
                {
                    var node = nodes.First();
                    retrivedCompanyName = node.GetAttributeValue("data-correction-value")
                        .Replace("&amp;amp;", "&")
                        .Replace("&amp;quot;", "\"");
                }
                else
                {
                    goto line_end;
                }
            }
            {
                var nodes = doc.DocumentNode.CssSelect("div.address");
                if (nodes.Count() > 0)
                {
                    var node = nodes.First();
                    address = node.InnerText.Trim().Split(',')[0];
                }
            }
            {
                var nodes = doc.DocumentNode.CssSelect("div.management-overview");
                if (nodes.Count() > 0)
                {
                    var node = nodes.First();
                    String innerHTML = node.InnerHtml;
                    String h3 = "<h3>Zeichnungsberechtigte</h3>";
                    int i2 = innerHTML.IndexOf(h3);
                    if (i2 > 0)
                    {
                        i2 += h3.Length;
                        int j2 = innerHTML.IndexOf("<h3>", i2);
                        int j3 = innerHTML.Length;
                        if (j2 == -1 || j2 > j3) j2 = j3;
                        String t1 = innerHTML.Substring(i2, j2 - i2).Trim();
                        String[] ta = t1.Split(new String[] { "</a>" }, StringSplitOptions.RemoveEmptyEntries);
                        int i3 = ta[0].LastIndexOf(">");
                        String name1Temp = ta[0].Substring(i3 + 1);
                        if (!String.IsNullOrWhiteSpace(name1Temp)) name1 = name1Temp;
                        if (ta.Length > 1)
                        {
                            int i4 = ta[1].LastIndexOf(">");
                            name2 = ta[1].Substring(i4 + 1);
                        }
                    }
                    else
                    {
                        var aArray = node.SelectNodes("a");
                        if (aArray.Count == 1)
                        {
                            name1 = aArray.First().InnerText;
                        }
                        else if (aArray.Count > 1)
                        {
                            var nodeFirst = aArray.First();
                            //var nodeNext = nodeFirst.NextSibling;
                            var nodeNext = nodeFirst.SelectSingleNode("following-sibling::*[1][self::a]");
                            if (nodeNext != null)
                            {
                                name1 = nodeFirst.InnerText;
                                name2 = nodeNext.InnerText;
                            }
                            else
                            {
                                name1 = nodeFirst.InnerText;
                            }
                        }
                    }
                }
                else
                {
                    var nodeNachbarschaft = doc.DocumentNode.CssSelect("#Nachbarschaft");
                    if (nodeNachbarschaft.Count() > 0)
                    {
                        var aOwnersRowArray = nodeNachbarschaft.CssSelect("a.ownersRow");
                        if (aOwnersRowArray.Count() > 0)
                        {
                            String newHref = aOwnersRowArray.First().GetAttributeValue("href");
                            String newPage = requestGet("https://www.companyhouse.de" + newHref);

                            doc.LoadHtml(newPage);

                            var nodesNew = doc.DocumentNode.CssSelect("div.management-overview");
                            if (nodesNew.Count() > 0)
                            {
                                var node = nodesNew.First();
                                String innerHTML = node.InnerHtml;
                                String h3 = "<h3>Zeichnungsberechtigte</h3>";
                                int i2 = innerHTML.IndexOf(h3);
                                if (i2 > 0)
                                {
                                    i2 += h3.Length;
                                    int j2 = innerHTML.IndexOf("<h3>", i2);
                                    int j3 = innerHTML.Length;
                                    if (j2 == -1 || j2 > j3) j2 = j3;
                                    String t1 = innerHTML.Substring(i2, j2 - i2).Trim();
                                    String[] ta = t1.Split(new String[] { "</a>" }, StringSplitOptions.RemoveEmptyEntries);
                                    int i3 = ta[0].LastIndexOf(">");
                                    String name1Temp = ta[0].Substring(i3 + 1);
                                    if (!String.IsNullOrWhiteSpace(name1Temp)) name1 = name1Temp;
                                    if (ta.Length > 1)
                                    {
                                        int i4 = ta[1].LastIndexOf(">");
                                        name2 = ta[1].Substring(i4 + 1);
                                    }
                                }
                                else
                                {
                                    var aArray = node.SelectNodes("a");
                                    if (aArray.Count == 1)
                                    {
                                        name1 = aArray.First().InnerText;
                                    }
                                    else if (aArray.Count > 1)
                                    {
                                        var nodeFirst = aArray.First();
                                        var nodeNext = nodeFirst.SelectSingleNode("following-sibling::*[1][self::a]");
                                        if (nodeNext != null)
                                        {
                                            name1 = nodeFirst.InnerText;
                                            name2 = nodeNext.InnerText;
                                        }
                                        else
                                        {
                                            name1 = nodeFirst.InnerText;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        line_end:
            item.RetrivedCompanyName = retrivedCompanyName == null ? NOT_FOUND : retrivedCompanyName;
            item.Address = address == null ? NOT_FOUND : address;
            item.Name1 = name1 == null ? NOT_FOUND : name1;
            item.Name2 = name2 == null ? NOT_FOUND : name2;
        }

        const String NOT_FOUND = "<Not Found>";

        private void button1_Click(object sender, EventArgs e)
        {
            int start = Convert.ToInt32(textBox1.Text);
            String filename = "Root---" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".txt";
            String data = File.ReadAllText("Root.txt", Encoding.UTF8);
            String[] dataArray = data.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<Item> list = new List<Item>();
            foreach (String d in dataArray)
            {
                String[] darray = d.Split(new char[] { '\t' });
                Item item = new Item();
                item.CompanyName = darray[0].Trim();
                if (darray.Length >= 2) item.RetrivedCompanyName = darray[1].Trim();
                if (darray.Length >= 3) item.Address = darray[2].Trim();
                if (darray.Length >= 4) item.Name1 = darray[3].Trim();
                if (darray.Length >= 5) item.Name2 = darray[4].Trim();
                list.Add(item);
            }

            int count = list.Count;
            //String resultAll = "";

            for (int i = 0; i < count; i++)
            {
                Item item = list[i];
                if (i >= start && !String.IsNullOrWhiteSpace(item.RetrivedCompanyName) && item.RetrivedCompanyName != NOT_FOUND && (String.IsNullOrWhiteSpace(item.Name1) || item.Name1 == NOT_FOUND))
                {
                    String companyName = item.CompanyName;
                    //if (companyName != "DOJATEC Sondermaschinen GmbH") continue;

                    String href = null;
                    String page1 = null;
                    {
                        String url = "https://www.companyhouse.de/Suche/" + HttpUtility.UrlEncode(
                            companyName
                                .Replace(" & ", " ").Replace(" / ", " ").Replace(" + ", " ").Replace(" * ", " ")
                                .Replace("/", "").Replace("(", "").Replace(")", "").Replace(":", "").Replace(";", "")
                        );
                        page1 = requestGet(url);
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(page1);
                        var aArray = doc.DocumentNode.CssSelect("a.resultRow");
                        String pattern = companyName.ToLower()
                                .Replace(" & ", " ").Replace(" / ", " ").Replace(" + ", " ").Replace(" * ", " ")
                                .Replace("/", " ")
                                .Replace(".", "").Replace(" ", "-").Replace("ä", "ae").Replace("ü", "u").Replace("ö", "o");
                        foreach (HtmlNode a in aArray)
                        {
                            String h = a.GetAttributeValue("href");
                            if (h.ToLower().StartsWith(pattern))
                            {
                                href = h;
                                break;
                            }
                            else if (href == null)
                            {
                                href = h;
                            }
                        }
                    }
                    if (href == null)
                    {
                        processPage(page1, item);
                    }
                    else
                    {
                        String url = "https://www.companyhouse.de" + href;
                        String page = requestGet(url);
                        processPage(page, item);
                    }

                    //{
                    //    String rr = "Es wurde kein Unternehmen und keine Person mit dem Suchbegriff";
                    //    if (response.Contains(rr))
                    //    {
                    //        if (String.IsNullOrWhiteSpace(item.Address))
                    //            item.Address = "<Not Found>";
                    //        goto goto_final;
                    //    }
                    //}
                    //{
                    //    String box = "<div class=\"subsiteContent delayed-render\">";
                    //    int r1 = response.IndexOf(box);
                    //    if (r1 >= 0)
                    //    {
                    //        r1 += box.Length;
                    //        int r2 = response.IndexOf("</div>", r1 - 1);
                    //        String rr = response.Substring(r1, r2 - r1);
                    //        if (String.IsNullOrWhiteSpace(rr))
                    //        {
                    //            if (String.IsNullOrWhiteSpace(item.Address))
                    //                item.Address = "<Not Found>";
                    //            goto goto_final;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("------------------ box not found -----------------------");
                    //    }
                    //}
                    String result = $"{i + 1}/{count}\t" + item.CompanyName + "\t\t" + item.RetrivedCompanyName + "\t\t" + item.Address + "\t\t" + item.Name1 + "\t\t" + item.Name2;
                    Console.WriteLine(result);
                }
                using (StreamWriter file = new StreamWriter(filename, true, Encoding.UTF8))
                {
                    String line = item.CompanyName + "\t" + item.RetrivedCompanyName + "\t" + item.Address + "\t" + item.Name1 + "\t" + item.Name2;
                    file.WriteLine(line);
                }
                if (i < start) Console.WriteLine($"{i + 1}/{count}\t");
            }
            Console.WriteLine("--- END ---");
        }

        class Item
        {
            public String CompanyName { get; set; }
            public String RetrivedCompanyName { get; set; }
            public String Address { get; set; }
            public String Name1 { get; set; }
            public String Name2 { get; set; }
        }
    }
}
