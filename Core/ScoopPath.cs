
namespace Coop.Core;

public class ScoopPath
{
	readonly string RootPath;
	// /scoop/buckets
	public string BucketsPath => Path.Combine(RootPath, "buckets");
	// /scoop/apps
	public string AppsPath => Path.Combine(RootPath, "apps");
	public ScoopPath()
	{
		var pathFromEnv = Environment.GetEnvironmentVariable("SCOOP");
		if (!string.IsNullOrEmpty(pathFromEnv)) { RootPath = pathFromEnv; return; }

		var defaultScoopPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		Console.WriteLine($"Warning: Scoop Path not found, fallback to {defaultScoopPath}");
		RootPath = defaultScoopPath;
		return;
	}

	public string GetBucketPath(string bucket)
	{
		var bucketPath = Path.Join(BucketsPath, bucket);
		if (Path.Exists(bucketPath)) { return bucketPath; }
		else
		{
			throw new DirectoryNotFoundException();
		}

	}


}
