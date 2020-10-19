using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sentences.Core;

namespace Sentences.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase
    {
        private readonly ILogger<TextController> _logger;
        private readonly ITextParser _parser;
        
        public TextController(ILogger<TextController> logger, ITextParser parser)
        {
            _logger = logger;
            _parser = parser;
        }
        
        [HttpGet]
        public IActionResult Get(string format, string text)
        {
            try
            {
                var sentences = _parser.Parse(text);
                var formatter = MakeTextFormatter(format);
                return Ok(formatter.Format(sentences));
            }
            catch (Exception e)
            {
                _logger.LogError("Text processing has failed.", e);
                return NoContent();
            }
        }

        private ITextFormatter MakeTextFormatter(string format)
        {
            return format switch
            {
                "xml" => new XmlTextFormatter(),
                "csv" => new CsvTextFormatter(),
                _ => throw new NotImplementedException()
            };
        }
    }
}