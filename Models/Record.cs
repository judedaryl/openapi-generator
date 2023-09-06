using DotLiquid;

namespace OpenAPI.Generator.Models;

public class Record : Drop {
    public required string Name { get; set; }
    public required IEnumerable<Property> Properties { get; set; }
}