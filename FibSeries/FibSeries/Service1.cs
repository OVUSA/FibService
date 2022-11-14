using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FibSeries
{
    public partial class Service1 : ServiceBase
    {
        public int number;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(ReadWrite);
            t.Start();
        }

        public void ReadWrite()
        {
            try
            {
                StreamReader st = new StreamReader(@"C:\Users\velic\source\repos\FibSeries\FibSeries\TextFile.txt");
                number = Int32.Parse(st.ReadToEnd());
                st.Close();

                try
                {
                    StreamWriter sw = new StreamWriter(@"C:\Users\velic\source\repos\FibSeries\FibService.txt", false);
                    sw.WriteLine(Fibonacci(number));
                    sw.Flush();
                    sw.Close();
                }
                catch (IOException ex)
                {
                    StreamWriter sw = new StreamWriter(@"C:\Users\velic\source\repos\FibSeries\FibService.txt");
                    sw.WriteLine(ex);
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (IOException ex)
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\velic\source\repos\FibSeries\FibService.txt");
                sw.WriteLine("Couldn't read the file " + ex);
                sw.Flush();
                sw.Close();
            }
        }
        public string Fibonacci(int number)
        {
            int f = 0;
            int f1 = 1;

            int[] nums = new int[number + 1];
            nums[0] = f;
            nums[1] = f1;

            for (int i = 2; i <= number; i++)
            {
                int f2 = f + f1;
                nums[i] = f2;
                f = f1;
                f1 = f2;
            }

            string st = string.Join(", ", nums);
            return st;
        }

        protected override void OnStop()
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\velic\source\repos\FibSeries\FibService.txt", true);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();
        }
    }
}

