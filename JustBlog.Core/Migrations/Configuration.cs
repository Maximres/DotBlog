namespace JustBlog.Core.Migrations
{
    using JustBlog.Core.Objects;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JustBlog.Core.Concrete.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JustBlog.Core.Concrete.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            //Post[] postSeed = null;
            //Category categorySeed = new Category {Name = "Programming", Description = "О программировании", UrlSlug = "programming", Posts = postSeed };
            //Tag[] tagSeed = { new Tag { Name = "CSharp", UrlSlug = "csharp", Description = "About C#", Posts = postSeed},
            //                  new Tag{Name = "ASP.NET", Description = "About ASP.NET", UrlSlug = "asp_dot_net", Posts = postSeed } };
            //postSeed = new Post[] {
            //    new Post {Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            //    ShortDescription = "Post About Coding",
            //    Meta = "what_is_coding",
            //    UrlSlug = "programming",
            //    PostedOn = DateTime.Now,
            //    Published = true,
            //    Title = "What is coding?",
            //    Category = categorySeed,
            //    Tags = tagSeed }
            //};
            //context.Caregories.AddOrUpdate(categorySeed);
            //context.SaveChanges();
            //context.Tags.AddOrUpdate(tagSeed);
            //context.SaveChanges();
            //context.Posts.AddOrUpdate(postSeed);
            //context.SaveChanges();
        }
    }
}
