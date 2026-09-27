using System;
using System.Linq;
using System.Collections.Generic;


using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            public class ReKeyUtil_Set
            {
                private readonly ReKey _thisKey;

                internal ReKeyUtil_Set() { throw new InvalidOperationException("You are not supposed to use this"); }
                internal ReKeyUtil_Set(ReKey binKey) {_thisKey = binKey; }



                public ReKeyUtil_SensitiveSet Sensitive
                {
                    get => field ??= new ReKeyUtil_SensitiveSet(_thisKey);
                    private set;
                } = null;
                public class ReKeyUtil_SensitiveSet
                {
                    private readonly ReKey _thisKey;
                    internal ReKeyUtil_SensitiveSet() { throw new InvalidOperationException("You are not supposed to use this"); }
                    internal ReKeyUtil_SensitiveSet(ReKey binKey) { _thisKey = binKey; }



                    public void PrAlphabet(List<Byte> prAlphabet)
                    {
                        _thisKey._prAlphabet.Clear();
                        _thisKey._prAlphabet.AddRange(prAlphabet);
                        _thisKey._isExportUpToDate = false;
                    }
                    public bool SafePrAlphabet(List<Byte> prAlphabet)
                    {
                        if (!_thisKey.IsValid.PrAlphabet(prAlphabet)) return false;
                        _thisKey._prAlphabet.Clear();
                        _thisKey._prAlphabet.AddRange(prAlphabet);
                        _thisKey._isExportUpToDate = false;
                        return true;
                    }

                    public void ExAlphabet(List<Byte> exAlphabet)
                    {
                        _thisKey._exAlphabet.Clear();
                        _thisKey._exAlphabet.AddRange(exAlphabet);
                        _thisKey._isExportUpToDate = false;
                    }
                    public bool SafeExAlphabet(List<Byte> exAlphabet)
                    {
                        if (!_thisKey.IsValid.ExAlphabet(exAlphabet)) return false;
                        _thisKey._exAlphabet.Clear();
                        _thisKey._exAlphabet.AddRange(exAlphabet);
                        _thisKey._isExportUpToDate = false;
                        return true;
                    }



                    public void Shifts(List<Byte> shifts)
                    {
                        _thisKey._shifts.Clear();
                        _thisKey._shifts.AddRange(shifts.Count > 0 ? shifts : [0]);
                        _thisKey._isExportUpToDate = false;
                    }
                    public bool SafeShifts(List<Byte> shifts)
                    {
                        if (shifts.Max() > _thisKey.ExLength) return false;
                        _thisKey._shifts.Clear();
                        _thisKey._shifts.AddRange(shifts.Count > 0 ? shifts : [0]);
                        _thisKey._isExportUpToDate = false;
                        return true;
                    }
                }



                public void AlphabetLengths()
                {
                    _thisKey._densePrMaxLength = 255;
                    _thisKey._denseExMaxLength = 31;
                }
                public void TargetShCount(Int32 count) => _thisKey._targetShCount = count;



                public void AlphabetLengths(Byte compactedPrMaxLength, Byte compactedExMaxLength)
                {
                    _thisKey._densePrMaxLength = compactedPrMaxLength > 0 ? compactedPrMaxLength : _thisKey._densePrMaxLength;
                    _thisKey._denseExMaxLength = compactedExMaxLength > 0 ? compactedExMaxLength : _thisKey._denseExMaxLength;
                }

                public void PrLength(Byte compactedPrMaxLength)
                    => _thisKey._densePrMaxLength = compactedPrMaxLength > 0 ? compactedPrMaxLength : _thisKey._densePrMaxLength;
                public void ExLength(Byte compactedExMaxLength)
                    => _thisKey._denseExMaxLength = compactedExMaxLength > 0 ? compactedExMaxLength : _thisKey._denseExMaxLength;
            }
        }
    }
}