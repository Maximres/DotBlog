namespace JustBlog.Core.Concrete
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using JustBlog.Core.Objects;

    public class BlogContext : DbContext
    {
        // Your context has been configured to use a 'BlogContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'JustBlog.Core.Concrete.BlogContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BlogContext' 
        // connection string in the application configuration file.
        public BlogContext()
            : base("name=BlogContext")
        {
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Category> Caregories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostConfig());
            modelBuilder.Configurations.Add(new TagConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            base.OnModelCreating(modelBuilder);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            Property(p => p.Title).HasMaxLength(500).IsRequired();
            Property(p => p.ShortDescription).HasMaxLength(5000).IsRequired();
            Property(p => p.Description).HasMaxLength(5000).IsRequired();
            Property(p => p.Meta).HasMaxLength(1000).IsRequired();
            Property(p => p.UrlSlug).HasMaxLength(200).IsRequired();
            Property(p => p.Published).IsRequired();
            Property(p => p.PostedOn).IsRequired();
            Property(p => p.Modified).IsOptional();
            HasMany(m => m.Tags).WithMany(w => w.Posts);
        }
    }

    public class TagConfig : EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(p => p.UrlSlug).HasMaxLength(50).IsRequired();
            Property(p => p.Description).HasMaxLength(200).IsOptional();
        }
    }

    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(p => p.UrlSlug).HasMaxLength(50).IsRequired();
            Property(p => p.Description).HasMaxLength(200).IsOptional();
            HasMany(h => h.Posts).WithRequired(w => w.Category).HasForeignKey(k => k.CategoryId).WillCascadeOnDelete(true);
        }
    }
    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}