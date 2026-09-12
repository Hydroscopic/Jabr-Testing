using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Encrypt
        {
            static public class WithNoiseAddition
            {
                static public List<Byte> Data(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
                {
                    List<Byte> result = Encrypt.Data(message, reKey, ref prevId, throwExceptions);
                    return result == null || result.Count < 1 ? []
                            : Noise.AddTo.Data(result, reKey, throwExceptions);
                }
                static public List<Byte> Data(List<Byte> message, ReKey reKey, bool throwExceptions = false)
                {
                    List<Byte> result = Encrypt.Data(message, reKey, throwExceptions);
                    return result == null || result.Count < 1 ? []
                            : Noise.AddTo.Data(result, reKey, throwExceptions);
                }
            }
        }
    }
}