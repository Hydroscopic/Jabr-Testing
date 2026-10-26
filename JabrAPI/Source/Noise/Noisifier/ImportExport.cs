using System;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public bool ImportFromBinary(Byte[] exportData, bool throwExceptions = false)
        {
            try
            {
                Byte primaryCount = exportData[0];
                if (exportData.Length < primaryCount + 1)
                {
                    if (throwExceptions)
                        throw new ArgumentException
                        (
                            $"Data length is insufficient for the specified primaryNoiseCount" +
                            $" {primaryCount} from exportData[0]",
                            nameof(exportData)
                        );
                    return false;
                }

                _primaryNoise = exportData[1..(primaryCount + 1)];


                Byte complexCount = exportData[primaryCount + 1];
                if (exportData.Length < complexCount + primaryCount + 2)
                {
                    if (throwExceptions)
                        throw new ArgumentException
                        (
                            $"Data length is insufficient for the specified complexNoiseCount" +
                            $" {complexCount} from exportData[{primaryCount + 2}]",
                            nameof(exportData)
                        );
                    return false;
                }

                _complexNoise = exportData[(primaryCount + 2)..(complexCount + primaryCount + 2)];
            }
            catch
            {
                if (throwExceptions) throw;
                return false;
            }
            return true;
        }


        public Byte[] ExportAsBinary()
            => [
                (Byte)_primaryNoise.Length,
                .. _primaryNoise,

                (Byte)_complexNoise.Length,
                .. _complexNoise
            ];
    }
}