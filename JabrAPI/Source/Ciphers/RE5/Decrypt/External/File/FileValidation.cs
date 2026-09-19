using System;



namespace JabrAPI.RE5
{
    static public partial class DecryptFile
    {
        static public (bool didSucceed, string resultFileName) WithValidation(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false)
        {
            if (Miscellaneous.IsReKeyValid(reKey, throwExceptions) &&
                Miscellaneous.IsNoisifierValid(reKey.Noisifier, throwExceptions))
            {
                try
                {
                    return (true, RE5.DecryptFile.Fast(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey));
                }
                catch { throw; }
            }
            return (false, "");
        }



        static public (bool didSucceed, string resultFileName) WithValidationAndDeNoising(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, ReKey reKey, bool deleteTempFileAfterUse = true, bool throwExceptions = false)
        {
            bool didSucceed = Noise.RemoveFrom.File(absoluteInputDirectory, fileName,
                absoluteOutputDirectory, reKey, throwExceptions);

            if (!didSucceed) return (false, "");
            return RE5.DecryptFile.WithValidation(absoluteInputDirectory, fileName,
                        absoluteOutputDirectory, reKey, throwExceptions);
        }
    }
}