using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using WPFLocalizeExtension.Providers;

namespace HalloWeltWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc />
        public App()
        {
            var provider = ResxLocalizationProvider.Instance;
            ResxLocalizationProvider.SetSupportLanguages(provider, new[]
            {
                "ar", "en", "de", "he"
            });

            ResxLocalizationProvider.SetDefaultAssembly(provider, "HelloWorldWPF");
            ResxLocalizationProvider.SetDefaultDictionary(provider, "Ressourcen");

        }
    }
}
