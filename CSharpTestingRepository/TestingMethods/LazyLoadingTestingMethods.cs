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
        public void CreateDataForLazyLoadingTesting()
        {
            var interceptorOn = false;
            var model2Context = new Model2(interceptorOn);
            var model2Repo = new LazyLoadingRepo(model2Context);

            var blogSite = model2Repo.CreateBlogSite(new BlogSite { BlogSiteName = "TestSite1", OwnerName = "White" });
            //var blog1 = model2Repo.CreateBlog(new Blog { });
        }

        public static void LoadDataWithDifferentLazyLoadingSetup(bool LazyLoading)
        {
            var interceptorOn = true;
            var model2Context = new Model2(interceptorOn);
            var model2Repo = new LazyLoadingRepo(model2Context);


        }
    }
}
