using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string path;
        private bool isEncryptText = true;
        Dictionary<char, int> symbolFrequencyes;

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            ToText.Text = isEncryptText ?
                EncryptText(FromText.Text) :
                DecryptText(FromText.Text);
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            isEncryptText = !isEncryptText;
        }

        private string EncryptText(string text)
        {
            StringBuilder result;
            if (!File.Exists(path) || new FileInfo("file").Length != 0)
            {
                GenerateCodes(); // need to returns collection
                // serealization
            }
            
            //work with collection and text
            // if symbol in text isn't a key then error
        }

        private void GenerateCodes()
        {
            List<int> possibleCodes = Enumerable.Range(0, 256).ToList();
            Dictionary<char, List<int>> symbolCodes = new Dictionary<char, List<int>>();

            while (possibleCodes.Count > 0)
            {
                foreach (var i in symbolFrequencyes)
                {
                    var addedCodes = new List<int>();
                    var rnd = new Random();

                    for (
                        int j = 0;
                        j < possibleCodes.Count / symbolFrequencyes.Count * i.Value;
                        j++
                        )
                    {
                        var index = rnd.Next(0, possibleCodes.Count - 1);
                        addedCodes.Add(possibleCodes[index]);
                        possibleCodes.RemoveAt(index);
                    }

                    symbolCodes[i.Key] =
                        symbolCodes[i.Key] == null ?
                        addedCodes :
                        symbolCodes[i.Key].Concat(addedCodes).ToList();
                }
            }
        }

        private string DecryptText(string text)
        {
            //deserealization
            //if file isn't exist Error
        }

        private Dictionary<char, List<int>> ReadJSON(string filename)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = File.ReadAllText(filename);
            var dict = JsonSerializer.Deserialize<Dictionary<char, List<int>>>(json, options);

            return dict;
        }

        private void WriteJSON(string filename, Dictionary<char, List<int>> dict)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = JsonSerializer.Serialize(dict, options);
            File.WriteAllText(filename, json);

        }

        public Form1()
        {
            InitializeComponent();

            var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.Parent.FullName;
            path = $"{baseDirectory}.txt";

            symbolFrequencyes =
                new Dictionary<char, int>()
                {

                };

            symbolFrequencyes = (Dictionary<char, int>)symbolFrequencyes.OrderBy(t => t.Value);
        }
    }
}
