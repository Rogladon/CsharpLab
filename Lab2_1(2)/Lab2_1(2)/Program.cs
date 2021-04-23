using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_1_2_ {
	class SharedRes {
		public static int count = 0;
		public static Mutex mtx = new Mutex();
	}

	class IncThread {
		int num;
		public Thread thrd;
		public IncThread(string name, int n) {
			thrd = new Thread(Run);
			num = n;
			thrd.Name = name;
			thrd.Start();
		}
		void Run() {
			Console.WriteLine($"{thrd.Name} waiting mutex");
			SharedRes.mtx.WaitOne();
			Console.WriteLine($"{thrd.Name} getting mutex");
			do {
				Thread.Sleep(100);
				SharedRes.count++;
				Console.WriteLine($"In Thread {thrd.Name}, SharedRes.count = {SharedRes.count} ");
				num--;
			} while (num > 0);

			Console.WriteLine($"{thrd.Name} Release mutex");
			SharedRes.mtx.ReleaseMutex();
		}
	}
	class DecThread {
		int num;
		public Thread thrd;
		public DecThread(string name, int n) {
			thrd = new Thread(Run);
			num = n;
			thrd.Name = name;
			thrd.Start();
		}
		void Run() {
			Console.WriteLine($"{thrd.Name} waiting mutex");
			SharedRes.mtx.WaitOne();
			Console.WriteLine($"{thrd.Name} getting mutex");
			do {
				Thread.Sleep(100);
				SharedRes.count--;
				Console.WriteLine($"In Thread {thrd.Name}, SharedRes.count = {SharedRes.count} ");
				num--;
			} while (num > 0);

			Console.WriteLine($"{thrd.Name} Release mutex");
			SharedRes.mtx.ReleaseMutex();
		}
	}
	class Program {
		static void Main() {
			IncThread mt1 = new IncThread("Increment Thred", 5);
			DecThread mt2 = new DecThread("Decrement Thred", 5);
			mt1.thrd.Join();
			mt2.thrd.Join();
			Console.WriteLine("All Threds finished!");

			Console.ReadKey();
		}
	}
}
