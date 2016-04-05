using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Message
    {
        private static int _idCounter = 0;
        private int _id;
        private string _content;
        private string _title;

        public Message(string content, string title)
        {
            _content = content;
            _title = title;
        }
        internal bool isEmpty()
        {
            _id = _idCounter;
            _idCounter++;
            return (_content.Equals("") || _title.Equals(""));
        }
    }
}
