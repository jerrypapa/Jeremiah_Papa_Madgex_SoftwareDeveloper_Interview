using Application.Services;
using Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Entities;
using Xunit;

namespace Tests
{
    public class VersionProviderTests
    {
        [Fact]
        public async Task GetVersion_WithInvalidFileFormat_ThrowsFileFormatException()
        {
            var versionRepo = new Mock<IVersionRepository>();
            versionRepo.Setup(repo => repo.GetVersion()).Throws<FileFormatException>();

            var versionProvider = new VersionProvider(versionRepo.Object);

            await Assert.ThrowsAsync<FileFormatException>(versionProvider.GetVersion);
        }

        [Fact]
        public async Task GetVersion_WithValidVersion_ReturnsVersionNumber()
        {
            var versionRepo = new Mock<IVersionRepository>();
            versionRepo.Setup(repo => repo.GetVersion()).ReturnsAsync(new VersionNumber(2,0,21, 1));

            var versionProvider = new VersionProvider(versionRepo.Object);
            var currentVersion = await versionProvider.GetVersion();

            Assert.Equal(2, currentVersion.FirstNumber);
            Assert.Equal(0, currentVersion.SecondNumber);
            Assert.Equal(21, currentVersion.Major);
            Assert.Equal(1, currentVersion.Minor);
        }
    }
}
