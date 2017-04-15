using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using TableCommandControl.Properties;

namespace TableCommandControl.View {
    public class ListBoxWriter : TextWriter //this class redirects console.writeline to debug listbox
    {
        private readonly ListBox _list;
        private StringBuilder _content = new StringBuilder();

        public ListBoxWriter(ListBox list)
        {
            _list = list;
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
        public override void Write(char value)
        {
            base.Write(value);

         
            if (value != '\n') {
                _content.Append(value);
                return;
            }
          
            if (App.Current.Dispatcher.Thread!=_list.Dispatcher.Thread)
            {
                try
                {
                    App.Current.Dispatcher.Invoke(() => _list.Items.Add(_content.ToString()));
        
                }
                catch (ObjectDisposedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                _list.Items.Add(_content.ToString());
              
                
            }
            _content = new StringBuilder();
        }
    }
}