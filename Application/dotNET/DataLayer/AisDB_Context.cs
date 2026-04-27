using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AisDB_Context : DbContext
    {
        public AisDB_Context(DbContextOptions<AisDB_Context> options) : base(options) {}

        // As my once good friend once said: sanity check - We have a Db with 5 tabels,
        // so lets maybe start with those, then scale more.
        // Bookmarks for certain interest would be awesome, but moves into realm of user framwork stuff
        // Which we know how to implement with JWT and such. But lets focus on the Psql made for now.
        // I imagine we can have end points for get/vessels, this could be for a overview display page. 

        // #1 tanker_positions
        // #2 tanker_staging
        // #3 tankers
        // #4 tracked_tankers
        // #5 voyages
        public DbSet<> Users {  get; set; }

    }
}

