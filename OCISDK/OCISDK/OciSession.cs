using OCISDK.Identity;
using OCISDK.ObjectStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OCISDK
{
    /// <summary>
    /// Oci Session Class
    /// </summary>
    public class OciSession : IOciSession
    {
        /// <summary>
        /// OciSigner
        /// </summary>
        public readonly OciSigner OciSigner;

        /// <summary>
        /// ClientConfigStream
        /// </summary>
        public readonly ClientConfigStream ClientConfigStream;

        /// <summary>
        /// constructer
        /// </summary>
        /// <param name="config"></param>
        public OciSession(ClientConfig config)
        {
            var streamConfig = new ClientConfigStream
            {
                AccountId = config.AccountId,
                DomainName = config.DomainName,
                Fingerprint = config.Fingerprint,
                HomeRegion = config.HomeRegion,
                IdentityDomain = config.IdentityDomain,
                Password = config.Password,
                PrivateKeyPassphrase = config.PrivateKeyPassphrase,
                TenancyId = config.TenancyId,
                UserId = config.UserId,
                UserName = config.UserName
            };

            using (var key = File.OpenText(config.PrivateKey))
            {
                streamConfig.PrivateKey = key;
            }

            OciSigner = new OciSigner(
                streamConfig.TenancyId,
                streamConfig.UserId,
                streamConfig.Fingerprint,
                streamConfig.PrivateKey,
                streamConfig.PrivateKeyPassphrase
            );

            ClientConfigStream = streamConfig;

        }

        /// <summary>
        /// constructer
        /// </summary>
        /// <param name="config"></param>
        public OciSession(ClientConfigStream config)
        {
            OciSigner = new OciSigner(
                config.TenancyId,
                config.UserId,
                config.Fingerprint,
                config.PrivateKey,
                config.PrivateKeyPassphrase
            );

            ClientConfigStream = config;
        }

        /// <summary>
        /// Get IdentityClinet
        /// </summary>
        /// <returns></returns>
        public IIdentityClient GetIdentityClient()
        {
            return new IdentityClient(ClientConfigStream, OciSigner);
        }

        /// <summary>
        /// Get IdentityClinet Async
        /// </summary>
        /// <returns></returns>
        public IIdentityClientAsync GetIdentityClientAsync()
        {
            return new IdentityClientAsync(ClientConfigStream, OciSigner);
        }

        /// <summary>
        /// Get ObjectStorageClient
        /// </summary>
        /// <returns></returns>
        public IObjectStorageClient GetObjectStorageClient()
        {
            return new ObjectStorageClient(ClientConfigStream, OciSigner);
        }
    }
}
