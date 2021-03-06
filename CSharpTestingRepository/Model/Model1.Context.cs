﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThreadSafeRepository.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Data.Common;

    public partial class LocalThreadSafeEntities : DbContext
    {
        public LocalThreadSafeEntities()
            : base("name=LocalThreadSafeEntities")
        {
        }

        // extra added ctor
        public LocalThreadSafeEntities(DbConnection sqlConnection, bool contextOwnsConn)
            : base(sqlConnection, contextOwnsConn)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UnsafeTable> UnsafeTables { get; set; }
    
        public virtual ObjectResult<WaitAndInsert_Result> WaitAndInsert(Nullable<int> iPC, Nullable<int> iFS, Nullable<int> createdUserID, Nullable<System.DateTime> createdDateTime)
        {
            var iPCParameter = iPC.HasValue ?
                new ObjectParameter("IPC", iPC) :
                new ObjectParameter("IPC", typeof(int));
    
            var iFSParameter = iFS.HasValue ?
                new ObjectParameter("IFS", iFS) :
                new ObjectParameter("IFS", typeof(int));
    
            var createdUserIDParameter = createdUserID.HasValue ?
                new ObjectParameter("CreatedUserID", createdUserID) :
                new ObjectParameter("CreatedUserID", typeof(int));
    
            var createdDateTimeParameter = createdDateTime.HasValue ?
                new ObjectParameter("CreatedDateTime", createdDateTime) :
                new ObjectParameter("CreatedDateTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WaitAndInsert_Result>("WaitAndInsert", iPCParameter, iFSParameter, createdUserIDParameter, createdDateTimeParameter);
        }
    }
}
