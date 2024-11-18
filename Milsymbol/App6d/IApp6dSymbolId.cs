namespace Pmad.Milsymbol.App6d
{
    public interface IApp6dSymbolId
    {
        App6dHqTfFd HqTfFd { get; }
        string Icon { get; }
        string Modifier1 { get; }
        string Modifier2 { get; }
        string Amplifier { get; }
        App6dContext Context { get; }
        App6dStandardIdentity StandardIdentity { get; }
        App6dStatus Status { get; }
        string SymbolSet { get; }
        string Version { get; }
        string OriginatorIdentifier { get; }
        string OriginatorSymbolSet { get; }
        string OriginatorData { get; }
    }
}