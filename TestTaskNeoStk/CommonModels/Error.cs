using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.CommonModels
{
    public class Error
    {
        /// <summary>
        /// Ключ ошибки
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public string Value { get; }

        public Error(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Error(string value) : this(string.Empty, value)
        {
            Value = value;
        }
    }
}
