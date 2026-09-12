using System;


namespace JabrAPI.RE5
{
    static public partial class EncryptFile
    {
        static public bool Safe(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, ref Int32 prevId, bool throwExceptions = false)
        {
            if (Miscellaneous.IsReKeyValid(reKey, throwExceptions) &&
                Miscellaneous.IsNoisifierValid(reKey.Noisifier, throwExceptions))
            {
                try
                {
                    RE5.EncryptFile.Fast(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId);
                    return true;
                }
                catch { throw; }
            }
            return false;
        }

        static public bool Safe(string absoluteInputDirectory, string fileName,
            string absoluteOutputDirectory, ReKey reKey, bool throwExceptions = false)
        {
            Int32 prevId = 0;
            return Safe(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey, ref prevId, throwExceptions);
        }
    }
}