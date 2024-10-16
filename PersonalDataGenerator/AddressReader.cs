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
            string filePath = ""; // Adjust the path to your SQL file
            string sqlContent = File.ReadAllText(filePath);

            
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
