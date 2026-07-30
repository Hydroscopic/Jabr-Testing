using System;
using System.Collections.Generic;


using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public List<Byte> Encrypt(List<Byte> message, BinaryKey reKey, out Exception? exception)
        {
            if (IsMessageAndReKeyValid(message, reKey, out exception) &&
                reKey.IsValid.ForEncryption(message, out exception))
            {
                try
                {
                    return FastEncrypt(message, reKey);
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return [];
        }
        static public List<Byte> Encrypt(List<Byte> message, BinaryKey reKey, bool throwExceptions = false)
        {
            List<Byte> result = Encrypt(message, reKey, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }



        static public bool EncryptFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception)
        {
            if (IsReKeyValid(reKey, out exception) &&
                IsNoisifierValid(reKey.Noisifier, out exception))
            {
                try
                {
                    FastEncryptFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey);
                    return true;
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return false;
        }
        static public bool EncryptFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false)
        {
            bool result = EncryptFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }



        static public List<Byte> EncryptWithNoise(List<Byte> message, BinaryKey reKey, out Exception? exception)
        {
            List<Byte> result = Encrypt(message, reKey, out exception);
            return result == null || result.Count < 1 ? []
                    : Noise.Add(result, reKey, out exception);
        }
        static public List<Byte> EncryptWithNoise(List<Byte> message, BinaryKey reKey, bool throwExceptions = false)
        {
            List<Byte> result = Encrypt(message, reKey, throwExceptions);
            return result == null || result.Count < 1 ? []
                    : Noise.Add(result, reKey, throwExceptions);
        }
    }
}