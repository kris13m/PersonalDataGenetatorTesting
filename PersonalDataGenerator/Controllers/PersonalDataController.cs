using Microsoft.AspNetCore.Mvc;
using PersonalDataGenerator.Dto;

namespace PersonalDataGenerator.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonalDataController : ControllerBase
{
    [HttpGet]
    public PersonalDataDto GetPersonalData()
    {
        PersonalData pd = new PersonalData();

        return new PersonalDataDto()
        {
            Cpr = pd.Cpr,
            FirstName = pd.FirstName,
            SurName = pd.SurName,
            Gender = pd.Gender,
            DateOfBirth = pd.DateOfBirth
        };
    }

    [HttpGet("/cpr")]
    public CprDto GetCpr()
    {
        PersonalData pd = new PersonalData();

        return new CprDto()
        {
            Cpr = pd.Cpr
        };
    }
    
    [HttpGet("/name-gender")]
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
    
    [HttpGet("/name-gender-dob")]
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
    
    [HttpGet("/cpr-name-gender")]
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
    
    [HttpGet("/cpr-name-gender-dob")]
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
}