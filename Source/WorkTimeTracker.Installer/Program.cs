using System.Text.Json;

var filePath = "Files.json";
var sourceDirectory = "";
var targetDirectory = "";

var files = JsonSerializer.Deserialize<List<string>>(filePath);

foreach (var file in files)
{
    var path = Path.Combine(sourceDirectory, file);
    File.Copy(path, targetDirectory, true);
}