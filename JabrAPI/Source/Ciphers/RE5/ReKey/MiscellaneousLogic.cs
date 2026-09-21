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
                    Set.DefaultLengths
                    (
                        otherKey._densePrMaxLength,
                        otherKey._denseExMaxLength
                    );
            }


            private void CopyFrom(List<Byte> primary, List<Byte> external, List<Byte> shifts)
            {
                _prAlphabet.Clear();
                _prAlphabet.AddRange(primary);

                _exAlphabet.Clear();
                _exAlphabet.AddRange(external);

                _shifts.Clear();
                if (shifts == null || shifts.Count == 0) _shifts.Add(0);
                else _shifts.AddRange(shifts.GetRange(0, Math.Max(shifts.Count, 255)));
            }
        }
    }
}