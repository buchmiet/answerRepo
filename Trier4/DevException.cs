using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Trier4
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DevException : Exception, ISerializable
    {
        // Konstruktor domyślny
        public DevException() { }

        // Konstruktor z wiadomością o błędzie
        public DevException(string message) : base(message) { }

        // Konstruktor z wiadomością o błędzie i wewnętrznym wyjątkiem
        public DevException(string message, Exception inner) : base(message, inner) { }

        // Konstruktor deserializacyjny
        protected DevException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // Jeżeli nie ma niestandardowych danych, wystarczy wywołać konstruktor bazowy
        }

        // Implementacja metody GetObjectData
        [Obsolete]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Wywołaj bazową metodę GetObjectData, aby serializować dane Exception
            base.GetObjectData(info, context);
        }
    }
}
