namespace Task_1 {
    public class Complex {
        private double real, imaginary;

        public double Real { set; get; }
        public double Imaginary { set; get; }

        public Complex() {
            Real = 0;
            Imaginary = 0;
        }

        public Complex(double real) {
            Real = real;
            Imaginary = 0;
        }

        public Complex(double real, double imaginary) {
            Real = real;
            Imaginary = imaginary;
        }

        public static Complex operator +(Complex a, Complex b) {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static Complex operator -(Complex a, Complex b) {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static Complex operator *(Complex a, Complex b) {
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        public static Complex operator /(Complex a, Complex b) {
            return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) / (b.Real * b.Real + b.Imaginary * b.Imaginary),
                (a.Imaginary * b.Real - a.Real * b.Imaginary) / (b.Real * b.Real + b.Imaginary * b.Imaginary));
        }

        public static bool operator ==(Complex a, Complex b) {
            return a.Real == b.Real && a.Imaginary == b.Imaginary;
        }

        public static bool operator !=(Complex a, Complex b) {
            return a.Real != b.Real || a.Imaginary != b.Imaginary;
        }

        public static bool operator <(Complex a, Complex b) {
            return a.Real < b.Real && a.Imaginary < b.Imaginary;
        }

        public static bool operator >(Complex a, Complex b) {
            return a.Real > b.Real && a.Imaginary > b.Imaginary;
        }

        public static bool operator <=(Complex a, Complex b) {
            return a.Real <= b.Real && a.Imaginary <= b.Imaginary;
        }

        public static bool operator >=(Complex a, Complex b) {
            return a.Real >= b.Real && a.Imaginary >= b.Imaginary;
        }

        public static implicit operator Complex(double a) {
            return new Complex(a, 0);
        }

        public static explicit operator double(Complex a) {
            return Math.Sqrt(a.Real * a.Real + a.Imaginary * a.Imaginary);
        }

        public override string ToString() {
            return $"{Real} + i * {Imaginary}";
        }

        public static bool operator true(Complex a) {
            return a.Real != 0 || a.Imaginary != 0;
        }

        public static bool operator false(Complex a) {
            return a.Real == 0 && a.Imaginary == 0;
        }

        public static Complex operator ++(Complex a) {
            return new Complex(a.Real + 1, a.Imaginary + 1);
        }

        public static Complex operator --(Complex a) {
            return new Complex(a.Real - 1, a.Imaginary - 1);
        }

        public override bool Equals(object? obj) {
            return this == (Complex)obj!;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
