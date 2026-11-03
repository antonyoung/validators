using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Postalcode.Models;
using System.Collections.Generic;


namespace AntonYoung.Validators.Postalcode.Infrastructure
{
    /// <summary>
    ///     interface: used to define the postalcode in memory internal logic as data source.
    /// </summary>as 
    public interface IPostalcodeModel
    {
        /// <summary>
        ///     used as the total set to define the internal logic for each country available as data source. 
        ///     engine: based on <seealso cref="System.Text.RegularExpressions.GroupCollection"/>.
        /// </summary>
        /// <exception cref=System.ArgumentException">
        ///     thrown, when country is not found in defined data source.
        /// </exception>
        IDictionary<Countries, PostalcodeRuleSetModel> Rules { get; }
    }
}