#region License
/*
 **************************************************************
 *  Author: Rick Strahl 
 *          © West Wind Technologies, 2008 - 2009
 *          http://www.west-wind.com/
 * 
 * Created: 09/08/2008
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 **************************************************************  
*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Globalization;
using System.Linq;

namespace Westwind.Utilities
{
    /// <summary>
    /// String utility class that provides a host of string related operations
    /// </summary>
    public static class StringUtils
    {

        /// <summary>
        /// Extracts a string from between a pair of delimiters. Only the first 
        /// instance is found.
        /// </summary>
        /// <param name="source">Input String to work on</param>
        /// <param name="StartDelim">Beginning delimiter</param>
        /// <param name="endDelim">ending delimiter</param>
        /// <param name="CaseInsensitive">Determines whether the search for delimiters is case sensitive</param>
        /// <returns>Extracted string or ""</returns>
        public static string ExtractString(string source,
            string beginDelim,
            string endDelim,
            bool caseSensitive = false,
            bool allowMissingEndDelimiter = false,
            bool returnDelimiters = false)
        {
            int at1, at2;

            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (caseSensitive)
            {
                at1 = source.IndexOf(beginDelim);
                if (at1 == -1)
                    return string.Empty;

                at2 = source.IndexOf(endDelim, at1 + beginDelim.Length);
            }
            else
            {
                //string Lower = source.ToLower();
                at1 = source.IndexOf(beginDelim, 0, source.Length, StringComparison.OrdinalIgnoreCase);
                if (at1 == -1)
                    return string.Empty;

                at2 = source.IndexOf(endDelim, at1 + beginDelim.Length, StringComparison.OrdinalIgnoreCase);
            }

            if (allowMissingEndDelimiter && at2 < 0)
            {
                if (!returnDelimiters)
                    return source.Substring(at1 + beginDelim.Length);
                else
                    return source.Substring(at1);
            }

            if (at1 > -1 && at2 > 1)
            {
                if (!returnDelimiters)
                    return source.Substring(at1 + beginDelim.Length, at2 - at1 - beginDelim.Length);

                return source.Substring(at1, at2 - at1 + endDelim.Length);
            }

            return string.Empty;
        }


    }

    public static class ShellUtils
    {

        /// <summary>
        /// Uses the Shell Extensions to launch a program based on URL moniker or file name
        /// Basically a wrapper around ShellExecute
        /// </summary>
        /// <param name="url">Any URL Moniker that the Windows Shell understands (URL, Word Docs, PDF, Email links etc.)</param>
        /// <returns></returns>
        public static int GoUrl(string url)
        {
            string TPath = Path.GetTempPath();

            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = true;
            info.Verb = "Open";
            info.WorkingDirectory = TPath;
            info.FileName = url;


            Process process = new Process();
            process.StartInfo = info;
            try
            {
                process.Start();
            }
            catch { }

            return 0;
        }
    }
}
