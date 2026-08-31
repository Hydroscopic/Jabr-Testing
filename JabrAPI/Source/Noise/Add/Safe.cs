using System;
using System.Linq;
using System.Collections.Generic;


using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public partial class AddTo
        {
            static public List<Byte> Data(List<Byte> message, IReKey reKey, bool throwExceptions = true)
            {
                if (IsMessageAndReKeyAndNoisifierValid(message, reKey, throwExceptions) &&
                    reKey.Noisifier.IsValid.ForMessageAndReKey(reKey, message, throwExceptions))
                {
                    try
                    {
                        return Noise.AddTo.FastData(message, reKey.Noisifier, [.. message.Distinct()]);
                    }
                    catch { throw; }
                }
                return [];
            }
            static public List<Byte> Data(List<Byte> message, Noisifier noisifier, bool throwExceptions = true)
            {
                if (IsMessageAndNoisifierValid(message, noisifier, throwExceptions) &&
                        noisifier.IsValid.ForMessage(message, throwExceptions))
                {
                    try
                    {
                        return Noise.AddTo.FastData(message, noisifier, [.. message.Distinct()]);
                    }
                    catch { throw; }
                }
                return [];
            }



            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, IReKey reKey, bool throwExceptions = true)
            {
                if (IsReKeyValid(reKey, throwExceptions) &&
                    IsNoisifierValid(reKey.Noisifier, throwExceptions))
                {
                    try
                    {
                        Noise.AddTo.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, Noisifier noisifier, bool throwExceptions = true)
            {
                if (IsNoisifierValid(noisifier, throwExceptions))
                {
                    try
                    {
                        Noise.AddTo.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }
        }
    }
}