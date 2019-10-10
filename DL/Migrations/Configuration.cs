namespace DL.Migrations
{
    using DL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DL.Entities.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DL.Entities.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            Authors author1 = new Entities.Authors { Id = 1, FirstName = "Стив ", LastName = "Макконнелл" };
            context.Authors.AddOrUpdate(author1);
            Genres genre1 = new Entities.Genres { Id = 1, Name = "Action" };
            context.Genres.AddOrUpdate(genre1);
            Books book1 = new Entities.Books { Id = 1, AuthorId = 1, Title = "TestBook1", GenreId = 1, Price = 1000, Pages = 150 };
            context.Books.AddOrUpdate(book1);
            Users user1 = new Entities.Users { Id = 1, Email = "testUser1@mail.ru", FIO = "TestUser1" };
            context.Users.AddOrUpdate(user1);


        }
    }
}
