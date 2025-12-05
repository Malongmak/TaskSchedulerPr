using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TaskSchedulerDemo.Models
{
    public class ClassTask : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int DurationSeconds { get; set; }

        private string _status;
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(); }
        }

        public CancellationTokenSource TokenSource { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
