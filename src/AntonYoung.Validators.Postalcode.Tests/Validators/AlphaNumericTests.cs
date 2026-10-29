using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AntonYoung.Validators.Postalcode.Tests
{
    /// <summary>
    ///     used as test class of postalcodes which are alpha numeric.
    /// </summary>
    public class AlphaNumericTests 
        : IClassFixture<DependencyFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        public AlphaNumericTests(DependencyFixture fixture)
            => _serviceProvider = fixture.ServiceProvider;

        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.UnitedKingdom, "SW1A 1AA")]
        [InlineData(Countries.UnitedKingdom, "GIR 0AA")]
        [InlineData(Countries.UnitedKingdom, "BT1 5GS")]
        [InlineData(Countries.UnitedKingdom, "EH1 1YZ")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void IsValid(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate valid postalcodes as expected format.
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> success
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);
            
            isValid
                .Should()
                .BeTrue();

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result, as given postalcode
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.UnitedKingdom, "x2")]
        [InlineData(Countries.UnitedKingdom, "2344 AAA")]
        [InlineData(Countries.UnitedKingdom, "EHA 1AA")]
        [InlineData(Countries.UnitedKingdom, "EH1 1CI")]
        [InlineData(Countries.UnitedKingdom, "GIR 1AA")]
        [InlineData(Countries.Ireland, "222 YD82")]
        public void InValid(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate invalid postalcodes
            bool isValid = postalcodeValidator
                .TryValidate(postalCode, country, out string result);

            //=> unsuccessful
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);
            
            isValid
                .Should()
                .BeFalse();

            //=> has error message
            postalcodeValidator.ErrorMessage
                .Should()
                .NotBeNullOrEmpty();

            //=> unformatted result as postalcode
            result
                .Should()
                .Be(postalCode);
        }

        [Theory]
        [InlineData(Countries.UnitedKingdom, "EC1A 1BB")]
        [InlineData(Countries.UnitedKingdom, "DN55 1PT")]
        [InlineData(Countries.UnitedKingdom, "CR2 6XH")]
        [InlineData(Countries.UnitedKingdom, "B33 8TH")]
        [InlineData(Countries.UnitedKingdom, "M1 1AE")]
        [InlineData(Countries.UnitedKingdom, "W1A 0AX")]
        [InlineData(Countries.Ireland, "D22 YD82")]
        public void WithOutSpace(Countries country, string postalCode)
        {
            var postalcodeValidator = _serviceProvider
                .GetService<IPostalcodeValidator>();

            //=> validate postalcode without whitespace
            bool isValid = postalcodeValidator
                .TryValidate(postalCode.Replace(" ", string.Empty), country, out string result);

            //=> success
            postalcodeValidator.IsValid
                .Should()
                .Be(isValid);

            isValid
                .Should()
                .BeTrue();

            //=> has no error message
            postalcodeValidator.ErrorMessage
                .Should()
                .BeNullOrEmpty();

            //=> formatted result has postalcode with whitespace
            result
                .Should()
                .Be(postalCode);
        }
    }
}