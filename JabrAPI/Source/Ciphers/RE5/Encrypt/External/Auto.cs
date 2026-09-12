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
                SAFE,
                FAST,
                SAFE_WITH_NOISE_ADDITION,
                FAST_WITH_NOISE_ADDITION,
            }



            static public List<Byte> Data(DataEncryptionMode mode, List<Byte> message,
                ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
            {
                return mode switch
                {
                    DataEncryptionMode.SAFE => RE5.EncryptData.Safe(message, reKey, ref prevId, throwExceptions),
                    DataEncryptionMode.FAST => RE5.EncryptData.Fast(message, reKey, ref prevId),

                    DataEncryptionMode.SAFE_WITH_NOISE_ADDITION
                        => RE5.EncryptData.SafePlusNoise(message, reKey, ref prevId, throwExceptions),
                    DataEncryptionMode.FAST_WITH_NOISE_ADDITION
                        => RE5.EncryptData.FastPlusNoise(message, reKey, ref prevId),
                        _ => []
                };
            }
            static public List<Byte> Data(DataEncryptionMode mode, List<Byte> message, ReKey reKey, bool throwExceptions = false)
            {
                Int32 prevId = 0;
                return Data(mode, message, reKey, ref prevId, throwExceptions);
            }



            static public void File(DataEncryptionMode mode, string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId, bool throwExceptions = false, bool deleteTempFileAfterUse = true)
            {
                switch (mode)
                {
                    case DataEncryptionMode.SAFE:
                        RE5.EncryptFile.Safe(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, ref prevId, throwExceptions); break;
                    case DataEncryptionMode.FAST:
                        RE5.EncryptFile.Fast(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, ref prevId); break;

                    case DataEncryptionMode.SAFE_WITH_NOISE_ADDITION:
                        //RE5.Encrypt.WithNoiseAddition.File(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId, throwExceptions);
                        break;
                    case DataEncryptionMode.FAST_WITH_NOISE_ADDITION:
                        RE5.EncryptFile.FastPlusNoise(absoluteInputDirectory, fileName,
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