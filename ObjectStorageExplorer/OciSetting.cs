using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorageExplorer
{
    public class OciSetting
    {
        public string TenancyId { get; set; }

        public string UserId { get; set; }

        public string Fingerprint { get; set; }

        public string KeyFilePath { get; set; }

        public string PassPhrase { get; set; }

        public bool Validation()
        {
            return !string.IsNullOrEmpty(TenancyId) && !string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Fingerprint) && !string.IsNullOrEmpty(KeyFilePath);
        }
    }
}
