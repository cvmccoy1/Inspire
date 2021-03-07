using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Inspire.ViewModels
{
    public class HtmlToTextConverter
    {
        /// <summary>
        /// Converts HTML to Text.
        /// </summary>
        /// <param name="html">A string reprenting the HTML to convert to text.</param>
        /// <returns>The converted text string.</returns>
        /// <remarks>Leveled from code seen on stackoverflow.com.</remarks>
        public static string HTMLToText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            Regex lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            Regex stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            Regex tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            string text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}
