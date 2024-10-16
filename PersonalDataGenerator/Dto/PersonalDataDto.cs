namespace PersonalDataGenerator.Dto;

public class PersonalDataDto
{
    public string Cpr { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }
    
}

public class CprDto
{
    public string Cpr { get; set; }
}

public class NameAndGenderDto
{
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }
}

public class NameGenderDobDto
{
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
}

public class CprNameGenderDto
{
    public string Cpr { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }
}

public class CprNameGenderDobDto
{
    public string Cpr { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
}