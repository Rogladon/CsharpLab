using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab2_3 {
	class SortVersion1 {
		List<int> array = new List<int>();
		List<int> result = new List<int>();
		int p, n;
		Random rnd = new Random();
		Stopwatch sw = new Stopwatch();
		public List<int> Result => result == new List<int>() ? null : result;
		public long Time => sw.ElapsedMilliseconds;
		public SortVersion1(int n, int p) {
			this.n = n;
			this.p = p;
		}
		public void Run() {
			//n = int.Parse(Console.ReadLine());
			//p = int.Parse(Console.ReadLine());
			sw.Start();
			for (int i = 0; i < n; i++) {
				array.Add(rnd.Next(0, 100));
			}
			int lenght = n / p;
			List<SortThread> sorts = new List<SortThread>();

			for (int i = 0; i < p; i++) {
				sorts.Add(new SortThread(array.Where((p,id) => 
					(id >= i*lenght && (id < (i+1)*lenght || i == p-1))).ToList()));
			}

			sorts.ForEach(p => p.wh.WaitOne());

			var arrays = sorts.Select(p => p.array).ToList();

			while(arrays.Count(p => p.Count != 0) != 0) {
				var minAr = arrays.Aggregate((x, y) => x[0] < y[0] ? x : y);
				result.Add(minAr[0]);
				minAr.RemoveAt(0);
				if (minAr.Count == 0)
					arrays.Remove(minAr);
			}
			sw.Stop();
		}
	}
	class SortThread {
		Thread thrd;
		public List<int> array;
		public AutoResetEvent wh = new AutoResetEvent(false);

		public SortThread(List<int> array) {
			this.array = array;
			thrd = new Thread(Run);
			thrd.Start();
		}
		void Run() {
			int temp;
			for (int i = 0; i < array.Count; i++) {
				for (int j = i + 1; j < array.Count; j++) {
					if (array[i] > array[j]) {
						temp = array[i];
						array[i] = array[j];
						array[j] = temp;
					}
				}
			}
			wh.Set();
		}
	}
	class Program {
		static int count = 10000;
		static void Main() {
			int p = 1;
			long time = 0;
			long timeOneThread;

			SortVersion1 s = new SortVersion1(count, p);
			s.Run();
			timeOneThread = s.Time;
			Console.WriteLine($"ThreadCount: {p}\nTime: {timeOneThread}");
			do {
				p *= 2;
				s = new SortVersion1(count, p);
				s.Run();
				time = s.Time;
				Console.WriteLine($"ThreadCount: {p}\nTime: {time}");
			}
			while (time < timeOneThread);

			Console.ReadKey();
		}
	}
}
