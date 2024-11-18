namespace Pmad.Milsymbol.App6d
{
    /// <summary>
    /// Values for second digit of "STANDARD IDENTITY" field (called "Standard Identity" within standard)
    /// </summary>
    public enum App6dStandardIdentity
    {
        /// <summary>
        /// PENDING
        /// </summary>
        Pending = 0,

        /// <summary>
        /// UNKNOWN
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// ASSUMED FRIEND
        /// </summary>
        AssumedFriend = 2,

        /// <summary>
        /// FRIEND
        /// </summary>
        Friend = 3,

        /// <summary>
        /// NEUTRAL
        /// </summary>
        Neutral = 4,

        /// <summary>
        /// SUSPECT/JOKER
        /// </summary>
        /// <remarks>
        /// The Standard Identities Joker and Faker are currently for US use only within APP-6 D V1.
        /// </remarks>
        Suspect = 5,

        /// <summary>
        /// HOSTILE/FAKER
        /// </summary>
        /// <remarks>
        /// The Standard Identities Joker and Faker are currently for US use only within APP-6 D V1.
        /// </remarks>
        Hostile = 6
    }
}
