using System;
using System.Linq;
using System.Text;

namespace HammingСode
{
    internal class HammingCode
    {
        private int sizeBlock;

        public HammingCode(int SizeBlock)
        {
            if (SizeBlock < 1)
            {
                throw new ArgumentException("size block cannot be less than 1");
            }

            sizeBlock = SizeBlock;
        }

        public string AddPartlyBits(string originalMessage)
        {
            ThrowIfNotBitsMessage(originalMessage);
            int addBits, countBlocks;
            int[][] matrixErrorsSyndroms =
                CalculateMatrixErrorSyndroms
                (
                    originalMessage,
                    out addBits,
                    out countBlocks
                );

            StringBuilder sb = new StringBuilder();

            for ( int i = 0; i < countBlocks - 1; i++)
            {
                sb.Append(string.Join("", matrixErrorsSyndroms[i]));
            }

            sb.Append(string.Join("", matrixErrorsSyndroms[countBlocks - 1].Take(sizeBlock - addBits)));
            sb.Append(matrixErrorsSyndroms[countBlocks - 1][sizeBlock]);
            sb.Append(string.Join("", matrixErrorsSyndroms[countBlocks]));

            return sb.ToString();
        }

        private int[][] CalculateMatrixErrorSyndroms
            (
            string originalMessage,
            out int addBits,
            out int countBlocks
            )
        {
            addBits = sizeBlock - originalMessage.Length % sizeBlock;
            if (addBits > 0)
            {
                originalMessage = $"{originalMessage}{new string('0', addBits)}";
            }

            countBlocks = originalMessage.Length / sizeBlock;
            var matrixErrorsSyndroms = new int[countBlocks + 1][];
            for (int i = 0; i < countBlocks; i++)
            {
                var block =
                    originalMessage.
                    Skip(i * sizeBlock).
                    Take(sizeBlock).
                    Select(t => t == '0' ? 0 : 1).
                    ToArray();

                var partlyBit = block[0];
                for (int j = 1; j < sizeBlock; j++)
                {
                    partlyBit ^= block[j];
                }

                matrixErrorsSyndroms[i] = block.Append(partlyBit).ToArray();
            }

            matrixErrorsSyndroms[countBlocks] = new int[sizeBlock + 1];
            for (int i = 0; i < sizeBlock; i++)
            {
                matrixErrorsSyndroms[countBlocks][i] = matrixErrorsSyndroms[0][i];
                for (int j = 1; j < countBlocks; j++)
                {
                    matrixErrorsSyndroms[countBlocks][i] ^= matrixErrorsSyndroms[j][i];
                }
            }

            matrixErrorsSyndroms[countBlocks][sizeBlock] = matrixErrorsSyndroms[countBlocks][0];
            for (int i = 1; i < sizeBlock; i++)
            {
                matrixErrorsSyndroms[countBlocks][sizeBlock] ^=
                    matrixErrorsSyndroms[countBlocks][i];
            }

            for (int i = 0; i < countBlocks; i++)
            {
                matrixErrorsSyndroms[countBlocks][sizeBlock] ^=
                matrixErrorsSyndroms[i][sizeBlock];
            }

            return matrixErrorsSyndroms;
        }

        private void ThrowIfNotBitsMessage(string originalMessage)
        {
            if
                (
                    originalMessage == null ||
                    originalMessage == string.Empty ||
                    originalMessage.Count(t => t == '0' || t == '1') != originalMessage.Length
                )
            {
                throw new Exception("It's not a bit message!");
            }
        }

        public string FixError(string sandedMessage)
        {
            ThrowIfNotBitsMessage(sandedMessage);

            var countBlocks = (int)Math.Ceiling((double)sandedMessage.Length / (sizeBlock + 1)) - 1;
            var addBits = countBlocks * (sizeBlock + 1) - sandedMessage.Length + sizeBlock + 1;

            StringBuilder sb = new StringBuilder();

            int[] partlyRows = new int[countBlocks];
            int[] partlyCols = new int[sizeBlock + 1];
            for (int i = 0; i * sizeBlock + i - 1 + sizeBlock < sandedMessage.Length - sizeBlock - 2; i++)
            {
                sb.Append(sandedMessage.Substring(i * sizeBlock + i, sizeBlock));
                partlyRows[i] = sandedMessage[i * sizeBlock + i + sizeBlock] == '0' ? 0 : 1;
            }

            sb.Append(sandedMessage.Substring(sandedMessage.Length - sizeBlock - 2 - 1, sizeBlock - addBits));
            partlyRows[countBlocks - 1] = sandedMessage[sandedMessage.Length - sizeBlock - 2] == '0' ? 0 : 1;
            
            for (int i = 0; i < sizeBlock + 1; i++)
            {
                partlyCols[i] = sandedMessage[sandedMessage.Length - sizeBlock + i - 1] == '0' ? 0 : 1;
            }

            int[][] matrixErrorsSyndroms =
                CalculateMatrixErrorSyndroms
                (
                    sb.ToString(),
                    out _,
                    out _
                );

            int syndromRow = -1;
            int syndromCol = -1;
            for (int i = 0; i < countBlocks; i++)
            {
                if (matrixErrorsSyndroms[i][sizeBlock] != partlyRows[i])
                {
                    if (syndromRow != -1)
                    {
                        throw new Exception("Error: cannot fix double error");
                    }
                    syndromRow = i;
                }
            }

            for (int i = 0; i < sizeBlock + 1; i++)
            {
                if (matrixErrorsSyndroms[countBlocks][i] != partlyCols[i])
                {
                    if (syndromCol != -1)
                    {
                        throw new Exception("Error: cannot fix double error");
                    }
                    syndromCol = i;
                }
            }

            if (syndromCol == sizeBlock)
            {
                throw new Exception("Error: cannot fix error in partly bit");
            }

            if (syndromCol == -1 && syndromRow == -1)
            {
                return sandedMessage;
            }

            if (Math.Min(syndromCol, syndromRow) == -1)
            {
                throw new Exception("Error: cannot fix error in partly bit");
            }

            var beforeError = string.Join("", sandedMessage.Take(syndromRow * (sizeBlock + 1) + syndromCol));
            var afterError = string.Join("", sandedMessage.Skip(syndromRow * (sizeBlock + 1) + syndromCol + 1));
            Console.WriteLine(beforeError);
            Console.WriteLine(beforeError);
            return $"{beforeError}{1 - matrixErrorsSyndroms[syndromRow][syndromCol]}{afterError}";
        }
    }
}