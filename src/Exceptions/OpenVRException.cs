using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcStSwitcher.Exceptions
{

    [Serializable]
    public class OpenVRException : Exception
    {
        public OpenVRException() { }
        public OpenVRException(string message) : base(message) { }
        public OpenVRException(string message, Exception inner) : base(message, inner) { }
        protected OpenVRException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
