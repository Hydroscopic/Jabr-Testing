using System;
using System.Collections.Generic;



namespace JabrAPI.RE5
{
    static public partial class DecryptData
    {
        static public List<Byte> WithValidation(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
        {
            if (Miscellaneous.IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                reKey.IsValid.ForDecryption(encrypted, throwExceptions))
            {
                try
                {
                    return RE5.DecryptData.Fast(encrypted, reKey);
                }
                catch { throw; }
            }
            return [];
        }



        static public List<Byte> WithValidationAndDeNoising(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
        {
            List<Byte> denoised = Noise.RemoveFrom.Data(encrypted, reKey, throwExceptions);
            return denoised == null || denoised.Count < 1 ? []
                    : RE5.DecryptData.WithValidation(denoised, reKey, throwExceptions);
        }

    }
}