using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using HtmlAgilityPack;

namespace BritScraper
{
    public static class Scraper
    {
        private static readonly HtmlWeb Web = new HtmlWeb();
        private static HtmlDocument _doc = new HtmlDocument();

        public static void LoadPage(Uri pageUri)
        {          
            _doc = Web.Load(pageUri);
        }

        public static void GetRebild()
        {
            Uri page = new Uri("https://rebild.dk/borger/arbejde-og-jobsoegning/ledige-stillinger/");
            LoadPage(page);

            var nodes = _doc.DocumentNode.SelectNodes("//*[@id='content-main']/div[2]/div/div/div/div/div/*");


            string category = "";

            foreach (HtmlNode node in nodes)
            {
                
                switch (node.Name)
                {
                    case "h3":
                        category = node.InnerText;
                        break;
                    case "div":
                        Job job = new Job
                        {
                            Employer = "Rebild Kommune",
                            Category = category
                        };
                        
                        HtmlNode titleNode = node.SelectSingleNode("./div[1]");
                        job.JobTitle = titleNode.InnerText.Trim();

                        string relativePath = titleNode.SelectSingleNode(".//a").GetAttributeValue("href", "");
                        job.Link = new Uri("http://" + page.Host).Append(relativePath);

                        //Convert date
                        CultureInfo danish = new CultureInfo("da-DK");
                        string date = node.SelectSingleNode("./div[2]").InnerText;
                        date = date.Substring(date.IndexOf(":", StringComparison.Ordinal)+1).Trim();
                        job.DueDate = DateTime.Parse(date, danish);

                        //Load page for job and get description
                        //LoadPage(job.Link);
                        //HtmlNode descNode = _doc.DocumentNode.SelectSingleNode("//*[@id='content-main']/div[1]/div/div/p");
                        //job.DescriptionText = descNode.InnerText;

                        BritScraper.Jobs.Add(job);
                        break;
                }
            }
        }

        public static void GetAalborg()
        {
            Uri page = new Uri("https://www.aalborg.dk/job-og-ledighed/job/");
            LoadPage(page);

            var categories = _doc.DocumentNode.SelectNodes("//*[@id='overview']/form/div");

            foreach (HtmlNode catNode in categories)
            {
                var category = catNode.SelectSingleNode(".//*[@class='header left']").InnerText;
                var jobs = catNode.SelectNodes(".//*[@class='jobs--singlejob']");

                foreach (HtmlNode jobNode in jobs)
                {
                    Job job = new Job
                    {
                        Employer = "Aalborg Kommune",
                        Category = category
                    };

                    job.JobTitle = jobNode.SelectSingleNode(".//a").InnerText.Trim();

                    string path = jobNode.SelectSingleNode(".//a").GetAttributeValue("href", "");
                    job.Link = new Uri(path);

                    CultureInfo danish = new CultureInfo("da-DK");
                    string date = jobNode.SelectSingleNode("./div[2]/span[2]").InnerText;
                    if (DateTime.TryParse(date, danish, DateTimeStyles.None, out DateTime result))
                        job.DueDate = result;

                    //Load page for job and get description
                    //LoadPage(job.Link);
                    //HtmlNode descNode = _doc.DocumentNode.SelectSingleNode("//*[@id='main-text']");
                    //job.DescriptionText = descNode.InnerText;

                    BritScraper.Jobs.Add(job);
                }             
            }
        }

        public static void GetFrederikshavn()
        {
            Uri page = new Uri("https://frederikshavn.dk/politik/om-kommunen/kommunen-som-arbejdsplads/ledige-stillinger/");
            LoadPage(page);

            var categories = _doc.DocumentNode.SelectNodes("//*[@class='emply__group']");

            foreach (HtmlNode catNode in categories)
            {
                string category = WebUtility.HtmlDecode(catNode.SelectSingleNode("./h2").InnerText);
                var jobs = catNode.SelectNodes(".//*[@class='emply__item']");

                foreach (HtmlNode jobNode in jobs)
                {
                    Job job = new Job
                    {
                        Employer = "Frederikshavn Kommune",
                        Category = category,
                        JobTitle = WebUtility.HtmlDecode(jobNode.SelectSingleNode(".//a").InnerText.Trim())
                    };


                    string relativePath = jobNode.SelectSingleNode(".//a").GetAttributeValue("href", "");
                    job.Link = new Uri(page.OriginalString).Append(relativePath);

                    CultureInfo danish = new CultureInfo("da-DK");
                    string date = jobNode.SelectSingleNode(".//*[@class='emply__date']").InnerText;
                    date = date.Substring(date.IndexOf(":", StringComparison.Ordinal) + 1).Trim();
                    if (DateTime.TryParse(date, danish, DateTimeStyles.None, out DateTime result))
                        job.DueDate = result;

                    //Load page for job and get description
                    //LoadPage(job.Link);
                    //HtmlNode descNode = _doc.DocumentNode.SelectSingleNode("//*[@id='content']/section/div[2]/div/article/div/div[2]/div/div");
                    //job.DescriptionText = WebUtility.HtmlDecode(descNode.InnerText);   
                    
                    BritScraper.Jobs.Add(job);
                }
            }
        }
    }

    public static class UriExtensions
    {
        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
        }
    }

}