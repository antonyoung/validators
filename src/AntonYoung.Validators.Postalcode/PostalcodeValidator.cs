using AntonYoung.Validators.Abstractions.Constants;
using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Abstractions.Extensions;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using System;
using System.Text.RegularExpressions;


namespace AntonYoung.Validators.Postalcode
{
    /// <summary>
    ///     used as all business logic behind postalcodes of European countries ( with the exceptions with preifix <see cref="Countries.Finland"/> )
    ///     validates and formats the postalcode according to the selected country.
    /// </summary>
    public class PostalcodeValidator 
        : IPostalcodeValidator
    {
        /// <summary>
        ///     used as internal business rules of European postalcodes.
        /// </summary>
        private readonly IPostalcodeModel _model;

        /// <summary>
        ///     used as configured default country of European postalcodes.
        /// </summary>
        private readonly Countries _defaultCountry;

        /// <summary>
        ///     used as internal business rules of postalcode of selected country.
        /// </summary>
        private PostalcodeRuleSetModel _logic;

        /// <summary>
        ///     used as the internal postalcode, that has to be validated to the set country.
        /// </summary>
        private string _input;

        /// <summary>
        ///     used as internal business logic, if postalcode of selected country is valid.
        /// </summary>
        private bool _isValid;

        /// <summary>
        ///     used as a postalcode example of the set country. ( default = <see cref="Countries.Netherlands"/> )
        /// </summary>
        public string Example { get => _logic.Example; }

        /// <summary>
        ///     used as error message in case of <seealso cref="IsValid"/> = false;
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        ///     used to validate given postal code against the set country.
        ///     when false an <seealso cref="ErrorMessage"/> will be given.
        /// </summary>
        public bool IsValid
        {
            get => _isValid;
            private set
            {
                if (!value)
                    ErrorMessage = ErrorMessage = $"Postal code \"{_input}\" is not valid. Use as example \"{Example}\".";

                _isValid = value;
            }
        }


        /// <summary>
        ///     used as constructor to initiliaze the class with the internal business rules of all internal postalcodes.
        /// </summary>
        /// <param name="model">
        ///     used as the interface, for the internal business logic to be used.
        /// </param>
        public PostalcodeValidator(
            IPostalcodeModel model, 
            Countries defaultCountry = Countries.Netherlands) 
            => (_model, _defaultCountry) = (model, defaultCountry);
        

        /// <summary>
        ///     validates and formats postalcode with default country The Netherlands and default formatter as expected.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated with default country <see cref="Countries.Netherlands"/>. 
        /// </param>
        /// <param name="result">
        ///     used as <see cref="IsValid"/> with default country <see cref="Countries.Netherlands"/> formatted as the default formatter.
        /// </param>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and with default country <see cref="Countries.Netherlands"/>
        /// </returns>
        public bool TryValidate(string value, out string result) 
            => TryValidate(value, _defaultCountry, out result);

        
        /// <summary>
        ///     validates and formats postalcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="result">
        ///     used as <see cref="IsValid"/> <paramref name="value"/> with <paramref name="country"/>` formatted with the default formatter.
        /// </param>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <see cref="IsValid"/> is a valid postal code or not? With <seealso cref="Formatters"/> as default <see cref="Formatters.None"/>
        /// </returns>
        public bool TryValidate(string value, Countries country, out string result) 
            => TryValidate(value, country, Formatters.None, out result);

        
        /// <summary>
        ///     validates and formats postalcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used.
        /// </param>
        /// <param name="result">
        ///     used as postalcode <see cref="IsValid"/> .
        /// </param>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not with default <paramref name="replace"/> as <see cref="string.Empty"/> to be used with the formatter.
        /// </returns>
        public bool TryValidate(string value, Countries country, Formatters formatter, out string result)
            => TryValidate(value, country, formatter, string.Empty, out result);

        
        /// <summary>
        ///     validates and formats postalcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used.
        /// </param>
        /// <param name="replace">
        ///     usead as custom replace value to be used with the given formatter.
        /// </param>
        /// <param name="result">
        ///     used as result postal code value with given <paramref name="country"/>, <paramref name="formatter"/> and <paramref name="replace"/>.. 
        /// </param>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///    <see cref="IsValid"/> if is valid or not.
        /// </returns>
        public bool TryValidate(string value, Countries country, Formatters formatter, string replace, out string result)
        {
            _input = result = value?.Trim() ?? string.Empty;

            if (!_model.Rules.TryGetValue(country, out _logic))
                throw new NotSupportedException(nameof(country));

            var match = Regex.Match(
                _input, 
                _logic.RegexPattern, 
                RegexOptions.Singleline, 
                TimeSpan.FromMilliseconds(5));

            IsValid = match.Success;

            if (_isValid) result = Format(match, formatter, replace);

            return _isValid;
        }


        /// <summary>
        ///     internal formatting logic, based on the groups that are in the match of whatever regular ecpression.
        /// </summary>
        /// <param name="match">
        ///     used as all groups that are matched in the used regular expression.
        /// </param> 
        /// <param name="formatter">
        ///      used as <see cref="Formatters"/> to be used as replace wildcard ( default: <see cref="Formatters.None"/> )
        /// </param>
        /// <param name="replace">
        ///     used as given formatter, as replace value ( default: <see cref="string.Empty"/> )
        /// </param>
        /// <exception cref="NotSupportedException"/>
        /// <returns>
        ///     the formatted postalcode of the match result and with given formatter. 
        /// </returns>
        private string Format(Match match, Formatters formatter, string replace)
        {
            var result = _logic.DisplayFormat;

            foreach (Group group in match.Groups)
            {
                if (group.Name.Equals(Replaces.Prefix, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(group.Value))
                    result = result.Replace(string.Format(Replaces.Default, group.Name), _logic.Prefix);

                if (!string.IsNullOrWhiteSpace(group.Value))
                    result = result.Replace(string.Format(Replaces.Default, group.Name), group.Value);
            }

            return result.ToUpperInvariant().Format(formatter, replace);
        }
    }
}