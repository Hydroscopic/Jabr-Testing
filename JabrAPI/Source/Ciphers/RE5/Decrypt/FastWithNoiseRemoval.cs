using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Decrypt
        {
            /// <summary>
            /// These variants <b>DO NOT validate the parameters!</b> (Fast variants)<br/>
            /// Denoising <see cref="Noise"/> information before Decrypting <see cref="RE5.Decrypt"/> it
            /// </summary>
            static public class FastWithNoiseRemoval
            {
                /// <summary>
                /// Returns the <b>Denoised</b> and <b>Decrypted</b> data of <paramref name="encrypted"/>
                /// </summary>
                /// <returns><b>Denoised</b> and <b>Decrypted</b> data of <paramref name="encrypted"/></returns>
                /// 
                /// <param name="encrypted">Obfuscated data</param>
                /// <param name="reKey">RE5 Encryption key for denoising and deciphering</param>
                static public List<Byte> Data(List<Byte> encrypted, ReKey reKey)
                {
                    List<Byte> denoised = Noise.FastRemove(encrypted, reKey.Noisifier);
                    return denoised == null || denoised.Count < 1 ? []
                        : RE5.Decrypt.Fast.Data(denoised, reKey);
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
                /// <param name="deleteTempFileAfterUse">Whether the Temporary FILE will be deleted at the end</param>
                static public string File(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true)
                {
                    string denoisedFileName = Noise.FastRemoveFromFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                    string resultFileName = RE5.Decrypt.Fast.File(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey);
                    if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
                    return resultFileName;
                }
            }
        }
    }
}