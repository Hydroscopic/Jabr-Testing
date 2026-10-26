using System;
using System.Linq;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public Int32 PrimaryNoiseCount => _primaryNoise.Length;
        public Int32 ComplexNoiseCount => _complexNoise.Length;

        public Byte[] PrimaryNoise => _primaryNoise;
        public Byte[] ComplexNoise => _complexNoise;
        public Byte[] Banned => _banned;

        public Byte RandomPrimaryByte => _primaryNoise[_random.Next(PrimaryNoiseCount)];
        public Byte RandomComplexByte => _complexNoise[_random.Next(ComplexNoiseCount)];


        public UInt64 RandomReseedInterval
        {
            get;
            set
            {
                field = value >= 128 ? value : 128;
                _random.UpdateReseedInterval(field);
            }
        }


        public Byte[] RandomPrimarySequence(Int32 count) =>
            [..
                Enumerable.Range(0, count).Select
                (
                    _ => _primaryNoise[_random.Next(PrimaryNoiseCount)]
                )
            ];
        public Byte[] RandomComplexSequence(Int32 count) =>
            [..
                Enumerable.Range(0, count).Select
                (
                    _ => _complexNoise[_random.Next(ComplexNoiseCount)]
                )
            ];
    }
}