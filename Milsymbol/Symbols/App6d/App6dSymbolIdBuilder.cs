using System;
using System.Text;

namespace Milsymbol.Symbols.App6d
{
    public class App6dSymbolIdBuilder : IApp6dSymbolId
    {
        private string _version = "10";
        private string _symbolSet = "00";
        private string _size = "00";
        private string _icon = "000000";
        private string _modifier1 = "00";
        private string _modifier2 = "00";

        public App6dSymbolIdBuilder()
        {

        }

        public App6dSymbolIdBuilder(string sdic)
            : this(new App6dSymbolId(sdic))
        {

        }

        public App6dSymbolIdBuilder(App6dSymbolId copy)
        {
            _version = copy.Version;
            StandardIdentity1 = copy.StandardIdentity1;
            StandardIdentity2 = copy.StandardIdentity2;
            _symbolSet = copy.SymbolSet;
            Status = copy.Status;
            DummyHqTaskForce = copy.DummyHqTaskForce;
            _size = copy.Size;
            _icon = copy.Icon;
            _modifier1 = copy.Modifier1;
            _modifier2 = copy.Modifier2;
        }

        public string Version 
        { 
            get { return _version; } 
            set 
            {
                if (value == null || value.Length != 2 || !App6dSymbolId.IsNumeric(value))
                {
                    throw new ArgumentException();
                }
                _version = value; 
            } 
        }

        public App6dStandardIdentity1 StandardIdentity1 { get; set; } = App6dStandardIdentity1.Reality;

        public App6dStandardIdentity2 StandardIdentity2 { get; set; } = App6dStandardIdentity2.Unknown;

        public string SymbolSet
        {
            get { return _symbolSet; }
            set
            {
                if (value == null || value.Length != 2 || !App6dSymbolId.IsNumeric(value))
                {
                    throw new ArgumentException();
                }
                _symbolSet = value;
            }
        }

        public App6dStatus Status { get; set; } = App6dStatus.Present;

        public App6dDummyHqTaskForce DummyHqTaskForce { get; set; } = App6dDummyHqTaskForce.None;

        public bool IsDummy
        {
            get { return DummyHqTaskForce.IsDummy(); }
            set { DummyHqTaskForce = DummyHqTaskForce & ~App6dDummyHqTaskForce.FeintDummy | (value ? App6dDummyHqTaskForce.FeintDummy : App6dDummyHqTaskForce.None); }
        }

        public bool IsHeadquarters
        {
            get { return DummyHqTaskForce.IsHeadquarters(); }
            set { DummyHqTaskForce = DummyHqTaskForce & ~App6dDummyHqTaskForce.Headquarters | (value ? App6dDummyHqTaskForce.Headquarters : App6dDummyHqTaskForce.None); }
        }

        public bool IsTaskForce
        {
            get { return DummyHqTaskForce.IsTaskForce(); }
            set { DummyHqTaskForce = DummyHqTaskForce & ~App6dDummyHqTaskForce.TaskForce | (value ? App6dDummyHqTaskForce.TaskForce : App6dDummyHqTaskForce.None); }
        }

        public string Size
        {
            get { return _size; }
            set
            {
                if (value == null || value.Length != 2 || !App6dSymbolId.IsNumeric(value))
                {
                    throw new ArgumentException();
                }
                _size = value;
            }
        }

        public string Icon
        {
            get { return _icon; }
            set
            {
                if (value == null || value.Length != 6 || !App6dSymbolId.IsNumeric(value))
                {
                    throw new ArgumentException();
                }
                _icon = value;
            }
        }

        public string Modifier1
        {
            get { return _modifier1; }
            set
            {
                if (value == null || value.Length != 2 || !App6dSymbolId.IsNumeric(value))
                {
                    throw new ArgumentException();
                }
                _modifier1 = value;
            }
        }

        public string Modifier2
        {
            get { return _modifier2; }
            set
            {
                if (value == null || value.Length != 2 || !App6dSymbolId.IsNumeric(value))
                {
                    throw new ArgumentException();
                }
                _modifier2 = value;
            }
        }

        public string ToSIDC()
        {
            var sb = new StringBuilder(20);
            sb.Append(Version);
            sb.Append((char)('0' + StandardIdentity1));
            sb.Append((char)('0' + StandardIdentity2));
            sb.Append(SymbolSet);
            sb.Append((char)('0' + Status));
            sb.Append((char)('0' + DummyHqTaskForce));
            sb.Append(Size);
            sb.Append(Icon);
            sb.Append(Modifier1);
            sb.Append(Modifier2);
            return sb.ToString();
        }

        public App6dSymbolId ToSymbolId()
        {
            return new App6dSymbolId(ToSIDC());
        }
    }
}
