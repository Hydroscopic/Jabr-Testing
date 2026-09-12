using System;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI.RE5
{
    static public partial class EncryptData
    {
        static public List<Byte> Fast(List<Byte> message, ReKey reKey, ref Int32 prevId)
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
            Int32 maxEncodingLength = exLength == 10 ?
                Utils.DigitCount(helper)  //  Optimisation for base 10 encoding
                : Numsys.AsList128<Int32>
                (
                    helper.ToString(),
                    10,
                    exLength
                ).Count;

            Int32 chunkSize  = (Int32)reKey.ChunkSize / (maxEncodingLength + 1);
            if   (chunkSize <= maxEncodingLength) chunkSize = maxEncodingLength + 1;

            Int32 chunkCount = (Int32)Math.Ceiling((double)messageLength / chunkSize);
            Int32 thisRoundLength, shDelta, shiftStartId = 0;

            List<Byte> result = new(messageLength * (maxEncodingLength + 1));


            for (var chunk = 0; chunk < chunkCount; chunk++)
            {
                thisRoundLength =
                    Math.Min
                    (
                        messageLength - chunk * chunkSize,
                        chunkSize
                    );

                shDelta = shiftStartId + thisRoundLength;

                shifts  = shDelta > shCount ?
                    [.. allShifts.GetRange(shiftStartId, shCount - shiftStartId),
                        .. allShifts.GetRange(0, Math.Min(shiftStartId, shDelta - shCount))]
                        : allShifts.GetRange(shiftStartId, thisRoundLength);
                shiftStartId = shDelta % shCount;


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
                        maxEncodingLength,
                        ref prevId
                    )
                );
            }

            return result;
        }
        static public List<Byte> Fast(List<Byte> message, ReKey reKey)
        {
            Int32 prevId = 0;
            return Fast(message, reKey, ref prevId);
        }


        static public List<Byte> FastPlusNoise(List<Byte> message, ReKey reKey, ref Int32 prevId)
        {
            List<Byte> result = RE5.EncryptData.Fast(message, reKey, ref prevId);
            return result == null || result.Count < 1 ? []
                    : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
        }
        static public List<Byte> FastPlusNoise(List<Byte> message, ReKey reKey)
        {
            List<Byte> result = RE5.EncryptData.Fast(message, reKey);
            return result == null || result.Count < 1 ? []
                    : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
        }
    }
}