﻿using OCISDK.Identity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCISDK.Identity.Response
{
    /// <summary>
    /// UpdateUserState Response
    /// </summary>
    public class UpdateUserStateResponse
    {
        /// <summary>
        /// rUnique Oracle-assigned identifier for the request. 
        /// If you need to contact Oracle about a particular request, please provide the request ID.
        /// </summary>
        public string OpcRequestId { get; set; }

        /// <summary>
        /// For optimistic concurrency control. See if-match.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// The response body will contain a single User resource.
        /// </summary>
        public UserDetails User { get; set; }
    }
}
