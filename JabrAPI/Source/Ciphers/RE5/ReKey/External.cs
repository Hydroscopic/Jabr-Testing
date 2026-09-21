using System;
using System.Collections.Generic;


using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            public List<Byte> PrAlphabet => _prAlphabet;
            public Int32 PrLength => _prAlphabet == null ? -1 : _prAlphabet.Count;

            public List<Byte> ExAlphabet => _exAlphabet;
            public Int32 ExLength => _exAlphabet == null ? -1 : _exAlphabet.Count;

            override public List<Byte> FinalAlphabet => _exAlphabet;
        }
    }
}