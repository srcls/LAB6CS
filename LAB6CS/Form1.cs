using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAB6CS
{
    public partial class Form1 : Form
    {


        Dictionary<string, List<string>> TwoLetters;
        Dictionary<string, List<string>> ThreeLetters;


        string file;
        List<string> lines;
   
        public List<string> readLines(string file, List<string> lines)
        {

            Encoding enc = Encoding.GetEncoding("iso-8859-2");
            lines = File.ReadAllLines(file, enc).ToList();
            for (int i = 0; i<lines.Count; i++)
            {
                lines[i] = lines[i].Split(' ')[1];

            }
            return lines;
        }

        public void TwoLettersDictionary(Dictionary<string, List<string>> dictionary, List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if ((lines[i].Length) < 2)
                    continue;
                if (dictionary.ContainsKey(lines[i].Substring(0, 2)))
                {
                    TwoLetters[lines[i].Substring(0, 2)].Add(lines[i]);
                }
                else
                {
                    List<string> newList = new List<string>();
                    newList.Add(lines[i]);
                    TwoLetters.Add(lines[i].Substring(0, 2), newList);
                }
            }


        }
        public void ThreeLettersDictionary(Dictionary<string, List<string>> dictionary, List<string> lines)
        {

            for (int i = 0; i < lines.Count; i++)
            {
                if ((lines[i].Length) < 3)
                    continue;

                if (dictionary.ContainsKey(lines[i].Substring(0, 3)))
                {
                    dictionary[lines[i].Substring(0, 3)].Add(lines[i]);
                }
                else
                {
                    List<string> newList = new List<string>();
                    newList.Add(lines[i]);
                    dictionary.Add(lines[i].Substring(0, 3), newList);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file = ofd.FileName;
                Console.WriteLine(file);

            }
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            lines = readLines(file, lines);
            watch.Stop();
            label1.Text = $"Reading all lines: {watch.ElapsedMilliseconds} ms";

            TwoLetters = new Dictionary<string, List<string>>();
            watch.Start();
            TwoLettersDictionary(TwoLetters, lines);
            watch.Stop();
            label2.Text = $"Making two letter dictionary:  {watch.ElapsedMilliseconds} ms";

            
            ThreeLetters = new Dictionary<string, List<string>>();
            watch.Start();
            ThreeLettersDictionary(ThreeLetters, lines);
            watch.Stop();
            label3.Text = $"Making three letter dictionary: {watch.ElapsedMilliseconds} ms";
            

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.ResetText();
            string key = textBox1.Text;
            if(key.Length == 2)
            {
                if (TwoLetters.ContainsKey(key))
                {
                    List<string> values = TwoLetters[key];
                    string suggestions = "";
                    for(int i = 0; i < 3; i++)
                    {
                        suggestions += values[i] + "\n";
                    }
                    label4.Text = suggestions;
                }
            }
            else if(key.Length == 3)
            {
                if (ThreeLetters.ContainsKey(key))
                {
                    List<string> values = ThreeLetters[key];
                    string suggestions = "";
                    for (int i = 0; i < 3; i++)
                    {
                        suggestions += values[i] + "\n";
                    }
                    label4.Text = suggestions;
                }
            }
        }
    }
}
