using System.Text.RegularExpressions;

namespace PersonalDataGenerator;

public class PersonalData
{
    private readonly Random _random = new Random();
    private readonly NameAndGenderReader _nameAndGenderReader = new NameAndGenderReader();
    public string Cpr { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }

    public string StreetName { get; set; }
    public string StreetNumber { get; set; }
    public string Floor { get; set; }
    public string Door { get; set; }
    public string ZipCode { get; set; }
    public string Town { get; set; }

    public string PhoneNumber { get; set; }
    public List<string> AddressAsArray { get; set; }

    public PersonalData()
    {
        SetNameAndGender();
        SetDateOfBirth();
        SetCpr();
        SetAddress();
        SetPhoneNumber();
    }


    public List<PersonalData> GenerateFakePersons(int numberOfPersons)
{
    if (numberOfPersons < 2 || numberOfPersons > 100)
    {
        throw new ArgumentOutOfRangeException("numberOfPersons", "The number of persons must be between 2 and 100.");
    }

    var persons = new List<PersonalData>();

    for (int i = 0; i < numberOfPersons; i++)
    {
        var pd = new PersonalData();
        persons.Add(pd);
    }

    return persons;
}

    /*
     * Currently doesn't account for leap years
     */
    public void SetDateOfBirth()
    {
        List<int> monthsWith31Days = new List<int>() { 1, 3, 5, 7, 8, 10, 12 }; // January, March, etc.
        List<int> monthsWith30Days = new List<int>() { 4,6,9,11 }; // April, June, etc.

        int year = _random.Next(1930, 2023);
        int month = _random.Next(1, 13);
        int day;

        if (monthsWith31Days.Contains(month))
        {
            day = _random.Next(1, 32);
        } else if (monthsWith30Days.Contains(month))
        {
            day = _random.Next(1, 31);
        } else
        {
            day = _random.Next(1, 29);
        }
        
        DateOnly newDate = new DateOnly(year, month, day);
        DateOfBirth = newDate;
    }
    
    public void SetCpr()
    {
        string cprPrefix = DateOfBirth.ToString("ddMMyy");

        int firstDigit = _random.Next(1, 10);
        int secondDigit = _random.Next(1, 10);
        int thirdDigit = _random.Next(1, 10);
        int lastDigit = _random.Next(1, 10);
        
        switch (Gender)
        {
            case "male":
                // if male has even number
                if (lastDigit % 2 == 0)
                {
                    if (lastDigit >= 5)
                    {
                        lastDigit--;
                    }
                    else
                    {
                        lastDigit++;
                    }
                }
                break;
            case "female":
                // if female has even number
                if (lastDigit % 2 == 1)
                {
                    if (lastDigit >= 5)
                    {
                        lastDigit--;
                    }
                    else
                    {
                        lastDigit++;
                    }
                }
                break;
        }
        
        string cprSuffix = $"{firstDigit}{secondDigit}{thirdDigit}{lastDigit}";
        
        Cpr = $"{cprPrefix}-{cprSuffix}";
    }

    public void SetNameAndGender()
        {
            int index = _random.Next(_nameAndGenderReader.NameAndGenderList.Count);
            NameAndGender nameAndGender = _nameAndGenderReader.NameAndGenderList[index];

            FirstName = nameAndGender.FirstName;
            SurName = nameAndGender.SurName;
            Gender = nameAndGender.Gender;
        }

    public void SetAddress()
{
    AddressReader addressReader = new();

    
    StreetName = GenerateRandomAlphabeticString(_random.Next(5, 15));

    StreetNumber = _random.Next(1, 1000).ToString();
    if (_random.Next(0, 2) == 1)
    {
        char letter = (char)_random.Next('A', 'Z' + 1);
        StreetNumber += letter;
    }

    
    Floor = _random.Next(0, 2) == 0 ? "st" : _random.Next(1, 100).ToString();

    
    string[] doorOptions = { "th", "mf", "tv" };
    if (_random.Next(0, 2) == 0)
    {
        Door = doorOptions[_random.Next(doorOptions.Length)];
    }
    else
    {
        char letter = (char)_random.Next('a', 'z' + 1);
        Door = _random.Next(0, 2) == 0 ? $"{letter}{_random.Next(1, 51)}" : $"{letter}-{_random.Next(1, 100)}";
    }

    var randomAddress = addressReader.PostalCodeAndTownList[_random.Next(addressReader.PostalCodeAndTownList.Count)];
    ZipCode = randomAddress.ZipCode;
    Town = randomAddress.Town;

}
    public string[] GetAddressAsArray()
    {
        return
        [
            StreetName,   
            StreetNumber, 
            Floor,        
            Door,         
            ZipCode,      
            Town          
        ];
    }

private string GenerateRandomAlphabeticString(int length)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå";
    return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[_random.Next(s.Length)]).ToArray());
}



public void SetPhoneNumber()
{
    Random random = new Random();

    // Get random phone prefix
    string phone = PhonePrefixes[random.Next(0, PhonePrefixes.Length)];
    int prefixLength = phone.Length;

    // Add random digits to fill the phone number to 8 digits in total
    for (int index = 0; index < (8 - prefixLength); index++)
    {
        phone += GetRandomDigit().ToString();
    }

    this.PhoneNumber = phone;
}
private static readonly string[] PhonePrefixes = 
{
    "2", "30", "31", "40", "41", "42", "50", "51", "52", "53", "60", "61", "71", "81", "91", "92", "93", "342", 
    "344", "345", "346", "347", "348", "349", "356", "357", "359", "362", "365", "366", "389", "398", "431", 
    "441", "462", "466", "468", "472", "474", "476", "478", "485", "486", "488", "489", "493", "494", "495", 
    "496", "498", "499", "542", "543", "545", "551", "552", "556", "571", "572", "573", "574", "577", "579", 
    "584", "586", "587", "589", "597", "598", "627", "629", "641", "649", "658", "662", "663", "664", "665", 
    "667", "692", "693", "694", "697", "771", "772", "782", "783", "785", "786", "788", "789", "826", "827", "829"
};

private static int GetRandomDigit()
{
    Random random = new Random();
    return random.Next(0, 10);
}

}
