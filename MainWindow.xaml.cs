using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TaskSchedulerDemo.Models;
using TaskSchedulerDemo.Services;
using TaskSchedulerDemo.Converters;

namespace TaskSchedulerDemo
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ClassTask> ScheduledTasks { get; set; }
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }
        private readonly AIChatbotService chatbotService;

        public MainWindow()
        {
            InitializeComponent();
            ScheduledTasks = new ObservableCollection<ClassTask>();
            ChatMessages = new ObservableCollection<ChatMessage>();
            TaskListView.ItemsSource = ScheduledTasks;
            ChatMessagesControl.ItemsSource = ChatMessages;
            chatbotService = new AIChatbotService();
            
            // Add welcome message
            ChatMessages.Add(new ChatMessage("Hello! I'm your AI Task Assistant. I can help you schedule and manage your class tasks. What would you like to work on today?", false));
        }

        private void AddAndRun_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskNameInput.Text)
                || !int.TryParse(DurationInput.Text, out int duration))
            {
                MessageBox.Show("Enter valid name and duration.");
                return;
            }

            var newTask = new ClassTask
            {
                Name = TaskNameInput.Text,
                DurationSeconds = duration,
                Status = "Scheduled",
                Progress = 0,
                TokenSource = new CancellationTokenSource()
            };

            ScheduledTasks.Add(newTask);

            _ = ProcessTaskLifecycle(newTask);

            TaskNameInput.Clear();
            DurationInput.Clear();
        }

        private async Task ProcessTaskLifecycle(ClassTask task)
        {
            try
            {
                task.Status = "Queued (Waiting 2 seconds)...";
                await Task.Delay(2000, task.TokenSource.Token);

                task.Status = "Processing...";

                await Task.Run(async () =>
                {
                    for (int i = 0; i <= 100; i += 10)
                    {
                        if (task.TokenSource.Token.IsCancellationRequested)
                            task.TokenSource.Token.ThrowIfCancellationRequested();

                        // Update UI on the main thread
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            task.Progress = i;
                        });

                        int stepDelay = (task.DurationSeconds * 1000) / 10;
                        await Task.Delay(stepDelay);
                    }
                }, task.TokenSource.Token);

                task.Status = "Completed Successfully";
                task.Progress = 100;
            }
            catch (OperationCanceledException)
            {
                task.Status = "Cancelled";
                task.Progress = 0;
            }
            catch (Exception ex)
            {
                task.Status = "Error: " + ex.Message;
            }
        }

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ClassTask task)
                task.TokenSource.Cancel();
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            ScheduledTasks.Clear();
        }

        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendChatMessage_Click(sender, null);
            }
        }

        private void SendChatMessage_Click(object sender, RoutedEventArgs e)
        {
            var userMessage = ChatInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage))
                return;

            // Add user message
            ChatMessages.Add(new ChatMessage(userMessage, true));
            
            // Clear input
            ChatInput.Clear();

            // Generate and add AI response
            _ = GenerateAIResponse(userMessage);
        }

        private async Task GenerateAIResponse(string userMessage)
        {
            await Task.Delay(500); // Simulate thinking time
            
            var response = chatbotService.GenerateResponse(userMessage);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChatMessages.Add(new ChatMessage(response, false));
                
                // Auto-scroll to bottom
                ChatScrollViewer.ScrollToEnd();
                
                // Check if AI suggested a task that can be auto-scheduled
                CheckAndScheduleTask(response);
            });
        }

        private void CheckAndScheduleTask(string aiResponse)
        {
            // Try to parse task from AI response
            var taskData = chatbotService.ParseTaskFromMessage(aiResponse);
            if (taskData != null)
            {
                var parts = taskData.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int duration))
                {
                    var taskName = parts[0].Trim();
                    
                    // Auto-schedule the suggested task
                    var newTask = new ClassTask
                    {
                        Name = taskName,
                        DurationSeconds = duration,
                        Status = "Scheduled by AI",
                        Progress = 0,
                        TokenSource = new CancellationTokenSource()
                    };
                    
                    ScheduledTasks.Add(newTask);
                    _ = ProcessTaskLifecycle(newTask);
                    
                    // Notify user
                    ChatMessages.Add(new ChatMessage($"I've automatically scheduled '{taskName}' for {duration} seconds!", false));
                }
            }
        }
    }
}
