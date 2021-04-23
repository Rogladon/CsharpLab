using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_5 {
	interface Interface1 {
		void Method1();
	}
	interface Interface2 {
		void Method2();
	}
	class BaseClass {
		public void Method() {
			Console.WriteLine("Метод клсса - BaseClass");
		}
	}

	namespace Interfaces {
		class DerivedClass : BaseClass, Interface1, Interface2 {
			public void Method1() {
				Console.WriteLine("Реализация метода Method1() из Interface1");
			}
			public void Method2() {
				Console.WriteLine("Реализация метода Method2() из Interface2");
			}
		}
	}
	class Program {
		static void Main() {
			Interfaces.DerivedClass instance = new Interfaces.DerivedClass();
			instance.Method();
			instance.Method1();
			instance.Method2();

			Console.WriteLine(new string('-', 40));

			BaseClass instance0 = instance as BaseClass;
			instance0.Method();

			Interface1 instance1 = instance as Interface1;
			instance1.Method1();

			Interface2 instance2 = instance as Interface2;
			instance2.Method2();

			Console.ReadKey();
		}
	}
}
