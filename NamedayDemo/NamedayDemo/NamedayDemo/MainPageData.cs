using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedayDemo
{
    class MainPageData : INotifyPropertyChanged
    {
        // Defaults to "Hello" if not set
        public string Greeting { get; set; } = "Hello NSCC!";
        // List of NamedayModel classes
        public List<NamedayModel> Namedays
        {
            get; set;
        }
        private NamedayModel _selectedNameday;

        public event PropertyChangedEventHandler PropertyChanged;

        public NamedayModel SelectedNameday
        {
            get
            {
                return _selectedNameday;
            }
            set
            {
                _selectedNameday = value;
                if (value == null)
                {
                    Greeting = "Hello World!";
                } else
                {
                    Greeting = "Hello " + value.NamesAsString;
                }
                PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs("Greeting"));
            }
        }
        public MainPageData()
        {
            Namedays = new List<NamedayModel>();
            for (int month = 1; month <= 12; month++)
            {
                Namedays.Add(new NamedayModel(month, 1, new string[] { "Adam" }));
                Namedays.Add(new NamedayModel(month, 24, new string[] { "Eve", "Ronan" }));
            }
        }
    }
}
