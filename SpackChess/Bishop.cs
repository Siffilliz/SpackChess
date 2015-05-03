using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Bishop : Piece
    { /// <summary>
        /// Läufer darf diagonal ziehen, so weit wie keine andere Figur im Weg steht.
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public override List<Square> GetValidMoves(Square currentLocation)
        {
            var validSquares = new List<Square>();

            int i = 0;
            bool canMoveUpLeft = true;
            bool canMoveUpRight = true;
            bool canMoveDownLeft = true;
            bool canMoveDownRight = true;

            while (i <= 8)
            {
                if (canMoveUpLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + i, currentLocation.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece == null)
                        {
                            validSquares.Add(potentialSquare);
                        }
                        else
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment)
                            {
                                validSquares.Add(potentialSquare);
                            }
                            canMoveUpLeft = false;
                        }
                    }
                }

            }

            return validSquares;
        }
    }
}
