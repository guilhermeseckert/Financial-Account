using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        decimal number;
        decimal Totaldeposits;
        decimal Totalwithdrawal;
        decimal Totalaccount;

        string path = @"bin\debug\TransactionError.dat";

        public Form1()
        {
            InitializeComponent();





        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }



        private void Btn_Process_Click(object sender, EventArgs e)
        {


            listBox1.Items.Clear();
            Total.Text = String.Empty;
            Totaldeposits = 0;
            Totalwithdrawal = 0;
            Totalaccount = 0;
            Total.BackColor = Color.DarkCyan;
            Total.ForeColor = Color.Gold;
            listBox1.Items.Add(TextFirstName.Text.ToLower() + " " + TextLastName.Text.ToLower());
            listBox1.Items.Add("=======================");
            loadCustomer();

         
            if (RadioDeposit.Checked == true )
            {
                listBox1.Items.Add("=====================");
                listBox1.Items.Add("Total Deposits =" + Totaldeposits.ToString("C"));
                listBox1.Items.Add("Total Account =" + Totalaccount.ToString("C"));
            } if (RadioDeposit.Checked == false )
            {
                listBox1.Items.Add("=====================");
                listBox1.Items.Add("Total Withdrawal =" + Totalwithdrawal.ToString("C"));
                listBox1.Items.Add("Total Account =" + Totalaccount.ToString("C"));
            }
           


        }



        private void loadCustomer()
        {

            try
            {

                using (var reader = new StreamReader(@"C:\Users\Guilherme\Desktop\DOc\Customer.dat"))
                {

                    int counter = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        string fullName = line.Trim();
                        var names = fullName.Split(' ');


                        if (names.Contains(TextFirstName.Text.ToLower()) || names.Contains(TextLastName.Text.ToLower()))
                        {
                            loadTransactions(counter);
                        } 

                        counter++;
                    } 
                    
                    reader.Close();

                }
            } catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }

        }


        private void loadTransactions(int counter)
        {

            var line = File.ReadLines(@"C:\Users\Guilherme\Desktop\DOc\Transaction.dat").Skip(counter).Take(1).First();

            NumberStyles style;
            CultureInfo culture;

            style = NumberStyles.Number;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            try {

                if (Decimal.TryParse(line.Trim(), style, culture, out number))
                {


                    if (number > 0 && RadioDeposit.Checked == true)
                    {

                        listBox1.Items.Add(number.ToString("C"));
                        Totaldeposits += number;


                    }

                    if (number < 0 && RadioDeposit.Checked == false)
                    {
                        listBox1.Items.Add(number.ToString("C"));
                        Totalwithdrawal += number;

                    }

                }

                else
                {
                    var name = File.ReadLines(@"C:\Users\Guilherme\Desktop\DOc\Customer.dat").Skip(counter).Take(1).First();
                    File.AppendAllText(@"C:\Users\Guilherme\Desktop\DOc\TransactionError.dat", name + " "+ line + Environment.NewLine);
                }

                Totalaccount += number;


                if (Totalaccount < 0)
                {

                    Total.BackColor = Color.Red;
                    Total.ForeColor = Color.Black;
                    Total.Text = "(" + Totalaccount.ToString("C") + ")";
                }

                if (Totalaccount > 0)
                {

                    Total.Text = Totalaccount.ToString("C");
                }
            } catch (Exception) {
                MessageBox.Show("Something went wrong");
            }

        } 

    }
 }

