using System;


using JabrAPI.Template;



namespace JabrAPI
{
    public partial class Noisifier
    {
        public SetHelper Set => _setHelper;


        public class SetHelper
        {
            private readonly Noisifier _noisifier;
            private readonly SensetiveSetHelper _sensitiveSetHelper;

            internal SetHelper(Noisifier noisifier)
            {
                _noisifier = noisifier;
                _sensitiveSetHelper = new(_noisifier);
            }



            public SensetiveSetHelper Sensitive => _sensitiveSetHelper;

            public class SensetiveSetHelper
            {
                private readonly Noisifier _noisifier;
                internal SensetiveSetHelper(Noisifier noisifier) => _noisifier = noisifier;
                

                public void PrimaryNoise(Byte[] primaryNoise) => _noisifier._primaryNoise = primaryNoise;
                public void ComplexNoise(Byte[] complexNoise) => _noisifier._complexNoise = complexNoise;
                public bool SafePrimaryNoise(IReKey reKey, Byte[] primaryNoise)
                {
                    if (!ValidateHelper.PrimaryForReKey(reKey, primaryNoise)) return false;
                    _noisifier._primaryNoise = primaryNoise;
                    return true;
                }
                public bool SafeComplexNoise(IReKey reKey, Byte[] complexNoise)
                {
                    if (!ValidateHelper.ComplexForReKey(reKey, complexNoise)) return false;
                    _noisifier._complexNoise = complexNoise;
                    return true;
                }
            }



            public void Banned(Byte[] banned) => _noisifier._banned = banned;
            public void Default(Byte primaryCount = 8, Byte complexCount = 16)
            {
                DefaultOnlyPrimary(primaryCount);
                DefaultOnlyComplex(complexCount);
            }
            public void Default(Byte[] banned, Byte primaryCount, Byte complexCount)
            {
                Banned(banned);
                Default(primaryCount, complexCount);
            }


            public void DefaultOnlyPrimary(Byte primaryCount = 8)  => _noisifier._primaryCount = primaryCount;
            public void DefaultOnlyComplex(Byte complexCount = 16) => _noisifier._complexCount = complexCount;
        }
    }
}