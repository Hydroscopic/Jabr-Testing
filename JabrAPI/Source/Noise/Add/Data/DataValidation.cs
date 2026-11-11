using System;
using System.Linq;


using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public partial class AddToData
        {
            static public (Byte[] result, bool didSucceed) Safe(Byte[] message, IReKey reKey, bool throwExceptions = true)
            {
                if (IsMessageAndReKeyAndNoisifierValid(message, reKey, throwExceptions) &&
                    reKey.Noisifier.IsValid.ForMessageAndReKey(reKey, message, throwExceptions))
                {
                    try { return (Noise.AddToData.Fast(message, reKey.Noisifier, [.. message.Distinct()]), true); }
                    catch { throw; }
                }
                return ([], false);
            }
            static public (Byte[] result, bool didSucceed) Safe(Byte[] message, Noisifier noisifier, bool throwExceptions = true)
            {
                if (IsMessageAndNoisifierValid(message, noisifier, throwExceptions) &&
                        noisifier.IsValid.ForMessage(message, throwExceptions))
                {
                    try { return (Noise.AddToData.Fast(message, noisifier, [.. message.Distinct()]), true); }
                    catch { throw; }
                }
                return ([], false);
            }
        }
    }
}