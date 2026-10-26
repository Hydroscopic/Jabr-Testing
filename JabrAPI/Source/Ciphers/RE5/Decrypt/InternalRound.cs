using System;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static internal partial class Internal
        {
            static internal Byte[] DecryptionRound(Byte[] encrypted,
                Byte[] prLogicMap, Byte[] exLogicMap, Byte[] allShifts,
                Int32 exLength, Int32 maxEncodingLength, Int32 realMessageLength)
            {
                Int32 shCount = allShifts.Length, shIdx = 0, encCurId = 0, decodedId;

                Int32 parsedEncoding = (Int32)Numsys.FastToDecimalFromCustom64
                    (
                        encrypted.AsSpan()[1..maxEncodingLength],
                        exLogicMap,
                        exLength
                    );

                decodedId = exLogicMap[encrypted[0]]
                          - allShifts[0]
                          + parsedEncoding * exLength;

                Byte[] result = new Byte[realMessageLength];
                result[0] = prLogicMap[decodedId];

                for (var curId = 1; curId < realMessageLength; curId++)
                {
                    encCurId += maxEncodingLength;
                    if (++shIdx == shCount) shIdx = 0;

                    decodedId = exLogicMap[encrypted[encCurId]]
                              - decodedId
                              - allShifts[shIdx];

                    parsedEncoding = (Int32)Numsys.FastToDecimalFromCustom64
                        (
                            encrypted.AsSpan()[(encCurId + 1)..(encCurId + maxEncodingLength)],
                            exLogicMap,
                            exLength
                        );

                    decodedId += parsedEncoding * exLength;
                    result[curId] = prLogicMap[decodedId];
                }

                return result;
            }



            static internal Int32 UnsanitizedDecryptionRound(
                Byte[] encrypted,
                Byte[] prLogicMap, Byte[] exLogicMap, Byte[] allShifts,
                Int32 shiftStartId,
                Int32 exLength, Int32 maxEncodingLength, Int32 realMessageLength,
                ref Int32 decodedId,
                ref Byte[] output)
            {
                Int32 shCount = allShifts.Length;
                Int32 shIdx = shiftStartId;
                Int32 encCurId = 0;

                Int32 parsedEncoding = (Int32)Numsys.FastToDecimalFromCustom64
                    (
                        encrypted.AsSpan()[1..maxEncodingLength],
                        exLogicMap,
                        exLength
                    );

                Int32 bufferId = exLogicMap[encrypted[0]] - decodedId
                             - allShifts[shIdx] + parsedEncoding * exLength;

                if (bufferId >= prLogicMap.Length || bufferId < 0) return 0;

                decodedId = bufferId;
                output[0] = prLogicMap[decodedId];
                Int32 decodedCount = 1;

                for (Int32 curId = 1; curId < realMessageLength; curId++)
                {
                    encCurId += maxEncodingLength;
                    if (++shIdx == shCount) shIdx = 0;

                    parsedEncoding = (Int32)Numsys.FastToDecimalFromCustom64
                        (
                            encrypted.AsSpan()[(encCurId + 1)..(encCurId + maxEncodingLength)],
                            exLogicMap,
                            exLength
                        );

                    bufferId = exLogicMap[encrypted[encCurId]] - decodedId
                             - allShifts[shIdx] + parsedEncoding * exLength;

                    if (bufferId >= prLogicMap.Length || bufferId < 0) break;

                    decodedId = bufferId;
                    output[curId] = prLogicMap[decodedId];
                    decodedCount++;
                }

                return decodedCount;
            }
        }
    }
}