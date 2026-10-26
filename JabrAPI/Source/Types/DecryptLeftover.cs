using System;



namespace JabrAPI
{
    public class DecryptLeftover
    {
        internal Byte[] _encrypted = [];
        internal Int32 _decodedId = 0;

        internal Int32 _maxEncodingLength = -1;
        internal Int32 _shiftStartId = 0;

        public bool ignoringIsActive = false;
    }
}