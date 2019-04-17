using System;
using System.Collections.Generic;

namespace Musiction.API.Models
{
    public class SourceFile
    {
        public int id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
    }

    public class TargetFile
    {
        public int id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
    }

    public class Job
    {
        public int id { get; set; }
        public string key { get; set; }
        public string status { get; set; }
        public bool sandbox { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? finished_at { get; set; }
        public SourceFile source_file { get; set; }
        public List<TargetFile> target_files { get; set; }
        public string target_format { get; set; }
        public int credit_cost { get; set; }
    }
}
