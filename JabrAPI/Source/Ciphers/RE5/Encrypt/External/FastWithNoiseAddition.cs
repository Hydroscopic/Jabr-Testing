using System;
using System.Linq;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class Encrypt
        {
            static public class FastWithNoiseAddition
            {
                static public List<Byte> Data(List<Byte> message, ReKey reKey, ref Int32 prevId)
                {
                    List<Byte> result = RE5.Encrypt.Fast.Data(message, reKey, ref prevId);
                    return result == null || result.Count < 1 ? []
                            : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
                }
                static public List<Byte> Data(List<Byte> message, ReKey reKey)
                {
                    List<Byte> result = RE5.Encrypt.Fast.Data(message, reKey);
                    return result == null || result.Count < 1 ? []
                            : Noise.AddTo.FastData(result, reKey.Noisifier, [.. message.Distinct()]);
                }



                static public string File(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId, bool deleteTempFileAfterUse = true)
                {
                    //Noise.FastAddToFile(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier, prevId);
                    //string resultFileName = RE5.Encrypt.Fast.File(absoluteOutputDirectory, denoisedFileName, absoluteOutputDirectory, reKey, prevId);
                    //if (deleteTempFileAfterUse) System.IO.File.Delete(System.IO.Path.Combine(absoluteOutputDirectory, denoisedFileName));
                    return "";
                }
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