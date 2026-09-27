using System;
using System.Collections.Generic;


using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            private readonly List<Byte> _prAlphabet = [];  // primary
            private readonly List<Byte> _exAlphabet = [];  // external

            private Byte _densePrMaxLength = 255, _denseExMaxLength = 32;
            public ReKeyUtil_IsValid IsValid
            {
                get => field ??= new ReKeyUtil_IsValid(this);
                private set;
            } = null;
            public ReKeyUtil_Set Set
            {
                get => field ??= new ReKeyUtil_Set(this);
                private set;
            } = null;



            public ReKey(List<Byte> primary, List<Byte> external, List<Byte> shifts)
            {
                Set!.Sensitive.PrAlphabet(primary);
                Set!.Sensitive.ExAlphabet(external);
                Set!.Sensitive.Shifts(shifts);
            }
            public ReKey(Int32 shiftCount)
            {
                Set!.TargetShCount(shiftCount);
            }
            public ReKey(ReKey otherKey, bool fullCopy = true)
            {
                CopyFrom(otherKey, fullCopy);
            }
            public ReKey(bool autoGenerate = true)
            {
                if (autoGenerate) ReGenerate();
                else Set!.AlphabetLengths();
            }
            public ReKey(Byte[] exportData)
            {
                ImportFromBinary(exportData);
            }
        }
    }
}