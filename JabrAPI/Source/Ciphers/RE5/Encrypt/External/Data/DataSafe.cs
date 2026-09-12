using System;
using System.Collections.Generic;



namespace JabrAPI.RE5
{
    static public partial class EncryptData
    {
        static public List<Byte> Safe(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
        {
            if (Miscellaneous.IsMessageAndReKeyValid(message, reKey, throwExceptions) &&
                reKey.IsValid.ForEncryption(message, throwExceptions))
            {
                try
                {
                    return RE5.EncryptData.Fast(message, reKey, ref prevId);
                }
                catch { throw; }
            }
            return [];
        }
        static public List<Byte> Safe(List<Byte> message, ReKey reKey, bool throwExceptions = false)
        {
            Int32 prevId = 0;
            return EncryptData.Safe(message, reKey, ref prevId, throwExceptions);
        }


        static public List<Byte> SafePlusNoise(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
        {
            List<Byte> result = EncryptData.Safe(message, reKey, ref prevId, throwExceptions);
            return result == null || result.Count < 1 ? []
                    : Noise.AddTo.Data(result, reKey, throwExceptions);
        }
        static public List<Byte> SafePlusNoise(List<Byte> message, ReKey reKey, bool throwExceptions = false)
        {
            List<Byte> result = EncryptData.Safe(message, reKey, throwExceptions);
            return result == null || result.Count < 1 ? []
                    : Noise.AddTo.Data(result, reKey, throwExceptions);
        }
    }
}