using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1 {
	class Document {
		public string Footer {
			set {
				footer.Content = value;
			}
		}
		public string Body {
			set {
				body.Content = value;
			}
		}
		Title title;
		Footer footer;
		Body body;

		public Document(string title) {
			this.title.Content = title;
			InitializeDocument();
		}
		private void InitializeDocument() { }
		public void Show() {
			title.Show();
			body.Show();
			footer.Show();
		}

	}
}
