using DotLiquid;
using OpenAPI.Generator.Enums;

namespace OpenAPI.Generator.Models;

public class Property : Drop {
    public required string Name { get; set; }
    public required string Type { get; set; }
    public Property[] Properties { get; set; } = new Property[] {};
}