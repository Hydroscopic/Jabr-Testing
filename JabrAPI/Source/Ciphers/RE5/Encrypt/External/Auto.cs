using System;
using System.Collections.Generic;



namespace JabrAPI.RE5
{
    static public partial class Encrypt
    {
        static public class Auto
        {
            public enum DataEncryptionMode
            {
                FAST,
                WITH_VALIDATION,
                FAST_WITH_NOISING,
                WITH_VALIDATION_AND_NOISING,
            }



            static public (List<Byte> result, bool didSucceed) Data(DataEncryptionMode mode, List<Byte> message,
                ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
            {
                return mode switch
                {
                    DataEncryptionMode.FAST
                        => (RE5.EncryptData.Fast(message, reKey, ref prevId), true),
                    DataEncryptionMode.WITH_VALIDATION
                        =>  RE5.EncryptData.WithValidation(message, reKey, ref prevId, throwExceptions),
                    
                    DataEncryptionMode.FAST_WITH_NOISING
                        => (RE5.EncryptData.FastWithNoising(message, reKey, ref prevId), true),
                    DataEncryptionMode.WITH_VALIDATION_AND_NOISING
                        =>  RE5.EncryptData.WithValidationAndNoising(message, reKey, ref prevId, throwExceptions),
                    _   => ([], false)
                };
            }
            static public (List<Byte> result, bool didSucceed) Data(DataEncryptionMode mode, List<Byte> message, ReKey reKey, bool throwExceptions = false)
            {
                Int32 prevId = 0;
                return Data(mode, message, reKey, ref prevId, throwExceptions);
            }



            static public void File(DataEncryptionMode mode, string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
            {
                switch (mode)
                {
                    case DataEncryptionMode.WITH_VALIDATION:
                        RE5.EncryptFile.WithValidation(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, ref prevId, throwExceptions); break;
                    case DataEncryptionMode.FAST:
                        RE5.EncryptFile.Fast(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, ref prevId); break;

                    case DataEncryptionMode.WITH_VALIDATION_AND_NOISING:
                        //RE5.Encrypt.WithNoiseAddition.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId, throwExceptions);
                        break;
                    case DataEncryptionMode.FAST_WITH_NOISING:
                        RE5.EncryptFile.FastWithNoising(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, ref prevId); break;
                    default: break;
                };
            }
            static public void File(DataEncryptionMode mode, string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
            {
                Int32 prevId = 0;
                File(mode, absoluteInputDirectory, fileName, absoluteOutputDirectory,
                    reKey, ref prevId, throwExceptions, deleteTempFileAfterUse);
            }
        }
    }
}