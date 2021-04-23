using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_2 {
	class Program {
		static double a = 1, b = 5, c = 6;
		static double _b, b2, ac4, d, dsqrt, a2, x1, x2;

		static EventWaitHandle _bWh_1 = new AutoResetEvent(false);
		static EventWaitHandle b2Wh = new AutoResetEvent(false);
		static EventWaitHandle ac4Wh = new AutoResetEvent(false);
		static EventWaitHandle dWh = new AutoResetEvent(false);
		static EventWaitHandle dsqrtWh_1 = new AutoResetEvent(false);
		static EventWaitHandle a2Wh_1 = new AutoResetEvent(false);
		static EventWaitHandle _bWh_2 = new AutoResetEvent(false);
		static EventWaitHandle a2Wh_2 = new AutoResetEvent(false);
		static EventWaitHandle dsqrtWh_2 = new AutoResetEvent(false);
		static EventWaitHandle x1Wh = new AutoResetEvent(false);
		static EventWaitHandle x2Wh = new AutoResetEvent(false);

		static void _B() {
			_b = -b;
			_bWh_1.Set();
			_bWh_2.Set();
			Console.WriteLine($"_b: {_b} done");
		}
		static void B2() {
			b2 = b*b;
			b2Wh.Set();
			Console.WriteLine($"b2: {b2} done");
		}
		static void AC4() {
			ac4 = a * c * 4;
			ac4Wh.Set();
			Console.WriteLine($"ac4: {ac4} done");
		}
		static void D() {
			b2Wh.WaitOne();
			ac4Wh.WaitOne();
			d = b2-ac4;
			dWh.Set();
			Console.WriteLine($"d: {d} done");
		}
		static void Dsqrt() {
			dWh.WaitOne();
			dsqrt = Math.Sqrt(d);
			dsqrtWh_1.Set();
			dsqrtWh_2.Set();
			Console.WriteLine($"dsqrt: {dsqrt} done");
		}
		static void A2() {
			a2 = 2 * a;
			a2Wh_1.Set();
			a2Wh_2.Set();
			Console.WriteLine($"a: {a} done");
		}
		static void X1() {
			a2Wh_1.WaitOne();
			_bWh_1.WaitOne();
			dsqrtWh_1.WaitOne();
			x1 = (_b+dsqrt)/a2;
			x1Wh.Set();
			Console.WriteLine($"x1: {x1} done");
		}
		static void X2() {
			a2Wh_2.WaitOne();
			_bWh_2.WaitOne();
			dsqrtWh_2.WaitOne();
			x2 = (_b - dsqrt) / a2;
			x2Wh.Set();
			Console.WriteLine($"x2: {x2} done");
		}
		static void Main() {
			Thread t_b = new Thread(_B);
			Thread tB2 = new Thread(B2);
			Thread tAc4 = new Thread(AC4);
			Thread tD = new Thread(D);
			Thread tDsqrt = new Thread(Dsqrt);
			Thread tA2 = new Thread(A2);
			Thread tX1 = new Thread(X1);
			Thread tX2 = new Thread(X2);

			t_b.Start();
			tB2.Start();
			tAc4.Start();
			tD.Start();
			tDsqrt.Start();
			tA2.Start();
			tX1.Start();
			tX2.Start();

			x1Wh.WaitOne();
			x2Wh.WaitOne();
			Console.WriteLine($"x1 = {x1}, x2 = {x2}");
			Console.ReadKey();
		}
	}
}
