using System.IO.Abstractions;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Application.Queries;

namespace TheatricalPlayersRefactoring.Application.Services
{
    public interface IFileBuilder
    {
        string CreateFile(Guid id, StatementFormat format, string directoryPath, string content);
        Task<FileDto> ReadFileAsync(string filePath);
    }

    public class FileBuilder : IFileBuilder
    {
        private readonly IFileSystem _fileSystem;
        public FileBuilder(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public string CreateFile(Guid id, StatementFormat format, string directoryPath, string content)
        {
            var fileName = $"bill_{id}_{DateTime.UtcNow:yyyyMMddHHmmss}.{Format(format)}";

            var filePath = _fileSystem.Path.Combine(directoryPath, fileName);

            _fileSystem.File.WriteAllText(filePath, content);

            return filePath;
        }

        public string Format(StatementFormat format)
        {
            switch (format)
            {
                case StatementFormat.Text:
                    return "txt";
                case StatementFormat.Xml:
                    return "xml";
                default:
                    throw new Exception("Formato não suportado");
            }
        }

        public async Task<FileDto> ReadFileAsync(string filePath)
        {
            var content = await _fileSystem.File.ReadAllTextAsync(filePath);
            var format = _fileSystem.Path.GetExtension(filePath).TrimStart('.');

            var contentType = format.ToLower() switch
            {
                "html" => "text/html",
                "txt" => "text/plain",
                _ => "text/plain"
            };

            return new FileDto(content,format, contentType);                
        }
    }
}