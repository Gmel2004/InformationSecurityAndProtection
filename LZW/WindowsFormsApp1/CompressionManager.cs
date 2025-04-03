using System;
using System.Collections.Generic;
using System.Timers;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public class CompressionProgressManager
    {
        private Timer timer;
        private List<IData> activeTasks = new List<IData>();

        public event Action<double> ProgressUpdated;

        public CompressionProgressManager() { }

        public void Start()
        {
            timer = new Timer(100);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        public void AddTask(IData inputData)
        {
            lock (activeTasks)
            {
                activeTasks.Add(inputData);
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (activeTasks)
            {
                double totalProgress = 0;
                foreach (var task in activeTasks)
                {
                    totalProgress += 100.0 * task.CurrentIndex / task.Length;
                }

                double averageProgress = activeTasks.Count > 0 ? totalProgress / activeTasks.Count : 0;
                ProgressUpdated?.Invoke(averageProgress);

                if (averageProgress >= 100.0)
                {
                    Stop();
                }
            }
        }

        public void Stop()
        {
            timer.Stop();
            activeTasks.Clear();
        }
    }
}