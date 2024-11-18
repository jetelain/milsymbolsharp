using System;
using System.Text;
using Jint.Native;

namespace Pmad.Milsymbol.App6d
{
    public class App6dSymbolIdBuilder : IApp6dSymbolId
    {
        private string _version = "10";
        private string _symbolSet = "00";
        private string _size = "00";
        private string _icon = "000000";
        private string _modifier1 = "00";
        private string _modifier2 = "00";
        private string _originatorIdentifier = "";
        private string _originatorSymbolSet = "";
        private string _originatorData = "";

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
            Context = copy.Context;
            StandardIdentity = copy.StandardIdentity;
            _symbolSet = copy.SymbolSet;
            Status = copy.Status;
            HqTfFd = copy.HqTfFd;
            _size = copy.Amplifier;
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

        public App6dContext Context { get; set; } = App6dContext.Reality;

        public App6dStandardIdentity StandardIdentity { get; set; } = App6dStandardIdentity.Unknown;

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

        public App6dHqTfFd HqTfFd { get; set; } = App6dHqTfFd.None;

        public bool IsFeintDummy
        {
            get { return HqTfFd.IsFeintDummy(); }
            set { HqTfFd = HqTfFd & ~App6dHqTfFd.FeintDummy | (value ? App6dHqTfFd.FeintDummy : App6dHqTfFd.None); }
        }

        public bool IsHeadquarters
        {
            get { return HqTfFd.IsHeadquarters(); }
            set { HqTfFd = HqTfFd & ~App6dHqTfFd.Headquarters | (value ? App6dHqTfFd.Headquarters : App6dHqTfFd.None); }
        }

        public bool IsTaskForce
        {
            get { return HqTfFd.IsTaskForce(); }
            set { HqTfFd = HqTfFd & ~App6dHqTfFd.TaskForce | (value ? App6dHqTfFd.TaskForce : App6dHqTfFd.None); }
        }

        public string Amplifier
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

        public string OriginatorIdentifier
        {
            get { return _originatorIdentifier; }
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Length != 3 || !App6dSymbolId.IsNumeric(value)))
                {
                    throw new ArgumentException();
                }
                _originatorIdentifier = value;
            }
        }
        public string OriginatorSymbolSet
        {
            get { return _originatorSymbolSet; }
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Length != 1 || !App6dSymbolId.IsNumeric(value)))
                {
                    throw new ArgumentException();
                }
                _originatorSymbolSet = value;
            }
        }
        public string OriginatorData
        {
            get { return _originatorData; }
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Length != 6 || !App6dSymbolId.IsNumeric(value)))
                {
                    throw new ArgumentException();
                }
                _originatorData = value;
            }
        }

        public string ToSIDC()
        {
            var sb = new StringBuilder(20);
            sb.Append(Version);
            sb.Append((char)('0' + Context));
            sb.Append((char)('0' + StandardIdentity));
            sb.Append(SymbolSet);
            sb.Append((char)('0' + Status));
            sb.Append((char)('0' + HqTfFd));
            sb.Append(Amplifier);
            sb.Append(Icon);
            sb.Append(Modifier1);
            sb.Append(Modifier2);
            if (!string.IsNullOrEmpty(OriginatorIdentifier))
            {
                sb.Append(OriginatorIdentifier);
                sb.Append((OriginatorSymbolSet ?? string.Empty).PadRight(1, '0'));
                sb.Append((OriginatorData ?? string.Empty).PadRight(6, '0'));
            }
            return sb.ToString();
        }

        public App6dSymbolId ToSymbolId()
        {
            return new App6dSymbolId(ToSIDC());
        }
    }
}
