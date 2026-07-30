using System;
using System.Linq;
using System.Collections.Generic;


using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public List<Byte> Add(List<Byte> message, IBinaryKey reKey,
            out Exception? exception)
        {
            if (IsMessageAndReKeyAndNoisifierValid(message, reKey, out exception) &&
                reKey.Noisifier.IsValid.ForMessageAndReKey(reKey, message, out exception))
            {
                try
                {
                    return FastAdd(message, reKey.Noisifier, [.. message.Distinct()]);
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return [];
        }
        static public List<Byte> Add(List<Byte> message, IBinaryKey reKey,
            bool throwExceptions = false)
        {
            List<Byte> result = Add(message, reKey, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }

        static public List<Byte> Add(List<Byte> message, BinaryNoisifier noisifier,
            out Exception? exception)
        {
            if (IsMessageAndNoisifierValid(message, noisifier, out exception) &&
                    noisifier.IsValid.ForMessage(message, out exception))
            {
                try
                {
                    return FastAdd(message, noisifier, [.. message.Distinct()]);
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return [];
        }
        static public List<Byte> Add(List<Byte> message, BinaryNoisifier noisifier,
            bool throwExceptions = false)
        {
            List<Byte> result = Add(message, noisifier, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }



        static public bool AddToFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, IBinaryKey reKey,
                out Exception? exception)
        {
            if (IsReKeyValid(reKey, out exception) &&
                IsNoisifierValid(reKey.Noisifier, out exception))
            {
                try
                {
                    FastAddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                    return true;
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return false;
        }
        static public bool AddToFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, IBinaryKey reKey,
            bool throwExceptions = false)
        {
            bool result = AddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out Exception? exception);
            if (!result && throwExceptions) throw exception!;
            return result;
        }

        static public bool AddToFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryNoisifier noisifier,
                out Exception? exception)
        {
            if (IsNoisifierValid(noisifier, out exception))
            {
                try
                {
                    FastAddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier);
                    return true;
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return false;
        }
        static public bool AddToFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryNoisifier noisifier,
            bool throwExceptions = false)
        {
            bool result = AddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier, out Exception? exception);
            if (!result && throwExceptions) throw exception!;
            return result;
        }
    }
}