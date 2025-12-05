using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskSchedulerDemo.Models
{
    public class ChatMessage : INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        private bool _isUser;
        public bool IsUser
        {
            get => _isUser;
            set { _isUser = value; OnPropertyChanged(); }
        }

        private System.DateTime _timestamp;
        public System.DateTime Timestamp
        {
            get => _timestamp;
            set { _timestamp = value; OnPropertyChanged(); }
        }

        public ChatMessage()
        {
            Timestamp = System.DateTime.Now;
        }

        public ChatMessage(string message, bool isUser) : this()
        {
            Message = message;
            IsUser = isUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
