namespace WorkTimeTracker.Installer.Packing
{
    internal sealed class ExtractPartialZipParameters
    {
        public string Input { get; set; }

        public string Temp { get; set; }

        public long Offset { get; set; }
    }
}