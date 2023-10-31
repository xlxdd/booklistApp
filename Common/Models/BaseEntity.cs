using MassTransit;

namespace Common.Models
{
    public record BaseEntity : IBaseEntity
    {
        public Guid Id { get; private set; } = NewId.NextGuid();
        public DateTime CreationTime { get; private set; } = DateTime.Now;
        public DateTime? DeletionTime { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
    }
}
