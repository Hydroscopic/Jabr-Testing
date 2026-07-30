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
            /// These variants <b>DO validate</b> the parameters and are surrounded in <i>try/catch</i><br/>
            /// </summary>
            static public class Safe
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
                    List<Byte> result = RE5.Decrypt.Safe.Data(encrypted, reKey, out Exception? exception);
                    if (exception != null && throwExceptions) throw exception;
                    return result;
                }



                /// <summary>
                /// Creates a FILE containing the Decrypted content<br/>
                /// Returns the NAME of the new FILE with the decrypted content
                /// </summary>
                /// <returns>The NAME of the new created file</returns>
                /// 
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
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
                /// Creates a FILE containing the Decrypted content<br/>
                /// Returns the NAME of the new FILE with the decrypted content
                /// </summary>
                /// <returns>The NAME of the new created file</returns>
                /// 
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="throwExceptions"></param>
                static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false)
                {
                    (bool result, string resultFileName) = RE5.Decrypt.Safe.File(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, out Exception? exception);

                    if (exception != null && throwExceptions) throw exception;
                    return (result, resultFileName);
                }



                /// <summary>
                /// Denoising <see cref="Noise"/> information before Decrypting <see cref="RE5.Decrypt"/> it
                /// </summary>
                static public class WithNoiseRemoval
                {
                    /// <summary>
                    /// Returns the <b>Decrypted</b> and <b>Denoised</b> data of <paramref name="encrypted"/>
                    /// </summary>
                    /// <returns><b>Decrypted</b> and <b>Denoised</b> data of <paramref name="encrypted"/></returns>
                    /// 
                    /// <param name="encrypted">Obfuscated data</param>
                    /// <param name="reKey">RE5 Encryption key for deciphering</param>
                    /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                    static public List<Byte> Data(List<Byte> encrypted, BinaryKey reKey, out Exception? exception)
                    {
                        List<Byte> denoised = Noise.Remove(encrypted, reKey, out exception);
                        return denoised == null || denoised.Count < 1 ? []
                                : RE5.Decrypt.Safe.Data(denoised, reKey, out exception);
                    }
                    /// <summary>
                    /// Returns the <b>Decrypted</b> and <b>Denoised</b> data of <paramref name="encrypted"/>
                    /// </summary>
                    /// <returns><b>Decrypted</b> and <b>Denoised</b> data of <paramref name="encrypted"/></returns>
                    /// 
                    /// <param name="encrypted">Obfuscated data</param>
                    /// <param name="reKey">RE5 Encryption key for deciphering</param>
                    /// <param name="throwExceptions"></param>
                    static public List<Byte> Data(List<Byte> encrypted, BinaryKey reKey, bool throwExceptions = false)
                    {
                        List<Byte> denoised = Noise.Remove(encrypted, reKey, throwExceptions);
                        return denoised == null || denoised.Count < 1 ? []
                                : RE5.Decrypt.Safe.Data(denoised, reKey, throwExceptions);
                    }



                    /// <summary>
                    /// Creates a FILE containing the Denoised and Decrypted content<br/><br/>
                    ///   
                    /// A temporary FILE is used for storing the denoised content<br/>
                    /// It will be deleted in the end based on <paramref name="deleteTempFileAfterUse"/><br/><br/>
                    /// 
                    /// Returns the NAME of the new FILE with the denoised and decrypted content
                    /// </summary>
                    /// <returns>The NAME of the new created file</returns>
                    /// 
                    /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                    /// <param name="fileName">Original FILE NAME</param>
                    /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                    /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                    /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                    static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                        string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception, bool deleteTempFileAfterUse = true)
                    {
                        bool didSucceed = Noise.RemoveFromFile(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, out exception);

                        if (!didSucceed) return (false, "");
                        return RE5.Decrypt.Safe.File(absoluteInputDirectory, fileName,
                                    absoluteOutputDirectory, reKey, out exception);
                    }
                    /// <summary>
                    /// Creates a FILE containing the Denoised and Decrypted content<br/><br/>
                    ///   
                    /// A temporary FILE is used for storing the denoised content<br/>
                    /// It will be deleted in the end based on <paramref name="deleteTempFileAfterUse"/><br/><br/>
                    /// 
                    /// Returns the NAME of the new FILE with the denoised and decrypted content
                    /// </summary>
                    /// <returns>The NAME of the new created file</returns>
                    /// 
                    /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                    /// <param name="fileName">Original FILE NAME</param>
                    /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                    /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                    /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                    static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                        string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
                    {
                        bool didSucceed = Noise.RemoveFromFile(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, out Exception? exception);

                        if (!didSucceed)
                        {
                            if (exception != null && throwExceptions) throw exception;
                            else return (false, "");
                        }
                        if (!didSucceed) return (false, "");

                        (didSucceed, string resultFileName) = RE5.Decrypt.Safe.File(absoluteInputDirectory, fileName,
                                    absoluteOutputDirectory, reKey, out exception);

                        if (exception != null && throwExceptions) throw exception;
                        return (didSucceed, resultFileName);
                    }
                }
            }
        }
    }
}