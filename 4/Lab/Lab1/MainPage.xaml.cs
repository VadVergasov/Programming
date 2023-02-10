namespace Lab.Lab1;

public class NaNException : Exception {
    public NaNException() : base() { }

    public NaNException(string message) : base(message) { }

    public NaNException(string message, Exception innerException) : base(message, innerException) { }
}

public partial class MainPage : ContentPage {

    private double Memory { get; set; } = double.NaN;
    private double First { get; set; } = double.NaN;
    private double Second { get; set; } = double.NaN;
    private bool IsFractional { get; set; }
    private bool Calculated { get; set; }
    private bool OperationChanged { get; set; }

    private delegate void Operation();

    Operation? CurrentOperation { get; set; }

    private void ExceptionHandler(Exception e) {
        switch (e) {
            case NaNException:
                Display.Text = "Результат не определен";
                break;
            case OverflowException:
                Display.Text = "Переполнение";
                break;
            case DivideByZeroException:
                Display.Text = "Деление на 0 невозможно";
                break;
        }
        foreach (var element in Content.GetVisualTreeDescendants()) {
            if (element is Button) {
                var button = (Button)element!;
                if (button.Text != "C") {
                    button.IsEnabled = false;
                }
            }
        }
        CurrentOperation = (Operation?)Delegate.RemoveAll(CurrentOperation, CurrentOperation);
    }

    public MainPage() {
        InitializeComponent();
    }

