﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookAssistant.Util {
    class HtmlUtils {
        public static string ReplaceHtmlTag(string html, int length = 0) {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
        public static string ReplaceHtmlTag(string html) {
            return ReplaceHtmlTag(html, -1);
        }
    }
}
