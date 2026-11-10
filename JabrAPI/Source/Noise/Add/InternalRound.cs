using System;


using AVcontrol;



namespace JabrAPI
{
    static public partial class Noise
    {
        static internal class Internal
        {
            static internal Int32 AdditionRound(
                Byte[] message, Byte[] result, Int32 resultOffset,
                Byte[] fakeSelection, Noisifier noisifier, SecureRandom random,
                Int32 maxRoundLength, Int32 maxSyntropy,
                double maxAvgNoiseCount,
                ref Int32 prevFinalUnnoised)
            {
                Int32 initialLength = message.Length, position = resultOffset;
                if (initialLength  >= maxRoundLength)
                {
                    prevFinalUnnoised = initialLength;
                    message.CopyTo(result.AsSpan(position));
                    return initialLength;
                }


                Int32 curLength = initialLength, chosenOffset,
                      minNoiseCount = noisifier.settings.ForceOptimalEntropy
                        && prevFinalUnnoised >= maxSyntropy ? 1 : 0;

                double overflow = random.NextDouble
                (
                    minNoiseCount,
                    Math.Max
                    (
                        Math.Min
                        (
                            maxAvgNoiseCount,
                            maxRoundLength - initialLength + 1
                                - initialLength / maxSyntropy
                        ),
                        1
                    )
                );
                chosenOffset = (Int32)Math.Floor(overflow);
                overflow -= chosenOffset;

                if (chosenOffset > 0)
                {
                    prevFinalUnnoised = 1;
                    WriteNoise(result, ref position, chosenOffset, fakeSelection, noisifier, random);
                    curLength += chosenOffset;
                }
                else prevFinalUnnoised += 2;

                if (curLength >= maxRoundLength)
                {
                    prevFinalUnnoised = initialLength + 1;
                    message.CopyTo(result.AsSpan(position));
                    return curLength;
                }



                for (var i = 1; i <= initialLength; i++)
                {
                    result[position++] = message[i - 1];

                    minNoiseCount = noisifier.settings.ForceOptimalEntropy
                            && prevFinalUnnoised >= maxSyntropy
                            && i < initialLength ? 1 : 0;

                    overflow += random.NextDouble
                    (
                        minNoiseCount,
                        Math.Max
                        (
                            Math.Min
                            (
                                maxAvgNoiseCount,
                                maxRoundLength - curLength
                                    - (initialLength - i) / maxSyntropy
                            ),
                            minNoiseCount
                        )
                    );
                    chosenOffset = (Int32)Math.Floor(overflow);
                    overflow -= chosenOffset;

                    if (chosenOffset > 0)
                    {
                        prevFinalUnnoised = 0;
                        WriteNoise(result, ref position, chosenOffset, fakeSelection, noisifier, random);
                        curLength += chosenOffset;
                    }

                    if (curLength >= maxRoundLength)
                    {
                        prevFinalUnnoised = initialLength - i;
                        message[i..].CopyTo(result.AsSpan(position));
                        return curLength;
                    }

                    prevFinalUnnoised++;
                }

                prevFinalUnnoised++;
                return curLength;
            }

            static private void WriteNoise(
                Byte[] result, ref Int32 position, Int32 noiseCount,
                Byte[] fakeSelection, Noisifier noisifier, SecureRandom random)
            {
                if (noiseCount >= 2 && random.NextBoolChance(
                    noisifier.settings.ComplexNoiseIntervalBiasPercents))
                {
                    Byte[] edges = noisifier.RandomComplexSequence(2);
                    result[position++] = edges[0];  //  start ignore

                    for (var j = 1; j < noiseCount - 1; j++)
                    {
                        if (j < noiseCount - 2 && random.NextBoolChance(
                            noisifier.settings.ComplexNoisePairBiasPercents))
                        {
                            Byte[] pair = noisifier.RandomComplexSequence(2);
                            result[position++] = pair[0];
                            result[position++] = pair[1];
                            j++;
                        }
                        else if (random.NextBoolChance(noisifier.settings.PrimaryNoiseBiasPercents))
                             result[position++] = noisifier.RandomPrimaryByte;
                        else result[position++] = fakeSelection[random.Next(fakeSelection.Length)];
                    }

                    result[position++] = edges[1];  // stop ignoring
                }
                else for (var k = 0; k < noiseCount; k++)
                    result[position++] = noisifier.RandomPrimaryByte;
            }
        }
    }
}