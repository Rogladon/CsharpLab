using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_1 {
	class SumArray {
		int sum;
		object lockOn = new object();

		public int Sum(int[] nums) {
			lock (lockOn) {
				sum = 0;
				for(int i = 0; i < nums.Length; i++) {
					sum += nums[i];
					Console.WriteLine("Текущая сумма для потока" +
						Thread.CurrentThread.Name + " равна " + sum);
					Thread.Sleep(10);
				}
				return sum;
			}
		}
	}
	class MyThread {
		public Thread thrd;
		int[] a;
		int answer;
		static SumArray sa = new SumArray();

		public MyThread(string name, int[] nums) {
			a = nums;
			thrd = new Thread(Run);
			thrd.Name = name;
			thrd.Start();
		}
		void Run() {
			Console.WriteLine(thrd.Name + " started");
			answer = sa.Sum(a);
			Console.WriteLine("Сумма для потока " + thrd.Name + " равна " + answer);
			Console.WriteLine(thrd.Name + " finished");
		}
	}

	class Program {
		static void Main() {
			int[] a = { 1, 2, 3, 4, 5 };
			MyThread mt1 = new MyThread("Thread #1", a);
			MyThread mt2 = new MyThread("Thread #2", a);
		}
	}
}
