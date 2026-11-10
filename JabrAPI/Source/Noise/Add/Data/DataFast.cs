using System;


using AVcontrol;



namespace JabrAPI
{
    static public partial class Noise
    {
        /// <summary>
        /// <b>Mixes useful data or file content with soffisticated noise</b><br/>
        /// All <i>noise generation settings</i> are <b>controlled</b> inside the <b><see cref="Noisifier"/> class</b>
        /// </summary>
        static public partial class AddToData
        {
            static public Byte[] Fast(Byte[] message, Noisifier noisifier, Byte[] fakeSelection)
            {
                SecureRandom random = new(noisifier.RandomReseedInterval);
                Int32 chunkSize = (Int32)noisifier.settings.ChunkSize, initialLength = message.Length,
                  hardChunkSize = (Int32)(chunkSize * noisifier.settings.HardChunkSizeToSoftCoefficient);
                if (chunkSize < 2) chunkSize = 2;
                if (hardChunkSize < chunkSize) hardChunkSize = chunkSize;


                Int32 outputLength = OutputInterval.OutputLength
                    (
                        initialLength,
                        noisifier.settings.DynamicOutputIntervals,
                        noisifier.settings.IntervalChoiceSetting,
                        noisifier.settings.LengthChoiceSetting,
                        random
                    );
                if (initialLength >= outputLength) return message;


                Int32 maxSyntropy = Miscellaneous.CalculateMaxNonEntropy
                    (
                        noisifier.settings.ExpectedEntropy,
                        initialLength,
                        outputLength
                    );
                double maxAvgNoiseCount = 2.0 * (outputLength - initialLength) / (initialLength + 1);
                double avgNoisePerCharInRound = (double)initialLength / outputLength;

                Byte[] result = new Byte[outputLength];
                fakeSelection = fakeSelection.Length < 1 ? noisifier.PrimaryNoise : fakeSelection;
                Int32 prevFinalUnnoised = 0, maxRoundLength, offset = 0, messageChunk, curCount = 0;

                for (var curOptimalSize = chunkSize; curCount + initialLength - offset < outputLength; curOptimalSize += chunkSize)
                {
                    random.Reseed();

                    maxRoundLength = Math.Min
                        (
                            hardChunkSize,
                            Math.Min
                            (
                                outputLength,
                                curOptimalSize
                            ) - curCount
                        );

                    messageChunk = curCount - outputLength + maxRoundLength >= 0
                        ? initialLength - offset
                        : Math.Min
                        (
                            initialLength - offset,
                            Math.Max
                            (
                                (Int32)(maxRoundLength * avgNoisePerCharInRound),
                                (Int32)(curCount * avgNoisePerCharInRound + 0.75) - offset
                            )   // 0.75 = ((outP / outP) + 0.5) / 2
                        );

                    var a = message[offset..(offset + messageChunk)];

                    curCount += Internal.AdditionRound
                    (
                        message[offset..(offset + messageChunk)],
                        result,   //  <- output
                        curCount,
                        fakeSelection,
                        noisifier,
                        random,
                        maxRoundLength,
                        maxSyntropy,
                        maxAvgNoiseCount,
                        ref prevFinalUnnoised
                    );
                    offset += messageChunk;
                }

                return result;
            }
        }
    }
}