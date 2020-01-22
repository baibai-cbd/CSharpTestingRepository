namespace ThreadSafeRepository.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;

    public class Model2 : DbContext
    {
        // Your context has been configured to use a 'Model2' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ThreadSafeRepository.Model.Model2' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model2' 
        // connection string in the application configuration file.
        public Model2()
            : base("name=Model2")
        {
        }

        public Model2(bool isInterceptorOn)
            : base("name=Model2")
        {
            _isInterceptorOn = isInterceptorOn;
        }

        protected readonly bool _isInterceptorOn;

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<EntityA> EntityAs { get; set; }
        public virtual DbSet<XrefB> XrefBs { get; set; }
        public virtual DbSet<EntityC> EntityCs { get; set; }
        public virtual DbSet<SmallEntityD> SmallEntityDs { get; set; }
        //
        public virtual DbSet<BlogSite> BlogSites { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XrefB>()
                .HasKey(x => x.XrefBId);

            modelBuilder.Entity<XrefB>()
                .HasRequired(x => x.EntityA)
                .WithRequiredDependent();

            modelBuilder.Entity<XrefB>()
                .HasRequired(x => x.EntityC)
                .WithRequiredDependent();

            modelBuilder.Entity<SmallEntityD>();

            modelBuilder.Entity<BlogSite>()
                .HasMany(x => x.Blogs)
                .WithRequired(x => x.blogSite);

            //base.OnModelCreating(modelBuilder);
            if (_isInterceptorOn)
            {
                DbInterception.Add(new LazyLoadingInterceptor());
            }
        }
    }
}