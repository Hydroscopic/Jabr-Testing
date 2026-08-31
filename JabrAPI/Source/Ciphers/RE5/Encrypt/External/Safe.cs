using System;
using System.Collections.Generic;


using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Encrypt
        {
            /// <summary>
            /// Returns the <b>Encrypted</b> <paramref name="message"/>
            /// </summary>
            /// <returns><b>Encrypted</b> <paramref name="message"/></returns>
            /// 
            /// <param name="message">secret data</param>
            /// <param name="reKey">RE5 Encryption key for enciphering</param>
            /// <param name="prevId">Shift for continious encryption</param>
            static public List<Byte> Data(List<Byte> message, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
            {
                if (IsMessageAndReKeyValid(message, reKey, throwExceptions) &&
                    reKey.IsValid.ForEncryption(message, throwExceptions))
                {
                    try
                    {
                        return RE5.Encrypt.Fast.Data(message, reKey, ref prevId);
                    }
                    catch { throw; }
                }
                return [];
            }
            /// <summary>
            /// Returns the <b>Encrypted</b> <paramref name="message"/>
            /// </summary>
            /// <returns><b>Encrypted</b> <paramref name="message"/></returns>
            /// 
            /// <param name="message">secret data</param>
            /// <param name="reKey">RE5 Encryption key for enciphering</param>
            static public List<Byte> Data(List<Byte> message, ReKey reKey, bool throwExceptions = false)
            {
                Int32 prevId = 0;
                return Data(message, reKey, ref prevId, throwExceptions);
            }



            /// <summary>
            /// Creates a <b>FILE</b> containing the <b>Encrypted</b> content<br/>
            /// Returns the <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b>
            /// </summary>
            /// <returns>The <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b></returns>
            /// 
            /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
            /// <param name="fileName">Original FILE NAME</param>
            /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
            /// <param name="reKey">RE5 Encryption key for enciphering</param>
            /// <param name="prevId">Shift for continious encryption</param>
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
            {
                if (IsReKeyValid(reKey, throwExceptions) &&
                    IsNoisifierValid(reKey.Noisifier, throwExceptions))
                {
                    try
                    {
                        RE5.Encrypt.Fast.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }

            /// <summary>
            /// Creates a <b>FILE</b> containing the <b>Encrypted</b> content<br/>
            /// Returns the <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b>
            /// </summary>
            /// <returns>The <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b></returns>
            /// 
            /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
            /// <param name="fileName">Original FILE NAME</param>
            /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
            /// <param name="reKey">RE5 Encryption key for enciphering</param>
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false)
            {
                Int32 prevId = 0;
                return File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId, throwExceptions);
            }
        }
    }
}