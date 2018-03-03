namespace Lands.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }

        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string EMail
        {
            get { return Resource.EMail; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string Countries
        {
            get { return Resource.Countries; }
        }

        public static string Search
        {
            get { return Resource.Search; }
        }

        public static string Country
        {
            get { return Resource.Country; }
        }

        public static string Information
        {
            get { return Resource.Information; }
        }

        public static string Capital
        {
            get { return Resource.Capital; }
        }

        public static string Population
        {
            get { return Resource.Population; }
        }

        public static string Area
        {
            get { return Resource.Area; }
        }

        public static string AlphaCode2
        {
            get { return Resource.AlphaCode2; }
        }

        public static string AlphaCode3
        {
            get { return Resource.AlphaCode3; }
        }

        public static string Region
        {
            get { return Resource.Region; }
        }

        public static string Subregion
        {
            get { return Resource.Subregion; }
        }

        public static string Demonym
        {
            get { return Resource.Demonym; }
        }

        public static string GINI
        {
            get { return Resource.GINI; }
        }

        public static string NativeName
        {
            get { return Resource.NativeName; }
        }

        public static string NumericCode
        {
            get { return Resource.NumericCode; }
        }

        public static string CIOC
        {
            get { return Resource.CIOC; }
        }

        public static string Borders
        {
            get { return Resource.Borders; }
        }

        public static string Currencies
        {
            get { return Resource.Currencies; }
        }

        public static string MyLanguages
        {
            get { return Resource.MyLanguages; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string MyProfile
        {
            get { return Resource.MyProfile; }
        }

        public static string Statics
        {
            get { return Resource.Statics; }
        }

        public static string LogOut
        {
            get { return Resource.LogOut; }
        }
    }
}