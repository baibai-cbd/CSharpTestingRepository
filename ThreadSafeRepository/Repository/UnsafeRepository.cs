using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository
{
    public class UnsafeRepository : IRepository
    {
        private LocalThreadSafeEntities entityContext;

        public UnsafeRepository(LocalThreadSafeEntities context)
        {
            this.entityContext = context;
        }

        public int CreateUsingSP(int ipc, int ifs, int userId)
        {
            return entityContext.Database.ExecuteSqlCommand("exec dbo.WaitAndInsert @ipc,@ifs,@userId,@time;", 
                                                new SqlParameter("@ipc", ipc), 
                                                new SqlParameter("@ifs", ifs),
                                                new SqlParameter("@userId", userId), 
                                                new SqlParameter("@time" , System.DateTime.UtcNow));
        }

        public UnsafeTable GetById(int id)
        {
            return entityContext.UnsafeTables.Find(id);
        }
    }
}
