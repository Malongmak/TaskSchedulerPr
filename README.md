# **TaskSchedulerPr**

## **📌 Overview**

TaskSchedulerPro is a modern WPF application designed to demonstrate how to create, schedule, execute, monitor, and cancel asynchronous tasks in real time. It provides a clean interface for managing multiple timed operations, making it ideal for classroom demonstrations, student assignments, and learning materials about C# async/await and WPF data binding.

This application showcases:

* Asynchronous task execution
* Real-time UI updates via `INotifyPropertyChanged`
* Cancellation using `CancellationTokenSource`
* Modern XAML UI design
* ObservableCollection-based dynamic task listing


## **🎯 Project Description (Problem Being Solved)**

Managing multiple timed tasks in educational, training, or demonstration environments can be inefficient when done manually. Tools like whiteboard timers, phone alarms, or verbal reminders often lead to inconsistency and lack real-time visual tracking.

TaskSchedulerPro solves this by providing:

* Automated task scheduling
* Real-time progress monitoring
* Visual feedback through progress bars and statuses
* Ability to cancel tasks instantly
* Centralized management for multiple running tasks

This makes it perfect for computer science classes, task simulation demos, and showing practical uses of concurrency in C#.


## **📁 Project Structure**

```
TaskSchedulerPro/
│
├── TaskSchedulerPro.csproj
│
├── MainWindow.xaml              ← UI layout
├── MainWindow.xaml.cs           ← UI logic + task scheduling
│
├── Models/
│   └── ClassTask.cs             ← Task model (INotifyPropertyChanged)
│
└── App.xaml
└── App.xaml.cs
```


## **✨ Features**

* Add tasks with custom names and durations
* Automatic queuing system (2-second waiting stage)
* Live progress updates in 10 steps
* Cancel running tasks at any time
* Modern dark UI design
* ObservableCollection-powered real-time list updates


## **🛠 Technologies Used**

* **C# (.NET)** – Task-based asynchronous programming
* **WPF** – UI framework
* **XAML** – UI markup
* **INotifyPropertyChanged** – Live data binding
* **Task & CancellationToken** – Concurrency management

## **🚀 Getting Started**

### **Prerequisites**

* Windows OS
* Visual Studio (2022 or later recommended)
* .NET Desktop Development workload

### **How to Run**

1. Clone or download the project.
2. Open `TaskSchedulerPro.csproj` in Visual Studio.
3. Build and run the project.
4. Enter a task name + duration, then click **Schedule Task**.


## **📌 How It Works**

1. The user enters a **task name** and **duration (seconds)**.
2. A new task is added to an `ObservableCollection<ClassTask>`.
3. The task enters a **queued phase**.
4. It proceeds through a **processing phase**, updating progress from 0% to 100%.
5. At any time, the user may cancel the task, causing an immediate state update.


## **📉 Task Lifecycle Diagram**

```
Create Task
    │
    ▼
Added to ListView
    │
    ▼
Queued (2 seconds)
    │
    ▼
Processing (10 progress steps)
    │
    ├──▶ Can be cancelled anytime
    │
    ▼
Completed / Cancelled / Error
```

## **📚 Educational Value**

This project is ideal for teaching or learning:

* Multithreading and asynchronous programming
* UI-thread safety and data binding
* WPF layout and component styling
* MVVM architecture fundamentals


## **🔧 Possible Extensions**

You can extend this project with:

* MVVM architecture
* Fluent UI 2.0 redesign
* Database persistence
* Logging system
* Sound or notification alerts
* Scheduling recurring tasks


## **📜 License**

This project is free to use for educational and academic purposes.


## **🙌 Contributions**

Feel free to request features or ask for enhancements such as:

* PDF documentation
* PowerPoint version
* UML diagrams
* Sequence diagrams
* Full MVVM rewrite

## **📝 Author**

Created as an instructional example for demonstrating WPF task scheduling concepts using modern C# best practices.
