namespace ThreadSafeRepository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ThreadSafeRepository.Model.Model2>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ThreadSafeRepository.Model.Model2";
        }

        protected override void Seed(ThreadSafeRepository.Model.Model2 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
