using DL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class MyInitializer : CreateDatabaseIfNotExists<Model1>
    {
        protected override void Seed(DL.Entities.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            Authors author = new Entities.Authors { Id = 1, FirstName = "Aleksandr ", LastName = "Pushkin" };
            Authors author2 = new Entities.Authors { Id = 2, FirstName = "Stiven ", LastName = "King" };
            context.Authors.AddOrUpdate(author);
            context.Authors.AddOrUpdate(author2);


            Genres genre = new Entities.Genres { Id = 1, Name = "Horror" };
            Genres genre2 = new Entities.Genres { Id = 2, Name = "Multik" };
            context.Genres.AddOrUpdate(genre);
            context.Genres.AddOrUpdate(genre2);


            Books book = new Entities.Books { Id = 1, AuthorId = 1, Title = "Romeo i Juletta", GenreId = 1, Price = 1000, Pages = 150 };
            Books book2 = new Entities.Books { Id = 2, AuthorId = 2, Title = "Fast & Furious", GenreId = 2, Price = 2000, Pages = 450 };
            context.Books.AddOrUpdate(book);
            context.Books.AddOrUpdate(book2);


            Users user = new Entities.Users { Id = 1, Email = "user@mail.ru", FIO = "Roberto Cavalli" };
            Users user2 = new Entities.Users { Id = 2, Email = "user2@mail.ru", FIO = "Yves Saint-Laurent" };
            context.Users.AddOrUpdate(user);
            context.Users.AddOrUpdate(user2);

            base.Seed(context);
        }
    }
}
