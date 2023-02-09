namespace Lab.Lab2;

public class Integral {
    private readonly EventHandler<double> Handler;
    private readonly IProgress<double> Progress;

    public async Task<double> Calculate(CancellationToken token) {
        double result = 0, step = 0.00000001;
        double percent = -1;
        for (double current = 0; current < 1; current += step) {
            if (token.IsCancellationRequested) {
                return double.NaN;
            }
            if (percent != Math.Round(current, 2)) {
                Progress.Report(percent);
                percent = Math.Round(current, 2);
            }
            result += step * Math.Sin(current + step / 2.0);
        }
        Progress.Report(percent);
        return result;
    }

    public Integral(IProgress<double> progress, EventHandler<double> handler) {
        Handler = handler;
        Progress = progress;
    }

    public async Task ThreadProc(CancellationToken token) {
        Handler?.Invoke(this, await Calculate(token));
    }
}

public partial class ProgressBar : ContentPage {
    private CancellationTokenSource cts = new();
    private Task SinCalculate = new(() => { });
    private readonly Progress<double> Progress;

    public ProgressBar() {
        InitializeComponent();
        Progress = new(report => { ProgressBarElement.Progress = report; });
    }

    private void Update(object? sender, double result) {
        MainThread.BeginInvokeOnMainThread(() => {
            if (double.IsNaN(result)) {
                Status.Text = "Canceled";
            } else {
                Status.Text = result.ToString();
            }
        });
    }

    private void Start(object? sender, EventArgs args) {
        if (SinCalculate.Status == TaskStatus.WaitingForActivation) {
            return;
        }
        Integral integral = new(Progress, Update);
        var token = cts.Token;
        SinCalculate = Task.Run(() => integral.ThreadProc(token), token);
        Status.Text = "Calculating";
    }

    private void Cancel(object? sender, EventArgs args) {
        if (SinCalculate.Status != TaskStatus.WaitingForActivation) {
            return;
        }
        cts.Cancel();
        cts.Dispose();
        cts = new();
    }
}