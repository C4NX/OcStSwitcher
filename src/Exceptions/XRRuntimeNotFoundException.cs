using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcStSwitcher.Exceptions
{

    [Serializable]
    public class XRRuntimeNotFoundException : Exception
    {
        public XRRuntimeNotFoundException() { }
        public XRRuntimeNotFoundException(string message) : base(message) { }
        public XRRuntimeNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected XRRuntimeNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
