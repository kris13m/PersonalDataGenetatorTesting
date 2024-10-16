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

    public PersonalData()
    {
        SetNameAndGender();
        SetDateOfBirth();
        SetCpr();
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
        
        Console.WriteLine(lastDigit % 2);
        
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

}