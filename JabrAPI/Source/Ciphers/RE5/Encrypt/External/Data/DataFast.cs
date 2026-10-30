using System;
using System.Linq;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class EncryptData
        {
            static public Byte[] Fast(Byte[] message, ReKey reKey, ref EncryptLeftover leftover)
            {
                Byte[] prLogicMap = reKey.PrAlphabet.ExportReverse, allShifts = reKey.Shifts,
                       exLogicMap = reKey.ExAlphabet.ExportDirect;

                Int32 exLength =  reKey.ExLength,
                      helper   = (reKey.PrLength * 2 + allShifts.Max() - 4 + exLength - 1) / exLength;
                Int32 encodingLength = exLength == 10
                    ? Utils.DigitCount(helper)
                    : Numsys.AsList64<Int32>(helper.ToString(), 10, exLength).Count;
                //  -4 bcs: (alphabet ids start at zero & dont reach .Length value) x 2
                // + 1 is to account for EncodingLength and the character it belongs to

                return Internal.EncryptionRound(
                    message, prLogicMap, exLogicMap, allShifts, new Byte[encodingLength],
                    exLength, encodingLength, ref leftover);
            }
            static public Byte[] Fast(Byte[] message, ReKey reKey)
            {
                EncryptLeftover leftover = new();
                return Fast(message, reKey, ref leftover);
            }



            static public Byte[] FastWithNoising(Byte[] message, ReKey reKey, ref EncryptLeftover prevId)
            {
                Byte[] result = RE5.EncryptData.Fast(message, reKey, ref prevId);
                return result == null || result.Length < 1 ? []
                        : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
            }
            static public Byte[] FastWithNoising(Byte[] message, ReKey reKey)
            {
                Byte[] result = RE5.EncryptData.Fast(message, reKey);
                return result == null || result.Length < 1 ? []
                        : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
            }
        }
    }
}