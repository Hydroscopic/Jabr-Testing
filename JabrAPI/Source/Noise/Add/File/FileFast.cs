using System;
using System.IO;


using AVcontrol;



namespace JabrAPI
{
    static public partial class Noise
    {
        /// <summary>
        /// <b>Mixes useful data or file content with soffisticated noise</b><br/>
        /// All <i>noise generation settings</i> are <b>controlled</b> inside the <b><see cref="Noisifier"/> class</b>
        /// </summary>
        static public partial class AddToFile
        {
            static public void Fast(string absoluteInputDirectory,
                string fileName, string absoluteOutputDirectory, Noisifier noisifier)
            {
                //SecureRandom random = new(noisifier.RandomReseedInterval);
                //Int32 chunkSize = (Int32)noisifier.settings.ChunkSize, initialLength = 0,
                //  hardChunkSize = (Int32)(chunkSize * noisifier.settings.HardChunkSizeToSoftCoefficient);
                //if (chunkSize < 2) chunkSize = 2;
                //if (hardChunkSize < chunkSize) hardChunkSize = chunkSize;

                //string finalFileName;
                //if (!noisifier.settings.KeepOriginalFileExtension)
                //{
                //    finalFileName = Path.ChangeExtension(fileName, "noisedv5");
                //    for (var i = 1; System.IO.File.Exists(Path.Combine(absoluteOutputDirectory, finalFileName)); i++)
                //        finalFileName = Path.ChangeExtension(fileName, $"noisedv5-{i}");
                //}
                //else finalFileName = fileName + ".noisedv5";


                //using FileStream lengthStream = new(Path.Combine(absoluteInputDirectory, fileName), FileMode.Open, FileAccess.Read);
                //using FileStream inputStream = new(Path.Combine(absoluteInputDirectory, fileName), FileMode.Open, FileAccess.Read);
                //using FileStream outputStream = new(Path.Combine(absoluteOutputDirectory, finalFileName), FileMode.Create, FileAccess.Write);

                //using (BinaryReader lengthReader = new(lengthStream))
                //{
                //    while (lengthReader.Read() != -1) initialLength++;

                //    lengthReader.Close();
                //    lengthReader.Dispose();
                //}
                //Int32 outputLength = OutputInterval.OutputLength
                //(
                //    initialLength,
                //    noisifier.settings.DynamicOutputIntervals,
                //    noisifier.settings.IntervalChoiceSetting,
                //    noisifier.settings.LengthChoiceSetting,
                //    random
                //);


                //if (initialLength >= outputLength)
                //{
                //    System.IO.File.Copy
                //    (
                //        Path.Combine(absoluteInputDirectory, fileName),
                //        Path.Combine(absoluteOutputDirectory, finalFileName),
                //        false  //  overwrite
                //    );
                //    return;
                //}


                //Int32 maxSyntropy = Miscellaneous.CalculateMaxNonEntropy
                //    (
                //        noisifier.settings.ExpectedEntropy,
                //        initialLength,
                //        outputLength
                //    );
                //double maxAvgNoiseCount = 2.0 * (outputLength - initialLength) / (initialLength + 1);
                //double avgNoisePerCharInRound = (double)initialLength / outputLength;

                //using BinaryReader reader = new(inputStream);
                //using BinaryWriter writer = new(outputStream);

                //Byte[] parsedChars = [];
                //bool isFileEnd = false;
                //Int32 prevFinalUnnoised = 0, offset = 0, processedCount = 0, maxRoundLength, messageChunk;

                //for (var curOptimalSize = chunkSize; processedCount + initialLength - offset < outputLength; curOptimalSize += chunkSize)
                //{
                //    random.Reseed();

                //    maxRoundLength = Math.Min
                //        (
                //            hardChunkSize,
                //            Math.Min
                //            (
                //                outputLength,
                //                curOptimalSize
                //            ) - processedCount
                //        );

                //    messageChunk = processedCount - outputLength + maxRoundLength >= 0
                //        ? initialLength - offset
                //        : Math.Min
                //        (
                //            initialLength - offset,
                //            Math.Max
                //            (
                //                (Int32)(maxRoundLength * avgNoisePerCharInRound),
                //                (Int32)(processedCount * avgNoisePerCharInRound
                //                    + 0.75 - processedCount / outputLength) - offset  // 0.75 = ((outP / outP) + 0.5) / 2
                //            )
                //        );

                //    if (!isFileEnd)
                //    {
                //        if (messageChunk == 0)
                //        {
                //            isFileEnd = true;
                //            parsedChars = [];
                //            maxRoundLength = outputLength - processedCount;
                //        }
                //        else
                //        {
                //            Byte[] readBuffer = new Byte[messageChunk];
                //            var actuallyRead = reader.Read(readBuffer, 0, messageChunk);

                //            parsedChars = readBuffer[0..actuallyRead];
                //            isFileEnd = actuallyRead == 0;
                //        }
                //    }
                //    else
                //    {
                //        parsedChars = [];
                //        maxRoundLength = outputLength - processedCount;
                //    }


                //    parsedChars = AdditionRound
                //    (
                //        [.. parsedChars],
                //        noisifier.PrimaryNoise,  //  Fake selection is not supported in file noise.Addition
                //        noisifier,
                //        random,
                //        maxRoundLength,
                //        maxSyntropy,
                //        maxAvgNoiseCount,
                //        ref prevFinalUnnoised
                //    );

                //    offset += messageChunk;
                //    processedCount += parsedChars.Length;
                //    writer.Write(parsedChars);
                //}
            }
        }
    }
}