    private void Sum() {
        if (Before.Text != First.ToString() + " + " && Before.Text != First.ToString() + " + " + Second.ToString() && OperationChanged) {
            if (double.IsNaN(First)) {
                First = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " + ";
            Display.Text = "0";
            OperationChanged = false;
        } else {
            if (double.IsNaN(Second)) {
                Second = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " + " + Second.ToString();
            if (double.IsInfinity(First + Second)) {
                ExceptionHandler(new OverflowException());
            } else {
                First += Second;
                Display.Text = First.ToString();
            }
            Calculated = true;
        }
    }

    private void Subs() {
        if (Before.Text != First.ToString() + " - " && Before.Text != First.ToString() + " - " + Second.ToString() && OperationChanged) {
            if (double.IsNaN(First)) {
                First = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " - ";
            Display.Text = "0";
            OperationChanged = false;
        } else {
            if (double.IsNaN(Second)) {
                Second = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " - " + Second.ToString();
            if (double.IsInfinity(First - Second)) {
                ExceptionHandler(new OverflowException());
            } else {
                First -= Second;
                Display.Text = First.ToString();
            }
            Calculated = true;
        }
    }

    private void Multiply() {
        if (Before.Text != First.ToString() + " × " && Before.Text != First.ToString() + " × " + Second.ToString() && OperationChanged) {
            if (double.IsNaN(First)) {
                First = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " × ";
            Display.Text = "0";
            OperationChanged = false;
        } else {
            if (double.IsNaN(Second)) {
                Second = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " × " + Second.ToString();
            if (double.IsInfinity(First * Second)) {
                ExceptionHandler(new OverflowException());
            } else {
                First *= Second;
                Display.Text = First.ToString();
            }
            Calculated = true;
        }
    }

    private void Divide() {
        if (Before.Text != First.ToString() + " ÷ " && Before.Text != First.ToString() + " ÷ " + Second.ToString() && OperationChanged) {
            if (double.IsNaN(First)) {
                First = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " ÷ ";
            Display.Text = "0";
            OperationChanged = false;
        } else {
            if (double.IsNaN(Second)) {
                Second = Convert.ToDouble(Display.Text);
            }
            Before.Text = First.ToString() + " ÷ " + Second.ToString();
            if (double.IsNaN(First / Second)) {
                ExceptionHandler(new NaNException());
            } else if (Second == 0) {
                ExceptionHandler(new DivideByZeroException());
            } else if (double.IsInfinity(First / Second)) {
                ExceptionHandler(new OverflowException());
            } else {
                First /= Second;
                Display.Text = First.ToString();
            }
            Calculated = true;
        }
    }

    private void PowerOf10() {
        if (double.IsNaN(First)) {
            First = Convert.ToDouble(Display.Text);
        }
        Before.Text = "10 ^ " + Display.Text;
        if (double.IsInfinity(Math.Pow(10, First))) {
            ExceptionHandler(new OverflowException());
        } else {
            First = Math.Pow(10, First);
            Display.Text = First.ToString();
        }
        Calculated = true;
    }

    private void Reverse() {
        if (double.IsNaN(First)) {
            First = Convert.ToDouble(Display.Text);
        }
        if (First == 0) {
            ExceptionHandler(new DivideByZeroException());
        } else {
            Before.Text = "1 / " + Display.Text;
            First = 1 / First;
            Display.Text = First.ToString();
            Calculated = true;
        }
    }

    private void Square() {
        if (double.IsNaN(First)) {
            First = Convert.ToDouble(Display.Text);
        }
        Before.Text = Display.Text + " × " + Display.Text;
        if (double.IsInfinity(Math.Pow(First, 2))) {
            ExceptionHandler(new OverflowException());
        } else {
            First = Math.Pow(First, 2);
            Display.Text = First.ToString();
        }
        Calculated = true;
    }

    private void OnBackspaceClicked(object sender, EventArgs e) {
        if (Display.Text == "0") {
            return;
        }
        Display.Text = Display.Text.Remove(Display.Text.Length - 1);
        if (Display.Text.Length == 0) {
            Display.Text = "0";
        }
    }

    private void OnNumberClicked(object sender, EventArgs e) {
        if (Display.Text.Length == 16 + (IsFractional ? 1 : 0)) {
            return;
        }
        if (Display.Text == "0") {
            Display.Text = "";
        }
        Display.Text += (sender as Button)!.Text;
    }

    private void OnDotClicked(object sender, EventArgs e) {
        if (!IsFractional) {
            IsFractional = true;
        } else {
            return;
        }
        Display.Text += ',';
    }

    private void OnMemorySaveClicked(object sender, EventArgs e) {
        if (double.IsNaN(First)) {
            return;
        }
        Memory = First;
    }

    private void OnMemoryClearClicked(object sender, EventArgs e) {
        Memory = 0;
    }

    private void OnMemoryReuseClicked(object sender, EventArgs e) {
        if (double.IsNaN(Memory)) {
            return;
        }
        First = Memory;
        Display.Text = First.ToString();
    }

    private void OnMemoryPlusClicked(object sender, EventArgs e) {
        if (double.IsNaN(First)) {
            return;
        }
        Memory += First;
    }

    private void OnMemoryMinusClicked(object sender, EventArgs e) {
        if (double.IsNaN(First)) {
            return;
        }
        Memory -= First;
    }

    private void OnCEClicked(object sender, EventArgs e) {
        Display.Text = "0";
    }

    private void OnCClicked(object sender, EventArgs e) {
        foreach (var element in Content.GetVisualTreeDescendants()) {
            if (element is Button) {
                (element as Button)!.IsEnabled = true;
            }
        }
        CurrentOperation = (Operation?)Delegate.RemoveAll(CurrentOperation, CurrentOperation);
        First = double.NaN;
        Second = double.NaN;
        Calculated = false;
        Display.Text = "0";
        Before.Text = "";
    }

    private void OnChangeSignClicked(object sender, EventArgs e) {
        if (double.IsNaN(First)) {
            return;
        }
        if (Math.Sign(First) == -1) {
            if (Display.Text[0] == '-') {
                Display.Text = Display.Text!.Remove(0, 1);
            }
        } else if (Math.Sign(First) == 1) {
            Display.Text = Display.Text!.Insert(0, "-");
        }
        if (double.IsNaN(Second)) {
            First *= -1;
        } else {
            Second *= -1;
        }
    }

    private void OnPowerOf10Clicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
        }
        Second = double.NaN;
        CurrentOperation = PowerOf10;
        CurrentOperation!.Invoke();
    }

    private void OnSquareClicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
        }
        Second = double.NaN;
        CurrentOperation = Square;
        CurrentOperation!.Invoke();
    }

    private void OnReverseClicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
        }
        Second = double.NaN;
        CurrentOperation = Reverse;
        CurrentOperation!.Invoke();
    }

    private void OnPlusClicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
            Calculated = false;
        }
        Second = double.NaN;
        CurrentOperation = Sum;
        CurrentOperation!.Invoke();
    }

    private void OnMinusClicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
            Calculated = false;
        }
        Second = double.NaN;
        CurrentOperation = Subs;
        CurrentOperation!.Invoke();
    }

    private void OnMultiplyClicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
            Calculated = false;
        }
        Second = double.NaN;
        CurrentOperation = Multiply;
        CurrentOperation!.Invoke();
    }

    private void OnDivisionClicked(object sender, EventArgs e) {
        OperationChanged = true;
        if (!Calculated) {
            CurrentOperation?.Invoke();
            Calculated = false;
        }
        Second = double.NaN;
        CurrentOperation = Divide;
        CurrentOperation!.Invoke();
    }

    private void OnResultClicked(object sender, EventArgs e) {
        CurrentOperation?.Invoke();
    }

    private void OnPercentClicked(object sender, EventArgs e) {
        if (double.IsNaN(First)) {
            Display.Text = "0";
        } else {
            if (CurrentOperation == Subs || CurrentOperation == Sum) {
                Second = Convert.ToDouble(Display.Text) / 100 * First;
            } else {
                Second = Convert.ToDouble(Display.Text) / 100;
            }
            Display.Text = Second.ToString();
        }
    }
}


