using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_4 {
	abstract class AbstractClass {
		public AbstractClass() {
			Console.WriteLine("1 AbstarctClass()");

			this.AbstractMethod();

			Console.WriteLine("2 AbstractClass()");
		}
		public abstract void AbstractMethod();
	}
	class ConcreteClass : AbstractClass {
		string s = "FIRST";

		public ConcreteClass() {
			Console.WriteLine("3 ConcreteClass()");
			s = "SECOND";
		}
		public override void AbstractMethod() {
			Console.WriteLine("Реализация метода AbstarctMethod() в ConcreteCLass {0}", s);
		}
	}
	class Program {
		static void Main() {
			AbstractClass instance = new ConcreteClass();

			Console.WriteLine(new string('-', 55));

			instance.AbstractMethod();

			Console.ReadKey();
		}
	}

}