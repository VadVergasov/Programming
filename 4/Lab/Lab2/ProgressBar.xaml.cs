namespace Lab.Lab2;

public class Integral {
    private readonly IProgress<double> Progress;

    public double Calculate(CancellationToken token) {
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

    public Integral(IProgress<double> progress) {
        Progress = progress;
    }
}

public partial class ProgressBar : ContentPage {
    private CancellationTokenSource cts = new();
    private Task<double>? SinCalculate;
    private readonly Progress<double> Progress;

    public ProgressBar() {
        InitializeComponent();
        Progress = new(report => { ProgressBarElement.Progress = report; });
    }

    private async void Start(object? sender, EventArgs args) {
        if (SinCalculate != null && SinCalculate.Status == TaskStatus.Running) {
            return;
        }
        Integral integral = new(Progress);
        var token = cts.Token;
        SinCalculate = Task.Run(() => { return integral.Calculate(token); }, token);
        Status.Text = "Calculating";
        await SinCalculate;
        if(SinCalculate.Status == TaskStatus.RanToCompletion) {
            if (double.IsNaN(SinCalculate.Result)) {
                Status.Text = "Canceled";
            } else {
                Status.Text = SinCalculate.Result.ToString();
            }
        }
    }

    private void Cancel(object? sender, EventArgs args) {
        if (SinCalculate != null &&  SinCalculate.Status == TaskStatus.Running) {
            cts.Cancel();
            cts.Dispose();
            cts = new();
        }
    }
}