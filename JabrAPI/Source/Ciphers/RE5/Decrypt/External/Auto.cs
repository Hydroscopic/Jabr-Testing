using System;
using System.Collections.Generic;



namespace JabrAPI.RE5
{
    static public partial class Decrypt
    {
        static public class Auto
        {
            public enum DataDecryptionMode
            {
                SAFE,
                FAST,
                SAFE_WITH_NOISE_ADDITION,
                FAST_WITH_NOISE_ADDITION,
            }



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