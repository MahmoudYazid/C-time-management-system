using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.SQLite;

namespace time_management_system_for_SPA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public  SQLiteConnection conn_()
        {
            return new SQLiteConnection(@"data source=times.db");

        }
        public void insert() 
        {
            using (SQLiteConnection insertcon=conn_()) 
            {
                insertcon.Open();
                string cmd = string.Format("INSERT INTO time(`name`,`id`,`day`,`day_name`,`month`,`year`,`clock`,`end_date`) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", addname.Text,addid.Text,calender.SelectedDates[0].Day.ToString(), calender.SelectedDates[0].DayOfWeek.ToString().ToLower(), calender.SelectedDates[0].Month.ToString(), calender.SelectedDates[0].Year.ToString(),addclock.Text,TOCLOCK.Text);/* m b m.yaz */
                SQLiteCommand cmd_ex = new SQLiteCommand(cmd , insertcon);
                cmd_ex.ExecuteNonQuery();
                MessageBox.Show("insert done");
            }
        
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            insert();
        
        }
        public void find() 
        {
            using(SQLiteConnection find_conn = conn_()) 
            {
                listbox_show.Items.Clear();
                find_conn.Open();
                string query = string.Format("SELECT `name`,`id`,`day`,`day_name`,`month`,`year`,`clock`,`end_date` FROM time WHERE  day='{0}' OR day_name='{0}' and clock='{1}' and end_date='{2}' ", findday.Text.ToLower(),findclock.Text,to_find_txt.Text);
                SQLiteCommand cmd = new SQLiteCommand(query, find_conn);
                SQLiteDataReader result = cmd.ExecuteReader();
                while (result.Read()) 
                {
                    
                    listbox_show.Items.Add(string.Format("name :{0} , id : {1}",result[0].ToString(), result[1].ToString()));
                }
                if(listbox_show.Items.Count.ToString() == "0" || listbox_show.Items.Count.ToString() == "NULL" || listbox_show.Items.Count.ToString() == null) 
                {
                    listbox_show.Items.Add("no date recorded in this time");
                    return;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(findday.Text!="" && findclock.Text != "" && to_find_txt.Text != "") 
            {
                find();
            }
            if (findday.Text == "" && findclock.Text == "" && to_find_txt.Text == "")
            {
                MessageBox.Show("COMPLETE THE FIELDS");
                return;
            }





            }
        public void findby()
        {
            using (SQLiteConnection find_conn = conn_())
            {
                listbox_show.Items.Clear();
                find_conn.Open();
                string query = string.Format("SELECT `name`,`id`,`day`,`day_name`,`month`,`year`,`clock`,`end_date` FROM time WHERE name='{0}' OR id='{0}' ", txt_find_by_name_or_id.Text );
                SQLiteCommand cmd = new SQLiteCommand(query, find_conn);
                SQLiteDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    
                    listbox_show.Items.Add(string.Format("`day`: {0}  ,  `day_name`: {1} ,  `month`: {2} ,  `year`: {3}  , `start`:  {4},`end_date` : {5} ", result[2].ToString(), result[3].ToString(), result[4].ToString(), result[5].ToString(), result[6].ToString(), result[7].ToString()));
                }
                if (listbox_show.Items.Count.ToString() == "0" || listbox_show.Items.Count.ToString() == "NULL" || listbox_show.Items.Count.ToString() == null)
                {
                    listbox_show.Items.Add("no date recorded in this time");
                    return;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            findby();

        }
        public void delete_this_term()
        {
            using (SQLiteConnection insertcon = conn_())
            {
                insertcon.Open();
                string cmd = string.Format("DROP TABLE time ");
                SQLiteCommand cmd_ex = new SQLiteCommand(cmd, insertcon);
                cmd_ex.ExecuteNonQuery();
                string cmd2 = string.Format("CREATE TABLE time(name  TEXT,id    TEXT,day   TEXT,day_name  TEXT,month TEXT,year  TEXT,clock TEXT,end_date TEXT)");
                SQLiteCommand cmd_ex2 = new SQLiteCommand(cmd2, insertcon);
                cmd_ex2.ExecuteNonQuery();
                MessageBox.Show("new table ready");

            }

        }
          

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            delete_this_term();
        }
        public void show_all_()
        {
            using (SQLiteConnection find_conn = conn_())
            {
                listbox_show.Items.Clear();
                find_conn.Open();
                string query = string.Format("SELECT `name`,`id`,`day`,`day_name`,`month`,`year`,`clock`,`end_date` FROM time ");
                SQLiteCommand cmd = new SQLiteCommand(query, find_conn);
                SQLiteDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {

                    listbox_show.Items.Add(string.Format("name :{0} , id : {1} ,`day`: {2}  ,  `day_name`: {3} ,  `month`: {4} ,  `year`: {5}  , `start`:  {6}, `end date`:  {7}", result[0].ToString(), result[1].ToString(), result[2].ToString(), result[3].ToString(), result[4].ToString(), result[5].ToString(), result[6].ToString(), result[7].ToString()));
                }
                if (listbox_show.Items.Count.ToString() == "0" || listbox_show.Items.Count.ToString() == "NULL" || listbox_show.Items.Count.ToString() == null)
                {
                    listbox_show.Items.Add("no date recorded in this time");
                    return;
                }
            }
        }

        private void show_all_Click(object sender, RoutedEventArgs e)
        {
            show_all_();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            listbox_show.Items.Clear();
        }
        public void delete_date()
        {
            using (SQLiteConnection insertcon = conn_())
            {
                insertcon.Open();
                string cmd = string.Format("DELETE FROM time WHERE id='{0}' or name='{0}' and day_name='{1}' and clock='{2}' and end_date='{3}' ", txt_find_by_name_or_id.Text, findday.Text,findclock.Text,to_find_txt.Text);

                SQLiteCommand cmd_ex = new SQLiteCommand(cmd, insertcon);
                cmd_ex.ExecuteNonQuery();
                MessageBox.Show("delete done");
            }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            delete_date();
        }
    }
}
