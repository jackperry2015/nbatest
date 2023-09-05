using Microsoft.EntityFrameworkCore;
using nba_API.Db.Models;
using nba_API.Dtos;

namespace nba_API.Db;

public partial class NbaContext : DbContext
{
    public NbaContext()
    {
        
    }

    public NbaContext(DbContextOptions<NbaContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<NbaTableDto> NbaTable { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }

    // When outside of test ENV call connection string in config rather than hardcoded local
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.GameId)
                .ValueGeneratedNever()
                .HasColumnName("GameID");
            entity.Property(e => e.AwayTeamId).HasColumnName("AwayTeamID");
            entity.Property(e => e.GameDateTime).HasColumnType("datetime");
            entity.Property(e => e.HomeTeamId).HasColumnName("HomeTeamID");
            entity.Property(e => e.MvpplayerId).HasColumnName("MVPPlayerID");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.GameAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Games_Teams1");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.GameHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Games_Teams");

            entity.HasOne(d => d.Mvpplayer).WithMany(p => p.Games)
                .HasForeignKey(d => d.MvpplayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Games_Players");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.PlayerId)
                .ValueGeneratedNever()
                .HasColumnName("PlayerID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("TeamID");
            entity.Property(e => e.Logo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Stadium)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TeamPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId);

            entity.ToTable("Team_Player");

            entity.Property(e => e.PlayerId)
                .ValueGeneratedNever()
                .HasColumnName("PlayerID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Player).WithOne(p => p.TeamPlayer)
                .HasForeignKey<TeamPlayer>(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team_Player_Players");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team_Player_Teams");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
