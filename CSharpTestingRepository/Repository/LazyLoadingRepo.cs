using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository.Repository
{
    public class LazyLoadingRepo : IDisposable
    {
        private readonly Model2 context;

        public LazyLoadingRepo(Model2 model2Context)
        {
            context = model2Context;
        }

        public BlogSite CreateBlogSite(BlogSite blogSite)
        {
            context.BlogSites.Add(blogSite);
            context.SaveChanges();
            return blogSite;
        }

        public Blog CreateBlog(Blog blog)
        {
            context.Blogs.Add(blog);
            context.SaveChanges();
            return blog;
        }

        public IEnumerable<Blog> GetBlogs()
        {
            return context.Blogs.AsEnumerable();
        }

        public BlogSite GetBlogSite(Guid id)
        {
            return context.BlogSites.Find(id);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
