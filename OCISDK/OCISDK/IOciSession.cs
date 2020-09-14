using OCISDK.Identity;
using OCISDK.ObjectStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCISDK
{
    /// <summary>
    /// OciSession interface
    /// </summary>
    public interface IOciSession
    {
        /// <summary>
        /// Get IdentityClinet
        /// </summary>
        /// <returns></returns>
        IIdentityClient GetIdentityClient();

        /// <summary>
        /// Get IdentityClinet Async
        /// </summary>
        /// <returns></returns>
        IIdentityClientAsync GetIdentityClientAsync();

        /// <summary>
        /// Get ObjectStorageClient
        /// </summary>
        /// <returns></returns>
        IObjectStorageClient GetObjectStorageClient();
    }
}
