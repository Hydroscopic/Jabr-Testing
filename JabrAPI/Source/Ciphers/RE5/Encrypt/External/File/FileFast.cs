using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI.RE5
{
    static public partial class EncryptFile
    {
        static public string Fast(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId)
        {
            List<Byte> prAlphabet = reKey.PrAlphabet, exAlphabet = reKey.ExAlphabet, allShifts = reKey.Shifts, shifts;
            Int32 exLength = reKey.ExLength, shCount = reKey.ShCount;


            Int32 helper = (Int32)Math.Ceiling
                (
                    (double)
                    (   //  -4 bcs: (alphabet ids start at zero & dont reach .Length value) x 2
                        reKey.PrLength * 2 + reKey.Shifts.Max() - 4
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

            Int32 chunkSize = (Int32)reKey.ChunkSize / (maxEncodingLength + 1);
            if (chunkSize <= maxEncodingLength) chunkSize = maxEncodingLength + 1;


            string finalFileName;
            if (!reKey.KeepOriginalFileExtension)
            {
                finalFileName = Path.ChangeExtension(fileName, "enc-re5");
                for (var i = 1; System.IO.File.Exists(Path.Combine(absoluteOutputDirectory, finalFileName)); i++)
                    finalFileName = Path.ChangeExtension(fileName, $"enc{i}-re5");
            }
            else finalFileName = fileName + ".re5";

            using FileStream inputStream  = new(Path.Combine(absoluteInputDirectory, fileName), FileMode.Open, FileAccess.Read);
            using FileStream outputStream = new(Path.Combine(absoluteOutputDirectory, finalFileName), FileMode.Create, FileAccess.Write);

            using BinaryReader reader = new(inputStream);
            using BinaryWriter writer = new(outputStream);


            Byte[] messageChunk = new Byte[chunkSize];
            Int32 offset = 0, shiftStartId = 0, shDelta, bytesRead;

            while ((bytesRead = reader.Read(messageChunk, 0, chunkSize)) > 0)
            {
                shDelta = shiftStartId + bytesRead;

                shifts = shDelta > shCount ?
                    [.. allShifts.GetRange(shiftStartId, shCount - shiftStartId),
                     .. allShifts.GetRange(0, Math.Min(shiftStartId, shDelta - shCount))]
                      : allShifts.GetRange(shiftStartId, bytesRead);
                shiftStartId = shDelta % shCount;

                writer.Write
                (
                    [..
                        Internal.EncryptionRound
                        (
                            new List<Byte>(messageChunk).GetRange(0, bytesRead),
                            prAlphabet,
                            exAlphabet,
                            shifts,
                            exLength,
                            maxEncodingLength,
                            ref prevId
                        )
                    ]
                );

                offset += bytesRead;
            }
            return finalFileName;
        }
        static public string Fast(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey)
        {
            Int32 prevId = 0;
            return Fast(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId);
        }


        static public string FastPlusNoise(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId, bool deleteTempFileAfterUse = true)
        {
            //Noise.FastAddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier, prevId);
            //string resultFileName = RE5.Encrypt.Fast.File(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey, prevId);
            //if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
            return "";
        }
        static public string FastPlusNoise(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true)
        {
            //Noise.FastAddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
            //string resultFileName = RE5.Encrypt.Fast.File(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey);
            //if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
            return "";
        }
    }
}