﻿using System;
using System.Collections.Concurrent;
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
        public static ConcurrentBag<Job> Jobs = new ConcurrentBag<Job>();

        public BritScraper()
        {
            InitializeComponent();
            olv_jobs.FilterMenuBuildStrategy = new MyFilterMenuBuilder();

    
            GetJobs();      
        }
  
        public void PopulateView()
        {
            List<Job> safeJobs = new List<Job>(Jobs);

            var currentJobs = olv_jobs.Objects?.Cast<Job>();
            var newJobs = currentJobs != null ? safeJobs.Except(currentJobs).ToList() : safeJobs;
                
            olv_jobs.AddObjects(newJobs);
            olv_jobs.AutoResizeColumns();
        }

        public async void GetJobs()
        {
            var sB = new StringBuilder(@"Loader... ");

            List<TaskWithName> tasks = new List<TaskWithName>
            {
                new TaskWithName(new Task(Scraper.GetRebild), "Rebild"),
                new TaskWithName(new Task(Scraper.GetAalborg), "Aalborg"),
                new TaskWithName(new Task(Scraper.GetFrederikshavn), "Frederikshavn"),
                new TaskWithName(new Task(Scraper.GetJammerbugt), "Jammerbugt"),
                new TaskWithName(new Task(Scraper.GetRanders), "Randers"),
                new TaskWithName(new Task(Scraper.GetVesthimmerland), "Vesthimmerland"),
                new TaskWithName(new Task(Scraper.GetMariagerfjord), "Mariagerfjord"),
                new TaskWithName(new Task(Scraper.GetBrønderslev), "Brønderslev")
            };

            foreach (TaskWithName task in tasks)
            {
                sB.Append(task.Name + " : ");
                task.Start();
            }
            tssl_bar_status.Text = sB.ToString();

            while (tasks.Count > 0)
            {
                var firstFinishedTask = await Task.WhenAny(tasks.Select(taskWithStatus => taskWithStatus.Task).ToList());

                sB.Replace(tasks.Find(p => p.Task == firstFinishedTask).Name + " : ", "");
                tssl_bar_status.Text = sB.ToString();
                tssl_bar.Refresh();

                tasks.RemoveAll(p => p.Task == firstFinishedTask);

                PopulateView();
            }

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
            var searchWords = (sender as TextBox)?.Text.Split(';').ToList();
            if (searchWords != null && searchWords.Count == 0)
                olv_jobs.ModelFilter = null;
            else if (searchWords != null)
                olv_jobs.ModelFilter = TextMatchFilter.Contains(olv_jobs, searchWords.ToArray());
        }

        private void olv_jobs_ItemActivate(object sender, EventArgs e)
        {
            var linkOriginalString = (olv_jobs.SelectedObject as Job)?.Link.OriginalString;
            if (linkOriginalString != null)
                System.Diagnostics.Process.Start(linkOriginalString);
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

    public class TaskWithName
    {
        public Task Task { get; set; }
        public string Name { get; set; }

        public TaskWithName(Task task, string name)
        {
            Task = task;
            Name = name;
        }

        public void Start()
        {
            Task.Start();
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
