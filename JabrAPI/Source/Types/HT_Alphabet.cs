using System;

using AVcontrol;



namespace JabrAPI
{
    public sealed class HT_Alphabet
    {
        private Byte[] _directAlphabet  = [];
        private Byte[] _reverseAlphabet = [];



        public Int32 Length => _directAlphabet.Length;

        public Byte Direct (Byte id) => _directAlphabet[id];
        public Byte Reverse(Byte id) => _reverseAlphabet[id];

        public Byte[] ExportDirect  => _directAlphabet;
        public Byte[] ExportReverse => _reverseAlphabet;



        public void Regenerate(Int32 decompressedLength, SecureRandom random)
        {
            _directAlphabet  = new Byte[decompressedLength];
            _reverseAlphabet = new Byte[decompressedLength];
            
            Span<Byte> deck = stackalloc Byte[256];
            DEFAULT.BYTES.CopyTo(deck);

            var direct  = _directAlphabet;
            var reverse = _reverseAlphabet;

            for (var slot = 0; slot < decompressedLength; slot++)
            {
                Int32 picked = random.Next(slot, 256);
                (deck[slot], deck[picked]) = (deck[picked], deck[slot]);

                Byte value = deck[slot];
                direct [slot]  = value;
                reverse[value] = (Byte)slot;
            }
        }


        public void CopyFrom(ReadOnlySpan<Byte> compressed)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(compressed.Length, 2);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(compressed.Length, 256);

            if (_directAlphabet.Length != compressed.Length)
            {
                _directAlphabet  = new Byte[compressed.Length];
                _reverseAlphabet = new Byte[compressed.Length];
            }

            compressed.CopyTo(_directAlphabet);
            for (var i = 0; i < _directAlphabet.Length; i++)
                _reverseAlphabet[_directAlphabet[i]] = (Byte)i;
        }
        public void CopyFrom(HT_Alphabet other)
        {
            other. _directAlphabet.CopyTo(_directAlphabet);
            other._reverseAlphabet.CopyTo(_reverseAlphabet);
        }
    }
}