using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Rook : Piece
    {

        public Rook(IChessboard chessboard, Alignments color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignments.Black)
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/RB.png"));
            }
            else
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/RW.png"));
            }
            
            this.GrPiece.Children.Add(this.m_graphic);
        }

        /// <summary>
        /// Turm darf auf der x-Gerade und y-Gerade ziehen, so weit wie keine andere Figur im Weg steht.
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public override List<Square> GetValidMoves(Square currentLocation)
        {
            var validSquares = new List<Square>();
            int x = currentLocation.XCoordinate;
            int y = currentLocation.YCoordinate;
            Square potentialSquare;
            // Richtungen können mittels 4 for Schleifen durchlaufen werden, Startwert ist die aktuelle Position, Zähler einmal +1, einmal -1
            // Ist ein Feld belegt, kann die Zählung abgebrochen werden 
            // in x Richtung nach rechts
            for (int i = x + 1; i <= 8; i++)            // prüfen was die for Schleife macht, wenn startkoordinate = 8 ist.
            {
                
                potentialSquare = this.m_chessboard.GetSquare(i, y);

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
                    break;
                }
            }
            // in x-Richtung nach links
            for (int i = x - 1; i >= 1; i--)          // prüfen was die for Schleife macht, wenn startkoordinate = 1 ist.
            {
                potentialSquare = this.m_chessboard.GetSquare(i, y);
               
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
                    break;
                }
            }
            // in y-Richtung nach oben
            for (int i = y + 1; i <= 8; i++)            // prüfen was die for Schleife macht, wenn startkoordinate = 8 ist.
            {
                potentialSquare = this.m_chessboard.GetSquare(x, i);

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
                    break;
                }
            }
            // in y-Richtung nach unten
            for (int i = y - 1; i >= 1; i--)          // prüfen was die for Schleife macht, wenn startkoordinate = 1 ist.
            {
                potentialSquare = this.m_chessboard.GetSquare(x, i);

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
                    break;
                }
            }

            return validSquares;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
