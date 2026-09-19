using System;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI.RE5
{
    static public partial class DecryptData
    {
        static public List<Byte> Fast(List<Byte> encrypted, ReKey reKey)
        {
            Int32 exLength = reKey.ExLength, shCount = reKey.ShCount, encLength = encrypted.Count;
            List<Byte> prAlphabet = reKey.PrAlphabet, exAlphabet = reKey.ExAlphabet, allShifts = reKey.Shifts, shifts;


            Int32 helper = (Int32)Math.Ceiling
                (
                    (double)
                    (   //  -4 bcs: (alphabet ids start at zero & dont reach .Length value) x 2
                        reKey.PrLength * 2 + allShifts.Max() - 4
                    ) / exLength
                );
            Int32 maxEncodingLength = exLength == 10 ?
                Utils.DigitCount(helper) + 1  // Optimisation for base 10 encoding
                : Numsys.AsList128<Int32>
                (
                    helper.ToString(),
                    10,
                    exLength
                ).Count + 1;  //  + 1 is to account for EncodingLength and the character it belongs to


            Int32 chunkSize = (Int32)reKey.ChunkSize / maxEncodingLength * maxEncodingLength;
            if   (chunkSize < maxEncodingLength) chunkSize = maxEncodingLength;

            Int32 chunkCount = (Int32)Math.Ceiling((double)encLength / chunkSize);
            Int32 shiftStartId = 0, decodedId = 0;
            Int32 realMessageLength, thisRoundLength, shDelta;


            List<Byte> result = new(encLength / maxEncodingLength);  //  Real message length


            for (var chunk = 0; chunk < chunkCount; chunk++)
            {
                thisRoundLength =
                    Math.Min
                    (
                        encLength - chunk * chunkSize,
                        chunkSize
                    );

                realMessageLength = thisRoundLength / maxEncodingLength;
                shDelta = shiftStartId + realMessageLength;

                shifts = shDelta > shCount ?
                    [.. allShifts.GetRange(shiftStartId, shCount - shiftStartId),
                     .. allShifts.GetRange(0, Math.Min(shiftStartId, shDelta - shCount))]
                      : allShifts.GetRange(shiftStartId, realMessageLength);
                shiftStartId = shDelta % shCount;


                result.AddRange
                (
                    Internal.DecryptionRound
                    (
                        encrypted.GetRange
                        (
                            chunk * chunkSize,
                            thisRoundLength
                        ),
                        prAlphabet,
                        exAlphabet,
                        shifts,
                        exLength,
                        maxEncodingLength,
                        realMessageLength,
                        ref decodedId
                    )
                );
            }

            return result;
        }



        static public List<Byte> FastWithDeNoising(List<Byte> encrypted, ReKey reKey)
        {
            List<Byte> denoised = Noise.RemoveFrom.Data(encrypted, reKey.Noisifier);
            return denoised == null || denoised.Count < 1 ? []
                : RE5.DecryptData.Fast(denoised, reKey);
        }
    }
}