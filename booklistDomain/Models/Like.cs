namespace booklistDomain.Models
{
    public record Like
    {
        public long Id { get; private set; }
        public Guid CommentId { get; private set; }
        public Guid LikerId { get; private set; }
        private Like() { }
        public static Like Create(Guid cid, Guid lid)
        {
            var like = new Like();
            like.CommentId = cid;
            like.LikerId = lid;
            return like;
        }
    }
}
