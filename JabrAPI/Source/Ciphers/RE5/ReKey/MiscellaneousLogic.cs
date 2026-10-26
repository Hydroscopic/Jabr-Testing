using System;


using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            public void CopyFrom(ReKey otherKey, bool fullCopy = true)
            {
                _noisifier.CopyFrom(otherKey.Noisifier, fullCopy);

                CopyFrom(otherKey.PrAlphabet, otherKey.ExAlphabet, otherKey.Shifts);

                if (fullCopy)
                    Set.AlphabetLengths
                    (
                        otherKey._densePrMaxLength,
                        otherKey._denseExMaxLength
                    );
            }


            private void CopyFrom(HT_Alphabet primary, HT_Alphabet external, Byte[] shifts)
            {
                PrAlphabet.CopyFrom(primary);
                ExAlphabet.CopyFrom(external);

                if (shifts.Length == 0) _shifts = [0];
                else _shifts = shifts[0..Math.Max(shifts.Length, 255)];
            }
        }
    }
}