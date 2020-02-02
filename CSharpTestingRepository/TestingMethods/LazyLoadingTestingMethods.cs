using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;
using ThreadSafeRepository.Repository;

namespace ThreadSafeRepository.TestingMethods
{
    public class LazyLoadingTestingMethods
    {
        public static Guid CreateDataForLazyLoadingTesting()
        {
            var interceptorOn = false;
            using (var model2Context = new Model2(interceptorOn))
            using (var model2Repo = new LazyLoadingRepo(model2Context))
            {
                var random = new Random();
                var blogSiteName = "TestSite" + random.Next(200).ToString();
                var blogTitle = "blog name " + random.Next(500).ToString();

                var blogSite = model2Repo.CreateBlogSite(new BlogSite { BlogSiteGuid = Guid.NewGuid(), BlogSiteName = blogSiteName, OwnerName = "White" });
                var blog1 = model2Repo.CreateBlog(new Blog { Title = blogTitle, AuthorName = "AAA", BlogSiteGuid = blogSite.BlogSiteGuid, createdDatetime = DateTime.UtcNow });

                return blogSite.BlogSiteGuid;
            }

        }

        public static void LoadDataWithDifferentLazyLoadingSetup(bool lazyLoading, Guid siteID)
        {
            var interceptorOn = true;
            using (var model2Context = new Model2(interceptorOn))
            using (var model2Repo = new LazyLoadingRepo(model2Context))
            {
                model2Context.Configuration.LazyLoadingEnabled = lazyLoading;

                var blogSite = model2Repo.GetBlogSite(siteID);
                Console.WriteLine("-");
                Console.WriteLine("-");
                Console.WriteLine($"this is {blogSite.BlogSiteName} by {blogSite.OwnerName}.");
                Console.WriteLine("-");
                Console.WriteLine("-");
                Console.WriteLine($"there is {blogSite.Blogs.Count} blogs in here.");
            }
        }
    }
}
