//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Udalosti.Autentifikacia.UI.Registracia.xaml", "Autentifikacia/UI/Registracia.xaml", typeof(global::Udalosti.Autentifikacia.UI.Registracia))]

namespace Udalosti.Autentifikacia.UI {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Autentifikacia/UI/Registracia.xaml")]
    public partial class Registracia : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Udalosti.Nastroje.Xamarin.Vstup meno;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Udalosti.Nastroje.Xamarin.Vstup email;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Udalosti.Nastroje.Xamarin.Vstup heslo;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Udalosti.Nastroje.Xamarin.Vstup potvrd;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ActivityIndicator nacitavanie;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(Registracia));
            meno = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Udalosti.Nastroje.Xamarin.Vstup>(this, "meno");
            email = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Udalosti.Nastroje.Xamarin.Vstup>(this, "email");
            heslo = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Udalosti.Nastroje.Xamarin.Vstup>(this, "heslo");
            potvrd = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Udalosti.Nastroje.Xamarin.Vstup>(this, "potvrd");
            nacitavanie = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ActivityIndicator>(this, "nacitavanie");
        }
    }
}
