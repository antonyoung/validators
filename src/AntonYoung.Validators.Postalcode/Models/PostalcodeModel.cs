using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode.Infrastructure;
using System;
using System.Collections.Generic;

namespace AntonYoung.Validators.Postalcode.Models
{
    public class PostalcodeModel : IPostalcodeModel
    {
        //=> constants: used 2 => n times.
        private const string EXAMPLE_4_DIGITS               = "1234";
        private const string EXAMPLE_5_DIGITS               = "12345";
        private const string EXAMPLE_5_DIGITS_WHITESPACE    = "123 45";

        private const string FORMAT_ALPHANUMERIC            = "<alphanumeric1> <alphanumeric2>";
        private const string FORMAT_DIGITS                  = "<digits>";
        private const string FORMAT_DIGITS_HYPHEN           = "<digits1>-<digits2>";
        private const string FORMAT_DIGITS_WHITESPACE       = "<digits1> <digits2>";
        private const string FORMAT_PREFIX                  = "<prefix><digits>";

        private const string PREFIX_CROATIA                 = "HR-";
        private const string PREFIX_FINLAND                 = "FI-";
        private const string PREFIX_LATVIA                  = "LV-";
        private const string PREFIX_LITHUANIA               = "LT-";
        private const string PREFIX_LUXEMBOURG              = "L-";
        private const string PREFIX_SLOVENIA                = "SI-";
        private const string PREFIX_SWEDEN                  = "SE-";

        private const string REPLACE_PREFIX                 = "<REPLACE_PREFIX>";

        private const string REGEX_4_DIGITS                 = "(?<digits>^[1-9][0-9]{3}$)";
        private const string REGEX_4_DIGITS_PREFIX          = "^(?<prefix>(?i)<REPLACE_PREFIX>)?(?<digits>[1-9][0-9]{3}$)";
        private const string REGEX_5_DIGITS                 = "(?<digits>^[1-9][0-9]{4}$)";
        private const string REGEX_5_DIGITS_PREFIX          = "^(?<prefix>(?i)<REPLACE_PREFIX>)?(?<digits>[1-9][0-9]{4}$)";
        private const string REGEX_5_DIGITS_WHITESPACE      = @"(?<digits1>^[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)";

