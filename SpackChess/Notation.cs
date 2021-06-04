using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpackChess
{
    public class Notation : INotation
    {
        protected List<string> m_gameRecord = new List<string>();  
       
        public List<string> GameRecord
        {
            get { return m_gameRecord; }         
        }

        public string GetFileName(bool save)
        {
            Microsoft.Win32.FileDialog saveGameDialog;

            if (save)
            {
                saveGameDialog = new Microsoft.Win32.SaveFileDialog();
            }
            else
            {
                saveGameDialog = new Microsoft.Win32.OpenFileDialog();
            }
                
            saveGameDialog.DefaultExt = ".txt";
            saveGameDialog.Filter = "Textdateien (.txt)|*.txt";

            Nullable<bool> dialogResult = saveGameDialog.ShowDialog();

            if (dialogResult == true)
            {
                return saveGameDialog.FileName;
            }
            else { return null; }
        }

        public void WriteFile(string fileName)
        {
            if (fileName != null)
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                StreamWriter writer = File.CreateText(fileName);
                int moveCounter = 1;
                for (int i = 0; i < this.GameRecord.Count; i++)
                {
                    int isWhiteMove = i + 1;                  
                    if (isWhiteMove % 2 == 1)
                    {                        
                        writer.Write(moveCounter);
                        writer.Write(".");
                        moveCounter++;                         
                    }
                    writer.Write(this.GameRecord[i]);
                    writer.Write(" ");
                }
                writer.Close();
            }           
        }
      
        public List<string> LoadSaveGame(string fileName)
        {
            if (fileName != null)
            {
                StreamReader reader = File.OpenText(fileName);
                string fileContent = reader.ReadToEnd();
                string[] movesSplit = fileContent.Split('.');
                List<string> movesForChessboard = new List<string>();
                //nochmal bei blank splitten, dann string die länge 1 haben ignorieren => nur die züge sind da. Züge an Chessboard "übergeben" und ausführen. Prüfen welche Farbe als letztes bewegt wurde => der andere ist dran.
                foreach (string move in movesSplit)
                {                    
                    string[] movesSplitAgain = move.Split(' ');              
                    foreach (string finalMove in movesSplitAgain)
                    {
                        if (finalMove.Length > 1)
                        {
                            movesForChessboard.Add(finalMove);
                        }
                    }
                }
                return movesForChessboard;          
            }
            return null;
        }
    }
}
