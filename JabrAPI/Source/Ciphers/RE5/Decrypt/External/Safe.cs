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
            static public List<Byte> Data(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
            {
                if (IsMessageAndReKeyValid(encrypted, reKey, throwExceptions) &&
                    reKey.IsValid.ForDecryption(encrypted, throwExceptions))
                {
                    try
                    {
                        return RE5.Decrypt.Fast.Data(encrypted, reKey);
                    }
                    catch { throw; }
                }
                return [];
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
            static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false)
            {
                if (IsReKeyValid(reKey, throwExceptions) &&
                    IsNoisifierValid(reKey.Noisifier, throwExceptions))
                {
                    try
                    {
                        return (true, RE5.Decrypt.Fast.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey));
                    }
                    catch { throw; }
                }
                return (false, "");
            }
        }
    }
}