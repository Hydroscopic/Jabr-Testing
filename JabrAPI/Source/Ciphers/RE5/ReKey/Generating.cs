using System;
using System.Collections.Generic;


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

                    _noisifier.Set.Banned(ExAlphabet);
                    _noisifier.Next(false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }



            private List<Byte> GenerateRandomAlphabet(Int32 length)
            {
                List<Byte> remainingChoices = [.. DEFAULT.BYTES];
                List<Byte> resultAlphabet = [];

                for (var remaining = 0; remaining < length; remaining++)
                {
                    Byte maxValueInclusive = (Byte)Math.Min(255, remainingChoices.Count - 1);
                    var chosen = _random.Next(maxValueInclusive);
                    var chosenId = _random.Next(resultAlphabet.Count);

                    resultAlphabet.Insert(chosenId, remainingChoices[chosen]);
                    remainingChoices.RemoveAt(chosen);
                }
                return resultAlphabet;
            }


            public void ReGeneratePrAlphabet(Byte compactedLength_willBeIncreasedByOne = 255)
            {
                if (compactedLength_willBeIncreasedByOne < 1)
                    throw new ArgumentOutOfRangeException
                    (
                        "Provided PrimaryAlphabet length must be in 1-255 range" +
                        "\nIt will later be converted from a 1-255 range to a 2-256",
                        nameof(compactedLength_willBeIncreasedByOne)
                    );

                _isExportUpToDate = false;
                _prAlphabet.Clear();
                _prAlphabet.AddRange(GenerateRandomAlphabet(compactedLength_willBeIncreasedByOne + 1));
            }
            public void ReGeneratePrAlphabet()
            {
                _isExportUpToDate = false;
                _prAlphabet.Clear();

                _prAlphabet.AddRange(_densePrMaxLength > 0
                    ? GenerateRandomAlphabet(_densePrMaxLength + 1)
                    : GenerateRandomAlphabet(255));
            }

            public void ReGenerateExAlphabet(Byte compactedLength_willBeIncreasedByOne = 255)
            {
                if (compactedLength_willBeIncreasedByOne < 1)
                    throw new ArgumentOutOfRangeException
                    (
                        "Provided PrimaryAlphabet length must be in 1-255 range" +
                        "\nIt will later be converted from a 1-255 range to a 2-256",
                        nameof(compactedLength_willBeIncreasedByOne)
                    );

                _isExportUpToDate = false;
                _exAlphabet.Clear();
                _exAlphabet.AddRange(GenerateRandomAlphabet(compactedLength_willBeIncreasedByOne + 1));
            }
            public void ReGenerateExAlphabet()
            {
                _isExportUpToDate = false;
                _exAlphabet.Clear();

                _exAlphabet.AddRange(_denseExMaxLength > 0
                    ? GenerateRandomAlphabet(_denseExMaxLength + 1)
                    : GenerateRandomAlphabet(8));
            }



            public void GenerateRandomShifts(Int32 count)
            {
                if (_exAlphabet == null || _exAlphabet.Count < 2)
                {
                    throw new ArgumentException
                    (
                        "Unable to generate shifts, external alphabet is undefined",
                        nameof(_exAlphabet)
                    );
                }

                _isExportUpToDate = false;
                GenerateRandomShifts(count, 0, (Byte)(_exAlphabet.Count - 1));
            }
            public void GenerateRandomShifts() => GenerateRandomShifts(_targetShCount);
        }
    }
}