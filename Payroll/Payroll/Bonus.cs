using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Payroll
{
    public partial class Bonus : Form
    {
        public Bonus()
        {
            InitializeComponent();
            ShowBonus();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BASTE\source\repos\Payroll\Payroll\Payroll.mdf;Integrated Security=True");
        private int key;

        private void Clear()
        {
            BonusTb.Text = "";
            AmountTb.Text = "";

            key = 0;
        }

        private void ShowBonus()
        {
            Con.Open();
            string Query = "Select * from BonusTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BonusDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (BonusTb.Text == "" || AmountTb.Text == "" ) 
            {
                MessageBox.Show("Missing Information");
            }
            else
            
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BonusTbl(BonusName,BonusAmount) values(@BN, @BA)", Con);
                    cmd.Parameters.AddWithValue("@BN", BonusTb.Text);
                    cmd.Parameters.AddWithValue("@BA", AmountTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved Sucessfully");
                    Con.Close();
                    ShowBonus();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void BonusDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BonusTb.Text = BonusDGV.SelectedRows[0].Cells[1].Value.ToString();
            AmountTb.Text = BonusDGV.SelectedRows[0].Cells[2].Value.ToString();
            if(BonusTb.Text == "")
            {
                Key = 0;
            }else
            {
                Key = Convert.ToInt32(BonusDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
                
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BonusTb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else

            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update BonusTbl Set BonusName=@BN,BonusAmount=@BA where BonusID=@key", Con);
                    cmd.Parameters.AddWithValue("@BN", BonusTb.Text);
                    cmd.Parameters.AddWithValue("@BA", AmountTb.Text);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Sucessfully");
                    Con.Close();
                    ShowBonus();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Bonus");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from BonusTbl Where BonusID=@BKey", Con);
                    cmd.Parameters.AddWithValue("@BKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Sucessfully");
                    Con.Close();
                    ShowBonus();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
     }
   }
   


