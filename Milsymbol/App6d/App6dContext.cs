namespace Pmad.Milsymbol.App6d
{
    /// <summary>
    /// Values for first digit of "STANDARD IDENTITY" field (called "Context" within standard)
    /// </summary>
    public enum App6dContext
    {
        /// <summary>
        /// REALITY
        /// </summary>
        Reality = 0,

        /// <summary>
        /// EXERCISE
        /// </summary>
        /// <remarks>
        /// Exercise and Simulation context are currently for US use only within APP-6 D V1.
        /// </remarks>
        Exercise = 1,

        /// <summary>
        /// SIMULATION
        /// </summary>
        /// <remarks>
        /// Exercise and Simulation context are currently for US use only within APP-6 D V1.
        /// </remarks>
        Simulation = 2
    }
}
