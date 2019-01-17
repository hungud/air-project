using System.Collections.Generic;
namespace SACS.Library.Configuration
{
    public class ConfigDataProvider:IConfigDataProvider
    {

        /// <summary>
        /// Gets the REST group setting or SOAP organization setting.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public string Group
        {
            get { return this.GetProperty("group"); }
        }

        /// <summary>
        /// Gets the REST user identifier or SOAP username.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId
        {
            get { return this.GetProperty("userId"); }
        }

        /// <summary>
        /// Gets the REST client secret or SOAP password.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret
        {
            get { return this.GetProperty("clientSecret"); }
        }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public string Domain
        {
            get { return this.GetProperty("domain"); }
        }

        /// <summary>
        /// Gets the environment URL address.
        /// </summary>
        /// <value>
        /// The environment URL address.
        /// </value>
        public string Environment
        {
            get { return this.GetProperty("environment"); }
        }
        private readonly Dictionary<string, string> properties = new Dictionary<string, string>();
        /// <summary>
        /// The configuration file path
        /// </summary>
        private readonly string configPath;
        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Value of the property.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Property is not present in the file.</exception>
        private string GetProperty(string property)
        {
            string value;
            bool present = this.properties.TryGetValue(property, out value);
            if (present)
            {
                return value;
            }
            else
            {
                string msg = string.Format("Property {0} is not present in file: {1}", property, this.configPath);
                throw new KeyNotFoundException(msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> AppConfigProperties
        {
            get
            {
                return App.Common.CommonFunctions.GetAppSettings();
            }
        }
    }
    /// <summary>
    /// The configuration provider.
    /// </summary>
    public interface IConfigDataProvider
    {
        /// <summary>
        /// Gets the REST group setting or SOAP organization setting.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        string Group { get; }

        /// <summary>
        /// Gets the REST user identifier or SOAP username.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string UserId { get; }

        /// <summary>
        /// Gets the REST client secret or SOAP password.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        string ClientSecret { get; }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        string Domain { get; }

        /// <summary>
        /// Gets the environment URL address.
        /// </summary>
        /// <value>
        /// The environment URL address.
        /// </value>
        string Environment { get; }
        /// <summary>
        /// Gets the AppConfig Properties.
        /// </summary>
        /// <value>
        /// The AppConfig Properties.
        /// </value>
        Dictionary<string, string> AppConfigProperties { get; }
    }
}