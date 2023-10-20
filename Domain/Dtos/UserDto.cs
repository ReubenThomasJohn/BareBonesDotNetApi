using System.Xml.Serialization;

namespace BareBonesDotNetApi.Entities;

public class UserDto
{
    [XmlElement(ElementName = "Username")]
    public required string Username { get; set; }
    [XmlElement(ElementName = "Password")]
    public required string Password { get; set; }
}