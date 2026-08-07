using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class Noise
    {
        /// <summary>
        /// Removes all previously mixed Noise from the content
        /// </summary>
        static public partial class RemoveFrom
        {
            static public List<Byte> FastData(List<Byte> message, Noisifier noisifier)
            {
                Int32 chunkSize  = (Int32)noisifier.settings.ChunkSize,
                      chunkCount = (Int32)Math.Ceiling((double)message.Count / chunkSize);

                if (chunkSize < 1)
                    throw new ArgumentException
                    (
                        $"Impossible to split data into chunks of size: {chunkSize}",
                        nameof(noisifier.settings)
                    );

                Byte[][] finalisedChunks = new Byte[chunkCount][];
                bool ignoringIsActive = false;
                List<Byte> primary = noisifier.PrimaryNoise, complex = noisifier.ComplexNoise;

                for (var chunk = 0; chunk < chunkCount; chunk++)
                {
                    finalisedChunks[chunk] =
                        RemovalRound
                        (
                            message.GetRange
                            (
                                chunk * chunkSize,
                                Math.Min
                                (
                                    chunkSize,
                                    message.Count - chunk * chunkSize
                                )
                            ),
                            ref ignoringIsActive,
                            primary,
                            complex
                        );
                }

                return [.. finalisedChunks.SelectMany(c => c)];
            }



            static public string FastFile(string absoluteInputDirectory,
                string fileName, string absoluteOutputDirectory, Noisifier noisifier)
            {
                List<Byte> primary = noisifier.PrimaryNoise, complex = noisifier.ComplexNoise;
                string finalFileName;

                Int32 chunkSize = (Int32)noisifier.settings.ChunkSize;
                if   (chunkSize < 2) chunkSize = 2;


                if (!noisifier.settings.KeepOriginalFileExtension)
                {
                    finalFileName = Path.ChangeExtension(fileName, "dnoisev5");
                    for (var i = 1; System.IO.File.Exists(Path.Combine(absoluteOutputDirectory, finalFileName)); i++)
                        finalFileName = Path.ChangeExtension(fileName, $"dnoisev5-{i}");
                }
                else finalFileName = fileName + ".dnoisev5";


                using FileStream inputStream  = new(Path.Combine(absoluteInputDirectory,  fileName),      FileMode.Open,   FileAccess.Read);
                using FileStream outputStream = new(Path.Combine(absoluteOutputDirectory, finalFileName), FileMode.Create, FileAccess.Write);

                using BinaryReader reader = new(inputStream);
                using BinaryWriter writer = new(outputStream);


                Int32 bytesRead;
                bool ignoringIsActive = false;
                Byte[] noisedChunk = new Byte[chunkSize];

                while ((bytesRead = reader.Read(noisedChunk, 0, chunkSize)) > 0)
                {
                    writer.Write
                    (
                        RemovalRound
                        (
                            [.. noisedChunk[0..bytesRead]],
                            ref ignoringIsActive,
                            primary,
                            complex
                        )
                    );
                }
                return finalFileName;
            }
        }
    }
}