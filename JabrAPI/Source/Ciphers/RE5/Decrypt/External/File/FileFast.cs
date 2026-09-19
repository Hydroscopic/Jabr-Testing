using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI.RE5
{
    static public partial class DecryptFile
    {
        static public string Fast(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey)
        {
            Int32 exLength = reKey.ExLength, shCount = reKey.ShCount;
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


            string finalFileName;
            if (!reKey.KeepOriginalFileExtension)
            {
                finalFileName = Path.ChangeExtension(fileName, "dec-re5");
                for (var i = 1; System.IO.File.Exists(Path.Combine(absoluteOutputDirectory, finalFileName)); i++)
                    finalFileName = Path.ChangeExtension(fileName, $"dec{i}-re5");
            }
            else finalFileName = Path.ChangeExtension(fileName, null);

            using FileStream inputStream  = new(Path.Combine(absoluteInputDirectory,  fileName),      FileMode.Open,   FileAccess.Read);
            using FileStream outputStream = new(Path.Combine(absoluteOutputDirectory, finalFileName), FileMode.Create, FileAccess.Write);

            using BinaryReader reader = new(inputStream);
            using BinaryWriter writer = new(outputStream);


            Byte[] messageChunk = new Byte[chunkSize];
            Int32 offset = 0, decodedId = 0, shiftStartId = 0;
            Int32 realMessageLength, shDelta, bytesRead;

            while ((bytesRead = reader.Read(messageChunk, 0, chunkSize)) > 0)
            {
                realMessageLength = bytesRead / maxEncodingLength;
                shDelta = shiftStartId + realMessageLength;

                shifts = shDelta > shCount ?
                    [.. allShifts.GetRange(shiftStartId, shCount - shiftStartId),
                     .. allShifts.GetRange(0, Math.Min(shiftStartId, shDelta - shCount))]
                      : allShifts.GetRange(shiftStartId, realMessageLength);
                shiftStartId = shDelta % shCount;


                writer.Write
                (
                    [..
                        Internal.DecryptionRound
                        (
                            new List<Byte>(messageChunk).GetRange(0, bytesRead),
                            prAlphabet,
                            exAlphabet,
                            shifts,
                            exLength,
                            maxEncodingLength,
                            realMessageLength,
                            ref decodedId
                        )
                    ]
                );

                offset += bytesRead;
            }
            return finalFileName;
        }



        static public string FastWithDeNoising(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true)
        {
            string denoisedFileName = Noise.RemoveFrom.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
            string resultFileName   = RE5.DecryptFile.Fast(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey);
            if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
            return resultFileName;
        }
    }
}