using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class DecryptData
        {
            static public (List<Byte> result, bool didSucceed) Safe(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
            {
                if (Miscellaneous.IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                    reKey.IsValid.ForDecryption(encrypted, throwExceptions))
                {
                    try
                    {
                        return (RE5.DecryptData.Fast(encrypted, reKey), true);
                    }
                    catch { throw; }
                }
                return ([], false);
            }
            static public (List<Byte> result, bool didSucceed) Safe(List<Byte> encrypted, ReKey reKey, ref DecryptLeftover leftover, bool throwExceptions = false)
            {
                if (Miscellaneous.IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                    reKey.IsValid.ForDecryption(encrypted, throwExceptions))
                {
                    try
                    {
                        return (RE5.DecryptData.Fast(encrypted, reKey, ref leftover), true);
                    }
                    catch { throw; }
                }
                return ([], false);
            }



            static public (List<Byte> result, bool didSucceed) SafeWithDeNoising(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
            {
                List<Byte> denoised = Noise.RemoveFrom.Data(encrypted, reKey, throwExceptions);
                return denoised == null || denoised.Count < 1 ? ([], false) : RE5.DecryptData.Safe(denoised, reKey, throwExceptions);
            }
            static public (List<Byte> result, bool didSucceed) SafeWithDeNoising(List<Byte> encrypted, ReKey reKey, ref DecryptLeftover leftover, bool throwExceptions = false)
            {
                List<Byte> denoised = Noise.RemoveFrom.Data(encrypted, reKey, ref leftover.ignoringIsActive, throwExceptions);
                return denoised == null || denoised.Count < 1 ? ([], false) : RE5.DecryptData.Safe(denoised, reKey, ref leftover, throwExceptions);
            }
        }
    }
}