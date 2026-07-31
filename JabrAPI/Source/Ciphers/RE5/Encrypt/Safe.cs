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
            /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
            static public List<Byte> Data(List<Byte> message, BinaryKey reKey, out Exception? exception)
            {
                if (IsMessageAndReKeyValid(message, reKey, out exception) &&
                    reKey.IsValid.ForEncryption(message, out exception))
                {
                    try
                    {
                        return RE5.Encrypt.Fast.Data(message, reKey);
                    }
                    catch (Exception innerException) { exception = innerException; }
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
            /// <param name="throwExceptions"></param>
            static public List<Byte> Data(List<Byte> message, BinaryKey reKey, bool throwExceptions = false)
            {
                List<Byte> result = RE5.Encrypt.Data(message, reKey, out Exception? exception);
                if (exception != null && throwExceptions) throw exception;
                return result;
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
            /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception)
            {
                if (IsReKeyValid(reKey, out exception) &&
                    IsNoisifierValid(reKey.Noisifier, out exception))
                {
                    try
                    {
                        RE5.Encrypt.Fast.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey);
                        return true;
                    }
                    catch (Exception innerException) { exception = innerException; }
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
            /// <param name="throwExceptions"></param>
            static public bool File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false)
            {
                bool result = RE5.Encrypt.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out Exception? exception);
                if (exception != null && throwExceptions) throw exception;
                return result;
            }
        }
    }
}