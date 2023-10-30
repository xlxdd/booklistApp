using booklistDomain.Entities;
using booklistDomain.Entities.Identity;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Models
{
    public record Like
    {
        public long Id { get; private set; }
        public Guid CommentId { get; private set; }
        public Guid LikerId { get; private set; }
        private Like() { }
        public static Like Create(Guid cid,Guid lid)
        {
            var like = new Like();
            like.CommentId = cid;
            like.LikerId = lid;
            return like;
        }
    }
}
