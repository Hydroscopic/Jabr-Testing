using System;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public void ReGenerate(bool throwExceptions = true)
        {
            try
            {
                GeneratePrimary(false);
                GenerateComplex(true);
            }
            catch { if (throwExceptions) throw; }
        }


        private Byte[] GenerateNoise(Byte targetCount, Byte[] banned, Byte[] otherNoise, bool banAlreadyUsed, bool isPrimary)
        {
            if (targetCount == 0) return [];
            Int32 allowedCount = 256 - banned.Length;
            Byte[] excluded = new Byte[256];

            if (!banAlreadyUsed && allowedCount < _primaryCount + _complexCount)
            {
                Int32 compromiseLeft = allowedCount / 3;
                targetCount = (Byte)(isPrimary ? compromiseLeft : allowedCount - compromiseLeft);
            }
            else if (banAlreadyUsed)
            {
                allowedCount -= otherNoise.Length;
                foreach (Byte b in otherNoise) excluded[b] = 1;
            }

            if (targetCount > allowedCount)
                throw new ArgumentOutOfRangeException(nameof(targetCount),
                    $"Target count {targetCount} exceeds remaining allowed bytes: {allowedCount}");

            Byte[] allowed = new Byte[allowedCount];
            allowedCount = 0;

            foreach (Byte b in banned) excluded[b] = 1;
            for (var b = 0; b < 256; b++)
                if (excluded[b] == 0) allowed[allowedCount++] = (Byte)b;

            _random.Shuffle(ref allowed);
            Array.Resize(ref allowed, targetCount);
            return allowed;
        }


        public void GeneratePrimary(Byte[] banned, bool banAlreadyUsedInComplex)
            => _primaryNoise = GenerateNoise(_primaryCount, banned,
                _complexNoise, banAlreadyUsedInComplex, isPrimary: true);
        public void GeneratePrimary(bool banAlreadyUsedInComplex)
            => GeneratePrimary( _banned, banAlreadyUsedInComplex);

        public void GenerateComplex(Byte[] banned, bool banAlreadyUsedInPrimary)
            => _complexNoise = GenerateNoise(_complexCount, banned,
                _primaryNoise, banAlreadyUsedInPrimary, isPrimary: false);
        public void GenerateComplex(bool banAlreadyUsedInPrimary)
            => GenerateComplex( _banned, banAlreadyUsedInPrimary);
    }
}