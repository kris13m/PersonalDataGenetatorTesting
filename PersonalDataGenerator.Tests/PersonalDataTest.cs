using NuGet.Frameworks;

namespace PersonalDataGenerator.Tests;

public class PersonalDataTest
{
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
    public void Test_GenerateFakePersons_ValidNumberOfPersons()
    {
        var pd = new PersonalData();
        
        // Generate 50 fake persons
        var persons = pd.GenerateFakePersons(50);

        // Assert that 50 persons were generated
        Assert.Equal(50, persons.Count);

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
            Assert.Matches(@"^\d{8}$", person.PhoneNumber);

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
    public void Test_DateOfBirth_Matches_CPR()
    {
        var pd = new PersonalData();

        string dateString = pd.DateOfBirth.ToString();
        string dateStringToCpr = dateString.Substring(0, 2) + dateString.Substring(3, 2)+ dateString.Substring(8, 2);
        string cpr = pd.Cpr.Substring(0,6);

        Assert.Equal(cpr,dateStringToCpr);
    }
}
