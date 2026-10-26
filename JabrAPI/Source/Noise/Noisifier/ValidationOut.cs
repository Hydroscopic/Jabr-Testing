using System;


using JabrAPI.Template;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public ValidateHelper IsValid => _validateHelper;



        public partial class ValidateHelper
        {
            private readonly Noisifier _noisifier;
            internal ValidateHelper(Noisifier noisifier) => _noisifier = noisifier;



            static public bool PrimaryForReKey(Byte[] exAlphabet, Byte[] primaryNoise, out Exception? exception)
            {
                return IsNoiseValid
                (
                    exAlphabet,
                    primaryNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with ExternalAlphabet bytes",
                        nameof(primaryNoise)
                    ),
                    out exception
                );
            }
            static public bool PrimaryForMessage(Byte[] message, Byte[] primaryNoise, out Exception? exception)
            {
                return IsNoiseValid
                (
                    message,
                    primaryNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with message bytes",
                        nameof(primaryNoise)
                    ),
                    out exception
                );
            }

            public bool PrimaryForReKey(IReKey reKey, out Exception? exception)
                => PrimaryForReKey(reKey.FinalAlphabet.ExportDirect, _noisifier._primaryNoise, out exception);
            public bool PrimaryForMessage(Byte[]  message, out Exception? exception)
                => PrimaryForMessage(message, _noisifier._primaryNoise, out exception);

            static public bool ComplexForReKey(Byte[] exAlphabet, Byte[] complexNoise, out Exception? exception)
            {
                return IsNoiseValid
                (
                    exAlphabet,
                    complexNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with ExternalAlphabet bytes",
                        nameof(complexNoise)
                    ),
                    out exception
                );
            }
            static public bool ComplexForMessage(Byte[]  message, Byte[] complexNoise, out Exception? exception)
            {
                return IsNoiseValid
                (
                    message,
                    complexNoise,
                    new ArgumentException
                    (
                        $"PrimaryNoise bytes can not overlap with message bytes",
                        nameof(complexNoise)
                    ),
                    out exception
                );
            }


            static private bool IsNoiseValid(Byte[] exAlphabet_or_message, Byte[] primaryNoise_or_complexNoise,
                ArgumentException errorMessage, out Exception? exception)
            {
                foreach (Byte noiseByte in primaryNoise_or_complexNoise)
                {
                    if (exAlphabet_or_message.Contains(noiseByte))
                    {
                        exception = new ArgumentException
                            (
                                errorMessage.Message +
                                $"\nDuplicate Byte: {noiseByte}",
                                errorMessage.ParamName
                            );
                        return false;
                    }
                }
                exception = null;
                return true;
            }
        }
    }
}