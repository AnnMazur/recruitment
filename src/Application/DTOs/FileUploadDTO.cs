using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FileUploadResponse
    {
        public string FileName { get; set; } = default!;
        public string OriginalFileName { get; set; } = default!;
        public long FileSize { get; set; }
        public string FileUrl { get; set; } = default!;
    }

   /* public class ResumeUploadRequest
    {
        public IFormFile File { get; set; } = default!;
        public Guid CandidateId { get; set; }
    } */
}
