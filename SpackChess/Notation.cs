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
        protected string m_saveGameFileName;

        public List<string> GameRecord
        {
            get { return m_gameRecord; }
            set
            {
                m_gameRecord = value;
            }
        }

        public void GetFileNameToSave()
        {
            Microsoft.Win32.SaveFileDialog saveGameDialog = new Microsoft.Win32.SaveFileDialog();

            saveGameDialog.DefaultExt = ".txt";
            saveGameDialog.Filter = "Textdateien (.txt)|*.txt";

            Nullable<bool> dialogResult = saveGameDialog.ShowDialog();

            if (dialogResult == true)
            {
                m_saveGameFileName = saveGameDialog.FileName;
            }                
        }

        public void WriteFile()
        {
            if (m_saveGameFileName != null)
            {
                if (File.Exists(m_saveGameFileName))
                {
                    File.Delete(m_saveGameFileName);
                }

                StreamWriter writer = File.CreateText(m_saveGameFileName);
                int moveCounter = 1;
                for (int i = 0; i < m_gameRecord.Count; i++)
                {
                    int isWhiteMove = i + 1;                  
                    if (isWhiteMove % 2 == 1)
                    {                        
                        writer.Write(moveCounter);
                        writer.Write(".");
                        moveCounter++;                         
                    }
                    writer.Write(m_gameRecord[i]);
                    writer.Write(" ");
                }
                writer.Close();
            }           
        }
    }
}
