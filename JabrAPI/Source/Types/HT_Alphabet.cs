using System;

using AVcontrol;



namespace JabrAPI
{
    public sealed class HT_Alphabet
    {
        private Byte[] _directAlphabet  = [];
        private Byte[] _reverseAlphabet = [];


        public Int32 Length => _directAlphabet.Length;


        public Byte[] ExportDirect  => _directAlphabet;
        public Byte[] ExportReverse => _reverseAlphabet;



        public void Regenerate(Int32 length, SecureRandom random)
        {
            _directAlphabet  = new Byte[length];
            _reverseAlphabet = new Byte[256];
            
            Span<Byte> deck = stackalloc Byte[256];
            DEFAULT.BYTES.CopyTo(deck);

            for (var slot = 0; slot < length; slot++)
            {
                Int32 picked = random.Next(slot, 256);
                (deck[slot], deck[picked]) = (deck[picked], deck[slot]);

                Byte value = deck[slot];
                _directAlphabet [slot] = value;
                _reverseAlphabet[value] = (Byte)slot;
            }
        }


        public void CopyFrom(Byte[] compressed)
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