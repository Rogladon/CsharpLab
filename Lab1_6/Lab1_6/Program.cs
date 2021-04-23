using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_6 {
	public class Element {
		private string name;
		private int field1;
		private int field2;
		public Element(string s, int a, int b) {
			name = s;
			field1 = a;
			field2 = b;
		}
		public int Field1 {
			get { return field1; }
			set { field1 = value; }
		}
		public int Field2 {
			get { return field2; }
			set { field2 = value; }
		}
		public string Name {
			get { return name; }
			set { name = value; }
		}
	}
	
	public class UserCollection : IEnumerable, IEnumerator {
		public Element[] elementsArray = null;
		public UserCollection() {
			elementsArray = new Element[4];
			elementsArray[0] = new Element("A", 1, 10);
			elementsArray[1] = new Element("B", 2, 20);
			elementsArray[2] = new Element("C", 3, 30);
			elementsArray[3] = new Element("D", 4, 40);
		}

		int position = -1;

		public bool MoveNext() {
			if (position < elementsArray.Length - 1) {
				position++;
				return true;
			} else {
				Reset();
				return false;
			}
		}

		public void Reset() {
			position = -1;
		}

		public object Current {
			get { return elementsArray[position]; }
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return this as IEnumerator;
		}
	}
	class Program {
		static void Main() {
			UserCollection myCollection = new UserCollection();
			
			foreach (Element element in myCollection) {
				Console.WriteLine("Name: {0} Field1: {1} Field2: {2}", element.Name,
				element.Field1, element.Field2);
			}

			Console.Write(new string('-', 29) + "\n");

			foreach (Element element in myCollection) {
				Console.WriteLine("Name: {0} Field1: {1} Field2: {2}", element.Name,
				element.Field1, element.Field2);
			}
			Console.Write(new string('-', 29) + "\n");

			UserCollection myElementsCollection = new UserCollection(); 

			IEnumerable enumerable = myElementsCollection as IEnumerable;

			IEnumerator enumerator = enumerable.GetEnumerator();
			while (enumerator.MoveNext()) { 
				Element element = enumerator.Current as Element;
				Console.WriteLine("Name: {0} Field1: {1} Field2: {2}", element.Name,
				element.Field1, element.Field2);
			}
			enumerator.Reset(); 

			Console.ReadKey();
		}
	}
}
