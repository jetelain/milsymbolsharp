using System;
using System.Linq;

namespace Milsymbol.App6d
{
    public class App6dSymbolId : IApp6dSymbolId, ISymbolId
    {
        private readonly string _sidc;

        public App6dSymbolId(string sidc)
        {
            if (sidc == null || sidc.Length != 20 || !IsNumeric(sidc))
            {
                throw new ArgumentException($"'{sidc}' is not a valid APP-6D SIDC, length must be 20", nameof(sidc));
            }
            _sidc = sidc;
        }

        public string Version => _sidc.Substring(0, 2);

        public App6dStandardIdentity1 StandardIdentity1 => (App6dStandardIdentity1)(_sidc[2] - '0');

        public App6dStandardIdentity2 StandardIdentity2 => (App6dStandardIdentity2)(_sidc[3] - '0');

        public string SymbolSet => _sidc.Substring(4, 2);

        public App6dStatus Status => (App6dStatus)(_sidc[6] - '0');

        public App6dFdHqTf FdHqTf => (App6dFdHqTf)(_sidc[7] - '0');

        public string Amplifier => _sidc.Substring(8, 2);

        public string Icon => _sidc.Substring(10, 6);

        public string Modifier1 => _sidc.Substring(16, 2);

        public string Modifier2 => _sidc.Substring(18, 2);

        public bool IsFeintDummy => FdHqTf.IsFeintDummy();

        public bool IsHeadquarters => FdHqTf.IsHeadquarters();

        public bool IsTaskForce => FdHqTf.IsTaskForce();

        /// <summary>
        /// Symbol identification coding
        /// </summary>
        public string SIDC => _sidc;

        public override string ToString()
        {
            return _sidc;
        }

        internal static bool IsNumeric(string s)
        {
            return s.All(c => c >= '0' && c <= '9');
        }
    }
}
