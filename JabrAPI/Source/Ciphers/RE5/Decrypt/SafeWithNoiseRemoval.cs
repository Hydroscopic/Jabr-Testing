using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Decrypt
        {
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
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                static public List<Byte> Data(List<Byte> encrypted, BinaryKey reKey, out Exception? exception)
                {
                    List<Byte> denoised = Noise.Remove(encrypted, reKey, out exception);
                    return denoised == null || denoised.Count < 1 ? []
                            : RE5.Decrypt.Data(denoised, reKey, out exception);
                }

                /// <summary>
                /// Returns the <b>Decrypted</b> and <b>Denoised</b> data of <paramref name="encrypted"/>
                /// </summary>
                /// <returns><b>Decrypted</b> and <b>Denoised</b> data of <paramref name="encrypted"/></returns>
                /// 
                /// <param name="encrypted">Obfuscated data</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="throwExceptions"></param>
                static public List<Byte> Data(List<Byte> encrypted, BinaryKey reKey, bool throwExceptions = false)
                {
                    List<Byte> denoised = Noise.Remove(encrypted, reKey, throwExceptions);
                    return denoised == null || denoised.Count < 1 ? []
                            : RE5.Decrypt.Data(denoised, reKey, throwExceptions);
                }



                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Denoised</b> and <b>Decrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Denoised content</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Denoised and Decrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Denoised and Decrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception, bool deleteTempFileAfterUse = true)
                {
                    bool didSucceed = Noise.RemoveFromFile(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, out exception);

                    if (!didSucceed) return (false, "");
                    return RE5.Decrypt.File(absoluteInputDirectory, fileName,
                                absoluteOutputDirectory, reKey, out exception);
                }

                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Denoised</b> and <b>Decrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Denoised content</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Denoised and Decrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Denoised and Decrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="throwExceptions"></param>
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

                    (didSucceed, string resultFileName) = RE5.Decrypt.File(absoluteInputDirectory, fileName,
                                absoluteOutputDirectory, reKey, out exception);

                    if (exception != null && throwExceptions) throw exception;
                    return (didSucceed, resultFileName);
                }
            }
        }
    }
}