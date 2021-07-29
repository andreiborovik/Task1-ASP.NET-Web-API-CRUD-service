using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Formatters
{
    public class InputCSVFormatter : TextInputFormatter
    {
        public InputCSVFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(User);
        }
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            using var reader = new StreamReader(httpContext.Request.Body, encoding);
            var nameLine = await ReadLineAsync(reader);
            var split = nameLine.Split(";".ToCharArray());
            var user = new User { Name = split[0] };
            return await InputFormatterResult.SuccessAsync(user);
        }
        private static async Task<string> ReadLineAsync(StreamReader reader)
        {
            var line = await reader.ReadLineAsync();

            return line;
        }
    }
}
