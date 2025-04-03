using System;
using System.Collections.Generic;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public class CompressionProgressManager
    {
        private readonly List<IData> activeTasks = new List<IData>();
        private System.Timers.Timer timer;
        public event Action<double> ProgressUpdated;

        public void Start()
        {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        public void AddTask(IData task)
        {
            lock (activeTasks)
                activeTasks.Add(task);
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("fee");
            double total = 0;
            foreach (var task in activeTasks)
                total += 100.0 * task.CurrentIndex / task.Length;

            double avg = activeTasks.Count > 0 ? total / activeTasks.Count : 0;
            ProgressUpdated?.Invoke(avg);

            if (avg >= 100)
                Stop();
        }

        public void Stop()
        {
            timer?.Stop();
            activeTasks.Clear();
        }
    }
}