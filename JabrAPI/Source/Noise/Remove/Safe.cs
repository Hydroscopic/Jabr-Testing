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
            static public List<Byte> Data(List<Byte> noised, IReKey reKey,
                out Exception? exception)
            {
                if (IsMessageAndReKeyAndNoisifierValid(noised, reKey, out exception) &&
                    reKey.Noisifier.IsValid.ForReKey(reKey, out exception))
                {
                    try
                    {
                        return Noise.RemoveFrom.FastData(noised, reKey.Noisifier);
                    }
                    catch (Exception innerException) { exception = innerException; }
                }
                return [];
            }
            static public List<Byte> Data(List<Byte> noised, IReKey reKey,
                bool throwExceptions = false)
            {
                List<Byte> result = Noise.RemoveFrom.Data(noised, reKey, out Exception? exception);
                if (exception != null && throwExceptions) throw exception;
                return result;
            }

            static public List<Byte> Data(List<Byte> noised, Noisifier noisifier,
                out Exception? exception)
            {
                if (IsMessageAndNoisifierValid(noised, noisifier, out exception))
                {
                    try
                    {
                        return Noise.RemoveFrom.FastData(noised, noisifier);
                    }
                    catch (Exception innerException) { exception = innerException; }
                }
                return [];
            }
            static public List<Byte> Data(List<Byte> noised, Noisifier noisifier,
                bool throwExceptions = false)
            {
                List<Byte> result = Noise.RemoveFrom.Data(noised, noisifier, out Exception? exception);
                if (exception != null && throwExceptions) throw exception;
                return result;
            }



            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, IReKey reKey,
                    out Exception? exception)
            {
                if (IsReKeyValid(reKey, out exception) &&
                    IsNoisifierValid(reKey.Noisifier, out exception))
                {
                    try
                    {
                        Noise.RemoveFrom.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                        return true;
                    }
                    catch (Exception innerException) { exception = innerException; }
                }
                return false;
            }
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, IReKey reKey,
                bool throwExceptions = false)
            {
                bool result = Noise.RemoveFrom.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out Exception? exception);
                if (!result && throwExceptions) throw exception!;
                return result;
            }
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, Noisifier noisifier,
                    out Exception? exception)
            {
                if (IsNoisifierValid(noisifier, out exception))
                {
                    try
                    {
                        Noise.RemoveFrom.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier);
                        return true;
                    }
                    catch (Exception innerException) { exception = innerException; }
                }
                return false;
            }
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, Noisifier noisifier,
                bool throwExceptions = false)
            {
                bool result = Noise.RemoveFrom.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier, out Exception? exception);
                if (!result && throwExceptions) throw exception!;
                return result;
            }
        }
    }
}