using System.Reflection;
using WorkTimeTracker.Installer.Packing;

await Main();


async Task Main()
{
    try
    {
        var workTimeTracker = "WorkTimeTracker";
        var input = Assembly.GetExecutingAssembly().Location.Replace("dll", "exe");
        var output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), workTimeTracker);
        var tmp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), workTimeTracker);

        var zipper = new Zip();
        var offset = zipper.GetZipOffset(new GetZipOffsetParameters { Input = input });
        zipper.ExtractPartialZip(new ExtractPartialZipParameters
        {
            Input = input,
            Temp = tmp,
            Offset = offset,
        });

        zipper.Unzip(new UnzipParameters
        {
            Output = output,
            Temp = tmp
        });
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}