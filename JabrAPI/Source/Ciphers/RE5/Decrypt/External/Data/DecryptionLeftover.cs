using System;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        public class DecryptionLeftover
        {
            internal List<Byte> _unsanitized = [];
            internal Int32 _decodedId = 0;

            internal Int32 _maxEncodingLength = -1;
            internal Int32 _chunkSize = -1;
            internal Int32 _shiftStartId = 0;

            internal bool ignoringIsActive = false;
        }
    }
}
