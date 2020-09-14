using OCISDK.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCISDK
{
    /// <summary>
    /// Client Setting Interface
    /// </summary>
    public interface IClientSetting
    {
        /// <summary>
        /// setter region
        /// </summary>
        /// <param name="region"></param>
        void SetRegion(string region);

        /// <summary>
        /// getter region
        /// </summary>
        /// <returns></returns>
        string GetRegion();

        /// <summary>
        /// Get WebRequestPolicy
        /// </summary>
        /// <returns></returns>
        IWebRequestPolicy GetWebRequestPolicy();

        /// <summary>
        /// Set WebRequestPolicy
        /// </summary>
        /// <param name="webRequestPolicy"></param>
        void SetWebRequestPolicy(IWebRequestPolicy webRequestPolicy);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IJsonSerializer GetJsonSerializer();

        /// <summary>
        /// Set JsonSerializer
        /// </summary>
        void SetJsonSerializer(IJsonSerializer jsonSerializer);

        /// <summary>
        /// get TenancyId
        /// </summary>
        /// <returns></returns>
        string GetTenancyId();
    }
}
