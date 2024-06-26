namespace Frogomen.Messaging.Domain.Entities.S3
{
	public class S3File
	{
		public string? Path { get; set; }
		public Stream? Stream { get; set; }
		public string? ContentType { get; set; }
		public long ContentLength { get; set; }
	}
}
