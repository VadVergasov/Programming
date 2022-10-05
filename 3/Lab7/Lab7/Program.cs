using Lab7;

Integral integrals = new();
integrals.handler += integrals.ShowProgress;

//while (integrals.Select(x => x.Thread.IsAlive).Aggregate((a, b) => a || b)) {
//    Thread.Sleep(50);
//    (int x, int y) = Console.GetCursorPosition();
//    Console.SetCursorPosition(0, 20);
//    foreach(var current in integrals) {
//        current.ShowProgress();
//    }

for(int i=0; i<5; i++) {
    new Thread(integrals.Calculate).Start();
}
   
