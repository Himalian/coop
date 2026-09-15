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

	/// <summary>
	/// Get path for given bucket name
	/// </summary>
	/// <param name="bucket">The name of the bucket.</param>
	/// <returns>The full path to the bucket directory.</returns>
	/// <exception cref="DirectoryNotFoundException">
	/// Thrown when the directory corresponding to <paramref name="bucket"/> does not exist.
	/// </exception>
	/// <example>
	/// <code>
	/// // Example usage:
	/// var path = ScoopPath.GetBucketPath("main");
	/// // Returns: D:/Scoop/buckets/main
	/// 
	/// ScoopPath.GetBucketPath("asdf");
	/// // Throws DirectoryNotFoundException
	/// </code>
	/// </example>
	public string GetBucketPath(string bucket)
	{
		var bucketPath = Path.Join(BucketsPath, bucket);
		if (Path.Exists(bucketPath))
		{
			return bucketPath;
		}

		throw new DirectoryNotFoundException($"Bucket not found: {bucketPath}");
	}

	/// <summary>
	/// list bucket name in `/scoop/buckets/`
	/// </summary>
	/// <todo>null safty and unit tests</todo>
	public string[] ListBucketName()
	{
		string[] bucketName = Directory.GetDirectories(BucketsPath).Select(Path.GetFileName).ToArray()!;
        return bucketName ?? throw new DirectoryNotFoundException("No buckets found in scoop");
    }
}
