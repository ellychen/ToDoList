using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ToDoList.Operation.Entity;

namespace ToDoList.Operation
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DbContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<SYS_USERS> SYS_USERS { get; set; }

        public DbSet<TASK> TASK { get; set; }
        public DbSet<TASK_TIMES> TASK_TIMES { get; set; }
        public DbSet<TASK_TAGS> TASK_TAGS { get; set; }
        public DbSet<TAG> TAG { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SYS_USERS>(eb => { eb.HasKey(e => new { e.NO_ACCOUNT }); })
                        .Entity<TASK>(eb => { eb.HasKey(e => new { e.PK }); })
                        .Entity<TASK_TIMES>(eb => { eb.HasKey(e => new { e.PK, e.NO_SEQ }); })
                        .Entity<TASK_TAGS>(eb => { eb.HasKey(e => new { e.PK_TASK, e.PK_TAG, e.DT_CREATE }); })
                        .Entity<TAG>(eb => { eb.HasKey(e => new { e.PK }); })

            ;




            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
