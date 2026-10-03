using System;
using System.Collections.Generic;



namespace JabrAPI
{
    public class DecryptLeftover
    {
        internal List<Byte> _encrypted = [];
        internal Int32 _decodedId = 0;

        internal Int32 _maxEncodingLength = -1;
        internal Int32 _chunkSize = -1;
        internal Int32 _shiftStartId = 0;

        public bool ignoringIsActive = false;
    }
}