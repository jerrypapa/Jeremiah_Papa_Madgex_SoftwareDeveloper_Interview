using Core.Entities;
using Core.Interfaces;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Infrastructure.Repositories
{
    public class FileVersionRepository : IVersionRepository
    {
        private const string FilePath = "D:\\Test\\Madgex.txt"; // Adjust the path as needed



        public async Task<VersionNumber> GetVersion()
        {
            if (!File.Exists(FilePath))
            {
                return new VersionNumber(2,2,21, 0);//For test purposes incase we dont have a file path yet
            }

            try
            {
                string versionString = await File.ReadAllTextAsync(FilePath);
                var parts = versionString.Split('.');


                if (parts.Length < 4 || !int.TryParse(parts[0], out int firstNo) || !int.TryParse(parts[1], out int secondNo)||!int.TryParse(parts[2], out int major) || !int.TryParse(parts[3], out int minor))
                {
                    throw new FileFormatException("Invalid version format in the file.");
                }

                return new VersionNumber(firstNo, secondNo, major, minor);
            }
            catch (Exception ex)
            {
                Log.Error(new FileFormatException("Error reading version of file.", ex).ToString());
                return new VersionNumber(0,0,0, 0);
            }
        }

        public async Task UpdateVersion(VersionNumber version)
        {
            try
            {
                var currentVersion = await GetVersion();

                var updatedVersionString = $"{version.FirstNumber}.{version.SecondNumber}.{version.Major}.{version.Minor}";

                await File.WriteAllTextAsync(FilePath, updatedVersionString);
            }
            catch (Exception ex)
            {
                throw new FileFormatException("Error writing to version file.", ex);
            }
        }
    }
}