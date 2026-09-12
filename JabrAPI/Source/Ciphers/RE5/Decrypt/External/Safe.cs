using System;
using System.Collections.Generic;


using static JabrAPI.Miscellaneous;



namespace JabrAPI.RE5
{
    static public partial class Decrypt
    {
        static public List<Byte> Data(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
        {
            if (IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                reKey.IsValid.ForDecryption(encrypted, throwExceptions))
            {
                try
                {
                    return RE5.Decrypt.Fast.Data(encrypted, reKey);
                }
                catch { throw; }
            }
            return [];
        }



        static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false)
        {
            if (IsReKeyValid(reKey, throwExceptions) &&
                IsNoisifierValid(reKey.Noisifier, throwExceptions))
            {
                try
                {
                    return (true, RE5.Decrypt.Fast.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey));
                }
                catch { throw; }
            }
            return (false, "");
        }
    }
}