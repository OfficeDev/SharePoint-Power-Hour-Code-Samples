using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using O365ApiFullStack.Models;

namespace O365ApiFullStack.Data {

  public class O365ApiFullStackkContext : DbContext {
    public O365ApiFullStackkContext()
      : base("O365ApiFullStackkContext") { }

    public DbSet<PerWebUserCache> PerUserCacheList { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}