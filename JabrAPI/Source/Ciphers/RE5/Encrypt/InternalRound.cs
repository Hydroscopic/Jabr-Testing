using System;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static internal partial class Internal
        {
            static internal Byte[] EncryptionRound(
                Byte[] message, Byte[] prLogicMap, Byte[] exLogicMap, Byte[] allShifts, Byte[] maxPadding,
                Int32 exLength, Int32 encodingLength, ref EncryptLeftover leftover)
            {
                Int32 messageLength = message.Length, shCount = allShifts.Length,
                    shIdx = leftover._shiftStartId, buffer = prLogicMap[message[0]], offset = 1,
                    curFinal = leftover._prevId + buffer + allShifts[shIdx];

                leftover._prevId = buffer;
                leftover._shiftStartId = (shIdx + message.Length) % shCount;

                Byte[] encrypted = new Byte[messageLength * (encodingLength + 1)];
                Array.Fill(maxPadding, exLogicMap[0]);

                List<Byte> encoding = Numsys.FromDecimalToCustomBigInteger
                (
                    curFinal / exLength,
                    exLogicMap,
                    exLength
                );

                encrypted[0] = exLogicMap[curFinal % exLength];
                Array.Copy(maxPadding, 0, encrypted, 1, encodingLength);
                encoding.CopyTo(encrypted, 1 + encodingLength - encoding.Count);

                for (var curId = 1; curId < messageLength; curId++)
                {
                    if (++shIdx == shCount) shIdx = 0;

                    buffer   = prLogicMap[message[curId]];
                    curFinal = buffer + leftover._prevId + allShifts[shIdx];
                    leftover._prevId  = buffer;

                    encoding = Numsys.FromDecimalToCustomBigInteger
                    (
                        curFinal / exLength,
                        exLogicMap,
                        exLength
                    );

                    offset += encodingLength;

                    encrypted[offset++] = exLogicMap[curFinal % exLength];
                    Array.Copy(maxPadding, 0, encrypted, offset, encodingLength);
                    encoding.CopyTo(encrypted, offset + encodingLength - encoding.Count);
                }

                return encrypted;
            }
        }
    }
}