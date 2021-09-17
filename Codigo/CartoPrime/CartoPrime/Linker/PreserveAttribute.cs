using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Linker
{
    public sealed class PreserveAttribute : System.Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}
