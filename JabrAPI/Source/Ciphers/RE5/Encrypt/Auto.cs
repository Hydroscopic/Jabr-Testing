using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Encrypt
        {
            /// <summary>
            /// Provides an <b>Alias</b> for choosing an <see cref="RE5.Encrypt"/> <i>encryption overload</i>
            /// </summary>
            static public class Auto
            {
                /// <summary>
                /// <i>Alias</i> for selecting an <b>Encryption overload</b>
                /// </summary>
                public enum DataEncryptionMode
                {
                    /// <summary><b>WITH</b> parameter validation</summary>
                    SAFE,

                    /// <summary><b>WITHOUT</b> parameter validation</summary>
                    FAST,

                    /// <summary><i>Data will be noised anfter encryption</i><b><br/>
                    /// WITH</b> parameter validation</summary>
                    SAFE_WITH_NOISE_ADDITION,

                    /// <summary><i>Data will be noised anfter encryption</i><b><br/>
                    /// WITHOUT</b> parameter validation</summary>
                    FAST_WITH_NOISE_ADDITION,
                }



                /// <summary>
                /// Returns the <b>Encrypted</b> <paramref name="message"/>
                /// </summary>
                /// <returns><b>Encrypted</b> <paramref name="message"/></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="message">secret data</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                static public List<Byte> Data(DataEncryptionMode mode, List<Byte> message, BinaryKey reKey, out Exception? exception)
                {
                    exception = null;
                    return mode switch
                    {
                        DataEncryptionMode.SAFE => RE5.Encrypt.Data(message, reKey, out exception),
                        DataEncryptionMode.FAST => RE5.Encrypt.Fast.Data(message, reKey),

                        DataEncryptionMode.SAFE_WITH_NOISE_ADDITION
                            => RE5.Encrypt.WithNoiseAddition.Data(message, reKey, out exception),
                        DataEncryptionMode.FAST_WITH_NOISE_ADDITION
                            => RE5.Encrypt.FastWithNoiseAddition.Data(message, reKey),
                          _ => []
                    };
                }

                /// <summary>
                /// Returns the <b>Encrypted</b> <paramref name="message"/>
                /// </summary>
                /// <returns><b>Encrypted</b> <paramref name="message"/></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="message">secret data</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                /// <param name="throwExceptions"></param>
                static public List<Byte> Data(DataEncryptionMode mode, List<Byte> message, BinaryKey reKey, bool throwExceptions = false)
                {
                    return mode switch
                    {
                        DataEncryptionMode.SAFE => RE5.Encrypt.Data(message, reKey, throwExceptions),
                        DataEncryptionMode.FAST => RE5.Encrypt.Fast.Data(message, reKey),

                        DataEncryptionMode.SAFE_WITH_NOISE_ADDITION
                            => RE5.Encrypt.WithNoiseAddition.Data(message, reKey, throwExceptions),
                        DataEncryptionMode.FAST_WITH_NOISE_ADDITION
                            => RE5.Encrypt.FastWithNoiseAddition.Data(message, reKey),
                        _ => []
                    };
                }





                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Encrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Encrypted content (if Noising is selected)</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                /// <param name="exception"><see cref="System.Exception"/> if something fails</param>
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public void File(DataEncryptionMode mode, string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, BinaryKey reKey, out Exception? exception, bool deleteTempFileAfterUse = true)
                {
                    exception = null;
                    switch (mode)
                    {
                        case DataEncryptionMode.SAFE: RE5.Encrypt.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, out exception); break;
                        case DataEncryptionMode.FAST: RE5.Encrypt.Fast.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;

                        case DataEncryptionMode.SAFE_WITH_NOISE_ADDITION:
                            //RE5.Encrypt.WithNoiseAddition.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, out exception);
                            break;
                        case DataEncryptionMode.FAST_WITH_NOISE_ADDITION:
                            RE5.Encrypt.FastWithNoiseAddition.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;
                        default: break;
                    };
                }

                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Encrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Encrypted content (if Noising is selected)</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Encrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="mode">Alias to chosen overload</param>
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                /// <param name="throwExceptions"></param>
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public void File(DataEncryptionMode mode, string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, BinaryKey reKey, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
                {
                    switch (mode)
                    {
                        case DataEncryptionMode.SAFE:
                            RE5.Encrypt.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, throwExceptions); break;
                        case DataEncryptionMode.FAST:
                            RE5.Encrypt.Fast.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;

                        case DataEncryptionMode.SAFE_WITH_NOISE_ADDITION:
                            //RE5.Encrypt.WithNoiseAddition.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, throwExceptions);
                            break;
                        case DataEncryptionMode.FAST_WITH_NOISE_ADDITION:
                            RE5.Encrypt.FastWithNoiseAddition.File(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey); break;
                        default: break;
                    };
                }
            }
        }
    }
}