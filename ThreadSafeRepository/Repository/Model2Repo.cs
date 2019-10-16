using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository.Repository
{
    public class Model2Repo
    {
        private readonly Model2 context;

        public Model2Repo(Model2 model2Context)
        {
            context = model2Context;
        }

        public XrefB CreateABCPair(string someName, string detail, string address, int zip)
        {
            var a = context.EntityAs.Add(new EntityA { SomeName = someName, Detail = detail });
            var c = context.EntityCs.Add(new EntityC { Address = address, Zipcode = zip });
            context.SaveChanges();
            var b = context.XrefBs.Add(new XrefB { EntityAId = a.EntityAId, EntityCId = c.EntityCId });
            context.SaveChanges();
            return b;
        }

        public SmallEntityD CreateSmallEntityD(bool isGood, string someInfo)
        {
            var d = context.SmallEntityDs.Add(new SmallEntityD { IsGood = isGood, Guid = Guid.NewGuid(), SomeInfo = someInfo });
            context.SaveChanges();
            return d;
        }

        public IEnumerable<SmallEntityD> GetSmallEntityDsByBool(bool flag)
        {
            return context.SmallEntityDs.Where(d => d.IsGood == flag);
        }

        public IEnumerable<int> GetSmallEntityDIdsByBool(bool flag)
        {
            return context.SmallEntityDs.Where(d => d.IsGood == flag).Select(d => d.SmallEntityDId);
        }

        public int RemoveSmallEntityDsByEntities(IEnumerable<SmallEntityD> smallEntityDs)
        {
            int count = smallEntityDs.Count();
            context.SmallEntityDs.RemoveRange(smallEntityDs);
            context.SaveChanges();
            return count;
        }

        public int RemoveSmallEntityDsByIds(IEnumerable<int> smallEntityDIds)
        {
            int count = smallEntityDIds.Count();
            var entities = context.SmallEntityDs.Where(d => smallEntityDIds.Contains(d.SmallEntityDId));
            context.SmallEntityDs.RemoveRange(entities);
            context.SaveChanges();
            return count;
        }

        public int RemoveAllSmallEntityDs()
        {
            context.SmallEntityDs.RemoveRange(context.SmallEntityDs.ToList());
            return context.SaveChanges();
        }
    }
}
