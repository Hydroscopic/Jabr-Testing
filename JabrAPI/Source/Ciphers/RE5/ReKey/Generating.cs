using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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



            private void GenerateRandomAlphabet(Span<byte> alphabet)
            {
                Span<byte> deck = stackalloc byte[256];
                DEFAULT.BYTES.CopyTo(deck);

                for (var slot = 0; slot < alphabet.Length; slot++)
                {
                    Int32 picked = _random.Next(slot, 256);
                    (deck[slot], deck[picked]) = (deck[picked], deck[slot]);
                }

                deck[..alphabet.Length].CopyTo(alphabet);
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

                CollectionsMarshal.SetCount(_prAlphabet, compactedLength_willBeIncreasedByOne + 1);
                GenerateRandomAlphabet(CollectionsMarshal.AsSpan(_prAlphabet));
            }
            public void ReGeneratePrAlphabet()
            {
                _isExportUpToDate = false;
                var length = _densePrMaxLength > 0 ? _densePrMaxLength + 1 : 255;

                CollectionsMarshal.SetCount(_prAlphabet, length);
                GenerateRandomAlphabet(CollectionsMarshal.AsSpan(_prAlphabet));
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

                CollectionsMarshal.SetCount(_exAlphabet, compactedLength_willBeIncreasedByOne + 1);
                GenerateRandomAlphabet(CollectionsMarshal.AsSpan(_exAlphabet));
            }
            public void ReGenerateExAlphabet()
            {
                _isExportUpToDate = false;
                var length = _denseExMaxLength > 0 ? _denseExMaxLength + 1 : 8;

                CollectionsMarshal.SetCount(_exAlphabet, length);
                GenerateRandomAlphabet(CollectionsMarshal.AsSpan(_exAlphabet));
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