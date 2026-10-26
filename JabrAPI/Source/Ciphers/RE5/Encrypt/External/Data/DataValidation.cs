using System;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class EncryptData
        {
            static public (Byte[] result, bool didSucceed) Safe(Byte[] message, ReKey reKey, ref EncryptLeftover leftover, bool throwExceptions = false)
            {
                if (Miscellaneous.IsMessageAndReKeyValid(message, reKey, throwExceptions) &&
                    reKey.IsValid.ForEncryption(message, throwExceptions))
                {
                    try { return (RE5.EncryptData.Fast(message, reKey, ref leftover), true); }
                    catch { throw; }
                }
                return ([], false);
            }
            static public (Byte[] result, bool didSucceed) Safe(Byte[] message, ReKey reKey, bool throwExceptions = false)
            {
                EncryptLeftover leftover = new();
                return EncryptData.Safe(message, reKey, ref leftover, throwExceptions);
            }


            static public (Byte[] result, bool didSucceed) SafeWithNoising(Byte[] message, ReKey reKey, ref EncryptLeftover leftover, bool throwExceptions = false)
            {
                var (result, didSucceed) = EncryptData.Safe(message, reKey, ref leftover, throwExceptions);
                return !didSucceed ? ([], false) :
                    (Noise.AddTo.Data(result, reKey, throwExceptions), true);
            }
            static public (Byte[] result, bool didSucceed) SafeWithNoising(Byte[] message, ReKey reKey, bool throwExceptions = false)
            {
                var (result, didSucceed) = EncryptData.Safe(message, reKey, throwExceptions);
                return !didSucceed ? ([], false) :
                    (Noise.AddTo.Data(result, reKey, throwExceptions), true);
            }
        }
    }
}