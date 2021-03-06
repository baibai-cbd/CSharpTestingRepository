﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeRepository.Model
{
    class Model2Entities
    {
    }

    public class EntityA
    {
        [Key]
        public int EntityAId { get; set; }
        public string SomeName { get; set; }
        public string Detail { get; set; }

        [NotMapped]
        public virtual XrefB XrefB { get; set; }
    }

    public class XrefB
    {
        public int XrefBId { get; set; }

        public int EntityAId { get; set; }
        public virtual EntityA EntityA { get; set; }

        public int EntityCId { get; set; }
        public virtual EntityC EntityC { get; set; }
    }

    public class EntityC
    {
        [Key]
        public int EntityCId { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }

        [NotMapped]
        public virtual XrefB XrefB { get; set; }
    }

    public class SmallEntityD
    {
        [Key]
        public int SmallEntityDId { get; set; }
        public bool IsGood { get; set; }
        public Guid Guid { get; set; }
        public string SomeInfo { get; set; }

        public override string ToString()
        {
            return $"this entity is {IsGood} and {SomeInfo}, with UUID {Guid}";
        }
    }

    public class BlogSite
    {
        public BlogSite()
        {
            Blogs = new HashSet<Blog>();
        }

        [Key]
        public Guid BlogSiteGuid { get; set; }
        public string BlogSiteName { get; set; }
        public string OwnerName { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }

    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
        public Guid BlogSiteGuid { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime createdDatetime { get; set; }

        public virtual BlogSite blogSite { get; set; }
    }
}
