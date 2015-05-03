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
            int x = currentLocation.XCoordinate;
            int y = currentLocation.YCoordinate;
            Square helpSquare;
            // Richtungen können mittels 4 for Schleifen durchlaufen werden, Startwert ist die aktuelle Position, Zähler einmal +1, einmal -1
            // Ist ein Feld belegt, kann die Zählung abgebrochen werden 
            // k ist ein zweite Zählervariable für die y-Koordinate
            int k;

            // in x Richtung nach rechts, y nach oben => obenrechts
            k = currentLocation.YCoordinate +1;
            for (int i = x; i == 8; i++)            // prüfen was die for Schleife macht, wenn startkoordinate = 8 ist.
            {
                helpSquare = this.m_chessboard.GetSquare(i, k);

                if (helpSquare.OccupyingPiece == null)
                {
                    validSquares.Add(helpSquare);
                }
                else
                {
                    if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                    {
                        validSquares.Add(helpSquare);
                    }
                    break;
                }
                k++;
            }
            // in x-Richtung nach links, y nach oben => obenlinks
            k = currentLocation.YCoordinate + 1;
            for (int i = x; i == 1; i--)          // prüfen was die for Schleife macht, wenn startkoordinate = 1 ist.
            {
                helpSquare = this.m_chessboard.GetSquare(i, k);

                if (helpSquare.OccupyingPiece == null)
                {
                    validSquares.Add(helpSquare);
                }
                else
                {
                    if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                    {
                        validSquares.Add(helpSquare);
                    }
                    break;
                }
                k++;
            }
            // in x-Richtung nach rechts, y nach unten => rechtsunten
            k = currentLocation.YCoordinate-1;
            for (int i = x; i == 8; i++)            // prüfen was die for Schleife macht, wenn startkoordinate = 8 ist.
            {
                helpSquare = this.m_chessboard.GetSquare(i, k);

                if (helpSquare.OccupyingPiece == null)
                {
                    validSquares.Add(helpSquare);
                }
                else
                {
                    if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                    {
                        validSquares.Add(helpSquare);
                    }
                    break;
                }
                k--;
            }
            // in x-Richtung nach links, y nach unten => linksunten
            k = currentLocation.YCoordinate - 1;
            for (int i = x; i == 1; i--)          // prüfen was die for Schleife macht, wenn startkoordinate = 1 ist.
            {
                helpSquare = this.m_chessboard.GetSquare(i, k);

                if (helpSquare.OccupyingPiece == null)
                {
                    validSquares.Add(helpSquare);
                }
                else
                {
                    if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                    {
                        validSquares.Add(helpSquare);
                    }
                    break;
                }
                k++;
            }

            return validSquares;
        }
    }
}
