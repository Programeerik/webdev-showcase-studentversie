using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShowcaseAPI.Models.ValidationAttributes
{
    public class AllowSpecificHtmlAttribute : ValidationAttribute
    {
        private readonly List<string> _allowedTags;

        public AllowSpecificHtmlAttribute(params string[] allowedTags)
        {
            _allowedTags = new List<string>(allowedTags);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input && !string.IsNullOrWhiteSpace(input))
            {
                //regexr.com/8cabk
                string pattern = @"<(/?)(\w+)[^>]*>";
                var matches = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    string tag = match.Groups[2].Value.ToLower();
                    if (!_allowedTags.Contains(tag))
                    {
                        return new ValidationResult($"De tag <{tag}> is niet toegestaan.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
