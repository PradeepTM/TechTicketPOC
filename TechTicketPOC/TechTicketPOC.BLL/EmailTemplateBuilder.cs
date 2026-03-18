using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL
{
    /// <summary>
    /// Builds the email body by replacing {{FieldName}} placeholders in the stored
    /// EmailTemplateBody with values collected from the user.  When no template body
    /// is defined a styled HTML table is generated automatically.
    /// </summary>
    public class EmailTemplateBuilder
    {
        // Compiled regex that matches {{FieldName}} placeholders in a single pass.
        private static readonly Regex PlaceholderRegex =
            new Regex(@"\{\{(\w+)\}\}", RegexOptions.Compiled);

        /// <summary>
        /// Replaces every {{FieldName}} token in the template body with the
        /// corresponding value from <paramref name="fieldValues"/>.
        /// Falls back to a default HTML table when no template body is stored.
        /// </summary>
        public string BuildEmailBody(EmailTemplateDTO template, Dictionary<string, string> fieldValues)
        {
            if (template == null)
                return string.Empty;

            if (!string.IsNullOrWhiteSpace(template.EmailTemplateBody))
                return ReplacePlaceholders(template.EmailTemplateBody, fieldValues);

            return BuildDefaultHtmlBody(template, fieldValues);
        }

        /// <summary>
        /// Builds the full email preview HTML, including the header block (To / CC /
        /// BCC / Subject) followed by the rendered body.
        /// </summary>
        public string BuildEmailPreviewHtml(EmailTemplateDTO template,
                                            string subject,
                                            Dictionary<string, string> fieldValues)
        {
            if (template == null)
                return string.Empty;

            var sb = new StringBuilder();

            sb.Append("<div style='font-family:Arial,sans-serif;font-size:13px;'>");

            // ── Header table ──────────────────────────────────────────────────
            sb.Append("<table style='width:100%;border-collapse:collapse;margin-bottom:15px;'>");

            var toList = FormatAddressList(template.To);
            sb.Append($"<tr><td style='padding:4px 8px;width:70px;font-weight:bold;'>To:</td>" +
                      $"<td style='padding:4px 8px;'>{WebUtility.HtmlEncode(toList)}</td></tr>");

            var ccList = FormatAddressList(template.CC);
            sb.Append($"<tr><td style='padding:4px 8px;font-weight:bold;'>CC:</td>" +
                      $"<td style='padding:4px 8px;'>{WebUtility.HtmlEncode(ccList)}</td></tr>");

            var bccList = FormatAddressList(template.BCC);
            sb.Append($"<tr><td style='padding:4px 8px;font-weight:bold;'>BCC:</td>" +
                      $"<td style='padding:4px 8px;'>{WebUtility.HtmlEncode(bccList)}</td></tr>");

            sb.Append($"<tr><td style='padding:4px 8px;font-weight:bold;'>Subject:</td>" +
                      $"<td style='padding:4px 8px;'>{WebUtility.HtmlEncode(subject)}</td></tr>");

            sb.Append("</table>");
            sb.Append("<hr style='border:1px solid #ddd;margin:10px 0;'/>");

            // ── Body ──────────────────────────────────────────────────────────
            sb.Append("<div style='margin-top:10px;'>");
            sb.Append(BuildEmailBody(template, fieldValues));
            sb.Append("</div>");

            sb.Append("</div>");
            return sb.ToString();
        }

        // ── Private helpers ───────────────────────────────────────────────────

        /// <summary>
        /// Single-pass substitution using a compiled regex.  Each {{FieldName}} token
        /// is replaced with the HTML-encoded value from <paramref name="fieldValues"/>.
        /// Unmatched placeholders are left unchanged.
        /// Field name matching is case-insensitive so that the stored template body
        /// does not need to match the exact casing of the FieldName in the database.
        /// </summary>
        private string ReplacePlaceholders(string templateBody, Dictionary<string, string> fieldValues)
        {
            return PlaceholderRegex.Replace(templateBody, m =>
            {
                var key = m.Groups[1].Value;
                if (fieldValues.TryGetValue(key, out var value))
                    return WebUtility.HtmlEncode(value ?? string.Empty);
                return m.Value; // leave unmatched placeholders untouched
            });
        }

        private string BuildDefaultHtmlBody(EmailTemplateDTO template,
                                            Dictionary<string, string> fieldValues)
        {
            var fieldMap = template.Fields?
                               .ToDictionary(f => f.FieldName, f => f.DisplayName)
                           ?? new Dictionary<string, string>();

            var sb = new StringBuilder();
            sb.Append("<table style='border-collapse:collapse;width:100%;'>");
            sb.Append("<thead><tr>" +
                      "<th style='text-align:left;padding:6px 10px;background:#f5f5f5;border:1px solid #ccc;'>Field</th>" +
                      "<th style='text-align:left;padding:6px 10px;background:#f5f5f5;border:1px solid #ccc;'>Value</th>" +
                      "</tr></thead><tbody>");

            foreach (var kvp in fieldValues)
            {
                var display = fieldMap.ContainsKey(kvp.Key) ? fieldMap[kvp.Key] : kvp.Key;
                sb.Append("<tr>");
                sb.Append($"<td style='padding:6px 10px;border:1px solid #ccc;font-weight:bold;'>" +
                          $"{WebUtility.HtmlEncode(display)}</td>");
                sb.Append($"<td style='padding:6px 10px;border:1px solid #ccc;'>" +
                          $"{WebUtility.HtmlEncode(kvp.Value ?? string.Empty)}</td>");
                sb.Append("</tr>");
            }

            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        private static string FormatAddressList(List<string> addresses)
        {
            if (addresses == null || addresses.Count == 0)
                return string.Empty;
            return string.Join("; ", addresses);
        }
    }
}
