namespace SimpleTrader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel
    {
        string ErrorMessage { set; }
        string SearchResultSymbol { set; }
        double StockPrice { set; }
        string Symbol { get; }
    }
}