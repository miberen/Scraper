using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace BritScraper
{
    public partial class BritScraper : Form
    {
        public static List<Job> Jobs = new List<Job>();

        public BritScraper()
        {
            InitializeComponent();
            olv_jobs.FilterMenuBuildStrategy = new MyFilterMenuBuilder();

            GetJobs();      
        }
  
        private void PopulateView()
        {
            olv_jobs.AddObjects(Jobs);
            olv_jobs.AutoResizeColumns();
        }

        public async void GetJobs()
        {
            tssl_bar_status.Text = @"Loader...";

            List<Task> tasks = new List<Task>
            {
                new Task(Scraper.GetRebild),
                new Task(Scraper.GetAalborg),
                new Task(Scraper.GetFrederikshavn)
            };
            foreach (Task task in tasks)
            {
                task.Start();
            }
            await Task.WhenAll(tasks.ToArray());

            PopulateView();

            tssl_bar_status.Text = @"Færdig!";
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            olv_jobs.ClearObjects();
            GetJobs();
        }

        private void btn_goToJob_Click(object sender, EventArgs e)
        {
            var linkOriginalString = (olv_jobs.SelectedObject as Job)?.Link.OriginalString;
            if (linkOriginalString != null)
                System.Diagnostics.Process.Start(linkOriginalString);
        }

        private void txb_filter_TextChanged(object sender, EventArgs e)
        {
            var searchWords = (sender as TextBox).Text.Split(';').ToList();
            if (searchWords.Count == 0)
                olv_jobs.ModelFilter = null;
            else
                olv_jobs.ModelFilter = TextMatchFilter.Contains(olv_jobs, searchWords.ToArray());

        }
    }

    public class Job
    {
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString => DueDate == DateTime.MinValue ? "!Hurtigst muligt!" : DueDate.ToShortDateString();
        public string DescriptionText { get; set; }
        public Uri Link { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Employer} {Category} {JobTitle} {DueDate} {Link} \n {DescriptionText}";
        }

        public Job(string employer, string jobTitle, DateTime dueDate, string descriptionText, Uri link)
        {
            Employer = employer;
            JobTitle = jobTitle;
            DueDate = dueDate;
            DescriptionText = descriptionText;
            Link = link;
        }

        public Job()
        {
            
        }
    }

    public class MyFilterMenuBuilder : FilterMenuBuilder
    {
        public override ToolStripDropDown MakeFilterMenu(ToolStripDropDown strip, ObjectListView listView, OLVColumn column)
        {

            //Button btn = new Button {Text = @"hello"};
            //strip.Controls.Add(btn);
            return base.MakeFilterMenu(strip, listView, column);
        }
    }
}
