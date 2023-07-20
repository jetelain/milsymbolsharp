namespace Milsymbol.App6d
{
    /// <summary>
    /// Values for "STATUS" field
    /// </summary>
    public enum App6dStatus
    {
        /// <summary>
        /// PRESENT
        /// </summary>
        Present = 0,

        /// <summary>
        /// PLANNED/ANTICIPATED/SUSPECT
        /// </summary>
        Planned = 1,

        /// <summary>
        /// PRESENT/FULLY CAPABLE
        /// </summary>
        PresentFullyCapable = 2,

        /// <summary>
        /// PRESENT/DAMAGED
        /// </summary>
        PresentDamaged = 3,

        /// <summary>
        /// PRESENT/DESTROYED
        /// </summary>
        PresentDestroyed = 4,

        /// <summary>
        /// PRESENT/FULL TO CAPACITY
        /// </summary>
        PresentFullToCapacity = 5
    }
}
