using System;
using System.Collections.Generic;


using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public List<Byte> Remove(List<Byte> noised, IBinaryKey reKey,
            out Exception? exception)
        {
            if (IsMessageAndReKeyAndNoisifierValid(noised, reKey, out exception) &&
                reKey.Noisifier.IsValid.ForReKey(reKey, out exception))
            {
                try
                {
                    return FastRemove(noised, reKey.Noisifier);
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return [];
        }
        static public List<Byte> Remove(List<Byte> noised, IBinaryKey reKey,
            bool throwExceptions = false)
        {
            List<Byte> result = Remove(noised, reKey, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }

        static public List<Byte> Remove(List<Byte> noised, BinaryNoisifier noisifier,
            out Exception? exception)
        {
            if (IsMessageAndNoisifierValid(noised, noisifier, out exception))
            {
                try
                {
                    return FastRemove(noised, noisifier);
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return [];
        }
        static public List<Byte> Remove(List<Byte> noised, BinaryNoisifier noisifier,
            bool throwExceptions = false)
        {
            List<Byte> result = Remove(noised, noisifier, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }


        static public bool RemoveFromFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, IBinaryKey reKey,
                out Exception? exception)
        {
            if (IsReKeyValid(reKey, out exception) &&
                IsNoisifierValid(reKey.Noisifier, out exception))
            {
                try
                {
                    FastRemoveFromFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                    return true;
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return false;
        }
        static public bool RemoveFromFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, IBinaryKey reKey,
            bool throwExceptions = false)
        {
            bool result = RemoveFromFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out Exception? exception);
            if (!result && throwExceptions) throw exception!;
            return result;
        }
        static public bool RemoveFromFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryNoisifier noisifier,
                out Exception? exception)
        {
            if (IsNoisifierValid(noisifier, out exception))
            {
                try
                {
                    FastRemoveFromFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier);
                    return true;
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return false;
        }
        static public bool RemoveFromFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryNoisifier noisifier,
            bool throwExceptions = false)
        {
            bool result = RemoveFromFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier, out Exception? exception);
            if (!result && throwExceptions) throw exception!;
            return result;
        }
    }
}