using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DatabaseModel;
using DataLayer.DatabaseModel.CasinoModel;
using Microsoft.Extensions.Logging;

namespace DataLayer
{

    public class CasinoDBContext : DbContext
    {
        //ruc server 
        const string ConnectionString = "host=cit.ruc.dk;db=cit11;uid=cit11;pwd=nICrojAxtDeX";

        //siemje - localhost database
        //const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=postgres";

        //atru - localhost database
        //const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=Bqm33etj";

        /* CASINO MODEL */
        public DbSet<Game> Games { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Salt> Salts { get; set; }
        public DbSet<MoneyPot> MoneyPots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //BASE
            /*
            modelBuilder.Entity<X>().ToTable("TABLENAME");
            modelBuilder.Entity<X>().HasKey(x => new { x.KEY }).HasName("KEYNAME");
            modelBuilder.Entity<X>().Property(x => x.PROPERTYNAME).HasColumnName("COLUMNNAME");
            */


            //CASINO MODEL

            //GAMES
            modelBuilder.Entity<Game>().ToTable("game");
            modelBuilder.Entity<Game>().HasKey(x => new { x.Gid }).HasName("game_pkey");
            modelBuilder.Entity<Game>().Property(x => x.Gid).HasColumnName("gid");
            modelBuilder.Entity<Game>().Property(x => x.Name).HasColumnName("name");
            modelBuilder.Entity<Game>().Property(x => x.MinBet).HasColumnName("minbet");
            modelBuilder.Entity<Game>().Property(x => x.MaxBet).HasColumnName("maxbet");
            modelBuilder.Entity<Game>().Property(x => x.Pid).HasColumnName("pid");

            modelBuilder.Entity<Game>()
                .HasOne(x => x.MoneyPot)
                .WithOne(x => x.Game)
                .HasForeignKey<Game>(x => x.Pid);

            //MONEYPOTS
            modelBuilder.Entity<MoneyPot>().ToTable("moneypot");
            modelBuilder.Entity<MoneyPot>().HasKey(x => new { x.Pid }).HasName("moneypot_pkey");
            modelBuilder.Entity<MoneyPot>().Property(x => x.Pid).HasColumnName("pid");
            modelBuilder.Entity<MoneyPot>().Property(x => x.Amount).HasColumnName("amount");

            //USERS
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Customer>().HasKey(x => new { x.Uid }).HasName("customer_pkey");
            modelBuilder.Entity<Customer>().Property(x => x.Uid).HasColumnName("uid");
            modelBuilder.Entity<Customer>().Property(x => x.Name).HasColumnName("name");
            modelBuilder.Entity<Customer>().Property(x => x.BirthDate).HasColumnName("birthdate");
            modelBuilder.Entity<Customer>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<Customer>().Property(x => x.Balance).HasColumnName("balance");

            //BETS
            modelBuilder.Entity<Bet>().ToTable("bets");
            modelBuilder.Entity<Bet>().HasKey(x => new { x.Uid, x.Gid }).HasName("bets_pkey");
            modelBuilder.Entity<Bet>().Property(x => x.Uid).HasColumnName("uid");
            modelBuilder.Entity<Bet>().Property(x => x.Gid).HasColumnName("gid");
            modelBuilder.Entity<Bet>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Bet)
                .HasForeignKey(x => x.Gid);
            modelBuilder.Entity<Bet>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Bet)
                .HasForeignKey(x => x.Uid);
            modelBuilder.Entity<Bet>().Property(x => x.Amount).HasColumnName("amount");
            modelBuilder.Entity<Bet>().Property(x => x.Date).HasColumnName("date");


            //SALT
            modelBuilder.Entity<Salt>().ToTable("salt");
            modelBuilder.Entity<Salt>().HasKey(x => new { x.Uid }).HasName("salt_pkey");
            modelBuilder.Entity<Salt>().Property(x => x.SSalt).HasColumnName("salt");
            modelBuilder.Entity<Salt>()
               .HasOne(x => x.Customer)
               .WithOne(x => x.Salt)
               .HasForeignKey<Salt>(x => x.Uid);


        }

    }

}