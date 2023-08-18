using ApplicationVersionProcessor.Core.Interfaces;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VersionProvider : IVersionProvider
    {
        private readonly IVersionRepository _versionRepository;

        public VersionProvider(IVersionRepository versionRepository)
        {
            _versionRepository = versionRepository;
        }
        /// <summary>
        /// Get the current version number.
        /// I will also test invalid File formats in one of the tests
        /// </summary>
        /// <returns>The current version number.</returns>
        /// 
        public async Task<VersionNumber> GetVersion()
        {
            return await _versionRepository.GetVersion();
        }
    }
}
