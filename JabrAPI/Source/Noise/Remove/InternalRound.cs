using System;
using System.Collections.Generic;


using AVcontrol;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public partial class RemoveFrom
        {
            static private Byte[] RemovalRound(
                Byte[] noised, ref bool ignoringIsActive,
                Byte[] primaryNoise, Byte[] complexNoise)
            {
                Int32 dataStartId = 0;
                List<Byte> dynamicResult = new(noised.Length);

                for (var i = 0; i < noised.Length; i++)
                {
                    var curChar = noised[i];

                    if (complexNoise.Contains(curChar))
                    {
                        if (!ignoringIsActive)
                            dynamicResult.AddRange(
                                Utils.Interval(noised, dataStartId, i));

                        ignoringIsActive = !ignoringIsActive;
                        dataStartId = i + 1;
                        continue;
                    }
                    else if (primaryNoise.Contains(curChar))
                    {
                        if (!ignoringIsActive)
                            dynamicResult.AddRange(
                                Utils.Interval(noised, dataStartId, i));

                        dataStartId = i + 1;
                        continue;
                    }
                }

                if (!ignoringIsActive)
                    dynamicResult.AddRange(
                        Utils.Interval(noised, dataStartId, noised.Length));

                return [.. dynamicResult];
            }
        }
    }
}