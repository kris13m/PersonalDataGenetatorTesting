using System.Text.RegularExpressions;

namespace PersonalDataGenerator
{
    public class PostalCodeAndTown
    {
        public string ZipCode { get; set; }
        public string Town { get; set; }
    }

    public class AddressReader
    {
        public List<PostalCodeAndTown> PostalCodeAndTownList { get; private set; }

        public AddressReader()
        {
            PostalCodeAndTownList = new List<PostalCodeAndTown>();
            ReadPostalCodesAndTowns();
        }

        public void ReadPostalCodesAndTowns()
        {
            // Get the current directory (bin/Debug/net8.0) and move up to the project root
            string baseDirectory = AppContext.BaseDirectory;
            string projectRoot = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
    
            // Construct the path to the addresses.sql file in the project root
            string filePath = Path.Combine(projectRoot, "Data", "addresses.sql");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The addresses.sql file was not found.", filePath);
            }

            // Read the file contents
            string sqlContent = File.ReadAllText(filePath);

            // Regex pattern to extract postal codes and towns from the SQL insert statements
            string pattern = @"\('(\d{4})', '([^']+)'\)";
            var matches = Regex.Matches(sqlContent, pattern);

            foreach (Match match in matches)
            {
                PostalCodeAndTownList.Add(new PostalCodeAndTown
                {
                    ZipCode = match.Groups[1].Value,
                    Town = match.Groups[2].Value
                });
            }
        }
    }
}
