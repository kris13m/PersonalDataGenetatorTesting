using Microsoft.AspNetCore.Mvc;
using PersonalDataGenerator.Dto;

namespace PersonalDataGenerator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonalDataController : ControllerBase
{
    [HttpGet]

    [HttpGet("cpr")]
    public CprDto GetCpr()
    {
        PersonalData pd = new PersonalData();

        return new CprDto()
        {
            Cpr = pd.Cpr
        };
    }
    
    [HttpGet("name-gender")]
    public NameAndGenderDto GetNameGender()
    {
        PersonalData pd = new PersonalData();

        return new NameAndGenderDto()
        {
            FirstName = pd.FirstName,
            SurName = pd.SurName,
            Gender = pd.Gender
        };
    }
    
    [HttpGet("name-gender-dob")]
    public NameGenderDobDto GetNameGenderDob()
    {
        PersonalData pd = new PersonalData();

        return new NameGenderDobDto()
        {
            FirstName = pd.FirstName,
            SurName = pd.SurName,
            Gender = pd.Gender,
            DateOfBirth = pd.DateOfBirth
        };
    }
    
    [HttpGet("cpr-name-gender")]
    public CprNameGenderDto GetCprNameGender()
    {
        PersonalData pd = new PersonalData();

        return new CprNameGenderDto()
        {
            Cpr = pd.Cpr,
            FirstName = pd.FirstName,
            SurName = pd.SurName,
            Gender = pd.Gender,
        };
    }
    
    [HttpGet("cpr-name-gender-dob")]
    public CprNameGenderDobDto GetCprNameGenderDob()
    {
        PersonalData pd = new PersonalData();

        return new CprNameGenderDobDto()
        {
            Cpr = pd.Cpr,
            FirstName = pd.FirstName,
            SurName = pd.SurName,
            Gender = pd.Gender,
            DateOfBirth = pd.DateOfBirth
        };
    }


    [HttpGet("address")]
    public AddressDto GetAddress()
    {
        PersonalData pd = new PersonalData();

        return new AddressDto()
        {
            StreetName = pd.StreetName,
            StreetNumber = pd.StreetNumber,
            Floor = pd.Floor,
            Door = pd.Door,
            ZipCode = pd.ZipCode,
            Town = pd.Town
        };

    }

    [HttpGet("phonenumber")]
    public PhonenumberDto GetPhoneNumber()
    {
        PersonalData pd = new PersonalData();
        return new PhonenumberDto()
        {
            PhoneNumber = pd.PhoneNumber
        };    
    }   

    [HttpGet("persons")]
    public PersonalDataDto GetPersonalData()
    {
        PersonalData pd = new PersonalData();

        return new PersonalDataDto()
        {
            Cpr = pd.Cpr,
            FirstName = pd.FirstName,
            SurName = pd.SurName,
            Gender = pd.Gender,
            DateOfBirth = pd.DateOfBirth,
            StreetName = pd.StreetName,
            StreetNumber = pd.StreetNumber,
            Floor = pd.Floor,
            Door = pd.Door,
            ZipCode = pd.ZipCode,
            Town = pd.Town,
            PhoneNumber = pd.PhoneNumber
        };
    }

    [HttpGet("persons/{count}")]
    public ActionResult<List<PersonalDataDto>> GetBulkPersonalData(int count)
    {
        if (count < 2 || count > 100)
        {
            return BadRequest("The number of persons must be between 2 and 100.");
        }

        var persons = new List<PersonalDataDto>();
        var pdList = new PersonalData().GenerateFakePersons(count);

        foreach (var pd in pdList)
        {
            persons.Add(new PersonalDataDto
            {
                Cpr = pd.Cpr,
                FirstName = pd.FirstName,
                SurName = pd.SurName,
                Gender = pd.Gender,
                DateOfBirth = pd.DateOfBirth,
                StreetName = pd.StreetName,
                StreetNumber = pd.StreetNumber,
                Floor = pd.Floor,
                ZipCode = pd.ZipCode,
                Town = pd.Town,
                PhoneNumber = pd.PhoneNumber
            });
        }

        return Ok(persons);
    }
    

}