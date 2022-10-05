using System.Diagnostics;

namespace Lab7 {
    internal class Integral {
        private readonly Semaphore sempaphore = new(2, 2);
        public event EventHandler<int>? handler;
        public Thread Thread { get; }

        public Integral(ThreadPriority priority = ThreadPriority.Normal) {
            //Thread = new(Calculate) {
            //    Priority = priority
            //};
            //Thread.Start();
        }

        public void Calculate() {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} ожидает начала выполненния");
            sempaphore.WaitOne();
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал выполнение");
            Stopwatch time = new();
            time.Start();
            double result = 0, step = 0.00000001;
            int percent = 0;
            for (double current = 0; current < 1; current += step) {
                if (percent != (int)(current * 100)) {
                    handler?.Invoke(this, percent);
                }
                percent = (int)(current * 100);
                result += step * Math.Sin(current + step / 2.0);
            }
            handler?.Invoke(this, 100);
            time.Stop();
            TimeSpan elapsed = time.Elapsed;
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} закончил вычисление за {elapsed}, результат {result}");
            sempaphore.Release();
        }

        public void ShowProgress(object sender, int percent) {
            Console.SetCursorPosition(0, Thread.CurrentThread.ManagedThreadId);
            int number_of_signs = percent;
            string progress = "[";
            for (int i = 0; i < 100; i++) {
                if (i < number_of_signs) {
                    progress += "=";
                } else if (i == number_of_signs) {
                    progress += ">";
                } else {
                    progress += " ";
                }
            }
            progress += "] ";
            progress += percent.ToString() + "%";
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{progress}");
        }
    }
}
