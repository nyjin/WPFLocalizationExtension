﻿#region Copyright information
// <copyright file="InheritingResxLocalizationProvider.cs">
//     Licensed under Microsoft Public License (Ms-PL)
//     http://wpflocalizeextension.codeplex.com/license
// </copyright>
// <author>Uwe Mayer</author>
#endregion

namespace WPFLocalizeExtension.Providers
{
    #region Usings
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Resources;
    using System.Windows;
    using WPFLocalizeExtension.Engine;
    using XAMLMarkupExtensions.Base;
    #endregion

    /// <summary>
    /// A singleton RESX provider that uses inheriting attached properties.
    /// </summary>
    public class InheritingResxLocalizationProvider : ResxLocalizationProviderBase, ILocalizeInstance
    {
        #region Dependency Properties
        /// <summary>
        /// <see cref="DependencyProperty"/> DefaultDictionary to set the fallback resource dictionary.
        /// </summary>
        public static readonly DependencyProperty DefaultDictionaryProperty =
                DependencyProperty.RegisterAttached(
                "DefaultDictionary",
                typeof(string),
                typeof(InheritingResxLocalizationProvider),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, AttachedPropertyChanged));

        /// <summary>
        /// <see cref="DependencyProperty"/> DefaultAssembly to set the fallback assembly.
        /// </summary>
        public static readonly DependencyProperty DefaultAssemblyProperty =
            DependencyProperty.RegisterAttached(
                "DefaultAssembly",
                typeof(string),
                typeof(InheritingResxLocalizationProvider),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, AttachedPropertyChanged));


        /// <summary>
        /// <see cref="DependencyProperty"/> DefaultAssembly to set the fallback assembly.
        /// </summary>
        public static readonly DependencyProperty SupportLangugesProperty =
            DependencyProperty.RegisterAttached(
                "SupportLanguges",
                typeof(string[]),
                typeof(InheritingResxLocalizationProvider),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, AttachedPropertyChanged));
        #endregion

        #region Dependency Property Callback
        /// <summary>
        /// Indicates, that one of the attached properties changed.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">The event argument.</param>
        private static void AttachedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Instance.OnProviderChanged(obj);
        }
        #endregion

        #region Dependency Property Management
        #region Get
        /// <summary>
        /// Getter of <see cref="DependencyProperty"/> default dictionary.
        /// </summary>
        /// <param name="obj">The dependency object to get the default dictionary from.</param>
        /// <returns>The default dictionary.</returns>
        public static string GetDefaultDictionary(DependencyObject obj)
        {
            return obj.GetValueSync<string>(DefaultDictionaryProperty);
        }

        /// <summary>
        /// Getter of <see cref="DependencyProperty"/> default assembly.
        /// </summary>
        /// <param name="obj">The dependency object to get the default assembly from.</param>
        /// <returns>The default assembly.</returns>
        public static string GetDefaultAssembly(DependencyObject obj)
        {
            return obj.GetValueSync<string>(DefaultAssemblyProperty);
        }

        /// <summary>
        /// Getter of <see cref="DependencyProperty"/> default assembly.
        /// </summary>
        /// <param name="obj">The dependency object to get the default assembly from.</param>
        /// <returns>The support languages.</returns>
        public static string[] GetSupportLanguages(DependencyObject obj)
        {
            return obj.GetValueSync<string[]>(SupportLangugesProperty);
        }
        #endregion

        #region Set
        /// <summary>
        /// Setter of <see cref="DependencyProperty"/> default dictionary.
        /// </summary>
        /// <param name="obj">The dependency object to set the default dictionary to.</param>
        /// <param name="value">The dictionary.</param>
        public static void SetDefaultDictionary(DependencyObject obj, string value)
        {
            obj.SetValueSync(DefaultDictionaryProperty, value);
        }

        /// <summary>
        /// Setter of <see cref="DependencyProperty"/> default assembly.
        /// </summary>
        /// <param name="obj">The dependency object to set the default assembly to.</param>
        /// <param name="value">The assembly.</param>
        public static void SetDefaultAssembly(DependencyObject obj, string value)
        {
            obj.SetValueSync(DefaultAssemblyProperty, value);
        }

        /// <summary>
        /// Setter of <see cref="DependencyProperty"/> default assembly.
        /// </summary>
        /// <param name="obj">The dependency object to set the default assembly to.</param>
        /// <param name="value">The support languages.</param>
        public static void SetSupportLanguages(DependencyObject obj, string[] value)
        {
            obj.SetValueSync(SupportLangugesProperty, value);
        }
        #endregion
        #endregion

        #region Singleton Variables, Properties & Constructor
        /// <summary>
        /// Gets the <see cref="ResxLocalizationProvider"/> singleton.
        /// </summary>
        public static InheritingResxLocalizationProvider Instance
        {
            get
            {
                return InstanceLocator.Resolve<InheritingResxLocalizationProvider>();
            }
        }

        /// <summary>
        /// The instance constructor.
        /// </summary>
        public InheritingResxLocalizationProvider()
        {
            ResourceManagerList = new Dictionary<string, ResourceManager>();
            AvailableCultures = new ObservableCollection<CultureInfo> { CultureInfo.InvariantCulture };
        }
        #endregion

        #region Abstract assembly & dictionary lookup
        /// <summary>
        /// Get the assembly from the context, if possible.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The assembly name, if available.</returns>
        protected override string GetAssembly(DependencyObject target)
        {
            return target?.GetValue(DefaultAssemblyProperty) as string;
        }

        /// <summary>
        /// Get the dictionary from the context, if possible.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The dictionary name, if available.</returns>
        protected override string GetDictionary(DependencyObject target)
        {
            return target?.GetValue(DefaultDictionaryProperty) as string;
        }

        /// <summary>
        /// Get the support languages from the context, if possible.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The support languages , if available.</returns>
        protected override string[] GetSupportLanguageSet(DependencyObject target)
        {
            return target?.GetValue(SupportLangugesProperty) as string[];
        }

        #endregion
    }
}
