namespace Lab1_2;

public class Integral {
    private readonly EventHandler<double> Handler;
    private readonly Microsoft.Maui.Controls.ProgressBar ProgressBarElement;

    public double Calculate(CancellationToken token) {
        double result = 0, step = 0.00000001;
        double percent = -1;
        for (double current = 0; current < 1; current += step) {
            if (token.IsCancellationRequested) {
                return double.NaN;
            }
            if (percent != Math.Round(current, 2)) {
                MainThread.BeginInvokeOnMainThread(() => { ProgressBarElement.Progress = percent; });
                percent = Math.Round(current, 2);
            }
            result += step * Math.Sin(current + step / 2.0);
        }
        MainThread.BeginInvokeOnMainThread(() => { ProgressBarElement.Progress = 1; });
        return result;
    }

    public Integral(Microsoft.Maui.Controls.ProgressBar element, EventHandler<double> handler) {
        Handler = handler;
        ProgressBarElement = element;
    }

    public void ThreadProc(object obj) {
        Handler?.Invoke(this, Calculate((CancellationToken)obj));
    }
}

public partial class ProgressBar : ContentPage {
    private CancellationTokenSource cts = new();

    public ProgressBar() {
        InitializeComponent();
    }

    private void Update(object? sender, double result) {
        MainThread.BeginInvokeOnMainThread(() => {
            if (double.IsNaN(result)) {
                Status.Text = "Canceled";
            } else {
                Status.Text = result.ToString();
            }
            CancelButton.IsEnabled = false;
            StartButton.IsEnabled = true;
        });
    }

    private void Start(object? sender, EventArgs args) {
        Integral integral = new(ProgressBarElement, Update);
        ThreadPool.QueueUserWorkItem(new WaitCallback(integral.ThreadProc!), cts.Token);
        Status.Text = "Calculating";
        CancelButton.IsEnabled = true;
        StartButton.IsEnabled = false;
    }

    private void Cancel(object? sender, EventArgs args) {
        cts.Cancel();
        cts = new();
        CancelButton.IsEnabled = false;
        StartButton.IsEnabled = true;
    }
}