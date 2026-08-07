using System;
using System.Collections.Generic;

using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            private readonly SetHelper _setHelper;
            private readonly ValidateHelper _validationHelper;

            private readonly List<Byte> _primaryAlphabet = [];
            private readonly List<Byte> _externalAlphabet = [];

            private Byte _compactedPrMaxLength = 255, _compactedExMaxLength = 32;



            public ReKey(List<Byte> primary, List<Byte> external, List<Byte> shifts)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                Set.Sensitive.PrAlphabet(primary);
                Set.Sensitive.ExAlphabet(external);
                Set.Sensitive.Shifts(shifts);
            }
            public ReKey(List<Byte> primary, List<Byte> external, Byte shift)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                Set.Sensitive.PrAlphabet(primary);
                Set.Sensitive.ExAlphabet(external);
                Set.Sensitive.Shift(shift);
            }
            public ReKey(Int32 shiftCount)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                Set.ShiftCount(shiftCount);
            }
            public ReKey(ReKey otherKey, bool fullCopy = true)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                CopyFrom(otherKey, fullCopy);
            }
            public ReKey(bool autoGenerate = true)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                if (autoGenerate) DefaultGenerate();
                else Set.Default();
            }

            public ReKey(List<Byte> exportData)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                ImportFromBinary(exportData);
            }
            public ReKey(Byte[] exportData)
            {
                _setHelper = new(this);
                _validationHelper = new(this);

                ImportFromBinary(exportData);
            }
        }
    }
}