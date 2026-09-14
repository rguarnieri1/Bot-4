using BotCripto.Services;

Console.WriteLine(@"
╔════════════════════════════════════════════════════════╗
║       🤖 BOT 1 - ERTF-Crypto - VERSIONE 1.1            ║
║                 Backtest + Live Trading                ║
╚════════════════════════════════════════════════════════╝
");

// Check se è backtest mode
var cmdArgs = Environment.GetCommandLineArgs();
bool isBacktestMode = cmdArgs.Length > 1 && cmdArgs[1].ToLower() == "--backtest";

if (isBacktestMode)
{
    await RunBacktestAsync();
}
else
{
    await RunLiveAsync();
}

async Task RunLiveAsync()
{
    var scheduler = new BotSchedulerService(initialCapital: 150m);

    Console.WriteLine("Strategia Principale Attiva:");
    Console.WriteLine("  ⭐ SMA Strategy (Simple Moving Average)");
    Console.WriteLine("     • Win Rate Atteso: 55-60%");
    Console.WriteLine("     • Configurazione: SMA 10, 50, 200");
    Console.WriteLine("     • Filtri: Volume, Momentum, Candle Confirmation");
    Console.WriteLine("\nImpostazioni:");
    Console.WriteLine("  • Capitale Iniziale: €150.00");
    Console.WriteLine("  • Intervallo Monitoraggio: 30 minuti");
    Console.WriteLine("  • Max Stocks: 25 titoli italiani");
    Console.WriteLine("  • Risk per Trade: 2% (€3.00)");
    Console.WriteLine("  • Max Position Size: 10% (€15.00)");
    Console.WriteLine("  • Data Source: Interactive Brokers (Real-Time)");
    Console.WriteLine("  • Notifiche Desktop: Abilitate");
    Console.WriteLine("  • Tracking Metriche: Ogni 10 cicli");

    try
    {
        await scheduler.StartAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Errore: {ex.Message}");
    }
    finally
    {
        scheduler.Stop();
    }
}

async Task RunBacktestAsync()
{
    Console.WriteLine("\n🔍 MODALITA' BACKTEST SINTETICO ATTIVATA\n");

    var backtest = new SyntheticBacktest(initialCapital: 100m);

    try
    {
        // Esegui backtest su 365 giorni (1 anno)
        // con 20 simboli, 55% win-rate atteso, 8 trade per simbolo
        var result = backtest.RunBacktest(
            numSymbols: 20,
            daysOfData: 365,
            expectedWinRate: 0.55m,
            tradesPerSymbol: 8
        );

        // Stampa report dettagliato
        backtest.PrintBacktestReport(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Errore nel backtest: {ex.Message}\n{ex.StackTrace}");
    }
}

