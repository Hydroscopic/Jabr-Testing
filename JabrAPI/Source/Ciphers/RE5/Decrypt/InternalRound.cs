using System;
using System.Collections.Generic;


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

                Int32 parsedEncoding = (Int32)Numsys.FastToDecimalFromCustom128
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

                    parsedEncoding = (Int32)Numsys.FastToDecimalFromCustom128
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



            static internal List<Byte> UnsanitizedDecryptionRound(
                List<Byte> encryptedChunk,
                List<Byte> prAlphabet, List<Byte> exAlphabet,
                List<Byte> shifts,
                Int32 exLength,
                Int32 maxEncodingLength,
                Int32 realMessageLength,
                ref Int32 decodedId)
            {
                if (encryptedChunk.Count < maxEncodingLength) return [];

                Int32 shCount = shifts.Count, encCurId = 0;
                Int32 parsedEncoding = (Int32)Numsys.ToDecimalFromCustomBigInteger
                (
                    encryptedChunk[1..maxEncodingLength],
                    exAlphabet,
                    exLength
                );


                Int32 bufferId = exAlphabet.IndexOf(encryptedChunk[0])
                    - decodedId
                    - shifts[0]
                    + parsedEncoding * exLength;

                if (bufferId >= prAlphabet.Count || bufferId < 0) return [];
                decodedId = bufferId;


                List<Byte> decrypted = new(realMessageLength);
                decrypted.AddRange(prAlphabet[decodedId]);


                for (var curId = 1; curId < realMessageLength; curId++)
                {
                    encCurId += maxEncodingLength;
                    bufferId = exAlphabet.IndexOf(encryptedChunk[encCurId])
                        - decodedId
                        - shifts[curId % shCount];

                    parsedEncoding = (Int32)Numsys.ToDecimalFromCustomBigInteger
                    (
                        encryptedChunk[(encCurId + 1)..(encCurId + maxEncodingLength)],
                        exAlphabet,
                        exLength
                    );

                    bufferId += parsedEncoding * exLength;
                    if (bufferId >= prAlphabet.Count || bufferId < 0) return decrypted;
                    decodedId = bufferId;

                    decrypted.AddRange(prAlphabet[decodedId]);
                }

                return decrypted;
            }
        }
    }
}