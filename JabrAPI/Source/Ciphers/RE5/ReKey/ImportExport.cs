using System;


using AVcontrol;
using JabrAPI.Template;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey : IReKey
        {
            public override bool ImportFromBinary(Byte[] exportData, bool throwExceptions = false)
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
                    exportData = exportData[(noisifierBytesCount + 2)..];


                    // 10 = 1b: PrLength, 2b: MinPrAlphabet, 1b: ExLength, 2b: MinExAlphabet, 4b: ShCount
                    if (exportData.Length < 10)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for a BinaryKey",
                                nameof(exportData)
                            );
                        return false;
                    }


                    // +1 is transforming our length range back from 1-255 to 2-256
                    Int32 parsedLengthGuide = exportData[0] + 1;

                    if (exportData.Length < parsedLengthGuide + 7)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified primary alphabet length" +
                                $" {parsedLengthGuide} from onlyReKeyData[0]",
                                nameof(exportData)
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
                                $"from onlyReKeyData[0]",
                                nameof(exportData)
                            );
                        return false;
                    }

                    PrAlphabet.CopyFrom(exportData[1..(1 + parsedLengthGuide)]);


                    //  reusing noisifierBytesCount as a offset for what we have already read
                    noisifierBytesCount = parsedLengthGuide;

                    // +1 is transforming our length range back from 1-255 to 2-256
                    parsedLengthGuide = exportData[noisifierBytesCount + 1] + 1;

                    if (exportData.Length < noisifierBytesCount + parsedLengthGuide + 4)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified external alphabet length" +
                                $" {parsedLengthGuide} from onlyReKeyData[{noisifierBytesCount + 1}]",
                                nameof(exportData)
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
                                nameof(exportData)
                            );
                        return false;
                    }


                    ExAlphabet.CopyFrom(exportData[(noisifierBytesCount + 2)..(noisifierBytesCount + 2 + parsedLengthGuide)]);
                    Int32 parsedShiftCount = FromBinary.BigEndian<Int32>(exportData
                            [  (
                                    noisifierBytesCount + 2 + parsedLengthGuide
                            )..(
                                    noisifierBytesCount + 6 + parsedLengthGuide)
                            ]  );

                    if (exportData.Length < noisifierBytesCount + 6 + parsedLengthGuide + parsedShiftCount)
                    {
                        if (throwExceptions)
                            throw new ArgumentException
                            (
                                $"Data length is insufficient for the specified shifts count" +
                                $" {parsedShiftCount} from onlyReKeyData[" +
                                $"{noisifierBytesCount + 6 + parsedLengthGuide}-" +
                                $"{noisifierBytesCount + 6 + parsedLengthGuide + parsedShiftCount}]",
                                nameof(exportData)
                            );
                        return false;
                    }

                    _shifts = parsedShiftCount <= 0 ? [0] : [..
                        Numsys.FromDecimalBigInteger<Byte>
                        (
                            Numsys.ToDecimalBigInteger<Byte>
                            (
                                exportData
                                [  (
                                        noisifierBytesCount + 6 + parsedLengthGuide
                                )..(
                                        noisifierBytesCount + 6 + parsedLengthGuide + parsedShiftCount
                                )  ],
                                256
                            ),
                            parsedLengthGuide
                        )   ];
                }
                catch (Exception)
                {
                    if (throwExceptions) throw;
                    return false;
                }
                return true;
            }


            public override Byte[] ExportAsBinary
            {
                get => _isExportUpToDate ?
                    field ??= ReCalculateExport()
                    : ReCalculateExport();
            } = null;

            private Byte[] ReCalculateExport()
            {
                _isExportUpToDate = true;
                Byte[] compressedShifts = ExAlphabet.Length == 0
                    ? [] : [..
                        Numsys.FromDecimalBigInteger<Byte>
                        (
                            Numsys.ToDecimalBigInteger<Byte>(_shifts, ExAlphabet.Length),
                            256
                    )   ];

                return
                [
                    .. _noisifier.ExportAsBinary(),

                    (Byte)(PrLength - 1),
                    .. PrAlphabet.ExportDirect,
                    (Byte)(ExLength - 1),
                    .. ExAlphabet.ExportDirect,

                    .. ToBinary.BigEndian(compressedShifts.Length),
                    .. compressedShifts
                ];
            }
        }
    }
}