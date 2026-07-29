using System;
using System.Collections.Generic;


using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public List<Byte> Decrypt(List<Byte> encrypted, BinaryKey reKey, out Exception? exception)
        {
            if (IsMessageAndReKeyValid(encrypted, reKey, out exception) &&
                reKey.IsValid.ForDecryption(encrypted, out exception))
            {
                try
                {
                    return FastDecrypt(encrypted, reKey);
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return [];
        }
        static public List<Byte> Decrypt(List<Byte> encrypted, BinaryKey reKey, bool throwExceptions = false)
        {
            List<Byte> result = Decrypt(encrypted, reKey, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }


        static public bool DecryptFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception)
        {
            if (IsReKeyValid(reKey, out exception) &&
                IsNoisifierValid(reKey.Noisifier, out exception))
            {
                try
                {
                    FastDecryptFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey);
                    return true;
                }
                catch (Exception innerException) { exception = innerException; }
            }
            return false;
        }
        static public bool DecryptFile(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false)
        {
            bool result = DecryptFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out Exception? exception);
            if (exception != null && throwExceptions) throw exception;
            return result;
        }



        static public List<Byte> DecryptWithNoise(List<Byte> encrypted, BinaryKey reKey, out Exception? exception)
        {
            List<Byte> denoised = Noise.Remove.Binary(encrypted, reKey, out exception);
            return denoised == null || denoised.Count < 1 ? []
                    : Decrypt(denoised, reKey, out exception);
        }
        static public List<Byte> DecryptWithNoise(List<Byte> encrypted, BinaryKey reKey, bool throwExceptions = false)
        {
            List<Byte> denoised = Noise.Remove.Binary(encrypted, reKey, throwExceptions);
            return denoised == null || denoised.Count < 1 ? []
                    : Decrypt(denoised, reKey, throwExceptions);
        }
    }
}