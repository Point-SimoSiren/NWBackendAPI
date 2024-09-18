using System;
using System.Collections.Generic;

namespace NWBackendAPI.Models
{
    public partial class Documentation
    {
        public int DocumentationId { get; set; }
        public string AvailableRoute { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
