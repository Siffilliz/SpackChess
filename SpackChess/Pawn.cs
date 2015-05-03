using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Pawn : Piece
    {
        /// <summary>
        /// Bauer darf ein Feld nach vorne wenn er sich nicht bewegt hat. Als ersten Zug zwei.
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        /// 

        private bool m_hasMoved = false; 

        public override List<Square> GetValidMoves(Square currentLocation)
        {
            var validSquares = new List<Square>();
            Square helpSquare;

            helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate +2);
            if (m_hasMoved == false)
            {
                validSquares.Add(helpSquare);
            }
            helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate + 1);
            validSquares.Add(helpSquare);

            helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate + 1);
            if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
            {
                validSquares.Add(helpSquare);
            }

            helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate + 1);
            if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
            {
                validSquares.Add(helpSquare);
            }

            return validSquares;
        }        
    }
}
