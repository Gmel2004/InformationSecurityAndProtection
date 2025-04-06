using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public class ProgressManager
    {
        private List<IData> activeTasks = new List<IData>();
        private System.Timers.Timer timer;
        public event Action<double> ProgressUpdated;

        public void Start<T>(List<T> tasks) where T : IData
        {
            if (tasks.Count == 0)
            {
                throw new Exception("activeTasks cannot be zero");
            }
            activeTasks = tasks.Select(t => (IData)t).ToList();
            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var avg = activeTasks.Select(t => 100 * t.CurrentIndex / t.Length).Average();
            ProgressUpdated?.Invoke(avg);
        }

        public void Stop()
        {
            OnTimerElapsed(null, null);
            timer.Stop();
            activeTasks.Clear();
        }
    }
}