using System;
using System.Linq;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Encrypt
        {
            /// <summary>
            /// These variants <b>DO NOT validate the parameters!</b> (Fast variants)<br/>
            /// Noising <see cref="Noise"/> information before Encrypting <see cref="RE5.Encrypt"/> it
            /// </summary>
            static public class FastWithNoiseAddition
            {
                /// <summary>
                /// Returns the <b>Noised</b> and <b>Encrypted</b> <paramref name="message"/>
                /// </summary>
                /// <returns><b>Noised</b> and <b>Encrypted</b> <paramref name="message"/></returns>
                /// 
                /// <param name="message">secret data</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                static public List<Byte> Data(List<Byte> message, ReKey reKey)
                {
                    List<Byte> result = RE5.Encrypt.Fast.Data(message, reKey);
                    return result == null || result.Count < 1 ? []
                            : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
                }



                /// <summary>
                /// Creates a <b>FILE</b> containing the <b>Noised</b> and <b>Encrypted</b> content<br/><br/>
                ///   
                /// <i>A temporary FILE is used for storing the Denoised content</i><br/>
                /// It will be <b>deleted</b> in the end <b>based on <paramref name="deleteTempFileAfterUse"/></b><br/><br/>
                /// 
                /// Returns the <i>NAME</i> of the new <b>Noised and Encrypted <i>FILE</i></b>
                /// </summary>
                /// <returns>The <i>NAME</i> of the new <b>Noised and Encrypted <i>FILE</i></b></returns>
                /// 
                /// <param name="absoluteInputDirectory">PATH to the original FILE</param>
                /// <param name="fileName">Original FILE NAME</param>
                /// <param name="absoluteOutputDirectory">Path where the temporary and output FILE will be stored</param>
                /// <param name="reKey">RE5 Encryption key for noising and enciphering</param>
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public string File(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true)
                {
                    //Noise.FastAddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                    //string resultFileName = RE5.Encrypt.Fast.File(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey);
                    //if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
                    return "";
                }
            }
        }
    }
}