using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBConnectTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var table = ConnectDB.Connect();
            foreach (DataRow data in table.Rows)
            {
                var item = new ListViewItem(new string[] { data["Number"].ToString(), data["Name"].ToString()});
                listView1.Items.Add(item);
            }
        }
    }
}
