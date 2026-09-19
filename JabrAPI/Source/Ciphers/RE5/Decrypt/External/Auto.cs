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
                FAST,
                WITH_VALIDATION,
                FAST_WITH_DENOISING,
                WITH_VALIDATITON_AND_DENOISING,
            }



            static public List<Byte> Data(DataDecryptionMode mode, List<Byte> encrypted, ReKey reKey, bool throwExceptions = false)
            {
                return mode switch
                {
                    DataDecryptionMode.FAST
                        => RE5.DecryptData.Fast(encrypted, reKey),
                    DataDecryptionMode.WITH_VALIDATION
                        => RE5.DecryptData.WithValidation(encrypted, reKey, throwExceptions),

                    DataDecryptionMode.FAST_WITH_DENOISING
                        => RE5.DecryptData.FastWithDeNoising(encrypted, reKey),
                    DataDecryptionMode.WITH_VALIDATITON_AND_DENOISING
                        => RE5.DecryptData.WithValidationAndDeNoising(encrypted, reKey, throwExceptions),
                      _ => []
                };
            }



            static public void File(DataDecryptionMode mode, string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
            {
                switch (mode)
                {
                    case DataDecryptionMode.FAST:
                        RE5.DecryptFile.Fast(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey); break;
                    case DataDecryptionMode.WITH_VALIDATION:
                        RE5.DecryptFile.WithValidation(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, throwExceptions); break;

                    case DataDecryptionMode.FAST_WITH_DENOISING:
                        RE5.DecryptFile.FastWithDeNoising(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey); break;
                    case DataDecryptionMode.WITH_VALIDATITON_AND_DENOISING:
                        //RE5.DecryptFile.WithValidationAndDenoising(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, throwExceptions);
                        break;
                    default: break;
                };
            }
        }
    }
}