using System;

using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            public override bool ReGenerate()
            {
                try
                {
                    ReGeneratePrAlphabet();
                    ReGenerateExAlphabet();
                    GenerateRandomShifts();

                    _noisifier.Set.Banned([.. ExAlphabet.ExportDirect]);
                    _noisifier.Next(false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }



            public void ReGeneratePrAlphabet(byte compactedLength = 255)
            {
                ArgumentOutOfRangeException.ThrowIfZero(compactedLength);
                _isExportUpToDate = false;
                _prAlphabet.Regenerate(compactedLength, _random);
            }

            public void ReGeneratePrAlphabet()
                => ReGeneratePrAlphabet(_densePrMaxLength > 0 ? _densePrMaxLength : DEFAULT_PR_ALPHABET_LENGTH);

            public void ReGenerateExAlphabet(byte compactedLength = 255)
            {
                ArgumentOutOfRangeException.ThrowIfZero(compactedLength);
                _isExportUpToDate = false;
                _exAlphabet.Regenerate(compactedLength, _random);
            }

            public void ReGenerateExAlphabet()
                => ReGenerateExAlphabet(_denseExMaxLength > 0 ? _denseExMaxLength : DEFAULT_EX_ALPHABET_LENGTH);



            public void GenerateRandomShifts(Int32 count)
            {
                if (_exAlphabet == null || _exAlphabet.Length < 2)
                {
                    throw new ArgumentException
                    (
                        "Unable to generate shifts, external alphabet is undefined",
                        nameof(_exAlphabet)
                    );
                }

                _isExportUpToDate = false;
                GenerateRandomShifts(count, 0, (Byte)(_exAlphabet.Length - 1));
            }
            public void GenerateRandomShifts() => GenerateRandomShifts(_targetShCount);
        }
    }
}