using System.Collections;
using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Models
{
    internal class OrbatUnitSample : IOrbatUnit, IEnumerable<OrbatUnitSample>
    {

        public OrbatUnitSample(string sdic = "30031000131211050000", string? uid = null, string? common = null) 
        {

            Sdic = sdic;
            UniqueDesignation = uid;
            CommonIdentifier = common;
        }

        public string Sdic { get; set; } = "30031000131211050000";

        public string? UniqueDesignation { get; set; }

        public string? CommonIdentifier { get; set; }

        public string? HigherFormation { get; set; }

        public List<OrbatUnitSample> SubUnits { get;} = new List<OrbatUnitSample>();

        IEnumerable<IOrbatUnit>? IOrbatUnit.SubUnits => SubUnits;

        string? IOrbatUnit.HigherFormation => CommonIdentifier?.Split('-')[1];

        string? IOrbatUnit.CommonIdentifier => CommonIdentifier?.Split('-')[0];

        string? IOrbatUnit.AdditionalInformation => null;

        string? IOrbatUnit.Href => null;

        public IEnumerator<OrbatUnitSample> GetEnumerator()
        {
            return SubUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SubUnits.GetEnumerator();
        }

        public void Add(OrbatUnitSample unit)
        {
            SubUnits.Add(unit);
        }
    }
}