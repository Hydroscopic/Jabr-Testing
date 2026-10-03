using System;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class EncryptData
        {
            static public List<Byte> Fast(List<Byte> message, ReKey reKey, ref EncryptLeftover leftover)
            {
                List<Byte> prAlphabet = reKey.PrAlphabet, exAlphabet = reKey.ExAlphabet, allShifts = reKey.Shifts, shifts;
                Int32 exLength = reKey.ExLength, messageLength = message.Count, shCount = reKey.ShCount;


                Int32 helper = (Int32)Math.Ceiling
                    (
                        (double)
                        (   //  -4 bcs: (alphabet ids start at zero & dont reach .Length value) x 2
                            reKey.PrLength * 2 + allShifts.Max() - 4
                        ) / exLength
                    );
                Int32 encodingLength = exLength == 10 ?
                    Utils.DigitCount(helper)  //  Optimisation for base 10 encoding
                  : Numsys.AsListBigInteger<Int32>
                    (
                        helper.ToString(),
                        10,
                        exLength
                    ).Count;

                helper = (Int32)reKey.ChunkSize;
                Int32 chunkSize = encodingLength == 0 || helper < encodingLength ? encodingLength
                      : (helper / encodingLength) * (encodingLength + 1);

                Int32 chunkCount = (Int32)Math.Ceiling((double)messageLength / chunkSize);
                Int32 thisRoundLength, shDelta;
                List<Byte> result = new(messageLength * (encodingLength + 1));


                for (var chunk = 0; chunk < chunkCount; chunk++)
                {
                    thisRoundLength =
                        Math.Min
                        (
                            messageLength - chunk * chunkSize,
                            chunkSize
                        );

                    shDelta = leftover._shiftStartId + thisRoundLength;

                    shifts = shDelta > shCount ?
                        [.. allShifts.GetRange(leftover._shiftStartId, shCount - leftover._shiftStartId),
                         .. allShifts.GetRange(0, Math.Min(leftover._shiftStartId, shDelta - shCount))]
                          : allShifts.GetRange(leftover._shiftStartId, thisRoundLength);
                    leftover._shiftStartId = shDelta % shCount;


                    result.AddRange
                    (
                        Internal.EncryptionRound
                        (
                            message.GetRange
                            (
                                chunk * chunkSize,
                                thisRoundLength
                            ),
                            prAlphabet,
                            exAlphabet,
                            shifts,
                            exLength,
                            encodingLength,
                            ref leftover._prevId
                        )
                    );
                }

                return result;
            }
            static public List<Byte> Fast(List<Byte> message, ReKey reKey)
            {
                EncryptLeftover leftover = new();
                return Fast(message, reKey, ref leftover);
            }



            static public List<Byte> FastWithNoising(List<Byte> message, ReKey reKey, ref EncryptLeftover prevId)
            {
                List<Byte> result = RE5.EncryptData.Fast(message, reKey, ref prevId);
                return result == null || result.Count < 1 ? []
                        : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
            }
            static public List<Byte> FastWithNoising(List<Byte> message, ReKey reKey)
            {
                List<Byte> result = RE5.EncryptData.Fast(message, reKey);
                return result == null || result.Count < 1 ? []
                        : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
            }
        }
    }
}