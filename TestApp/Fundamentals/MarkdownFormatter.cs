using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    // Markdown syntax
    // https://www.markdownguide.org/basic-syntax/

    public class MarkdownFormatter
    {
        public string FormatAsBold(string content)
        {
            Validate(content);

            return $"**{content}**";
        }

        public string FormatAsItalic(string content)
        {
            Validate(content);

            return $"*{content}*";
        }

        public static void Validate(string content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(content))
                throw new FormatException(nameof(content));
        }
    }
}
