﻿////////////////////////////////////////////////
// © https://github.com/badhitman - @FakeGov 
////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore;
using SharedLib;

namespace DbcLib;

/// <inheritdoc/>
public abstract partial class StockSharpAppLayerContext : DbContext
{
    /// <summary>
    /// FileName
    /// </summary>
    private static readonly string _ctxName = nameof(StockSharpAppContext);

    /// <summary>
    /// db Path
    /// </summary>
    public static string DbPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _ctxName, $"{(AppDomain.CurrentDomain.FriendlyName.Equals("ef", StringComparison.OrdinalIgnoreCase) ? "StockSharpAppData" : AppDomain.CurrentDomain.FriendlyName)}.db3");


    /// <inheritdoc/>
    public StockSharpAppLayerContext(DbContextOptions options)
        : base(options)
    {
        FileInfo _fi = new(DbPath);

        if (_fi.Directory?.Exists != true)
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath)!);

        Database.Migrate();
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
#if DEBUG
        options.EnableSensitiveDataLogging(true);
        // options.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
#endif
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FixMessageAdapterModelDB>()
        .HasKey(b => b.Id);

        modelBuilder.Entity<FixMessageAdapterModelDB>()
        .HasIndex(b => b.LastUpdatedAtUTC);

        modelBuilder.Entity<FixMessageAdapterModelDB>()
        .HasIndex(b => b.Name);

        modelBuilder.Entity<FixMessageAdapterModelDB>()
        .HasIndex(b => b.IsOnline);

        modelBuilder.Entity<FixMessageAdapterModelDB>()
        .HasIndex(b => b.AdapterTypeName);

        modelBuilder.Entity<PortfolioTradeModelDB>()
       .HasKey(b => b.Id);

        modelBuilder.Entity<InstrumentStockSharpModelDB>()
       .HasKey(b => b.Id);

        modelBuilder.Entity<OrderStockSharpModelDB>()
       .HasKey(b => b.IdPK);

        modelBuilder.Entity<MyTradeStockSharpModelDB>()
       .HasKey(b => b.Id);

        modelBuilder.Entity<InstrumentMarkersModelDB>()
       .HasKey(b => b.Id);
    }


    /// <inheritdoc/>
    public DbSet<FixMessageAdapterModelDB> Adapters { get; set; }

    /// <inheritdoc/>
    public DbSet<PortfolioTradeModelDB> Portfolios { get; set; }

    /// <inheritdoc/>
    public DbSet<ExchangeStockSharpModelDB> Exchanges { get; set; }

    /// <inheritdoc/>
    public DbSet<BoardStockSharpModelDB> Boards { get; set; }

    /// <inheritdoc/>
    public DbSet<InstrumentStockSharpModelDB> Instruments { get; set; }

    /// <inheritdoc/>
    public DbSet<InstrumentMarkersModelDB> InstrumentsMarkers { get; set; }

    /// <inheritdoc/>
    public DbSet<OrderStockSharpModelDB> Orders { get; set; }

    /// <inheritdoc/>
    public DbSet<MyTradeStockSharpModelDB> MyTrades { get; set; }
}