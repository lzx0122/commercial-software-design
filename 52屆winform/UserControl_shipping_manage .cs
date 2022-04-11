﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _52屆winform
{
    public partial class UserControl1 : UserControl
    {

        private C_SQLServerLib lib;
        C_SQLconn conclass = new C_SQLconn();
        SqlDataReader connReader;
        SqlCommand cmd;
        public UserControl1(C_SQLServerLib e)
        {
            InitializeComponent();
            lib = e;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            reloading();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }


        private void reloading()
        {

            dataGridView1.DataSource = lib.GetDataSet($"select IMO,英文船名,中文船名,載噸重,船舶種類中文名稱,船商名稱,船東 from 船舶基本資料" +
                $" inner join 船種 on 船舶基本資料.船種DI=船種.船種ID" +
                $" inner join 船公司 on 船舶基本資料.船公司ID=船公司.船公司ID ", "船舶基本資料").Tables["船舶基本資料"];

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows[0].Index != -1)
                {
                    if (dataGridView1.SelectedRows[0].Cells[0].Value != null) { 
                        textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                        textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                        textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                        textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();






                    }
                }

            }
            catch (Exception ex) { 
            
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string isNull = lib.GetSQLvalue($"select IMO from 船舶基本資料 where IMO='{textBox1.Text}'", "IMO");
            try
            {
                if (isNull == null)
                {
                    cmd = new SqlCommand();
                    conclass.openConn();
                    cmd.CommandText = $"insert into 船舶基本資料 (IMO,英文船名,中文船名,載噸重,船種DI,船公司ID,船東) " +
                        $"select '{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}'" +
                        $",(select 船種ID from 船種 where 船舶種類中文名稱='{textBox5.Text}')," +
                        $"(select 船公司ID from 船公司 where 船商名稱='{textBox6.Text}'),'{textBox7.Text}'";
                    cmd.Connection = conclass.conn;
                    cmd.ExecuteNonQuery();
                    reloading();
                    MessageBox.Show("修改完成", "訊息");
                }
                else {

                    MessageBox.Show("IMO資料不可重複", "錯誤");
                }
            }
            catch (Exception ex) {

               

            }
           
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand();
                conclass.openConn();
                cmd.CommandText = $"DELETE FROM 船舶基本資料 WHERE IMO='{textBox1.Text}' ";
                cmd.Connection = conclass.conn;
                cmd.ExecuteNonQuery();
                reloading();
                MessageBox.Show($"已成功刪除{textBox1.Text}", "訊息");
            }
            catch (Exception ex)
            {
                MessageBox.Show("","錯誤");
            }
        }
    }
}
