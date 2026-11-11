using System;


using AVcontrol;
using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            public override bool ImportFromBinary(Byte[] exportData, bool throwExceptions = false, bool doLegacy = true)
            {
                try
                {
                    if (exportData.Length < 2)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient even for an empty noisifier",
                                nameof(exportData)
                            );
                        return false;
                    }

                    Int32 noisifierBytesCount = exportData[0];
                    if (exportData.Length < noisifierBytesCount + 1)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified PrimaryNoise in the imported noisifier\n" +
                                $"PrimaryNoiseCount: {noisifierBytesCount} from exportData[0]",
                                nameof(exportData)
                            );
                        return false;
                    }

                    noisifierBytesCount += exportData[noisifierBytesCount + 1];
                    if (exportData.Length < noisifierBytesCount + 2)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the exported noisifier in bytes count\n" +
                                $"Specified noisifier bytes: {noisifierBytesCount} from exportData[0] + exportData[" +
                                $"{noisifierBytesCount - exportData[noisifierBytesCount + 1]}",
                                nameof(exportData)
                            );
                        return false;
                    }


                    _noisifier.ImportFromBinary(exportData[0..(noisifierBytesCount + 2)], throwExceptions);
                    Byte[] onlyReKeyData = exportData[(noisifierBytesCount + 2)..];


                    if (onlyReKeyData.Length < 10)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient even for an empty BinaryKey",
                                nameof(onlyReKeyData)
                            );
                        return false;
                    }
                    Int32 parsedShiftCount = FromBinary.BigEndian<Int32>(onlyReKeyData[0..4]);


                    //  6 is the lowest possible length of an exported key
                    //  1x2 bytes reserved for PrLength and ExLength
                    //  and 1x2x2 reserved for both smallest primary and external alphabets of 2 value
                    if (onlyReKeyData.Length < parsedShiftCount + 6)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified shifts count" +
                                $" {parsedShiftCount} from onlyReKeyData[0-4]",
                                nameof(onlyReKeyData)
                            );
                        return false;
                    }

                    // +1 is transforming our length range back from 1-255 to 2-256
                    Int32 parsedLengthGuide = onlyReKeyData[parsedShiftCount + 4] + 1;

                    if (onlyReKeyData.Length < parsedShiftCount + parsedLengthGuide + 3)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified primary alphabet length" +
                                $" {parsedLengthGuide} from onlyReKeyData[{parsedShiftCount + 4}]",
                                nameof(onlyReKeyData)
                            );
                        return false;
                    }
                    else if (parsedLengthGuide < 2)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Primary alphabet length cant be less than 2 (required)" +
                                $"\nParsed length: {parsedLengthGuide} " +
                                $"from onlyReKeyData[{parsedShiftCount + 4}]",
                                nameof(onlyReKeyData)
                            );
                        return false;
                    }

                    PrAlphabet.CopyFrom(onlyReKeyData[(parsedShiftCount + 5)..(parsedShiftCount + 5 + parsedLengthGuide)]);


                    //  reusing noisifierBytesCount as a offset for what we have already read
                    noisifierBytesCount = parsedShiftCount + parsedLengthGuide + 4;

                    // +1 is transforming our length range back from 1-255 to 2-256
                    parsedLengthGuide = onlyReKeyData[noisifierBytesCount + 1] + 1;

                    if (onlyReKeyData.Length < noisifierBytesCount + parsedLengthGuide)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified external alphabet length" +
                                $" {parsedLengthGuide} from onlyReKeyData[{noisifierBytesCount + 1}]",
                                nameof(onlyReKeyData)
                            );
                        return false;
                    }
                    else if (parsedLengthGuide < 2)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"External alphabet length cant be less than 2 (required)" +
                                $"\nParsed length: {parsedLengthGuide} " +
                                $"from onlyReKeyData[{noisifierBytesCount + 1}]",
                                nameof(onlyReKeyData)
                            );
                        return false;
                    }

                    if (parsedShiftCount > 0)
                    {
                        if (doLegacy) _shifts = onlyReKeyData[4..(4 + parsedShiftCount)];
                        else _shifts = [.. Numsys.FromDecimalBigInteger<Byte>
                            (
                                Numsys.ToDecimalBigInteger<Byte>(onlyReKeyData[4..(4 + parsedShiftCount)], 256),
                                parsedLengthGuide
                        )   ];
                    }
                    else _shifts = [0];
                    ExAlphabet.CopyFrom(onlyReKeyData[(noisifierBytesCount + 2)..(noisifierBytesCount + 2 + parsedLengthGuide)]);
                }
                catch (Exception)
                {
                    if (throwExceptions) throw;
                    return false;
                }
                return true;
            }


            public override Byte[] ExportAsBinary(bool doLegacy) => ReCalculateExport(doLegacy);
            //{
            //    get => _isExportUpToDate ?
            //        field ??= ReCalculateExport(doLegacy)
            //        : ReCalculateExport(doLegacy);
            //} = null;

            private Byte[] ReCalculateExport(bool doLegacy)
            {
                _isExportUpToDate = true;
                Byte[] compressedShifts = doLegacy ? _shifts :
                    ExAlphabet.Length == 0
                    ? [0] : [..
                        Numsys.FromDecimalBigInteger<Byte>
                        (
                            Numsys.ToDecimalBigInteger<Byte>(_shifts, ExAlphabet.Length),
                            256
                    )   ];

                return [
                    .. _noisifier.ExportAsBinary(),
                    .. ToBinary.BigEndian(compressedShifts.Length),
                    .. compressedShifts,

                    (Byte)(PrLength - 1),
                    .. PrAlphabet.ExportDirect,
                    (Byte)(ExLength - 1),
                    .. ExAlphabet.ExportDirect
                ];
            }
        }
    }
}