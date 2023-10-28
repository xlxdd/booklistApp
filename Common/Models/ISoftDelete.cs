namespace Common.Models
{
    public interface ISoftDelete
    {
        bool IsDeleted { get;}
        void SoftDelete();
    }
}
