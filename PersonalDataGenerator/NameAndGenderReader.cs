namespace PersonalDataGenerator;

using System.Text.Json;
using System.Text.Json.Serialization;


/*
 * Class used to deserialize json
 */
public class Persons
{
    [JsonPropertyName("persons")]
    public List<NameAndGender> NameAndGenderList { get; set; }
    
}

/*
 * Class used to deserialize json
 */
public class NameAndGender
{
    [JsonPropertyName("name")]
    public string FirstName { get; set; }
    [JsonPropertyName("surname")]
    public string SurName { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
}

public class NameAndGenderReader
{
    
    public List<NameAndGender> NameAndGenderList = new List<NameAndGender>();

    public NameAndGenderReader()
    {
        ReadNamesAndGendersFromJson();
    }

    public void ReadNamesAndGendersFromJson()
    {
        string filePath = "./Data/person-names.json";
        string jsonString = File.ReadAllText(filePath);

        var jsonData = JsonSerializer.Deserialize<Persons>(jsonString);
        
        foreach (var nameAndGender in jsonData.NameAndGenderList)
        {
            NameAndGenderList.Add(nameAndGender);
        }
    }
}