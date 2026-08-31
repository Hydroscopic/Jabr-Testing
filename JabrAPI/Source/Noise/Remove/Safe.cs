using System;
using System.Collections.Generic;


using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public partial class RemoveFrom
        {
            static public List<Byte> Data(List<Byte> noised, IReKey reKey, bool throwExceptions = false)
            {
                if (IsMessageAndReKeyAndNoisifierValid(noised, reKey, throwExceptions) &&
                    reKey.Noisifier.IsValid.ForReKey(reKey, throwExceptions))
                {
                    try
                    {
                        return Noise.RemoveFrom.FastData(noised, reKey.Noisifier);
                    }
                    catch { throw; }
                }
                return [];
            }
            static public List<Byte> Data(List<Byte> noised, Noisifier noisifier, bool throwExceptions = false)
            {
                if (IsMessageAndNoisifierValid(noised, noisifier, throwExceptions))
                {
                    try
                    {
                        return Noise.RemoveFrom.FastData(noised, noisifier);
                    }
                    catch { throw; }
                }
                return [];
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