using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface IBaseEntity
    {
        public Guid Id { get;}
        public DateTime CreationTime { get; }
        public DateTime? DeletionTime { get; }
        public DateTime? LastModificationTime { get;}
    }
}
