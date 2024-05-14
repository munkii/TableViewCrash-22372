// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppResourcesExtension.cs" company="Magic Bullet Ltd">
//     Copyright (c) Magic Bullet Ltd. All rights reserved.
// </copyright>
// <summary>
//  Uses the resources files to return text. Returns a blank string on error or text not found.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShinyMAUIApp
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Uses the resources files to return text. Returns a blank string on error or text not found.
    /// </summary>
    [ContentProperty("Text")]
    [ExcludeFromCodeCoverage]
    public class AppResourcesExtension : IMarkupExtension
    {
        /// <summary>
        /// The full namespace of the AppResources.resx file.
        /// </summary>
        private const string ResourceId = "ShinyMAUIApp.Resources.Resource1";

        /// <summary>
        /// Gets or sets the text. This is the key to look for in the .resx file.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Uses the resources files to return text. Returns a blank string on error or text not found.
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Text == null)
            {
                return null;
            }

            // HACK using a SpirometerDataModel class to get the assembly that the resx files are stored in. File kept here instead of common project because of reliance on Xamarin.Forms.
            ResourceManager resourceManager = new ResourceManager(
                ResourceId,
                Assembly.GetExecutingAssembly());

            try
            {
                object value = resourceManager.GetString(this.Text, CultureInfo.CurrentCulture);
                return value;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}