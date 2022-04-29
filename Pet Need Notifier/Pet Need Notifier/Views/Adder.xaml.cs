using MySqlConnector;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pet_Need_Notifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adder : ContentPage
    {

        public Adder()
        {
            InitializeComponent();
            list.ItemsSource = Notifications;

            


        }
        public List<String> Weekdays = new List<String>();
        public List<String> Types = new List<String>();

        public class Notification
        {
            public string Notification_Type { get; set; }
            public TimeSpan Time { get; set; }
            public DayOfWeek Weekday { get; set; }
            public int NotificationId { get; set; }


            public async void CreateNotification()
            {
                DateTime nextNotificationTime = DateTime.Today.AddDays(((int)DateTime.Today.DayOfWeek - (int)Weekday) <= 0 ? ((int)Weekday) - (int)DateTime.Today.DayOfWeek : (7 + (int)Weekday) - (int)DateTime.Today.DayOfWeek) + Time;
                var notification = new NotificationRequest
                {
                    NotificationId = this.NotificationId,
                    Title = Notification_Type,
                    Description = (Notification_Type.Equals("Litter")) ? "Clean the litterbox!" : (Notification_Type.Equals("Food")) ? "Feed your pet!" : "Give your pet Medicine!",
                    ReturningData = "Completed",
                    Schedule =
                    {
                        NotifyTime = nextNotificationTime ,
                        RepeatType = NotificationRepeat.Weekly

                    }
                };
                await NotificationCenter.Current.Show(notification);
            }



        }


        private ObservableCollection<Notification> _Notifications;
        public ObservableCollection<Notification> Notifications
        {
            get
            {
                return _Notifications ?? (_Notifications = new ObservableCollection<Notification>());
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            sendTimeToDB();
            foreach (String i in Types)
            {
                foreach (String j in Weekdays)
                {

                    Notification notification = new Notification()
                    {
                        Notification_Type = i,
                        Weekday = ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), j)),
                        Time = Event_Time.Time,
                        NotificationId = DbGetID(Event_Time.Time)
                    };

                    Notifications.Add(notification);
                    populateDB(j, i, notification.NotificationId);
                    notification.CreateNotification();

                }
            }


        }






        private void Day_Clicked(object sender, EventArgs e)
        {
            if (!Weekdays.Contains(((Button)sender).Text))
            {
                Weekdays.Add(((Button)sender).Text);
                ((Button)sender).BackgroundColor = Color.White;
                ((Button)sender).TextColor = Color.FromHex("FFA500");
            }
            else
            {
                Weekdays.Remove(((Button)sender).Text);
                ((Button)sender).BackgroundColor = Color.FromHex("FFA500");
                ((Button)sender).TextColor = Color.White;
            }

        }
        private void Type_Clicked(object sender, EventArgs e)
        {
            if (!Types.Contains(((Button)sender).Text))
            {
                Types.Add(((Button)sender).Text);
                ((Button)sender).BackgroundColor = Color.White;
                ((Button)sender).TextColor = Color.FromHex("FFA500");
            }
            else
            {
                Types.Remove(((Button)sender).Text);
                ((Button)sender).BackgroundColor = Color.FromHex("FFA500");
                ((Button)sender).TextColor = Color.White;
            }

        }

        void sendTimeToDB()
        {

            MySqlConnection connection = new MySqlConnection("server=68.234.189.227;user=AronLaptop;database=petneed;port=3306;password=lolwut37");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
                 "use petneed; \n" +
                "insert into notifs(Username,NotifTime) values (\"" + Preferences.Get("username", "") + "\",\"" + Event_Time.Time + "\")";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) { }

            connection.Close();
            return;
        }
        private int DbGetID(TimeSpan Time)
        {
            int ID = 0;


            MySqlConnection connection = new MySqlConnection("server=68.234.189.227;user=AronLaptop;database=petneed;port=3306;password=lolwut37");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
                 "use petneed; \n" +
                "Select NotifID  \n" +
                "From notifs \n" +
                "Where notifs.NotifTime = \"" + Time + "\" \n";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ID = reader.GetInt16("NotifID");

            }
            connection.Close();
            return ID;

        }
        void populateDB(string weekday, string typ, int ID)
        {

            MySqlConnection connection = new MySqlConnection("server=68.234.189.227;user=AronLaptop;database=petneed;port=3306;password=lolwut37");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
                 "use petneed; \n" +
                "insert into weekdays(NotifID, Weekday ) values("+ ID + ", \""+weekday+"\"); " +
                "insert into notiftypes(NotifID, NotifType ) values ("+ ID + ",\"" + typ+"\");";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) { }

            connection.Close();
            return;
        }

        private void Clear(object sender, EventArgs e)
        {
            Notifications.Clear();
        }
    }
}