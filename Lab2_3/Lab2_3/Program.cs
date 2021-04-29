using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab2_3 {

	class SortVersion2 {
		List<int> array = new List<int>();
		List<int> result = new List<int>();
		int p, n;
		Random rnd = new Random();
		Stopwatch sw = new Stopwatch();
		public List<int> Result => result == new List<int>() ? null : result;
		public long Time => sw.ElapsedMilliseconds;
		struct Border {
			int min;
			int max;
			public Border(int min, int max) {
				this.min = min;
				this.max = max;
			}
			public bool IsConsains(int p) {
				if(p >= min && p < max) {
					return true;
				}
				return false;
			}
		}

		public SortVersion2(int n, int p) {
			this.n = n;
			this.p = p;
		}
		public void Run() {
			sw.Start();
			array = GetArray1(n);
			int lenght = (max - min)/p;
			List<Border> borders = new List<Border>();
			for(int i = 0; i< p; i++) {
				borders.Add(new Border(min + lenght * i, (i == p-1)?max+1:min + lenght * (i + 1)));
			}
			List<SortThread> sorts = new List<SortThread>();
			for (int i = 0; i < p; i++) {
				sorts.Add(new SortThread(array.Where(p => borders[i].IsConsains(p)).ToList()));
			}
			for (int i = 0; i < sorts.Count; i++) {
				Console.WriteLine($"Thread - {i}, Count - {sorts[i].array.Count}");
			}
			sorts.ForEach(p => p.wh.WaitOne());

			var arrays = sorts.Select(p => p.array).ToList();

			foreach(var i in sorts) {
				result.AddRange(i.array);
			}
			sw.Stop();
		}
		private int max => array.Max();
		private int min => array.Min();
		private List<int> GetArray1(int n) {
			List<int> arr = new List<int>();
			for(int i = 0; i< n; i++) {
				arr.Add(rnd.Next(0, 1000));
			}
			return arr;
		}
		private List<int> GetArray2(int n) {
			List<int> arr = new List<int>();
			int count = 0;
			for(int i = 0; i < n; i++) {
				if(count < n * 0.1f) {
					if(rnd.Next(0,2) == 1) {
						arr.Add(1000);
						count++;
						continue;
					}
				}
				arr.Add(1);
			}
			return arr;
		}
	}
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
			sw.Start();
			array = GetArray1(n);
			int lenght = n / p;
			List<SortThread> sorts = new List<SortThread>();

			for (int i = 0; i < p; i++) {
				sorts.Add(new SortThread(array.Where((x,id) => 
					(id >= i*lenght && (id < (i+1)*lenght || i == p-1))).ToList()));
			}
			for(int i = 0; i < sorts.Count; i++) {
				Console.WriteLine($"Thread - {i}, Count - {sorts[i].array.Count}");
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
		private List<int> GetArray1(int n) {
			List<int> arr = new List<int>();
			for (int i = 0; i < n; i++) {
				arr.Add(rnd.Next(0, 1000));
			}
			return arr;
		}
		private List<int> GetArray2(int n) {
			List<int> arr = new List<int>();
			int count = 0;
			for (int i = 0; i < n; i++) {
				if (count < n * 0.1f) {
					if (rnd.Next(0, 2) == 1) {
						arr.Add(1000);
						count++;
						continue;
					}
				}
				arr.Add(1);
			}
			return arr;
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
			int p = 4;
			long time = 0;
			SortVersion1 s = new SortVersion1(count, p);
			s.Run();
			time = s.Time;
			Console.WriteLine($"ThreadCount1: {p}\nTime: {time}");
			SortVersion2 s2 = new SortVersion2(count, p);
			s2.Run();
			time = s2.Time;
			Console.WriteLine($"ThreadCount2: {p}\nTime: {time}");
			Console.ReadKey();
		}
	}
}
