using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public record BaseEntity:IBaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreationTime { get; private set; } = DateTime.Now;
        public DateTime? DeletionTime { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
    }
}
