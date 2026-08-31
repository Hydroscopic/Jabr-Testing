using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Encrypt
        {
            /// <summary>
            /// Noising <see cref="Noise"/> information after Encrypting <see cref="RE5.Encrypt"/> it
            /// </summary>
            static public class WithNoiseAddition
            {
                /// <summary>
                /// Returns the <b>Encrypted</b> and <b>Noised</b> <paramref name="message"/>
                /// </summary>
                /// <returns><b>Encrypted</b> and <b>Noised</b> <paramref name="message"/></returns>
                /// 
                /// <param name="message">secret data</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                /// <param name="prevId">Shift for continious encryption</param>
                static public List<Byte> Data(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
                {
                    List<Byte> result = Encrypt.Data(message, reKey, ref prevId, throwExceptions);
                    return result == null || result.Count < 1 ? []
                            : Noise.AddTo.Data(result, reKey, throwExceptions);
                }
                /// <summary>
                /// Returns the <b>Encrypted</b> and <b>Noised</b> <paramref name="message"/>
                /// </summary>
                /// <returns><b>Encrypted</b> and <b>Noised</b> <paramref name="message"/></returns>
                /// 
                /// <param name="message">secret data</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
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