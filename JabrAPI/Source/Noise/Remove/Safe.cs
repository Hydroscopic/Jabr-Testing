using System;


using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public partial class RemoveFrom
        {
            static public (Byte[] result, bool didSucceed) Data(Byte[] noised, IReKey reKey, bool throwExceptions = false)
            {
                if (IsMessageAndReKeyAndNoisifierValid(noised, reKey, throwExceptions) &&
                    reKey.Noisifier.IsValid.ForReKey(reKey, throwExceptions))
                {
                    try { return (Noise.RemoveFrom.FastData(noised, reKey.Noisifier), true); }
                    catch { throw; }
                }
                return ([], false);
            }
            static public (Byte[] result, bool didSucceed) Data(Byte[] noised, IReKey reKey, ref bool ignoringIsActive, bool throwExceptions = false)
            {
                if (IsMessageAndReKeyAndNoisifierValid(noised, reKey, throwExceptions) &&
                    reKey.Noisifier.IsValid.ForReKey(reKey, throwExceptions))
                {
                    try { return (Noise.RemoveFrom.FastData(noised, reKey.Noisifier, ref ignoringIsActive), true); }
                    catch { throw; }
                }
                return ([], false);
            }
            
            static public (Byte[] result, bool didSucceed) Data(Byte[] noised, Noisifier noisifier, bool throwExceptions = false)
            {
                if (IsMessageAndNoisifierValid(noised, noisifier, throwExceptions))
                {
                    try { return (Noise.RemoveFrom.FastData(noised, noisifier), true); }
                    catch { throw; }
                }
                return ([], false);
            }
            static public (Byte[] result, bool didSucceed) Data(Byte[] noised, Noisifier noisifier, ref bool ignoringIsActive, bool throwExceptions = false)
            {
                if (IsMessageAndNoisifierValid(noised, noisifier, throwExceptions))
                {
                    try { return (Noise.RemoveFrom.FastData(noised, noisifier, ref ignoringIsActive), true); }
                    catch { throw; }
                }
                return ([], false);
            }



            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, IReKey reKey, bool throwExceptions = false)
            {
                if (IsReKeyValid(reKey, throwExceptions) &&
                    IsNoisifierValid(reKey.Noisifier, throwExceptions))
                {
                    try
                    {
                        Noise.RemoveFrom.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, Noisifier noisifier, bool throwExceptions = false)
            {
                if (IsNoisifierValid(noisifier, throwExceptions))
                {
                    try
                    {
                        Noise.RemoveFrom.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }
        }
    }
}