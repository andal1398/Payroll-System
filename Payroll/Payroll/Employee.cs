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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            ShowEmployee();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BASTE\source\repos\Payroll\Payroll\Payroll.mdf;Integrated Security=True");
        

        private void ShowEmployee()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter (Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpGenCb.SelectedIndex == -1 || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpSalaryTb.Text == "" || EmpPos.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EmpName,EmpGender,EmpAddress,EmpPhone,JoinDate,EmpBaseSalary,EmpPosition) values (@EN, @EG, @EA, @EP, @JD, @EBS, @EPOS)", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", EmpGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@JD", JD.Value.Date);
                    cmd.Parameters.AddWithValue("EBS", EmpSalaryTb.Text);
                    cmd.Parameters.AddWithValue("@EPOS", EmpPos.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Save Sucessfully");
                    Con.Close();
                    ShowEmployee();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        int key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            JD.Text = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
            EmpSalaryTb.Text = EmployeeDGV.SelectedRows[0].Cells[6].Value.ToString();
            EmpPos.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[7].Value.ToString();
            
            if(EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpGenCb.SelectedIndex == -1 || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpSalaryTb.Text == "" || EmpPos.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTbl Set EmpName=@EN,EmpGender=@EG,EmpAddress=@EA,EmpPhone=@EP,JoinDate=@JD,EmpBaseSalary=@EBS,EmpPosition =@EPOS where EmpID=@EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", EmpGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@JD", JD.Value.Date);
                    cmd.Parameters.AddWithValue("EBS", EmpSalaryTb.Text);
                    cmd.Parameters.AddWithValue("@EPOS", EmpPos.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EmpKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Sucessfully");
                    Con.Close();
                    ShowEmployee();
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
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmployeeTbl where EmpID=@EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EmpKey", key);


                    int v = cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Sucessfully");
                    Con.Close();
                    ShowEmployee();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Bonus obj = new Bonus();
            obj.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
        
        }
    }
  }
 










