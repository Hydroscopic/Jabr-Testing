using System;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class DecryptData
        {
            static public Byte[] Fast(Byte[] encrypted, ReKey reKey)
            {
                Byte[] prLogicMap = reKey.PrAlphabet.ExportDirect;
                Byte[] exLogicMap = reKey.ExAlphabet.ExportReverse;
                Byte[] exDigits = reKey.ExAlphabet.ExportDirect;

                Byte[] allShifts = new Byte[reKey.Shifts.Count];
                Int32 maxShift = 0;
                for (var i = 0; i < reKey.Shifts.Count; i++)
                {
                    allShifts[i] = reKey.Shifts[i];
                    if (allShifts[i] > maxShift) maxShift = allShifts[i];
                }

                Int32 exLength = reKey.ExLength, encLength = encrypted.Length;

                Int32 helper = (reKey.PrLength * 2 + maxShift - 4 + exLength - 1) / exLength;
                Int32 maxEncodingLength = exLength == 10
                    ? Utils.DigitCount(helper) + 2
                    : Numsys.AsList64<Int32>(helper.ToString(), 10, exLength).Count + 1;
                // + 1 is to account for EncodingLength and the character it belongs to

                Int32 realMessageLength = encLength / maxEncodingLength;

                return Internal.DecryptionRound(
                    encrypted, prLogicMap, exLogicMap, allShifts,
                    exLength, maxEncodingLength, realMessageLength);
            }
            static public Byte[] Fast(List<Byte> encrypted, ReKey reKey, ref DecryptLeftover leftover)
            {
                //Int32 exLength = reKey.ExLength, shCount = reKey.ShCount,
                //     encLength = encrypted.Count + leftover._encrypted.Count;
                //List<Byte> prAlphabet = reKey.PrAlphabet, shifts,
                //    exAlphabet = reKey.ExAlphabet, allShifts = reKey.Shifts;


                //if (leftover._maxEncodingLength == -1)
                //{
                //    Int32 helper = (Int32)Math.Ceiling
                //        (
                //            (double)
                //            (   //  -4 bcs: (alphabet ids start at zero & dont reach .Length value) x 2
                //                reKey.PrLength * 2 + allShifts.Max() - 4
                //            ) / exLength
                //        );
                //    leftover._maxEncodingLength = exLength == 10 ?
                //        Utils.DigitCount(helper) + 1  // Optimisation for base 10 encoding
                //      : Numsys.AsListBigInteger<Int32>
                //        (
                //            helper.ToString(),
                //            10,
                //            exLength
                //        ).Count;

                //    leftover._chunkSize = leftover._maxEncodingLength == 0 || helper < leftover._maxEncodingLength ? ++leftover._maxEncodingLength
                //        : (helper / leftover._maxEncodingLength) * ++leftover._maxEncodingLength;
                //    // ++1 is to account for EncodingLength and the character it belongs to
                //}


                //Int32 chunkCount = (Int32)Math.Ceiling((double)encLength / leftover._chunkSize);
                //Int32 realMessageLength, thisRoundLength, shDelta;
                //List<Byte> result = new(encLength / leftover._maxEncodingLength);  // Real message length


                //encrypted.InsertRange(0, leftover._encrypted);
                //for (var chunk = 0; chunk < chunkCount; chunk++)
                //{
                //    thisRoundLength =
                //        Math.Min
                //        (
                //            encLength - chunk * leftover._chunkSize,
                //            leftover._chunkSize
                //        );

                //    realMessageLength = thisRoundLength / leftover._maxEncodingLength;
                //    shDelta = leftover._shiftStartId + realMessageLength;

                //    shifts = shDelta > shCount ?
                //        [.. allShifts.GetRange(leftover._shiftStartId, shCount - leftover._shiftStartId),
                //         .. allShifts.GetRange(0, Math.Min(leftover._shiftStartId, shDelta - shCount))]
                //          : allShifts.GetRange(leftover._shiftStartId, realMessageLength);


                //    var triedDecrypting = Internal.UnsanitizedDecryptionRound
                //        (
                //            encrypted.GetRange
                //            (
                //                chunk * leftover._chunkSize,
                //                thisRoundLength
                //            ),
                //            prAlphabet,
                //            exAlphabet,
                //            shifts,
                //            exLength,
                //            leftover._maxEncodingLength,
                //            realMessageLength,
                //            ref leftover._decodedId
                //        );

                //    if (triedDecrypting.Count > 0)
                //    {
                //        result.AddRange(triedDecrypting);
                //        leftover._shiftStartId = shDelta % shCount;
                //    }
                //}

                //leftover._encrypted = encrypted[(result.Count * leftover._maxEncodingLength)..];
                //return result;
                return [];
            }



            static public Byte[] FastWithDeNoising(List<Byte> encrypted, ReKey reKey)
            {
                List<Byte> denoised = Noise.RemoveFrom.FastData(encrypted, reKey.Noisifier);
                return denoised == null || denoised.Count < 1 ? [] : RE5.DecryptData.Fast([.. denoised], reKey);
            }
            static public Byte[] FastWithDeNoising(List<Byte> encrypted, ReKey reKey, ref DecryptLeftover leftover)
            {
                List<Byte> denoised = Noise.RemoveFrom.FastData(encrypted, reKey.Noisifier, ref leftover.ignoringIsActive);
                return denoised == null || denoised.Count < 1 ? [] : RE5.DecryptData.Fast(denoised, reKey, ref leftover);
            }
        }
    }
}