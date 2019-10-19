using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository.Repository
{
    public interface IRepository
    {
        UnsafeTable GetById(int id);

        int CreateUsingSP(int ipc, int ifs, int userId);
    }
}
