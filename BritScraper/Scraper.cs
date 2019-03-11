using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;
using System.Net;

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

        public static void GetJammerbugt()
        {
            Uri page = new Uri("https://signatur.frontlab.com/ExtJobs/DefaultHosting/JobList.aspx?ClientId=1475");
            LoadPage(page);

            var nodes = _doc.DocumentNode.SelectNodes("//*[@class='job-list-classic']");

            foreach (var node in nodes)
            {
                var catNodes = node.SelectNodes(".//tr");
                catNodes.RemoveAt(0);

                var category = node.SelectSingleNode(".//h2").InnerText;

                foreach (var catNode in catNodes)
                {
                    var titleNode = catNode.SelectSingleNode(".//a/text()");

                    string title;
                    if (titleNode != null)
                        title = titleNode.InnerText;
                    else
                        continue;


                    string date =
                        catNode.SelectSingleNode(
                                ".//td[2]")
                            .InnerText;

                    string relativeLink = catNode.SelectSingleNode(".//a").GetAttributeValue("href", "");
                    var finalLink = new Uri("http://" + page.Host).Append(relativeLink);

                    Job job = new Job
                    {
                        Employer = "Jammerbugt Kommune",
                        Category = category,
                        DueDate = DateTime.Parse(date, new CultureInfo("da-DK")),
                        JobTitle = title,
                        Link = finalLink
                    };
                    BritScraper.Jobs.Add(job);
                }
            }
        }

        public static void GetRanders()
        {
            Uri page = new Uri("https://job.randers.dk/ledige-job-i-randers-kommune/");
            LoadPage(page);

            var nodes = _doc.DocumentNode.SelectNodes("//*[@id='contentArea']/div/div[1]/section/div[1]/*");

            string category = "";

            foreach (var node in nodes)
            {              
                switch (node.Name)
                {
                    case "h2":
                        category = WebUtility.HtmlDecode(node.InnerText);
                        break;
                    case "ul":
                        
                        var jobNodes = node.SelectNodes("./*");

                        foreach (var jobNode in jobNodes)
                        {
                            Job job = new Job {Category = category};
                            var titleNode = jobNode.SelectSingleNode(".//a");
                            job.JobTitle = WebUtility.HtmlDecode(titleNode.InnerText.Trim());
                            job.Employer = "Randers Kommune";

                            string relativeLink = titleNode.GetAttributeValue("href", "");
                            job.Link = page.Append(relativeLink);

                            LoadPage(job.Link);

                            string date =
                                _doc.DocumentNode.SelectSingleNode(
                                    "//*[@id='contentArea']/div/div[2]/aside/article[1]/div/p[3]").InnerText;

                            date = date.Replace("Ansøgningsfrist", "").Trim();

                            job.DueDate = DateTime.Parse(date, new CultureInfo("da-DK"));

                            BritScraper.Jobs.Add(job);
                        }                        
                        break;
                    default:
                       continue;
                }
            }
        }

        public static void GetVesthimmerland()
        {
            Uri page = new Uri("https://www.vesthimmerland.dk/ledige-stillinger");

            HtmlDocument doc = Web.Load(page);

            var countNode = doc.DocumentNode.SelectNodes("//*[@id='searchPage1']/div[6]/ul/li");
            int pageCount = countNode.Select(p => int.TryParse(p.InnerText.Trim(), out int result) ? result : 0).Max();
            
            for (int i = 1; i <= pageCount; i++)
            {
                doc = Web.Load(page.Append($"?page={i}"));
                var nodes = doc.DocumentNode.SelectNodes("//*[@class='job-list search-row']/*");

                foreach (var node in nodes)
                {
                    var aNode = node.SelectSingleNode(".//a");
                    Job job = new Job
                    {
                        Employer = "Vesthimmerland Kommune",
                        JobTitle = WebUtility.HtmlDecode(aNode.GetAttributeValue("title", "")),
                        Link =  new Uri(aNode.GetAttributeValue("href", "")),
                        Category = "N/A",
                    };

                    if (DateTime.TryParse(node.SelectSingleNode(".//*[@class='applyDate']").InnerText
                        .Replace("Ansøgningsfrist", "")
                        .Trim(), new CultureInfo("da-DK"), DateTimeStyles.None, out DateTime result))
                    {
                        job.DueDate = result;
                    }
                        
                    BritScraper.Jobs.Add(job);
                }
            }
        }

        public static void GetMariagerfjord()
        {
            Uri page = new Uri("https://mariagerfjord.emply.net/overview/Mariagerfjord.aspx?mediaId=537a7324-00fa-42c1-822d-d0256092ddb9");

            HtmlDocument doc = Web.Load(page);

            var nodes = doc.DocumentNode.SelectNodes("//*[@id='form1']/table/*");

            foreach (var node in nodes)
            {
                var aNode = node.SelectSingleNode(".//a");
                if (aNode is null)
                    continue;

                Job job = new Job
                {
                    Employer = "Mariagerfjord Kommune",
                    JobTitle = WebUtility.HtmlDecode(aNode.InnerText),
                    Category = "N/A",
                    Link = new Uri(aNode.GetAttributeValue("href", "")),
                };

                if (DateTime.TryParse(node.SelectSingleNode("./td[2]").InnerText, new CultureInfo("da-DK"), DateTimeStyles.None, out DateTime result))
                {
                    job.DueDate = result;
                }

                BritScraper.Jobs.Add(job);
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