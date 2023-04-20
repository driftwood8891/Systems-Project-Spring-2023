using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Systems_Project_Spring_2023.Models
{
    public class LogFileHelper
    {
        private readonly IWebHostEnvironment _env;

        public LogFileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<LogItem> GetLogItems(string logFileName)
        {
            var logItems = new List<LogItem>();

            try
            {
                // build file path
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                // Read all lines from the log file
                var lines = File.ReadAllLines(logFilePath);

                // Parse each line to create a LogItem object
                foreach (var line in lines)
                {
                    var fields = line.Split('|');
                    if (fields.Length == 3)
                    {
                        var logItem = new LogItem
                        {
                            Event = fields[0],
                            Description = fields[1],
                            Date = DateTime.Parse(fields[2])
                        };
                        logItems.Add(logItem);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur while reading the log file
                Console.WriteLine($"Error reading log file: {ex.Message}");
            }

            return logItems;
        }
    }

    public class LogItem
    {
        public string? Event { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
