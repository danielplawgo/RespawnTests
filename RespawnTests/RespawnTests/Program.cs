using FizzWare.NBuilder;
using Respawn;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespawnTests
{
    class Program
    {
        private static Checkpoint _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                "__MigrationHistory"
            },
            WithReseed = true
        };

        static void Main(string[] args)
        {
            SeedData();

            _checkpoint.Reset(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString).Wait();
        }

        static void SeedData()
        {
            using (var db = new DataContext())
            {
                IList<Category> categories = null;
                IList<Person> people = null;

                if (db.Categories.Any() == false)
                {
                    categories = Builder<Category>.CreateListOfSize(5).Build();

                    db.Categories.AddRange(categories);
                }

                if (db.People.Any() == false)
                {
                    people = Builder<Person>.CreateListOfSize(5).Build();

                    db.People.AddRange(people);
                }

                if (db.Books.Any() == false)
                {
                    if (categories == null)
                    {
                        categories = db.Categories.ToList();
                    }

                    if (people == null)
                    {
                        people = db.People.ToList();
                    }

                    var books = Builder<Book>.CreateListOfSize(10)
                        .All()
                        .With(b => b.Category = Pick<Category>.RandomItemFrom(categories))
                        .With(b => b.Authors = Pick<Person>.UniqueRandomList(With.Between(1).And(3).Elements).From(people))
                        .Build();

                    db.Books.AddRange(books);
                }

                db.SaveChanges();
            }
        }
    }
}
