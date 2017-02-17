namespace ArduinoConn.AMDB
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DBCLASS;

    public class DBMODEL : DbContext
    {
        public DBMODEL()
            : base("name=DBMODEL")
        {
        }
        public virtual DbSet<ArtificialIntelligence1> ArtificialIntelligence { get; set; }
        public virtual DbSet<MainLog1> MainLog { get; set; }
    }
}