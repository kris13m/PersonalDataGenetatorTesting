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
}