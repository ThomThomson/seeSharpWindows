using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Simple calculator to demonstrate WPF.
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean HasDecimal = false;
   
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(this.btnC))
            {
                this.screen.Text = "";
            }
            else if (sender.Equals(this.btnDot))
            {
                if (!HasDecimal)
                {
                    screen.Text += ".";
                    HasDecimal = true;
                }
            }
            else if (sender.Equals(btnDiv) ||
                sender.Equals(btnPlus) ||
                sender.Equals(btnMinus) ||
                sender.Equals(btnMult))
            {
                HasDecimal = false;
                screen.Text += ((Button)(sender)).Content;
                checkOp();
            }
            else if (sender.Equals(btnEquals))
            {
                //do the math.
                screen.Text = Evaluate(screen.Text);
                HasDecimal = false;
            }
            else
            {
                screen.Text += (sender as Button).Content;
                HasDecimal = false;
            }

        }

        private void checkOp()
        {
            String s = this.screen.Text;
            if (Regex.IsMatch(s, "^[\\+*/]", RegexOptions.IgnoreCase))
            {
                this.screen.Text = "";
            }
        }
        private string Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            Double exp = double.Parse((string)row["expression"]);
            return exp.ToString();
        }
    }
}
