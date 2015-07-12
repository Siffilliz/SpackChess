using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    public interface IChessboard
    {
        List<Square> AllSquares
        {
            get;
        }

        Square GetSquare(int x, int y);

        string LastMove
        {
            get;
            set;
        }

        Alignment WhosTurnIsIt
        {
            get;
            set;
        }

        Alignment WhosTurnIsItNot
        {
            get;
        }
        
        bool IsKingThreatened(Alignment color);

        Square GetKingLocation(Alignment color);

        void ResetGame();

        void MovePiece(Square oldSquare, Square newSquare);        
        
    }
}
