using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OCISDK
{
    public class ThreadSafeSigner : IOciSigner
    {
        private IOciSigner Signer { get; }

        private readonly object _syncLock = new object();

        public ThreadSafeSigner(IOciSigner signer)
        {
            Signer = signer;
        }

        void IOciSigner.SignRequest(HttpWebRequest request, bool useLessHeadersForPut)
        {
            lock (_syncLock)
                Signer.SignRequest(request, useLessHeadersForPut);
        }
    }
}
