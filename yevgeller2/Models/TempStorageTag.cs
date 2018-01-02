using System;
using System.Diagnostics;

namespace yevgeller2.Models
{
    [DebuggerDisplay("{IdNo}/{Name}/{UserId}/{TimeStamp}")]
    public class TempStorageTag : IEquatable<TempStorageTag>
    {
        public int Id { get; set; }
        public int IdNo { get; set; }
        public string Name { get; set; }
        public ProjectAction Action { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }

        public bool Equals(TempStorageTag other)
        {
            if (IdNo == other.IdNo &&
                Name == other.Name &&
                Action == other.Action &&
                UserId == other.UserId)
                return true;

            return false;
        }
    }
}