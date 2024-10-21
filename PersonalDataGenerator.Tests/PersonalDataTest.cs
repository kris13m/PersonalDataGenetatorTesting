using NuGet.Frameworks;

namespace PersonalDataGenerator.Tests;

public class PersonalDataTest
{
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(50)]
    [InlineData(99)]
    [InlineData(100)]
    public void Test_GenerateFakePersons_ValidNumberOfPersons(int amountOfPersons)
    {
        var pd = new PersonalData();

        // Generate a number of fake persons
        var persons = pd.GenerateFakePersons(amountOfPersons);

        // Assert that the number of persons were generated
        Assert.Equal(amountOfPersons, persons.Count);

        // Check that each person has a valid CPR, FirstName, SurName, and Gender
        foreach (var person in persons)
        {
            Assert.False(string.IsNullOrEmpty(person.Cpr), "CPR should not be null or empty");
            Assert.False(string.IsNullOrEmpty(person.FirstName), "First name should not be null or empty");
            Assert.False(string.IsNullOrEmpty(person.SurName), "Surname should not be null or empty");
            Assert.False(string.IsNullOrEmpty(person.Gender), "Gender should not be null or empty");

            // Check that the CPR format is correct
            Assert.Matches(@"^\d{6}-\d{4}$", person.Cpr);

            // Check that the phone number format is correct
            Assert.Matches(@"^\d{2} \d{2} \d{2} \d{2}$", person.PhoneNumber);

            // Check that the address is valid
            Assert.False(string.IsNullOrEmpty(person.StreetName), "Street name should not be null or empty");
            Assert.False(string.IsNullOrEmpty(person.StreetNumber), "Street number should not be null or empty");
            Assert.False(string.IsNullOrEmpty(person.ZipCode), "ZipCode should not be null or empty");
            Assert.False(string.IsNullOrEmpty(person.Town), "Town should not be null or empty");
        }
    }

    [Fact]
    public void Test_GenerateFakePersons_InvalidNumberOfPersons_ThrowsException()
    {
        var pd = new PersonalData();

        // Assert that an exception is thrown for less than 2 persons
        Assert.Throws<ArgumentOutOfRangeException>(() => pd.GenerateFakePersons(1));

        // Assert that an exception is thrown for more than 100 persons
        Assert.Throws<ArgumentOutOfRangeException>(() => pd.GenerateFakePersons(101));
    }
    
    
    [Fact]
    public void Test_Male_Uneven_Last_CprDigit()
    {
        var pd = new PersonalData();

        pd.Gender = "male";
        pd.SetCpr();

        var lastDigitAsString = pd.Cpr.Last().ToString();
        var lastDigitAsInt = Int32.Parse(lastDigitAsString);
        var moduloResult = (lastDigitAsInt % 2);

        Assert.Equal(1, moduloResult);
    }

    [Fact]
    public void Test_Female_Even_Last_CprDigit()
    {
        var pd = new PersonalData();

        pd.Gender = "female";
        pd.SetCpr();

        var lastDigitAsString = pd.Cpr.Last().ToString();
        var lastDigitAsInt = Int32.Parse(lastDigitAsString);
        var moduloResult = (lastDigitAsInt % 2);

        Assert.Equal(0, moduloResult);
    }
    [Fact]
    public void Test_StreetName_AlphabeticOnly()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        Assert.Matches("^[a-zA-ZæøåÆØÅ]+$", pd.StreetName);
    }

    [Fact]
    public void Test_StreetNumber_ValidRangeAndOptionalLetter()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        Assert.Matches(@"^\d{1,3}[A-Z]?$", pd.StreetNumber);
    }

    [Fact]
    public void Test_Floor_EitherStOrValidNumber()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        // Assert that the floor is either "st" or a number between 1 and 99
        Assert.Matches(@"^st$|^[1-9][0-9]?$", pd.Floor);
    }

    [Fact]
    public void Test_Door_ValidFormat()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        // Assert that the door is one of the allowed formats: "th", "mf", "tv" or pattern like "c3", "d-14"
        Assert.Matches(@"^th|mf|tv|[a-z]\d{1,2}$|[a-z]-\d{1,3}$", pd.Door);
    }

    [Fact]
    public void Test_PostalCode_ValidFourDigitCode()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        // Assert that the postal code is a valid four-digit number
        Assert.Matches(@"^\d{4}$", pd.ZipCode);
    }

    [Fact]
    public void Test_Town_AlphabeticOnly()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        // Assert that the town name contains only alphabetic characters and spaces
        Assert.Matches("^[a-zA-ZæøåÆØÅ ]+$", pd.Town);
    }

    [Fact]
    public void Test_GetAddressAsArray_ReturnsCorrectArrayLength()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        string[] addressArray = pd.GetAddressAsArray();

        // Assert that the array contains exactly 6 elements (StreetName, StreetNumber, Floor, Door, ZipCode, Town)
        Assert.Equal(6, addressArray.Length);

        Console.WriteLine(addressArray);
    }

    [Fact]
    public void Test_GetAddressAsArray_ReturnsValidElements()
    {
        var pd = new PersonalData();
        pd.SetAddress();

        string[] addressArray = pd.GetAddressAsArray();

        // Check that each element in the array is not null or empty
        foreach (var element in addressArray)
        {
            Assert.False(string.IsNullOrEmpty(element));
        }
    }

    [Fact]
    public void Test_PhoneNumber_ValidFormat()
    {
        var pd = new PersonalData();
        pd.SetPhoneNumber();
        // Assert that the phone number format is correct
        Assert.Matches(@"^\d{2} \d{2} \d{2} \d{2}$", pd.PhoneNumber);
    }
    
    [Fact]
    public void Test_CPR_has_format_ddMMyy_and_matches_DateOfBirth()
    {
        var pd = new PersonalData();

        Assert.Equal(pd.Cpr.ToString().Substring(0, 2), pd.DateOfBirth.Day.ToString("00"));
        Assert.Equal(pd.Cpr.ToString().Substring(2, 2), pd.DateOfBirth.Month.ToString("00"));
        Assert.Equal(pd.Cpr.ToString().Substring(4, 2), pd.DateOfBirth.Year.ToString().Substring(2, 2));
    }

    [Fact]
    public void Test_GenerateFakePersons_UniquePhoneNumbers()
    {

        var pd = new PersonalData();
        var numberOfPersons = 100;

        // Generate fake persons
        var persons = pd.GenerateFakePersons(numberOfPersons);

        // Create a HashSet to store unique phone numbers
        var phoneNumbers = new HashSet<string>();

        foreach (var person in persons)
        {
            // Attempt to add the phone number to the HashSet
            // If the phone number is already present, the count will not increase
            Assert.True(phoneNumbers.Add(person.PhoneNumber), "Duplicate phone number found: " + person.PhoneNumber);
        }

    }

}
