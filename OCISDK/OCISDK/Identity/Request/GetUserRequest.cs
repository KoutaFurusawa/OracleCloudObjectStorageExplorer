﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OCISDK.Identity.Request
{
    /// <summary>
    /// GetUser Request
    /// </summary>
    public class GetUserRequest
    {
        /// <summary>
        /// The OCID of the user.
        /// <para>Required: yes</para>
        /// </summary>
        public string UserId { get; set; }
    }
}
