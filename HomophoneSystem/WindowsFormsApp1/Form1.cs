using System;
using System.Collections.Generic;
using System.Drawing;
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
        Dictionary<char, float> symbolFrequencyes;

        private void FromText_Changed(object sender, EventArgs e)
        {
            ToText.ForeColor = Color.Black;
            ToText.Font = new Font(ToText.Font.FontFamily, 14);

            try
            {
                ToText.Text = isEncryptText ?
                    EncryptText(FromText.Text) :
                    DecryptText(FromText.Text);
            }
            catch (Exception ex)
            {
                ToText.Text = ex.Message;
                ToText.Font = new Font(ToText.Font.FontFamily, 25);
                ToText.ForeColor = Color.Red;
            }
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            isEncryptText = !isEncryptText;
            FromText.Text = ToText.Text;
            string c = FromTextLabel.Text;
            FromTextLabel.Text = ToTextLabel.Text;
            ToTextLabel.Text = c;
        }

        private string EncryptText(string text)
        {
            if (text.Length == 0)
            {
                return string.Empty;
            }

            Dictionary<char, List<string>> symbolCodes = new Dictionary<char, List<string>>();
            StringBuilder encryptedText = new StringBuilder();

            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                symbolCodes = GenerateCodes();
                WriteJSON(path, symbolCodes);
            }
            else
            {
                symbolCodes = ReadJSON(path);
            }

            var rnd = new Random();
            foreach (var i in text)
            {
                if (!symbolCodes.ContainsKey(i))
                {
                    throw new Exception($"An unknown '{i}' symbol has been encountered");
                }

                var codes = symbolCodes[i];
                encryptedText.Append(codes[rnd.Next(0, codes.Count - 1)]);
            }

            return encryptedText.ToString();
        }

        private Dictionary<char, List<string>> GenerateCodes()
        {
            List<string> possibleCodes =
                Enumerable.Range(0, 999).
                Select(t => $"{new string('0', 3 - t.ToString().Length)}{t}").
                ToList();

            Dictionary<char, List<string>> symbolCodes = new Dictionary<char, List<string>>();

            while (possibleCodes.Count > 0)
            {
                var count = possibleCodes.Count;
                foreach (var i in symbolFrequencyes)
                {
                    var addedCodes = new List<string>();
                    var rnd = new Random();

                    var codesCount = Math.Round(
                        count / symbolFrequencyes.Count * Math.Min(1, i.Value * 10));
                    
                    Console.WriteLine($"{possibleCodes.Count}");
                    
                    if (codesCount == 0)
                    {
                        codesCount = 1;
                    }

                    for (int j = 0; j <= codesCount; j++)
                    {
                        var index = rnd.Next(0, possibleCodes.Count - 1);
                        addedCodes.Add(possibleCodes[index]);
                        possibleCodes.RemoveAt(index);
                    }

                    symbolCodes[i.Key] =
                        !symbolCodes.ContainsKey(i.Key) ?
                        addedCodes :
                        symbolCodes[i.Key].Concat(addedCodes).ToList();

                    if (possibleCodes.Count == 0)
                    {
                        break;
                    }
                }
            }

            return symbolCodes;
        }

        private string DecryptText(string text)
        {
            if (
                !File.Exists(path) ||
                new FileInfo(path).Length == 0 ||
                text.Length % 3 != 0
                )
            {
                throw new Exception("Error. The text is not decipherable");
            }

            var symbolCodes = ReadJSON(path);
            StringBuilder decryptedText = new StringBuilder();

            for (int i = 0; i < text.Length; i += 3)
            {
                KeyValuePair<char, List<string>> pair;
                try
                {
                    pair = symbolCodes.
                        First(t => t.Value.Contains(text.Substring(i, 3)));
                }
                catch
                {
                    throw new Exception("Error. The text is not decipherable");
                }

                decryptedText.Append(pair.Key);
            }

            return decryptedText.ToString();
        }

        private Dictionary<char, List<string>> ReadJSON(string filename)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = File.ReadAllText(filename);
            var dict = JsonSerializer.Deserialize<Dictionary<char, List<string>>>(json, options);

            return dict;
        }

        private void WriteJSON(string filename, Dictionary<char, List<string>> dict)
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
                .Parent.FullName;
            path = $"{baseDirectory}/codes.txt";

            symbolFrequencyes =
                new Dictionary<char, float>()
                {
                    {' ', 0.175f},
                    {'О', 0.090f}, {'о', 0.090f},
                    {'Е', 0.072f}, {'е', 0.072f},
                    {'А', 0.062f}, {'а', 0.062f},
                    {'И', 0.062f}, {'и', 0.062f},
                    {'Н', 0.053f}, {'н', 0.053f},
                    {'Т', 0.053f}, {'т', 0.053f},
                    {'С', 0.045f}, {'с', 0.045f},
                    {'Р', 0.040f}, {'р', 0.040f},
                    {'В', 0.038f}, {'в', 0.038f},
                    {'Л', 0.035f}, {'л', 0.035f},
                    {'К', 0.028f}, {'к', 0.028f},
                    {'М', 0.026f}, {'м', 0.026f},
                    {'Д', 0.025f}, {'д', 0.025f},
                    {'П', 0.023f}, {'п', 0.023f},
                    {'У', 0.021f}, {'у', 0.021f},
                    {'Я', 0.018f}, {'я', 0.018f},
                    {'Ы', 0.016f}, {'ы', 0.016f},
                    {'Ь', 0.014f}, {'ь', 0.014f},
                    {'Б', 0.014f}, {'б', 0.014f},
                    {'Ч', 0.012f}, {'ч', 0.012f},
                    {'Й', 0.010f}, {'й', 0.010f},
                    {'Х', 0.009f}, {'х', 0.009f},
                    {'Ж', 0.007f}, {'ж', 0.007f},
                    {'Ю', 0.006f}, {'ю', 0.006f},
                    {'Ш', 0.006f}, {'ш', 0.006f},
                    {'Ц', 0.004f}, {'ц', 0.004f},
                    {'Щ', 0.003f}, {'щ', 0.003f},
                    {'Э', 0.003f}, {'э', 0.003f},
                    {'Ф', 0.002f}, {'ф', 0.002f},
                    {'Ё', 0.005f}, {'ё', 0.005f},
                    {'Г', 0.016f}, {'г', 0.016f},
                    {'З', 0.009f}, {'з', 0.009f},
                    {'Ъ', 0.014f}, {'ъ', 0.014f},

                    {'E', 0.123f}, {'e', 0.123f},
                    {'T', 0.096f}, {'t', 0.096f},
                    {'A', 0.081f}, {'a', 0.081f},
                    {'O', 0.079f}, {'o', 0.079f},
                    {'N', 0.072f}, {'n', 0.072f},
                    {'I', 0.071f}, {'i', 0.071f},
                    {'S', 0.066f}, {'s', 0.066f},
                    {'R', 0.060f}, {'r', 0.060f},
                    {'H', 0.051f}, {'h', 0.051f},
                    {'L', 0.040f}, {'l', 0.040f},
                    {'D', 0.036f}, {'d', 0.036f},
                    {'C', 0.032f}, {'c', 0.032f},
                    {'U', 0.031f}, {'u', 0.031f},
                    {'M', 0.022f}, {'m', 0.022f},
                    {'F', 0.022f}, {'f', 0.022f},
                    {'B', 0.016f}, {'b', 0.016f},
                    {'G', 0.016f}, {'g', 0.016f},
                    {'V', 0.009f}, {'v', 0.009f},
                    {'K', 0.005f}, {'k', 0.005f},
                    {'Q', 0.002f}, {'q', 0.002f},
                    {'X', 0.002f}, {'x', 0.002f},
                    {'J', 0.001f}, {'j', 0.001f},
                    {'Z', 0.001f}, {'z', 0.001f},
                    {'W', 0.019f}, {'w', 0.019f},
                    {'Y', 0.019f}, {'y', 0.019f},
                    {'P', 0.020f}, {'p', 0.020f}
                };

            symbolFrequencyes =
                symbolFrequencyes.
                OrderBy(t => t.Value).
                ToDictionary(t => t.Key, t => t.Value);
        }
    }
}
