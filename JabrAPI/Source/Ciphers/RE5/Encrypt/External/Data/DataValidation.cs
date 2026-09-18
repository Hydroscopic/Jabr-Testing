using System;
using System.Collections.Generic;



namespace JabrAPI.RE5
{
    static public partial class EncryptData
    {
        static public (List<Byte> result, bool didSucceed) WithValidation(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
        {
            if (Miscellaneous.IsMessageAndReKeyValid(message, reKey, throwExceptions) &&
                reKey.IsValid.ForEncryption(message, throwExceptions))
            {
                try
                {
                    return (RE5.EncryptData.Fast(message, reKey, ref prevId), true);
                }
                catch { throw; }
            }
            return ([], false);
        }
        static public (List<Byte> result, bool didSucceed) WithValidation(List<Byte> message, ReKey reKey, bool throwExceptions = false)
        {
            Int32 prevId = 0;
            return EncryptData.WithValidation(message, reKey, ref prevId, throwExceptions);
        }


        static public (List<Byte> result, bool didSucceed) WithValidationAndNoising(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
        {
            var (result, didSucceed) = EncryptData.WithValidation(message, reKey, ref prevId, throwExceptions);
            return !didSucceed ? ([], false) :
                (Noise.AddTo.Data(result, reKey, throwExceptions), true);
        }
        static public (List<Byte> result, bool didSucceed) WithValidationAndNoising(List<Byte> message, ReKey reKey, bool throwExceptions = false)
        {
            var (result, didSucceed) = EncryptData.WithValidation(message, reKey, throwExceptions);
            return !didSucceed ? ([], false) :
                (Noise.AddTo.Data(result, reKey, throwExceptions), true);
        }
    }
}