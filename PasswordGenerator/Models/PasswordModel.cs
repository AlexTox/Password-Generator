using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGenerator.Models
{
    public class PasswordModel
    {
        public int Count { get; set; }
        public int Length { get; set; }
        public bool IsIncludeLowercase { get; set; }
        public bool IsIncludeUppercase { get; set; }
        public bool IsIncludeNumeric { get; set; }
        public bool IsIncludeSpecial { get; set; }
        public List<string> Passwords { get; set; }
    }
}
