using System;
using System.Collections.Generic;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public void CopyFrom(Noisifier otherNoisifier, bool fullCopy = true)
        {
            _primaryNoise = otherNoisifier.PrimaryNoise;
            _complexNoise = otherNoisifier.ComplexNoise;

            if (fullCopy)
                Set.Default
                (
                    otherNoisifier.Banned,
                    otherNoisifier._primaryCount,
                    otherNoisifier._complexCount
                );
        }


        private List<Byte> GetRemainingAllowed(Byte[] banned, Int32 targetCount, bool banAlreadyUsed)
        {
            List<Byte> result = [.. DEFAULT.BYTES];

            Int32 allowedTotal = 256 - banned.Length;
            if (!banAlreadyUsed && allowedTotal < _primaryCount + _complexCount)
            {
                Byte compromiseLeft = (Byte)(allowedTotal / 3);
                _primaryCount = compromiseLeft;
                _complexCount = (Byte)(allowedTotal - compromiseLeft);
            }
            else foreach (Byte bannedByte in _primaryNoise.Length == 0
                ? _complexNoise : _primaryNoise)
                    result.RemoveAll(c => c == bannedByte);

            foreach (Byte bannedByte in banned) result.RemoveAll(c => c == bannedByte);


            _random.Shuffle(ref result);

            Int32 chosenId, curCount = result.Count;
            while (curCount > targetCount)
            {
                chosenId = _random.Next(curCount);

                result.RemoveRange
                (
                    chosenId,
                    Math.Min
                    (
                        curCount - chosenId - 1,
                        _random.Next(1, curCount - targetCount)
                    )
                );

                curCount = result.Count;
            }

            return result;
        }
    }
}