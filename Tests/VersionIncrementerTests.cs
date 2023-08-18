using Application.Services;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class VersionIncrementerTests
    {
        [Fact]
        public async Task IncrementVersion_WithInvalidReleaseType_ThrowsInvalidReleaseTypeException()
        {
            var versionRepo = new Mock<IVersionRepository>();
            var versionIncrementer = new VersionIncrementer(versionRepo.Object);

            await Assert.ThrowsAsync<InvalidReleaseTypeException>(() => versionIncrementer.IncrementVersion("invalid"));
        }

        [Fact]
        public async Task IncrementVersion_WithValidFeatureRelease_IncrementsMajorVersionAndResetsMinor()
        {
            var versionRepo = new Mock<IVersionRepository>();
            versionRepo.Setup(repo => repo.GetVersion()).ReturnsAsync(new VersionNumber(2,1,1, 5));

            var versionIncrementer = new VersionIncrementer(versionRepo.Object);
            var newVersion = await versionIncrementer.IncrementVersion("feature");

            Assert.Equal(2, newVersion.FirstNumber);
            Assert.Equal(1, newVersion.SecondNumber);
            Assert.Equal(2, newVersion.Major);
            Assert.Equal(0, newVersion.Minor);
        }

        [Fact]
        public async Task IncrementVersion_WithValidBugFixRelease_IncrementsMinorVersion()
        {
            var versionRepo = new Mock<IVersionRepository>();
            versionRepo.Setup(repo => repo.GetVersion()).ReturnsAsync(new VersionNumber(3,0,1, 5));

            var versionIncrementer = new VersionIncrementer(versionRepo.Object);
            var newVersion = await versionIncrementer.IncrementVersion("bugfix");

            Assert.Equal(3, newVersion.FirstNumber);
            Assert.Equal(0, newVersion.SecondNumber);
            Assert.Equal(1, newVersion.Major);
            Assert.Equal(6, newVersion.Minor);
        }
     
    }
}
