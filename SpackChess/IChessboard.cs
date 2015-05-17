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

        String LastMove
        {
            get;
            set;
        }

        //Boolean SquareAttacked(Square squareToExamine, Alignments attackingAlignment)
        //{

        //}
    }
}
