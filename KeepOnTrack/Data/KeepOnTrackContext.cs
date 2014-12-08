using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using KeepOnTrack.Models;

namespace KeepOnTrack.Data {

  public class KeepOnTrackContext : DbContext {
    public KeepOnTrackContext()
      : base("KeepOnTrackContext") { }

    public DbSet<PerWebUserCache> PerUserCacheList { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}