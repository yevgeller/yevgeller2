using System.Diagnostics;

namespace yevgeller2.DTOs
{
    [DebuggerDisplay("Id:{IdNo},{TagName}")]
    public class TagDto
    {
        public int IdNo { get; set; }
        public string TagName { get; set; }
    }
}