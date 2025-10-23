namespace ProfileCore.Infrastructure
{
    public abstract class Auditable
    {
        /// <summary>
        /// DateTime of creation. This value will never change
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// DateTime of last value update. Should be updated when entity data updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}