using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP
{
    public class Message
    {
        private string _content;
        private string _title;

        public Message(string content, string title)
        {
            _content = content;
            _title = title;
        }
        internal bool isEmpty()
        {
            return (_content.Equals("") || _title.Equals(""));
        }
    }
}
