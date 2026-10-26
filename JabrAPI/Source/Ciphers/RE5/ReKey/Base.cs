using System;


using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            static private readonly Byte DEFAULT_PR_ALPHABET_LENGTH = 255;
            static private readonly Byte DEFAULT_EX_ALPHABET_LENGTH = 15;


            public readonly HT_Alphabet PrAlphabet = new();
            public readonly HT_Alphabet ExAlphabet = new();
            override internal HT_Alphabet FinalAlphabet => ExAlphabet;

            public Int32 PrLength => PrAlphabet == null ? -1 : PrAlphabet.Length;
            public Int32 ExLength => ExAlphabet == null ? -1 : ExAlphabet.Length;

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



            public ReKey(Byte[] primary, Byte[] external, Byte[] shifts)
            {
                Set!.Sensitive.PrAlphabet(primary);
                Set!.Sensitive.ExAlphabet(external);
                Set!.Sensitive.Shifts(shifts);
            }
            public ReKey(bool autoGenerate = true)
            {
                if (autoGenerate) ReGenerate();
                else Set!.AlphabetLengths();
            }
            public ReKey(Int32 shiftCount) => Set!.TargetShCount(shiftCount);
            public ReKey(ReKey otherKey, bool fullCopy = true) => CopyFrom(otherKey, fullCopy);
            public ReKey(Byte[] exportData) => ImportFromBinary(exportData);
        }
    }
}