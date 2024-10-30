using System;
using System.Collections.Generic;

namespace NWBackendAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int AccessLevelId { get; set; }
    }
}
