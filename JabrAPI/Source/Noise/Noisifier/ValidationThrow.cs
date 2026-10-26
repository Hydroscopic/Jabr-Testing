using System;


using JabrAPI.Template;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public partial class ValidateHelper
        {
            public bool ForMessage(Byte[] message, bool throwExceptions = false)
                     => ComplexForMessage(message, throwExceptions)
                     && PrimaryForMessage(message, throwExceptions);
            public bool ForReKey(IReKey reKey, bool throwExceptions = false)
                     => ComplexForReKey(reKey, throwExceptions)
                     && PrimaryForReKey(reKey, throwExceptions);
            public bool ForMessageAndReKey(IReKey reKey, Byte[] message, bool throwExceptions = false)
                     => ForMessage(message, throwExceptions)
                     && ForReKey(reKey, throwExceptions);



            static public bool PrimaryForReKey(IReKey reKey, Byte[] primaryNoise, bool throwExceptions = false)
                => PrimaryForReKey(reKey.FinalAlphabet.ExportDirect, primaryNoise, throwExceptions);
            static public bool PrimaryForReKey(Byte[] exAlphabet, Byte[] primaryNoise, bool throwExceptions = false)
            {
                return IsNoiseValid
                (
                    exAlphabet,
                    primaryNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with ExternalAlphabet chars",
                        nameof(primaryNoise)
                    ),
                    throwExceptions
                );
            }
            static public bool PrimaryForMessage(Byte[] message, Byte[] primaryNoise, bool throwExceptions = false)
            {
                return IsNoiseValid
                (
                    message,
                    primaryNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with message chars",
                        nameof(primaryNoise)
                    ),
                    throwExceptions
                );
            }


            public bool PrimaryForReKey(IReKey reKey, bool throwExceptions = false)
                => PrimaryForReKey(reKey.FinalAlphabet.ExportDirect, _noisifier._primaryNoise, throwExceptions);
            public bool PrimaryForMessage(Byte[] message, bool throwExceptions = false)
                => PrimaryForMessage(message, _noisifier._primaryNoise, throwExceptions);


            static public bool ComplexForReKey(IReKey reKey, Byte[] complexNoise, bool throwExceptions = false)
                => ComplexForReKey(reKey.FinalAlphabet.ExportDirect, complexNoise, throwExceptions);
            static public bool ComplexForReKey(Byte[] exAlphabet, Byte[] complexNoise, bool throwExceptions = false)
            {
                return IsNoiseValid
                (
                    exAlphabet,
                    complexNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with ExternalAlphabet chars",
                        nameof(complexNoise)
                    ),
                    throwExceptions
                );
            }
            static public bool ComplexForMessage(Byte[] message, Byte[] complexNoise, bool throwExceptions = false)
            {
                return IsNoiseValid
                (
                    message,
                    complexNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with message chars",
                        nameof(complexNoise)
                    ),
                    throwExceptions
                );
            }


            public bool ComplexForReKey(IReKey reKey, bool throwExceptions = false)
                => ComplexForReKey(reKey.FinalAlphabet.ExportDirect, _noisifier._complexNoise, throwExceptions);
            public bool ComplexForMessage(Byte[] message, bool throwExceptions = false)
                => ComplexForMessage(message, _noisifier._complexNoise, throwExceptions);



            static private bool IsNoiseValid(Byte[] exAlphabet_or_message, Byte[] primaryNoise_or_complexNoise,
                ArgumentException errorMessage, bool throwExceptions = false)
            {
                foreach (Byte noiseByte in primaryNoise_or_complexNoise)
                {
                    if (exAlphabet_or_message.Contains(noiseByte))
                    {
                        if (throwExceptions) throw new ArgumentException
                            (
                                errorMessage.Message +
                                $"\nDuplicate Byte: {noiseByte}",
                                errorMessage.ParamName
                            );
                        return false;
                    }
                }
                return true;
            }
        }
    }
}