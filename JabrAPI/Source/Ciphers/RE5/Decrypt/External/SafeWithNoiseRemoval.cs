using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Decrypt
        {
            static public class WithNoiseRemoval
            {
                static public List<Byte> Data(List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
                {
                    List<Byte> denoised = Noise.RemoveFrom.Data(encrypted, reKey, throwExceptions);
                    return denoised == null || denoised.Count < 1 ? []
                            : RE5.Decrypt.Data(denoised, reKey, throwExceptions);
                }



                static public (bool didSucceed, string resultFileName) File(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true, bool throwExceptions = false)
                {
                    bool didSucceed = Noise.RemoveFrom.File(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, throwExceptions);

                    if (!didSucceed) return (false, "");
                    return RE5.Decrypt.File(absoluteInputDirectory, fileName,
                                absoluteOutputDirectory, reKey, throwExceptions);
                }
            }
        }
    }
}