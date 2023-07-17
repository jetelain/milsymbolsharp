namespace Milsymbol.App6d
{
    public interface IApp6dSymbolId
    {
        App6dFdHqTf FdHqTf { get; }
        string Icon { get; }
        string Modifier1 { get; }
        string Modifier2 { get; }
        string Amplifier { get; }
        App6dStandardIdentity1 StandardIdentity1 { get; }
        App6dStandardIdentity2 StandardIdentity2 { get; }
        App6dStatus Status { get; }
        string SymbolSet { get; }
        string Version { get; }
    }
}