using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository.Repository
{
    public class LazyLoadingRepo
    {
        private readonly Model2 context;

        public LazyLoadingRepo(Model2 model2Context)
        {
            context = model2Context;
        }


    }
}
