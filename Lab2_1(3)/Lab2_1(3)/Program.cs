using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_1_3_ {
	class MyThread{
		public Thread thrd;
		static Semaphore sem = new Semaphore(2, 2);

		public MyThread(string name) {
			thrd = new Thread(Run);
			thrd.Name = name;
			thrd.Start();
		}
		void Run() {
			Console.WriteLine($"{thrd.Name} waiting access");
			sem.WaitOne();
			Console.WriteLine($"{thrd.Name} getting access");
			for(char ch='A';ch < 'D'; ch++) {
				Console.WriteLine($"{thrd.Name} : {ch} ");
				Thread.Sleep(500);
			}
			Console.WriteLine($"{thrd.Name} release access");

			sem.Release();
		}
	}


	class Program {
		static void Main() {
			MyThread mt1 = new MyThread("Thread #1");
			MyThread mt2 = new MyThread("Thread #2");
			MyThread mt3 = new MyThread("Thread #3");
			mt1.thrd.Join();
			mt2.thrd.Join();
			mt3.thrd.Join();

			Console.WriteLine("All threads finished!");
			Console.ReadLine();
		}
	}
}
