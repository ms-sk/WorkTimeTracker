using System.Reflection;

namespace WorkTimeTracker.Installer.Packing
{
    public class Zip
    {
        internal long GetZipOffset(GetZipOffsetParameters parameters)
        {
            using (var processFile = new FileStream(parameters.Input, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[4];

                while (processFile.Position < processFile.Length - buffer.Length)
                {
                    processFile.Read(buffer, 0, buffer.Length);
                    processFile.Seek(-3, SeekOrigin.Current);

                    if (IsZipArchiveHeader(buffer))
                    {
                        return processFile.Seek(-1, SeekOrigin.Current);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        internal void ExtractPartialZip(ExtractPartialZipParameters parameters)
        {
            using (var tempStream = new FileStream(parameters.Temp, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var inputStream = new FileStream(parameters.Input, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    inputStream.Position = parameters.Offset;
                    inputStream.CopyTo(tempStream);
                }
            }
        }

        internal void Unzip(UnzipParameters parmeters)
        {
            using (var tempStream = new FileStream(parmeters.Temp, FileMode.Open))
            {
                using (var outputStream = new FileStream(parmeters.Output, FileMode.Create))
                {
                    tempStream.CopyTo(outputStream);
                }
            }
        }

        static bool IsZipArchiveHeader(byte[] buffer)
        {
            if (buffer == null)
            {
                return false;
            }

            if (buffer.Length != 4)
            {
                return false;
            }

            return buffer[0] == 0x50 && buffer[1] == 0x4B && buffer[2] == 0x03 && buffer[3] == 0x04;
        }
    }
}