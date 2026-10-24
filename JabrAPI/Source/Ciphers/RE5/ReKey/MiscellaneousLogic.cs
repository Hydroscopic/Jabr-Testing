using System;
using System.Collections.Generic;


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


            private void CopyFrom(HT_Alphabet primary, HT_Alphabet external, List<Byte> shifts)
            {
                _prAlphabet.CopyFrom(primary);
                _exAlphabet.CopyFrom(external);

                _shifts.Clear();
                if (shifts.Count == 0) _shifts.Add(0);
                else _shifts.AddRange(shifts.GetRange(0, Math.Max(shifts.Count, 255)));
            }
        }
    }
}