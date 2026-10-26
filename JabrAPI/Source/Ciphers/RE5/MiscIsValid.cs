using System;


using JabrAPI.Template;



namespace JabrAPI
{
    static internal partial class Miscellaneous
    {
        static internal bool IsMessageValid(Byte[] message, bool throwExceptions = false)
        {
            if (message == null || message.Length < 1)
            {
                if (throwExceptions)
                    throw new ArgumentException
                    (
                        "Message is invalid - cannot be null or empty",
                        nameof(message)
                    );
                return false;
            }
            return true;
        }
        static internal bool IsNoisifierValid(Noisifier noisifier, bool throwExceptions = false)
        {
            if (noisifier == null)
            {
                if (throwExceptions)
                    throw new ArgumentException
                    (
                        "BinaryNoisifier is undefined (null)",
                        nameof(noisifier)
                    );
                return false;
            }
            return true;
        }
        static internal bool IsReKeyValid(IReKey reKey, bool throwExceptions = false)
        {
            if (reKey == null)
            {
                if (throwExceptions)
                    throw new ArgumentException
                    (
                        "Encryption key is undefined (null)",
                        nameof(reKey)
                    );
                return false;
            }
            return true;
        }


        static internal bool IsMessageAndNoisifierValid(
            Byte[] message, Noisifier noisifier, bool throwExceptions = false)
        {
            bool isValid = IsMessageValid(message, throwExceptions);
            if (isValid) isValid = IsNoisifierValid(noisifier, throwExceptions);
            return isValid;
        }
        static internal bool IsMessageAndReKeyValid(
            Byte[] message, IReKey binKey, bool throwExceptions = false)
        {
            bool isValid = IsMessageValid(message, throwExceptions);
            if  (isValid) isValid = IsReKeyValid(binKey, throwExceptions);
            return isValid;
        }
        static internal bool IsMessageAndReKeyAndNoisifierValid(
            Byte[] message, IReKey reKey, bool throwExceptions = false)
        {
            bool isValid = IsReKeyValid(reKey, throwExceptions);
            if (isValid) isValid = IsMessageAndNoisifierValid(message, reKey.Noisifier, throwExceptions);
            return isValid;
        }
    }
}