using System;
using System.Collections.Generic;
using System.Text;

namespace BookAssistant.taobaoCSV
{
    class TBColumn
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string defaultValue;

        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        private bool quoteMark;

        public bool QuoteMark
        {
            get { return quoteMark; }
            set { quoteMark = value; }
        }
        public TBColumn(string key, string name, string defaultValue,bool quoteMark) {
            this.key = key;
            this.name = name;
            this.defaultValue = defaultValue;
            this.quoteMark = quoteMark;
        }
    }
}
