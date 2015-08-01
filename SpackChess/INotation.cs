using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    public interface INotation
    {
        List<string> GameRecord
        {
            get;
            set;
        }

        string GetFileName(bool save);

        void WriteFile(string filename);

        List<string> LoadSaveGame(string filename);       
    }
}
