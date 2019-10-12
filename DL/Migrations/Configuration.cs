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

            Authors author = new Entities.Authors { Id = 1, FirstName = "Aleksandr ", LastName = "Pushkin" };
            context.Authors.AddOrUpdate(author);
            Genres genre = new Entities.Genres { Id = 1, Name = "Multik" };
            context.Genres.AddOrUpdate(genre);
            Books book = new Entities.Books { Id = 1, AuthorId = 1, Title = "Romeo i Juletta", GenreId = 1, Price = 1000, Pages = 150 };
            context.Books.AddOrUpdate(book);
            Users user = new Entities.Users { Id = 1, Email = "user@mail.ru", FIO = "Roberto Cavalli" };
            context.Users.AddOrUpdate(user);


        }
    }
}
