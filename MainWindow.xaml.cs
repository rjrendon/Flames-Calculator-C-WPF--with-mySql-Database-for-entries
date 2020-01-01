using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Flames_Layout2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=root");

        public MainWindow()
        {
            InitializeComponent();
        }
      

        string name1, name2;
        string lettersOnly = "[^a-zA-Z]";
        string resultFlames;


        private void TxtBoxName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            name1 = TxtBoxName1.Text;
        }
        private void TxtBoxName1_previewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(lettersOnly).IsMatch(e.Text);
        }

        private void TxtBoxName2_TextChanged(object sender, TextChangedEventArgs e)
        {
            name2 = TxtBoxName2.Text;
        }
        private void TxtBoxName2_previewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(lettersOnly).IsMatch(e.Text);

        }
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            CalcBtn.IsEnabled = true;

            TextBlkShowRes.Text = String.Empty;
            TxtBoxName1.Text = String.Empty;
            TxtBoxName2.Text = String.Empty;
            name1 = String.Empty;
            name2 = String.Empty;
            CalcInfoName1.Text = String.Empty;
            CalcInfoName2.Text = String.Empty;
            MatchedTxtBox.Text = String.Empty;
            TotalMrchLetters.Text = String.Empty;

            ImageStatus.Source = new Uri("C:/shared/Programming/Programming/Programming/wpf c#/Flames Layout2/Images/gif/default.gif");

            TxtBoxName1.IsEnabled = true;
            TxtBoxName2.IsEnabled = true;


        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            FlamesCalcGif.Position = TimeSpan.FromMilliseconds(1);
            ImageStatus.Position = TimeSpan.FromMilliseconds(1);


        }

       

        private void CalcBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name1 = null;
                name2 = null;
            }
            catch(NullReferenceException)
            {
                throw;
            }
            finally
            {
                name1 = TxtBoxName1.Text;
                name2 = TxtBoxName2.Text;

            }


            name1 = name1.Replace(" ", "");
            name2 = name2.Replace(" ", "");


            MatchedTxtBox.Text = "Matched Letters"; 
           
            int flamesCount = 0;
           
            for (int i = 0; i < name1.Length; i++)
            {
              
                if (name2.Contains(name1[i]))
                {
                    flamesCount++;
                    char b = name1[i];
                    Console.WriteLine(b.ToString().ToUpper());
                  
                    CalcInfoName1.Text += b.ToString().ToUpper()+" , ";
                }

            }
            for (int j = 0; j < name2.Length; j++)
            {

                if (name1.Contains(name2[j]))
                {
                    flamesCount++;
                    char g = name2[j];
                     Console.WriteLine(g.ToString().ToUpper());
                   CalcInfoName2.Text +=  g.ToString().ToUpper()+ " , " ;

                }
                

            }
            TotalMrchLetters.Text = "Total Matched Letters: "+flamesCount;
            

            // ternary version :)  
            //  flamesCount = flamesCount % 6 != 0 ? flamesCount % 6 : (flamesCount == 0 ? 0 : 6);


            //ez version 
            if (flamesCount % 6 != 0)
            {
                flamesCount = flamesCount % 6;
            }
            else if (flamesCount == 0)
            {
                flamesCount = 0;
            }
            else
                flamesCount = 6;

            CalcBtn.IsEnabled = true;


            switch (flamesCount)
            {
                
                case 1:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " are FRIENDS only";
                    ImageStatus.Source = new Uri("Images/gif/friends.gif", UriKind.Relative);
                    resultFlames = "Friends";
                    break; 
                case 2:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " are LOVERS ";
                    ImageStatus.Source = new Uri("Images/gif/lovers.gif", UriKind.Relative);
                    resultFlames = "Lovers";
                    break;
                case 3:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " are  ANGRY to each other";
                    ImageStatus.Source = new Uri("Images/gif/angry.gif", UriKind.Relative);
                    resultFlames = "Angry";
                    break;
                case 4:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " are  soon or now to be MARRIED ";
                    ImageStatus.Source = new Uri("Images/gif/marriage.gif", UriKind.Relative);
                    resultFlames = "Marriage";
                    break;
                case 5:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " are  ENEMIES";
                    ImageStatus.Source = new Uri ("Images/gif/enemies.gif", UriKind.Relative);
                    resultFlames = "Enemies";
                    break;
                case 6:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " are  SWEETHEARTS";
                    ImageStatus.Source = new Uri("Images/gif/sweethearts.gif", UriKind.Relative);
                    resultFlames = "Sweethearts";
                    break;
                default:
                    TextBlkShowRes.Text = name1 + " and " + name2 + " has no match maybe someday";
                    ImageStatus.Source = new Uri("Images/gif/nothing.gif", UriKind.Relative);
                    resultFlames = "No Match";
                    if (TxtBoxName1.Text == string.Empty || TxtBoxName2.Text == string.Empty)
                    {
                        CalcBtn.IsEnabled = true;
                        ImageStatus.Source = new Uri("Images/gif/noinput2.gif", UriKind.Relative);

                        TextBlkShowRes.Text = "No Input :(";
                        string showMessage = "No Input, Don't be shy";
                        MessageBox.Show(showMessage);
                       
                    }
                    break;

           

            }
            CalcBtn.IsEnabled = false;
            TxtBoxName1.IsEnabled = false;
            TxtBoxName2.IsEnabled = false;
 

            string insertQuery = "INSERT INTO flames_results.entries(name1,name2,result) VALUES('" + TxtBoxName1.Text + "','" + TxtBoxName2.Text + "','" + resultFlames + "')";

            try
            {
              
                connection.Open();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();

            }

            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            if (TxtBoxName1.Text == String.Empty && TxtBoxName2.Text == String.Empty)
            {
                connection.Close();
                MessageBox.Show("Data Not Inserted, No input");
               
            }

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Data Recorded :)");
                }

                else
                {
                    MessageBox.Show("Data Not Inserted Check Database Connection");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }
    }
}
