using System.Security.Cryptography;
using System.Text;
using Microsoft.Build.Framework;

namespace Microsoft.NET.Build.Tasks
{
    public class Sha256Hash : TaskBase
    {
        private HashAlgorithm HashAlgorithm = SHA256.Create();

        [Required]
        public string Input { get; set; }

        [Output]
        public string Hash { get; set; }

        override protected void ExecuteCore()
        {
            if (Input == null)
            {
                throw new BuildErrorException($"{nameof(Input)} is null.");
            }

            var hash = HashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(Input));
            
            var hashStringBuilder = new StringBuilder(hash.Length * 2);

            foreach(var hashByte in hash)
            {
                hashStringBuilder.Append($"{hashByte:x2}");
            }
            
            Hash = hashStringBuilder.ToString();
        }
    }
}