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

                    _noisifier.Set.Banned(ExAlphabet.ExportDirect);
                    _noisifier.ReGenerate(false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }


            public void ReGeneratePrAlphabet(Byte compactedLength = 255)
            {
                ArgumentOutOfRangeException.ThrowIfZero(compactedLength);
                _isExportUpToDate = false;
                PrAlphabet.Regenerate(compactedLength + 1, _random);
            }
            public void ReGenerateExAlphabet(Byte compactedLength = 255)
            {
                ArgumentOutOfRangeException.ThrowIfZero(compactedLength);
                _isExportUpToDate = false;
                ExAlphabet.Regenerate(compactedLength + 1, _random);
            }

            public void ReGeneratePrAlphabet()
                => ReGeneratePrAlphabet(_densePrMaxLength > 0 ? _densePrMaxLength : DEFAULT_PR_ALPHABET_LENGTH);
            public void ReGenerateExAlphabet()
                => ReGenerateExAlphabet(_denseExMaxLength > 0 ? _denseExMaxLength : DEFAULT_EX_ALPHABET_LENGTH);


            public void GenerateRandomShifts(Int32 count)
            {
                if (ExAlphabet == null || ExAlphabet.Length < 2)
                {
                    throw new ArgumentException
                    (
                        "Unable to generate shifts, external alphabet is undefined",
                        nameof(ExAlphabet)
                    );
                }

                _isExportUpToDate = false;
                GenerateRandomShifts(count, 0, (Byte)(ExAlphabet.Length - 1));
            }
            public void GenerateRandomShifts() => GenerateRandomShifts(_targetShCount);
        }
    }
}