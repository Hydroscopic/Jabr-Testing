using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Decrypt
        {
            /// <summary>
            /// Provides an <b>Alias</b> for choosing a <see cref="RE5.Decrypt"/> <i>decryption overload</i>
            /// </summary>
            static public class Auto
            {
                /// <summary>
                /// <i>Alias</i> for selecting a <b>Decryption overload</b>
                /// </summary>
                public enum DataDecryptionMode
                {
                    /// <summary><b>WITH</b> parameter validation</summary>
                    SAFE,

                    /// <summary><b>WITHOUT</b> parameter validation</summary>
                    FAST,

                    /// <summary><i>Data will be denoised before decryption</i><b><br/>
                    /// WITH</b> parameter validation</summary>
                    SAFE_WITH_NOISE_ADDITION,

                    /// <summary><i>Data will be denoised before decryption</i><b><br/>
                    /// WITHOUT</b> parameter validation</summary>
                    FAST_WITH_NOISE_ADDITION,
                }



                /// <summary>
                /// Returns the <b>Decrypted</b> data of <paramref name="encrypted"/>
                /// </summary>
                /// <returns><b>Decrypted</b> data of <paramref name="encrypted"/></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="encrypted">Obfuscated data</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                static public List<Byte> Data(DataDecryptionMode mode, List<Byte> encrypted, ReKey reKey, out Exception? exception)
                {
                    exception = null;
                    return mode switch
                    {
                        DataDecryptionMode.SAFE => RE5.Decrypt.Data(encrypted, reKey, out exception),
                        DataDecryptionMode.FAST => RE5.Decrypt.Fast.Data(encrypted, reKey),

                        DataDecryptionMode.SAFE_WITH_NOISE_ADDITION
                            => RE5.Decrypt.WithNoiseRemoval.Data(encrypted, reKey, out exception),
                        DataDecryptionMode.FAST_WITH_NOISE_ADDITION
                            => RE5.Decrypt.FastWithNoiseRemoval.Data(encrypted, reKey),
                        _ => []
                    };
                }

                /// <summary>
                /// Returns the <b>Decrypted</b> data of <paramref name="encrypted"/>
                /// </summary>
                /// <returns><b>Decrypted</b> data of <paramref name="encrypted"/></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="encrypted">Obfuscated data</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="throwExceptions"></param>
                static public List<Byte> Data(DataDecryptionMode mode, List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
                {
                    return mode switch
                    {
                        DataDecryptionMode.SAFE => RE5.Decrypt.Data(encrypted, reKey, throwExceptions),
                        DataDecryptionMode.FAST => RE5.Decrypt.Fast.Data(encrypted, reKey),

                        DataDecryptionMode.SAFE_WITH_NOISE_ADDITION
                            => RE5.Decrypt.WithNoiseRemoval.Data(encrypted, reKey, throwExceptions),
                        DataDecryptionMode.FAST_WITH_NOISE_ADDITION
                            => RE5.Decrypt.FastWithNoiseRemoval.Data(encrypted, reKey),
                        _ => []
                    };
                }





                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Decrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Denoised content (if Denoising is selected)</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public void File(DataDecryptionMode mode, string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, out Exception? exception, bool deleteTempFileAfterUse = true)
                {
                    exception = null;
                    switch (mode)
                    {
                        case DataDecryptionMode.SAFE:
                            RE5.Decrypt.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, out exception); break;
                        case DataDecryptionMode.FAST:
                            RE5.Decrypt.Fast.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;

                        case DataDecryptionMode.SAFE_WITH_NOISE_ADDITION:
                            //RE5.Decrypt.WithNoiseRemoval.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out exception);
                            break;
                        case DataDecryptionMode.FAST_WITH_NOISE_ADDITION:
                            RE5.Decrypt.FastWithNoiseRemoval.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;
                        default: break;
                    };
                }

                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Decrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Denoised content (if Denoising is selected)</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Decrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                /// <param name="throwExceptions"></param>
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public void File(DataDecryptionMode mode, string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
                {
                    switch (mode)
                    {
                        case DataDecryptionMode.SAFE:
                            RE5.Decrypt.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, throwExceptions); break;
                        case DataDecryptionMode.FAST:
                            RE5.Decrypt.Fast.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;

                        case DataDecryptionMode.SAFE_WITH_NOISE_ADDITION:
                            //RE5.Decrypt.WithNoiseRemoval.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, throwExceptions);
                            break;
                        case DataDecryptionMode.FAST_WITH_NOISE_ADDITION:
                            RE5.Decrypt.FastWithNoiseRemoval.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;
                        default: break;
                    };
                }
            }
        }
    }
}