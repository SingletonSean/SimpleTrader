using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services
{
    public interface IMajorIndexService
    {
        Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
    }
}
