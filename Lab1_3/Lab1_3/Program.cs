using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_3 {
	class BaseClass {
		public virtual void Method() {
			Console.WriteLine("Method from BaseClass");
		}
	}
	class DerivedClass : BaseClass {
		//Переопределение метода базовго класса
		public override void Method() {
			Console.WriteLine("Method from derivedClass");
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
			instanceDown.Method();

			//Delay
			Console.ReadKey();
		}

	}
}
