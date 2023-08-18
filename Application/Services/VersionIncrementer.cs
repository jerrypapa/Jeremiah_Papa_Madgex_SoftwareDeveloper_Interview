using ApplicationVersionProcessor.Core.Interfaces;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VersionIncrementer : IVersionIncrementer
    {
        private readonly IVersionRepository _versionRepository;

        public VersionIncrementer(IVersionRepository versionRepository)
        {
            _versionRepository = versionRepository;
        }
        /// <summary>
        /// Increment the version number based on the release type.
        /// </summary>
        /// <param name="releaseType">The type of release (Feature or BugFix).</param>
        /// <returns>The updated version number.</returns>
        public async Task<VersionNumber> IncrementVersion(string releaseType)
        {
            var version =await _versionRepository.GetVersion();

            releaseType = releaseType.ToLower(); // Normalize input

            var updatedVersion = releaseType switch
            {
                "feature" => new VersionNumber(version.FirstNumber,version.SecondNumber,version.Major + 1, 0),
                "bugfix" => new VersionNumber(version.FirstNumber, version.SecondNumber,version.Major, version.Minor + 1),
                _ => throw new InvalidReleaseTypeException(),
            };
           
            await _versionRepository.UpdateVersion(updatedVersion);

            return updatedVersion;
        }
    }

}
