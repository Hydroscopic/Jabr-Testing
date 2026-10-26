using System;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class DecryptData
        {
            static public (Byte[] result, bool didSucceed) Safe(Byte[] encrypted, ReKey reKey, bool throwExceptions = false)
            {
                if (Miscellaneous.IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                    reKey.IsValid.ForDecryption(encrypted, throwExceptions))
                {
                    try { return (RE5.DecryptData.Fast(encrypted, reKey), true); }
                    catch { if (throwExceptions) throw; }
                }
                return ([], false);
            }
            static public (Byte[] result, bool didSucceed) Safe(Byte[] encrypted, ReKey reKey, ref DecryptLeftover leftover, bool throwExceptions = false)
            {
                if (Miscellaneous.IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                    reKey.IsValid.ForDecryption(encrypted, throwExceptions))
                {
                    try { return (RE5.DecryptData.Fast(encrypted, reKey, ref leftover), true); }
                    catch { if (throwExceptions) throw; }
                }
                return ([], false);
            }



            static public (Byte[] result, bool didSucceed) SafeWithDeNoising(Byte[] encrypted, ReKey reKey, bool throwExceptions = false)
            {
                Byte[] denoised = Noise.RemoveFrom.Data(encrypted, reKey, throwExceptions);
                return denoised == null || denoised.Length < 1 ? ([], false) : RE5.DecryptData.Safe(denoised, reKey, throwExceptions);
            }
            static public (Byte[] result, bool didSucceed) SafeWithDeNoising(Byte[] encrypted, ReKey reKey, ref DecryptLeftover leftover, bool throwExceptions = false)
            {
                Byte[] denoised = Noise.RemoveFrom.Data(encrypted, reKey, ref leftover.ignoringIsActive, throwExceptions);
                return denoised == null || denoised.Length < 1 ? ([], false) : RE5.DecryptData.Safe(denoised, reKey, ref leftover, throwExceptions);
            }
        }
    }
}