using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerDemo.Models;

namespace TaskSchedulerDemo.Services
{
    public class AIChatbotService
    {
        private readonly List<string> taskSuggestions = new List<string>
        {
            "Study for Mathematics exam - 30 minutes",
            "Complete Physics assignment - 45 minutes", 
            "Review Chemistry notes - 20 minutes",
            "Practice Biology lab report - 60 minutes",
            "Read History chapter - 25 minutes",
            "Write English essay - 40 minutes",
            "Solve Programming exercises - 35 minutes",
            "Prepare for presentation - 50 minutes"
        };

        private readonly Dictionary<string, string> responses = new Dictionary<string, string>
        {
            ["hello"] = "Hello! I'm your AI Task Assistant. I can help you schedule and manage your class tasks. What would you like to work on today?",
            ["hi"] = "Hi there! I'm here to help you organize your study schedule. What subjects do you need to focus on?",
            ["help"] = "I can help you:\n• Suggest study tasks for different subjects\n• Estimate time needed for assignments\n• Create balanced study schedules\n• Set reminders for deadlines\n\nJust tell me what you need help with!",
            ["schedule"] = "I can help you create a study schedule! Let me suggest some tasks:\n\n" + string.Join("\n", new List<string> {"Study for Mathematics exam - 30 minutes", "Complete Physics assignment - 45 minutes", "Review Chemistry notes - 20 minutes"}),
            ["suggest"] = "Here are some study suggestions:\n• Mathematics: Practice problems (30-45 min)\n• Physics: Review concepts (25-35 min)\n• Chemistry: Memorize formulas (20-30 min)\n• Biology: Read textbook chapter (40-50 min)\n\nWhich subject interests you?",
            ["math"] = "For Mathematics, I suggest:\n• Practice algebra problems (25 min)\n• Review calculus concepts (30 min)\n• Work on geometry proofs (35 min)\n• Complete homework assignment (40 min)\n\nWhich would you like to schedule?",
            ["physics"] = "For Physics, I recommend:\n• Review Newton's laws (20 min)\n• Practice mechanics problems (35 min)\n• Study thermodynamics (30 min)\n• Complete lab report (45 min)\n\nWhat sounds good?",
            ["chemistry"] = "For Chemistry, consider:\n• Review periodic table (15 min)\n• Practice chemical equations (25 min)\n• Study organic chemistry (35 min)\n• Complete lab work (40 min)\n\nReady to schedule one?",
            ["biology"] = "For Biology, try:\n• Read textbook chapter (30 min)\n• Review cell biology (25 min)\n• Study genetics (35 min)\n• Complete lab report (40 min)\n\nWhich topic?",
            ["deadline"] = "For deadline management, I recommend:\n• Start early with small tasks (15-20 min)\n• Break large assignments into chunks\n• Set daily progress goals\n• Review work 24 hours before due\n\nWhat's your deadline?",
            ["break"] = "Great idea! For study breaks, I suggest:\n• 5-minute stretch every 30 minutes\n• 10-minute walk every hour\n• 30-minute break after 2 hours of studying\n• Proper meal breaks\n\nHow's your study routine?",
            ["focus"] = "To improve focus:\n• Use Pomodoro technique (25 min focus, 5 min break)\n• Remove distractions (phone away)\n• Set clear goals for each session\n• Stay hydrated and take breaks\n\nNeed help setting this up?"
        };

        public string GenerateResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Please tell me what you'd like help with!";

            var input = userInput.ToLowerInvariant().Trim();

            // Check for exact matches first
            foreach (var key in responses.Keys)
            {
                if (input.Contains(key))
                    return responses[key];
            }

            // Check for task-related keywords
            if (input.Contains("task") || input.Contains("schedule") || input.Contains("plan"))
            {
                return GenerateTaskSuggestion(input);
            }

            // Check for subject keywords
            if (input.Contains("subject") || input.Contains("study"))
            {
                return "I can help you plan study sessions for:\n• Mathematics\n• Physics\n• Chemistry\n• Biology\n• History\n• English\n• Programming\n\nWhich subject would you like to focus on?";
            }

            // Check for time-related keywords
            if (input.Contains("time") || input.Contains("duration") || input.Contains("how long"))
            {
                return "Here are typical time estimates:\n• Quick review: 15-20 minutes\n• Practice problems: 25-35 minutes\n• New concepts: 30-45 minutes\n• Major assignments: 45-60 minutes\n\nWhat type of task are you planning?";
            }

            // Default response with suggestions
            return "I'm here to help with your study planning! You can ask me about:\n• Task suggestions for specific subjects\n• Time estimates for assignments\n• Study schedule recommendations\n• Deadline management\n\nWhat would you like to know?";
        }

        private string GenerateTaskSuggestion(string input)
        {
            var random = new Random();
            var suggestions = taskSuggestions.OrderBy(x => random.Next()).Take(3).ToList();
            
            return "Here are some task suggestions:\n" + 
                   string.Join("\n", suggestions.Select((s, i) => $"{i + 1}. {s}")) +
                   "\n\nWould you like me to schedule one of these?";
        }

        public string ParseTaskFromMessage(string message)
        {
            // Simple parsing to extract task name and duration
            var parts = message.Split(new[] { '-', '–', '—' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                var taskName = parts[0].Trim();
                var durationPart = parts[1].Trim();
                
                // Extract duration number
                var durationMatch = System.Text.RegularExpressions.Regex.Match(durationPart, @"(\d+)");
                if (durationMatch.Success && int.TryParse(durationMatch.Groups[1].Value, out int duration))
                {
                    return $"{taskName}|{duration}";
                }
            }
            return null;
        }
    }
}
