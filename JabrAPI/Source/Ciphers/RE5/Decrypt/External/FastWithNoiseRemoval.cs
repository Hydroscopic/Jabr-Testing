using System;
using System.Collections.Generic;



namespace JabrAPI.RE5
{
    static public partial class Decrypt
    {
        static public class FastWithNoiseRemoval
        {
            static public List<Byte> Data(List<Byte> encrypted, ReKey reKey)
            {
                List<Byte> denoised = Noise.RemoveFrom.Data(encrypted, reKey.Noisifier);
                return denoised == null || denoised.Count < 1 ? []
                    : RE5.Decrypt.Fast.Data(denoised, reKey);
            }



            static public string File(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true)
            {
                string denoisedFileName = Noise.RemoveFrom.FastFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                string resultFileName = RE5.Decrypt.Fast.File(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey);
                if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
                return resultFileName;
            }
        }
    }
}