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
        public long Id { get; init; }
        public Guid CommentId { get; init; }
        public Guid LikerId { get; init; }
        private Like() { }
    }
}
