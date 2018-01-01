using System.Diagnostics;
using yevgeller2.Models;

namespace yevgeller2.DTOs
{
    [DebuggerDisplay("Id:{IdNo},{TagName}")]
    public class TagDto
    {
        public int IdNo { get; set; }
        public string TagName { get; set; }
        public ProjectAction Action { get; set; }
    }
}