        /// <summary>
        ///     data source off all set countries and their internal business logic.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     throws ArgumentException, in case country is not found in current Dictionary, 
        /// </exception> 
        public IDictionary<Countries, PostalcodeRuleSetModel> Rules
        {
            get => new Dictionary<Countries, PostalcodeRuleSetModel>()
            {
                { Countries.Austria,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_4_DIGITS,
                        Example = EXAMPLE_4_DIGITS
                    }
                },
                { Countries.Belgium,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_4_DIGITS,
                        Example = EXAMPLE_4_DIGITS
                    }
                },
                { Countries.Bulgaria,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_4_DIGITS,
                        Example = EXAMPLE_4_DIGITS
                    }
                },
                { Countries.Croatia,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_PREFIX,
                        RegexPattern = REGEX_5_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_CROATIA),
                        Prefix = PREFIX_CROATIA,
                        Example = $"{PREFIX_CROATIA}{EXAMPLE_5_DIGITS}"
                    }
                },
                { Countries.Cyprus,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_4_DIGITS,
                        Example = EXAMPLE_4_DIGITS
                    }
                },
                { Countries.Czechia,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS_WHITESPACE,
                        RegexPattern = REGEX_5_DIGITS_WHITESPACE,
                        Example = EXAMPLE_5_DIGITS_WHITESPACE
                    }
                },
                { Countries.Denmark,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_4_DIGITS,
                        Example = EXAMPLE_4_DIGITS
                    }
                },
                { Countries.Estonia,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_5_DIGITS,
                        Example = EXAMPLE_5_DIGITS
                    }
                },
                //=> Finland: The postal code must be preceded by ‘FI-’ = default (or by ‘AX-’ for the Åland Islands) 
                { Countries.Finland,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_PREFIX,
                        RegexPattern = "^(?<prefix>(?i)(FI-)|(AX-))?(?<digits>[1-9][0-9]{4}$)",
                        Prefix = PREFIX_FINLAND,
                        Example = $"{PREFIX_FINLAND}{EXAMPLE_5_DIGITS}"
                    }
                },
                { Countries.France,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_5_DIGITS,
                        Example = EXAMPLE_5_DIGITS
                    }
                },
                { Countries.Germany,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_5_DIGITS,
                        Example = EXAMPLE_5_DIGITS
                    }
                },
                { Countries.Greece,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS_WHITESPACE,
                        RegexPattern = REGEX_5_DIGITS_WHITESPACE,
                        Example = EXAMPLE_5_DIGITS_WHITESPACE
                    }
                },
                { Countries.Hungary,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_4_DIGITS,
                        Example = EXAMPLE_4_DIGITS
                    }
                },
                { Countries.Italy,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_5_DIGITS,
                        Example = EXAMPLE_5_DIGITS
                    }
                },
                { Countries.Ireland,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_ALPHANUMERIC,
                        RegexPattern = @"^(?<alphanumeric1>[AC-FHKNPRTV-Y][0-9]{2}|D6W)(?<whitespace>\s?)(?<alphanumeric2>[0-9AC-FHKNPRTV-Y]{4})\z",
                        Example = "D22 YD82"
                    }
                },
                { Countries.Latvia,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_PREFIX,
                        RegexPattern = REGEX_4_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_LATVIA),
                        Prefix = PREFIX_LATVIA,
                        Example = $"{PREFIX_LATVIA}{EXAMPLE_4_DIGITS}"
                    }
                },
                { Countries.Lithuania,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_PREFIX,
                        RegexPattern = REGEX_5_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_LITHUANIA),
                        Prefix = PREFIX_LITHUANIA,
                        Example = $"{PREFIX_LITHUANIA}{EXAMPLE_5_DIGITS}"
                    }
                },
                { Countries.Luxembourg,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_PREFIX,
                        RegexPattern = REGEX_4_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_LUXEMBOURG),
                        Prefix = PREFIX_LUXEMBOURG,
                        Example = $"{PREFIX_LUXEMBOURG}{EXAMPLE_4_DIGITS}"
                    }
                },
                { Countries.Malta,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = "<letters> <digits>",
                        RegexPattern = @"(?<letters>^[A-Za-z]{3})(?<whitespace>\s?)(?<digits>[1-9][0-9]{3}$)",
                        Example = "ABC 1234"
                    }
                },
                { Countries.Netherlands,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = "<digits> <letters>",
                        RegexPattern = @"(?<digits>^[1-9][0-9]{3})(?<whitespace>\s?)(?<letters>[A-Za-z]{2}$)",
                        Example = "1234 AB"
                    }
                },
                { Countries.Poland,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS_HYPHEN,
                        RegexPattern = @"(?<digits1>^[1-9][0-9]{1})(?<hyphen>-?)(?<digits2>[0-9]{3}$)",
                        Example = "12-345"
                    }
                },
                { Countries.Portugal,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS_HYPHEN,
                        RegexPattern = @"(?<digits1>^[1-9][0-9]{3})(?<hyphen>-?)(?<digits2>[0-9]{3}$)",
                        Example = "1234-567"
                    }
                },
                { Countries.Romania,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = "(?<digits>^[1-9][0-9]{5}$)",
                        Example = "123456"
                    }
                },
                { Countries.Slovakia,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS_WHITESPACE,
                        RegexPattern = REGEX_5_DIGITS_WHITESPACE,
                        Example = EXAMPLE_5_DIGITS_WHITESPACE
                    }
                },
                { Countries.Slovenia,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_PREFIX,
                        RegexPattern = REGEX_4_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_SLOVENIA),
                        Prefix = PREFIX_SLOVENIA,
                        Example = $"{PREFIX_SLOVENIA}{EXAMPLE_4_DIGITS}"
                    }
                },
                { Countries.Spain,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_DIGITS,
                        RegexPattern = REGEX_5_DIGITS,
                        Example = EXAMPLE_5_DIGITS
                    }
                },
                { Countries.Sweden,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = "<prefix><digits1> <digits2>",
                        RegexPattern = @"^(?<prefix>(?i)SE-)?(?<digits1>[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)",
                        Prefix = PREFIX_SWEDEN,
                        Example = $"{PREFIX_SWEDEN}{EXAMPLE_5_DIGITS_WHITESPACE}"
                    }
                },
                { Countries.UnitedKingdom,
                    new PostalcodeRuleSetModel {
                        DisplayFormat = FORMAT_ALPHANUMERIC,
                        RegexPattern =
                            @"(?x)\A
                              (?<alphanumeric1>
                                  GIR(?=(?<whitespace>\s?)0AA\z)
                                  | [A-PR-UWYZ][0-9]{1,2}
                                  | [A-PR-UWYZ][A-HK-Y][0-9]{1,2}
                                  | [A-PR-UWYZ][0-9][A-HJKPSTUW]
                                  | [A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRVWXY]
                              )
                              (?<whitespace>\s?)(?<alphanumeric2>[0-9][ABD-HJLNP-UW-Z]{2})\z",
                        Example = "DN55 1PT"
                    }
                }
            };
        }
    }
}