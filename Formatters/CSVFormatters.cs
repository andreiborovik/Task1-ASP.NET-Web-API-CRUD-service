using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Task1.Formatters
{
    public class CSVFormatters : TextOutputFormatter
    {
        public CSVFormatters()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return true;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            await context.HttpContext.Response.WriteAsync(context.Object.ToString(),selectedEncoding);
        }
    }
}
