using JabrAPI.Template;
using static JabrAPI.Miscellaneous;



namespace JabrAPI
{
    static public partial class Noise
    {
        static public partial class AddToFile
        {
            static public bool Safe(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, IReKey reKey, bool throwExceptions = true)
            {
                if (IsReKeyValid(reKey, throwExceptions) &&
                    IsNoisifierValid(reKey.Noisifier, throwExceptions))
                {
                    try
                    {
                        Noise.AddToFile.Fast(absoluteInputDirectory, fileName, absoluteOutputDirectory, reKey.Noisifier);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }
            static public bool Safe(string absoluteInputDirectory, string fileName,
                string absoluteOutputDirectory, Noisifier noisifier, bool throwExceptions = true)
            {
                if (IsNoisifierValid(noisifier, throwExceptions))
                {
                    try
                    {
                        Noise.AddToFile.Fast(absoluteInputDirectory, fileName, absoluteOutputDirectory, noisifier);
                        return true;
                    }
                    catch { throw; }
                }
                return false;
            }
        }
    }
}