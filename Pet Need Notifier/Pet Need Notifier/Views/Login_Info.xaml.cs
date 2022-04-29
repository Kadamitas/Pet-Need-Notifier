using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;
using MySqlConnector;

namespace Pet_Need_Notifier.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Info : ContentPage
    {
        
        public Login_Info()
        {
            InitializeComponent();
           

        }

        async void Log_In_Button(object sender, EventArgs e)
        {
            if ( UserExists(user_label.Text) && PassCheck(user_label.Text, pass_label.Text))
            { // Change later to Query for username
                Preferences.Set("username", user_label.Text);
                await Shell.Current.GoToAsync($"//{nameof(Adder)}");
            }
            else
            {
                await DisplayAlert("Invalid Login", "Username or Password Invalid", "OK");
            }

        }
         async void Sign_Up_Button(object sender, EventArgs e)
        {
            if (! UserExists(user_label.Text))
            {

                Preferences.Set("username", user_label.Text);
                SignUp(user_label.Text, pass_label.Text);
                await DisplayAlert("Account Created!", "Try logging in", "OK");

            }
            else
            {
                await DisplayAlert("Username Exists!", "Try logging in", "OK");
            }
        }

        void Keep_Logged_In(object sender, EventArgs e)
        {
           
            
            Preferences.Set("keeplogged", keep_logged.IsToggled);

        }

        string Hashify(String pass)
        {
            SHA256Managed sha = new SHA256Managed();
            if (String.IsNullOrEmpty(pass))
            {
                return String.Empty;
            }
            else
            {
                byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(pass);
                byte[] hashBytes = sha.ComputeHash(stringBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }
        void SignUp(string username, string password)
        {


            MySqlConnection connection = new MySqlConnection("server=68.234.189.227;user=AronLaptop;database=petneed;port=3306;password=lolwut37");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
                 "use petneed; \n" +
                "insert into users(Username,UserPassword) values (\"" + username + "\"," + " \"" + Hashify(password) + "\");";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()){ }

                connection.Close();
            return;
        }
        bool PassCheck(string username, string attemptedPassword)
        {
            string actualPassword = "";


            MySqlConnection connection = new MySqlConnection("server=68.234.189.227;user=AronLaptop;database=petneed;port=3306;password=lolwut37");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
                 "use petneed; \n" +
                "Select UserPassword  \n" +
                "From users \n" +
                "Where users.Username = \"" + username + "\" \n";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                actualPassword = reader.GetString("UserPassword");

            }
            connection.Close();
            return (Hashify(attemptedPassword) == actualPassword);
        }
        bool UserExists(string username)
        {
            bool exists = false;
            MySqlConnection connection = new MySqlConnection("server=68.234.189.227;user=AronLaptop;database=petneed;port=3306;password=lolwut37");
            connection.Open();



            MySqlCommand command = connection.CreateCommand();
            string comm = "use petneed; \n" +
            "SELECT EXISTS( \n" +
                "SELECT Username \n" +
                "FROM users \n" +
                "WHERE Username = \"" +
                username + "\" \n)" +
              " as exist \n";
            command.CommandText = comm;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                exists = reader.GetBoolean("exist");

            }
            connection.Close();
            return exists;
        }

    }
}