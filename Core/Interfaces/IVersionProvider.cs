using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionProcessor.Core.Interfaces
{
    public interface IVersionProvider
    {
        Task<VersionNumber> GetVersion();
    }
}
