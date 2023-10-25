using booklistDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Interfaces
{
    public interface IBookRepository
    {
        /// <summary>
        /// 本仓库对应聚合book
        /// 操作 表book bookcategories
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<BookCategory>> GetBookCategoriesAsync();//获取书的所有种类
        public Task<IEnumerable<BookCategory>> GetBookCategoriesByBookIdAsync(Guid id);//获取一本书的所有类别
        public Task<IEnumerable<Book>> GetBooksAsync(int skipNum,int takeNum);//获取所有的书
        public Task<IEnumerable<Book>> GetBooksByCategoryAsync(BookCategory bookCategory,int skipNum,int takeNum);//获取某一种类的书
    }
}
