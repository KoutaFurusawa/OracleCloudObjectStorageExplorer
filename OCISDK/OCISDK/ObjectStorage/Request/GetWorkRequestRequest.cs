﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OCISDK.ObjectStorage.Request
{
    /// <summary>
    /// GetWorkRequest request
    /// </summary>
    public class GetWorkRequestRequest
    {
        /// <summary>
        /// The ID of the asynchronous request.
        /// <para>Required: yes</para>
        /// </summary>
        public string WorkRequestId { get; set; }

        /// <summary>
        /// The client request ID for tracing.
        /// <para>Required: no</para>
        /// </summary>
        public string OpcClientRequestId { get; set; }
    }
}
