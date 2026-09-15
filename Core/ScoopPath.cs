
namespace Coop.Core;

public class ScoopPath
{
	private readonly string ScoopRootPath;
	ScoopPath(string? pathFromArg)
	{
		if (!string.IsNullOrEmpty(pathFromArg)) { ScoopRootPath = pathFromArg; return; }
		var pathFromEnv = Environment.GetEnvironmentVariable("SCOOP");
		if (!string.IsNullOrEmpty(pathFromEnv)) { ScoopRootPath = pathFromEnv; return; }

		var defaultScoopPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		Console.WriteLine($"Warning: Scoop Path not found, fallback to {defaultScoopPath}");
		ScoopRootPath = defaultScoopPath;
		return;
	}

	public string GetAppsPath()
	{
		return Path.Join(ScoopRootPath, "apps");
	}
	public string GetBucketsRootPath()
	{
		return Path.Join(ScoopRootPath, "buckets");
	}
	public string GetBucketPath(string bucket){
		var bucketPath = Path.Join(GetBucketsRootPath(),bucket);
		if(Path.Exists(bucketPath)){return bucketPath;}
		else{
			throw new DirectoryNotFoundException();
		}

	}


}
