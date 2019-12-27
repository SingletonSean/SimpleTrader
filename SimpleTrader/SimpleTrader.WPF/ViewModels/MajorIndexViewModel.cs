using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexViewModel
    {
        private readonly IMajorIndexService _majorIndexService;

        public MajorIndex DowJones { get; set; }
        public MajorIndex Nasdaq { get; set; }
        public MajorIndex SP500 { get; set; }

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }

        private void LoadMajorIndexes()
        {
            _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task => 
            {
                if(task.Exception == null)
                {
                    DowJones = task.Result;
                }
            });

            _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if(task.Exception == null)
                {
                    Nasdaq = task.Result;
                }
            });

            _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if(task.Exception == null)
                {
                    SP500 = task.Result;
                }
            });
        }
    }
}