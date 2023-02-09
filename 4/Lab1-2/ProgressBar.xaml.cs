namespace Lab1_2;

public class Integral {
    private readonly EventHandler<double> Handler;
    private readonly Microsoft.Maui.Controls.ProgressBar ProgressBarElement;

    public async Task<double> Calculate(CancellationToken token) {
        double result = 0, step = 0.00000001;
        double percent = -1;
        for (double current = 0; current < 1; current += step) {
            if (token.IsCancellationRequested) {
                return double.NaN;
            }
            if (percent != Math.Round(current, 2)) {
                await MainThread.InvokeOnMainThreadAsync(() => { ProgressBarElement.Progress = percent; });
                percent = Math.Round(current, 2);
            }
            result += step * Math.Sin(current + step / 2.0);
        }
        await MainThread.InvokeOnMainThreadAsync(() => { ProgressBarElement.Progress = 1; });
        return result;
    }

    public Integral(Microsoft.Maui.Controls.ProgressBar element, EventHandler<double> handler) {
        Handler = handler;
        ProgressBarElement = element;
    }

    public async Task ThreadProc(CancellationToken token) {
        Handler?.Invoke(this, await Calculate(token));
    }
}

public partial class ProgressBar : ContentPage {
    private CancellationTokenSource cts = new();
    private Task SinCalculate = new(() => { });

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
        });
    }

    private void Start(object? sender, EventArgs args) {
        if (SinCalculate.Status == TaskStatus.WaitingForActivation) {
            return;
        }
        Integral integral = new(ProgressBarElement, Update);
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