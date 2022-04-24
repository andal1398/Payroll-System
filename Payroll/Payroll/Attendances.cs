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
    public partial class Attendances : Form
    {
        public Attendances()
        {
            InitializeComponent();
            ShowAttendance();
            GetEmployee();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BASTE\source\repos\Payroll\Payroll\Payroll.mdf;Integrated Security=True");

        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAbsent.Text = "";
            EmpExcused.Text = "";
            EmpPresent.Text = "";
            Key = 0;                  

        }
        private void ShowAttendance()
        {
            Con.Open();
            string Query = "Select * from AttendanceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AttendanceDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetEmployee()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from EmployeeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpID", typeof(int));
            dt.Load(Rdr);
            EmpIDCb.ValueMember = "EmpID";
            EmpIDCb.DataSource = dt;
            Con.Close();
        }
        private void GetEmployeeName()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl where EmpID=" + EmpIDCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                EmpNameTb.Text = dr["EmpName"].ToString();
            }
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPresent.Text == "" || EmpAbsent.Text == "" || EmpExcused.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string Period = AttDate.Value.Month + "-" + AttDate.Value.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AttendanceTbl(EmpID,EmpName,DayPresent,DayAbsent,DayExcused,Period,)values(@EI,@EN,@DP,@DA,@DE,@Per)", Con);
                    cmd.Parameters.AddWithValue("@EI", EmpIDCb.Text);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@DP", EmpPresent.Text);
                    cmd.Parameters.AddWithValue("@DA", EmpAbsent.Text);
                    cmd.Parameters.AddWithValue("@DE", EmpExcused.Text);
                    cmd.Parameters.AddWithValue("@Per", Period);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Sucessfully");
                    Con.Close();
                    ShowAttendance();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }



        int Key = 0;
        private void AttendanceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = AttendanceDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpIDCb.SelectedItem = AttendanceDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpPresent.Text = AttendanceDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpAbsent.Text = AttendanceDGV.SelectedRows[0].Cells[4].Value.ToString();
            EmpExcused.Text = AttendanceDGV.SelectedRows[0].Cells[5].Value.ToString();
            
            if (EmpNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AttendanceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EmpIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetEmployeeName();
        }

        private void EmpIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPresent.Text == "" || EmpExcused.Text == "" || EmpAbsent.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string Period = AttDate.Value.Month + "-" + AttDate.Value.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AttendanceTbl Set EmpID=@EI,EmpName=@EN,DayPresent=@DP,DayAbsent=@DA,DayExcused=@PR,Period=@PR where AttNum=@AttKey", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@DP", EmpPresent.Text);
                    cmd.Parameters.AddWithValue("@DA", EmpAbsent.Text);
                    cmd.Parameters.AddWithValue("@DE", EmpExcused.Text);
                    cmd.Parameters.AddWithValue("@PR", Period);
                    cmd.Parameters.AddWithValue("@AttKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Sucessfully");
                    Con.Close();
                    ShowAttendance();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AttendanceTbl where AttNum=@AttKey", Con);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);


                    int v = cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Sucessfully");
                    Con.Close();
                    ShowAttendance();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}


