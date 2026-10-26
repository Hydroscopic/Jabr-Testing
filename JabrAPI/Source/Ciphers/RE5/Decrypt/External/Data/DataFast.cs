using System;
using System.Linq;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class DecryptData
        {
            static public Byte[] Fast(Byte[] encrypted, ReKey reKey)
            {
                Byte[] prLogicMap = reKey.PrAlphabet.ExportDirect, allShifts = reKey.Shifts,
                       exLogicMap = reKey.ExAlphabet.ExportReverse;

                Int32 exLength = reKey.ExLength, encLength = encrypted.Length;

                Int32 helper = (reKey.PrLength * 2 + allShifts.Max() - 4 + exLength - 1) / exLength;
                Int32 maxEncodingLength = exLength == 10
                    ? Utils.DigitCount(helper) + 1
                    : Numsys.AsList64<Int32>(helper.ToString(), 10, exLength).Count + 1;
                // + 1 is to account for EncodingLength and the character it belongs to

                Int32 realMessageLength = encLength / maxEncodingLength;

                return Internal.DecryptionRound(
                    encrypted, prLogicMap, exLogicMap, allShifts,
                    exLength, maxEncodingLength, realMessageLength);
            }
            static public Byte[] Fast(Byte[] encrypted, ReKey reKey, ref DecryptLeftover leftover)
            {
                Byte[] prLogicMap = reKey.PrAlphabet.ExportDirect, allShifts = reKey.Shifts,
                       exLogicMap = reKey.ExAlphabet.ExportReverse, combined;
                Int32 exLength = reKey.ExLength, shCount = reKey.ShCount;

                if (leftover._maxEncodingLength == -1)
                {
                    Int32 helper = (reKey.PrLength * 2 + allShifts.Max() - 4 + exLength - 1) / exLength;
                    leftover._maxEncodingLength = exLength == 10
                        ? Utils.DigitCount(helper) + 1
                        : Numsys.AsList64<Int32>(helper.ToString(), 10, exLength).Count + 1;
                }

                Int32 maxEnc  = leftover._maxEncodingLength, leftoverLength = leftover._encrypted.Length,
                    encLength = leftoverLength + encrypted.Length, potentialDecodedLength = encLength / maxEnc;

                if (leftoverLength == 0) combined = encrypted;
                else
                {
                    combined = new Byte[encLength];
                    Buffer.BlockCopy(leftover._encrypted, 0, combined, 0, leftoverLength);
                    Buffer.BlockCopy(encrypted, 0, combined, leftoverLength, encrypted.Length);
                }

                if (potentialDecodedLength == 0)
                {
                    leftover._encrypted = combined;
                    return [];
                }

                Byte[] result = new Byte[potentialDecodedLength];

                Int32 decodedCount = Internal.UnsanitizedDecryptionRound(
                    combined,
                    prLogicMap, exLogicMap, allShifts,
                    leftover._shiftStartId,
                    exLength, maxEnc, potentialDecodedLength,
                    ref leftover._decodedId,
                    ref result);

                Int32 consumedEncLength = decodedCount * maxEnc, remainderLength = encLength - consumedEncLength;

                if (remainderLength > 0)
                {
                    leftover._encrypted = new Byte[remainderLength];
                    Buffer.BlockCopy(combined, consumedEncLength, leftover._encrypted, 0, remainderLength);
                }
                else leftover._encrypted = [];

                leftover._shiftStartId = (leftover._shiftStartId + decodedCount) % shCount;
                if (decodedCount < potentialDecodedLength) Array.Resize(ref result, decodedCount);

                return result;
            }



            static public Byte[] FastWithDeNoising(Byte[] encrypted, ReKey reKey)
            {
                Byte[] denoised = Noise.RemoveFrom.FastData(encrypted, reKey.Noisifier);
                return denoised == null || denoised.Length < 1 ? [] : RE5.DecryptData.Fast(denoised, reKey);
            }
            static public Byte[] FastWithDeNoising(Byte[] encrypted, ReKey reKey, ref DecryptLeftover leftover)
            {
                Byte[] denoised = Noise.RemoveFrom.FastData(encrypted, reKey.Noisifier, ref leftover.ignoringIsActive);
                return denoised == null || denoised.Length < 1 ? [] : RE5.DecryptData.Fast(denoised, reKey, ref leftover);
            }
        }
    }
}