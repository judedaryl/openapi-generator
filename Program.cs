using System.Text.RegularExpressions;
using DotLiquid;
using Microsoft.OpenApi.Readers;
using OpenAPI.Generator.Models;
Console.WriteLine("henlo");
// // See https://aka.ms/new-console-template for more information



var client = new HttpClient();
var stream = await client.GetStreamAsync("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/main/examples/v3.0/petstore.json");
var document = new OpenApiStreamReader().Read(stream, out var diagnostic);
var schemas = document.Components.Schemas;

var records = new List<Record>();

foreach(var (name, schema) in schemas) {
    if (schema.Type != "object") continue;

    var properties = new List<Property>();
    foreach(var (propertyName, property) in schema.Properties) {
        if (property == null) continue;

        var prop = new Property {
            Name = Regex.Replace(propertyName, @"\b\p{Ll}", match => match.Value.ToUpper()),
            Type = property.Type
        };
        properties.Add(prop);
    }
    var record = new Record {
        Name = name,
        Properties = properties
    };
    records.Add(record);
}

foreach(var record in records) {
    var recordTemplate = await File.ReadAllTextAsync("Templates/Record.template");
    Template template = Template.Parse(recordTemplate);
    var variables = Hash.FromAnonymousObject(record);
    var render = template.Render(variables);

    await File.WriteAllTextAsync($"{record.Name}.cs", render, default);
}
