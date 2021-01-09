using SimpleTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> _filterAssets;
        private readonly ObservableCollection<AssetViewModel> _assets;

        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assets => assets) { }

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
        {
            _assetStore = assetStore;
            _filterAssets = filterAssets;
            _assets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            assetViewModels = _filterAssets(assetViewModels);

            DisposeAssets();
            _assets.Clear();
            foreach (AssetViewModel viewModel in assetViewModels)
            {
                _assets.Add(viewModel);
            }
        }

        private void DisposeAssets()
        {
            foreach (AssetViewModel asset in _assets)
            {
                asset.Dispose();
            }
        }

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }

        public override void Dispose()
        {
            _assetStore.StateChanged -= AssetStore_StateChanged;
            DisposeAssets();

            base.Dispose();
        }
    }
}
