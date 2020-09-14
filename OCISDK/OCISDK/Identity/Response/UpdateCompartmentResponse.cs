using OCISDK.Identity.Model;

namespace OCISDK.Identity.Response
{
    /// <summary>
    /// UpdateCompartment Response
    /// </summary>
    public class UpdateCompartmentResponse
    {
        /// <summary>
        /// For optimistic concurrency control. See if-match.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// response header parameter opcRequestId
        /// </summary>
        public string OpcRequestId { get; set; }

        /// <summary>
        /// The response body will contain a single Compartment resource.
        /// </summary>
        public Compartment Compartment { get; set; }
    }
}
