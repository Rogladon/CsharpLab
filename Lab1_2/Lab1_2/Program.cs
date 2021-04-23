using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2 {
	class BaseClass {
		public void Method() {
			Console.WriteLine("Method from BaseClass");
		}
	}

	class DerivedClass : BaseClass {
		//Замещение метода базового класса
		public void Method() {
			Console.WriteLine("Method from DerivedClass");
		}
	}

	class Programm {
		static void Main() {
			DerivedClass instance = new DerivedClass();
			instance.Method();

			//UpCast
			BaseClass instanceUp = instance;
			instanceUp.Method();

			//DownCast
			DerivedClass instanceDown = (DerivedClass)instanceUp;

			//Delay.
			Console.ReadKey();
		}
	}
}
