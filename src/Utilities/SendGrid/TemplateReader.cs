using System.Collections.Generic;
using System.IO;
using HandlebarsDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Portolo.Email
{
    public class TemplateReader
    {
        public static string WorkingDirectory = string.Empty;

        private static readonly IDictionary<string, EmailTemplate> Templates = new Dictionary<string, EmailTemplate>();

        private readonly IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();

        private readonly string sourcePath;

        public TemplateReader(string sourcePath, string partialsPath = "")
        {
            this.sourcePath = sourcePath;
            if (!string.IsNullOrEmpty(partialsPath))
            {
                this.RegisterPartials(partialsPath);
            }
        }

        public EmailTemplate GetTemplate(string name)
        {
            if (!Templates.TryGetValue(name, out var template))
            {
                var source = this.deserializer.Deserialize<TemplateFile>(this.ReadTemplate(name));
                Templates[name] = template = new EmailTemplate
                {
                    Name = name,
                    Subject = Handlebars.Compile(source.Subject),
                    BodyText = Handlebars.Compile(source.BodyText),
                    BodyHtml = Handlebars.Compile(source.BodyHtml)
                };
            }

            return template;
        }

        private void RegisterPartials(string partialsPath)
        {
            foreach (var partial in Directory.EnumerateFiles(Path.Combine(WorkingDirectory, partialsPath)))
            {
                Handlebars.RegisterTemplate(Path.GetFileNameWithoutExtension(partial), File.ReadAllText(partial));
            }
        }

        private string ReadTemplate(string name)
        {
            return File.ReadAllText($"{Path.Combine(WorkingDirectory, this.sourcePath, name)}.yml");
        }

        private class TemplateFile
        {
            public string Subject { get; set; }
            public string BodyHtml { get; set; }
            public string BodyText { get; set; }
        }
    }
}
