using System;
using System.Collections.Generic;


using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Decrypt
        {
            /// <summary>
            /// Returns the <b>Decrypted</b> data of <paramref name="encrypted"/>
            /// </summary>
            /// <returns><b>Decrypted</b> data of <paramref name="encrypted"/></returns>
            /// 
            /// <param name="encrypted">Obfuscated data</param>
            /// <param name="reKey">RE5 Encryption key for deciphering</param>
            /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
            static public List<Byte> Data(List<Byte> encrypted, BinaryKey reKey, out Exception? exception)
            {
                if (IsMessageAndReKeyValid(encrypted, reKey, out exception) &&
                    reKey.IsValid.ForDecryption(encrypted, out exception))
                {
                    try
                    {
                        return RE5.Decrypt.Fast.Data(encrypted, reKey);
                    }
                    catch (Exception innerException) { exception = innerException; }
                }
                return [];
            }

            /// <summary>
            /// Returns the <b>Decrypted</b> data of <paramref name="encrypted"/>
            /// </summary>
            /// <returns><b>Decrypted</b> data of <paramref name="encrypted"/></returns>
            /// 
            /// <param name="encrypted">Obfuscated data</param>
            /// <param name="reKey">RE5 Encryption key for deciphering</param>
            /// <param name="throwExceptions"></param>
            static public List<Byte> Data(List<Byte> encrypted, BinaryKey reKey, bool throwExceptions = false)
            {
                List<Byte> result = RE5.Decrypt.Data(encrypted, reKey, out Exception? exception);
                if (exception != null && throwExceptions) throw exception;
                return result;
            }



            /// <summary>
            /// Creates a <b>FILE</b> containing the <b>Decrypted</b> content<br/>
            /// Returns the <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b>
            /// </summary>
            /// <returns>The <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b></returns>
            /// 
            /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
            /// <param name="fileName">Original FILE NAME</param>
            /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
            /// <param name="reKey">RE5 Encryption key for deciphering</param>
            /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
            static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception)
            {
                if (IsReKeyValid(reKey, out exception) &&
                    IsNoisifierValid(reKey.Noisifier, out exception))
                {
                    try
                    {
                        return (true, RE5.Decrypt.Fast.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey));
                    }
                    catch (Exception innerException) { exception = innerException; }
                }
                return (false, "");
            }

            /// <summary>
            /// Creates a <b>FILE</b> containing the <b>Decrypted</b> content<br/>
            /// Returns the <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b>
            /// </summary>
            /// <returns>The <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b></returns>
            /// 
            /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
            /// <param name="fileName">Original FILE NAME</param>
            /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
            /// <param name="reKey">RE5 Encryption key for deciphering</param>
            /// <param name="throwExceptions"></param>
            static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false)
                {
                    (bool result, string resultFileName) = RE5.Decrypt.File(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, out Exception? exception);

                    if (exception != null && throwExceptions) throw exception;
                    return (result, resultFileName);
                }
        }
    }
}