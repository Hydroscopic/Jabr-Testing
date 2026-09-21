using System;
using System.Collections.Generic;



namespace JabrAPI
{
    static public partial class RE5
    {
        public partial class ReKey
        {
            public class ReKeyUtil_IsValid
            {
                internal ReKeyUtil_IsValid(ReKey thisKey) { _thisKey = thisKey; }
                private readonly ReKey _thisKey;



                public ReKeyUtil_PartiallyValid Partially
                {
                    get => field ??= new ReKeyUtil_PartiallyValid(_thisKey);
                    private set;
                } = null;

                public class ReKeyUtil_PartiallyValid
                {
                    internal ReKeyUtil_PartiallyValid(ReKey thisKey) { _thisKey = thisKey; }
                    private readonly ReKey _thisKey;

                    public bool PrAlphabet(bool throwExceptions = false)
                        => PrALphabet(_thisKey.PrAlphabet, throwExceptions);
                    static public bool PrALphabet(List<Byte> primary, bool throwExceptions = false)
                    {
                        if (primary == null || primary.Count < 2 || primary.Count > 256)
                        {
                            if (!throwExceptions) return false;
                            throw new ArgumentException
                            (
                                "Primary alphabet is not set, too short or too long",
                                nameof(primary)
                            );
                        }
                        for (var curId = 0; curId < primary.Count; curId++)
                        {
                            for (var id2 = curId + 1; id2 < primary.Count; id2++)
                            {
                                if (primary[curId] == primary[id2])
                                {
                                    if (!throwExceptions) return false;
                                    throw new ArgumentException
                                    (
                                        $"Primary alphabet contains duplicates characters" +
                                        $"\nDuplicate byte: {primary[curId]}",
                                        nameof(primary)
                                    );
                                }
                            }
                        }

                        return true;
                    }


                    public bool ExAlphabet(bool throwExceptions = false)
                        => ExAlphabet(_thisKey.ExAlphabet, throwExceptions);
                    static public bool ExAlphabet(List<Byte> external, bool throwExceptions = false)
                    {
                        if (external == null || external.Count < 2 || external.Count > 256)
                        {
                            if (!throwExceptions) return false;
                            throw new ArgumentException
                            (
                                "External alphabet is not set, too short or too long",
                                nameof(external)
                            );
                        }
                        for (var curId = 0; curId < external.Count; curId++)
                        {
                            for (var id2 = curId + 1; id2 < external.Count; id2++)
                            {
                                if (external[curId] == external[id2])
                                {
                                    if (!throwExceptions) return false;
                                    throw new ArgumentException
                                    (
                                        "External alphabet contains duplicates characters" +
                                        $"Duplicate byte: {external[curId]}",
                                        nameof(external)
                                    );
                                }
                            }
                        }

                        return true;
                    }
                }



                public bool ForEncryption(List<Byte> message, bool throwExceptions = false)
                {
                    return ReKeyUtil_PartiallyValid.ExAlphabet(_thisKey.ExAlphabet, throwExceptions)
                                && PrAlphabet(message, _thisKey.PrAlphabet, throwExceptions);
                }
                public bool ForDecryption(List<Byte> message, bool throwExceptions = false)
                {
                    return ReKeyUtil_PartiallyValid.PrALphabet(_thisKey.PrAlphabet, throwExceptions)
                                && ExAlphabet(message, _thisKey.ExAlphabet, throwExceptions);
                }


                public bool PrAlphabet(List<Byte> message, bool throwExceptions = false)
                    => PrAlphabet(message, _thisKey.PrAlphabet, throwExceptions);
                static public bool PrAlphabet(List<Byte> message, List<Byte> primary, bool throwExceptions = false)
                {
                    if (!ReKeyUtil_PartiallyValid.PrALphabet(primary, throwExceptions)) return false;

                    foreach (Byte b in message)
                    {
                        if (!primary.Contains(b))
                        {
                            if (!throwExceptions) return false;
                            throw new ArgumentException
                            (
                                $"Message contains bytes not present in the primary alphabet" +
                                $"\nMissing byte: {b}",
                                nameof(primary)
                            );
                        }
                    }
                    return true;
                }

                
                public bool ExAlphabet(List<Byte> encrypted, bool throwExceptions = false)
                    => ExAlphabet(encrypted, _thisKey.ExAlphabet, throwExceptions);
                static public bool ExAlphabet(List<Byte> encrypted, List<Byte> external, bool throwExceptions = false)
                {
                    if (!ReKeyUtil_PartiallyValid.ExAlphabet(external, throwExceptions)) return false;

                    foreach (Byte b in encrypted)
                    {
                        if (!external.Contains(b))
                        {
                            if (throwExceptions) return false;
                            throw new ArgumentException
                            (
                                $"Message contains bytes not present in the external alphabet" +
                                $"\nMissing byte: {b}",
                                nameof(external)
                            );
                        }
                    }
                    return true;
                }
            }
        }
    }
}