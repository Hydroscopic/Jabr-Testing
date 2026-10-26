using System;


using AVcontrol;



namespace JabrAPI
{
    static public partial class RE5
    {
        static internal partial class Internal
        {
            static internal Byte[] EncryptionRound(
                Byte[] messageChunk,
                Byte[] prAlphabet, Byte[] exAlphabet, Byte[] shifts,
                Int32 exLength, Int32 encodingLength,
                ref Int32 prevId)
            {
                Int32 messageLength = messageChunk.Length, shCount = shifts.Length;
                Int32 buffer = prAlphabet.IndexOf(messageChunk[0]), curFinal = prevId + buffer + shifts[0];
                prevId = buffer;

                Byte[] encoding = [.. Numsys.ToCustomAsBinaryBigInteger
                (
                    Split.BigEndian<Int32, Byte>(curFinal / exLength, 10),
                    10,
                    exLength,
                    [.. exAlphabet],
                    encodingLength
                )];

                Byte[] encrypted = new Byte[messageLength * (encodingLength + 1)];

                encrypted[0] = exAlphabet[curFinal % exLength];
                Array.Copy(encoding, 0, encrypted, 1, encoding.Length);

                for (var curId = 1; curId < messageLength; curId++)
                {
                    buffer   = prAlphabet.IndexOf(messageChunk[curId]);
                    curFinal = buffer + prevId + shifts[curId % shCount];
                    prevId   = buffer;

                    encoding = [.. Numsys.ToCustomAsBinaryBigInteger
                    (
                        Split.BigEndian<Int32, Byte>(curFinal / exLength, 10),
                        10,
                        exLength,
                        [.. exAlphabet],
                        encodingLength
                    )];

                    encrypted[curId * (encodingLength + 1)] = exAlphabet[curFinal % exLength];
                    Array.Copy(encoding, 0, encrypted, curId * (encodingLength + 1) + 1, encoding.Length);
                }

                return encrypted;
            }
        }
    }
}