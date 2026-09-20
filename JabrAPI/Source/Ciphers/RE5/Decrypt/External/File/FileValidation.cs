using System;



namespace JabrAPI
{
    static public partial class RE5
    {
        static public partial class DecryptFile
        {
            static public (string resultFileName, bool didSucceed) WithValidation(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false)
            {
                if (Miscellaneous.IsReKeyValid(reKey, throwExceptions) &&
                    Miscellaneous.IsNoisifierValid(reKey.Noisifier, throwExceptions))
                {
                    try
                    {
                        return (DecryptFile.Fast(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey), true);
                    }
                    catch { throw; }
                }
                return ("", false);
            }



            static public (string resultFileName, bool didSucceed) WithValidationAndDeNoising(string absoluteInputDirectory, string fileName,
                    string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true, bool throwExceptions = false)
            {
                bool didSucceed = Noise.RemoveFrom.File(absoluteInputDirectory, fileName,
                    absoluteOutputDirectory, reKey, throwExceptions);

                if (!didSucceed) return ("", false);
                return DecryptFile.WithValidation(absoluteInputDirectory, fileName,
                            absoluteOutputDirectory, reKey, throwExceptions);
            }
        }
    }
}