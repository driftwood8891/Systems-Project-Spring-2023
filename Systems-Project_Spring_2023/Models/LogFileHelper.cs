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

        /** Returns List of Log Items from log.txt **/
        public List<LogItem> GetLogItems(string logFileName)
        {
	        var logItems = new List<LogItem>();

	        try
	        {
		        // build file path
		        var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
		        // Read all lines from the log file
                // do it start at the end (reverse()) so that the most recent events are loaded first
		        var lines = File.ReadAllLines(logFilePath).Reverse();

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

        /** Removes logs after 365 days **/
        public void RemoveOldLogs()
        {
            try
            {
                // build file path
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                // Read all lines from the log file
                var lines = File.ReadAllLines(logFilePath);

                // Filter out log items that are older than 120 days
                var filteredLines = lines.Where(line =>
                {
                    var fields = line.Split('|');
                    if (fields.Length == 3)
                    {
                        var date = DateTime.Parse(fields[2]);
                        var age = DateTime.Now.Subtract(date).TotalDays;
                        return age <= 365;
                    }
                    return true;
                });

                // Write the filtered lines back to the log file
                File.WriteAllLines(logFilePath, filteredLines);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur while reading or writing the log file
                Console.WriteLine($"Error removing old log items: {ex.Message}");
            }
        }

        /** Adds a New Log to the Log File **/
        public void LogEvent(string evt, string desc) {
            // Code that generates a report in the log.txt file
            var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
            var logEntry = $"{evt}|{desc}|{DateTime.Now.ToString()}{Environment.NewLine}";

            // Open the log file in append mode with write-only access
            using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

            // Create a StreamWriter to write to the file
            using var streamWriter = new StreamWriter(fileStream);

            // Write the log entry to the file
            streamWriter.WriteLine(logEntry);

            // Flush the StreamWriter to make sure the entry is written to the file
            streamWriter.Flush();
        }
    }



        
    

    public class LogItem
    {
        public string? Event { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }

}
