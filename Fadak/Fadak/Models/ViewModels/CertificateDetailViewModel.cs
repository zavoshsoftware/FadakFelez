using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.ViewModels
{
    public class CertificateDetailViewModel
    {
        public Certificates CurrentCertificate { get; set; }
        public IEnumerable<Certificates> SimilarCertificates { get; set; }
    }
}