using Microsoft.AspNetCore.Mvc;

namespace booklistAPI.BookLists.Request
{
    public class BookListRequest
    {
        public IFormFile CoverImage { get; set; }//封面
        public string Title { get; set; }//标题
        public string Descrpition { get; set; }//简介
        public IEnumerable<Guid> Books { get; set; }//书
        
    }
}
