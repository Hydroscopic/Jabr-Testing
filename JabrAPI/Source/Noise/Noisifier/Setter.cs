using System;
using System.Collections.Generic;


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

                internal SensetiveSetHelper(Noisifier noisifier)
                {
                    _noisifier = noisifier;
                }



                public void PrNoise(List<Byte> prNoise)
                         => PrimaryNoise(prNoise);
                public void PrimaryNoise(List<Byte> primaryNoise)
                {
                    _noisifier._primaryNoise.Clear();
                    _noisifier._primaryNoise.AddRange(primaryNoise);
                }
                public bool SafePrNoise(IReKey reKey, List<Byte> prNoise)
                         => SafePrimaryNoise(reKey, prNoise);
                public bool SafePrimaryNoise(IReKey reKey, List<Byte> primaryNoise)
                {
                    if (!ValidateHelper.PrimaryForReKey(reKey, primaryNoise)) return false;
                    _noisifier._primaryNoise.Clear();
                    _noisifier._primaryNoise.AddRange(primaryNoise);
                    return true;
                }



                public void CplxNoise(List<Byte> cplxNoise)
                         => ComplexNoise(cplxNoise);
                public void ComplexNoise(List<Byte> complexNoise)
                {
                    _noisifier._complexNoise.Clear();
                    _noisifier._complexNoise.AddRange(complexNoise);
                }
                public bool SafeCplxNoise(IReKey reKey, List<Byte> cplxNoise)
                         => SafeComplexNoise(reKey, cplxNoise);
                public bool SafeComplexNoise(IReKey reKey, List<Byte> complexNoise)
                {
                    if (!ValidateHelper.ComplexForReKey(reKey, complexNoise)) return false;
                    _noisifier._complexNoise.Clear();
                    _noisifier._complexNoise.AddRange(complexNoise);
                    return true;
                }
            }



            public void Default(List<Byte> banned)
            {
                _noisifier._banned.Clear();
                _noisifier._banned.AddRange(banned);
            }
            public void Default(Byte primaryCount = 8, Byte complexCount = 16)
            {
                DefaultOnlyPr(primaryCount);
                DefaultOnlyCplx(complexCount);
            }
            public void Default(List<Byte> banned, Byte primaryCount, Byte complexCount)
            {
                Default(banned);
                Default(primaryCount, complexCount);
            }


            public void DefaultOnlyPr(Byte prCount = 8)
                => DefaultOnlyPrimary(prCount);
            public void DefaultOnlyCplx(Byte cplxCount = 16)
                => DefaultOnlyComplex(cplxCount);

            public void DefaultOnlyPrimary(Byte primaryCount = 8)
                => _noisifier._primaryCount = primaryCount;
            public void DefaultOnlyComplex(Byte complexCount = 16)
                => _noisifier._complexCount = complexCount;
        }
    }
}