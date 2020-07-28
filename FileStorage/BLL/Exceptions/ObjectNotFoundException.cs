using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message) { }
    }
}
