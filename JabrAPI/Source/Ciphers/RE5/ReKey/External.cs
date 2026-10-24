using System;
using System.Collections.Generic;


using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            internal HT_Alphabet PrAlphabet => _prAlphabet;
            public Int32 PrLength => _prAlphabet == null ? -1 : _prAlphabet.Length;

            internal HT_Alphabet ExAlphabet => _exAlphabet;
            public Int32 ExLength => _exAlphabet == null ? -1 : _exAlphabet.Length;

            override internal HT_Alphabet FinalAlphabet => _exAlphabet;
        }
    }
}