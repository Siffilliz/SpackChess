using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Knight : PieceBase
    {
        public Knight(IChessboard chessboard, Alignments color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignments.Black)
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/NB.png"));
            }
            else
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/NW.png"));
            }
            
            this.GrPiece.Children.Add(this.m_graphic);
        }
        /// <summary>
        /// Die offizielle FIDE-Beschreibung lautet: Der Springer darf auf eines der Felder ziehen, die seinem Standfeld am nächsten,
        /// aber nicht auf gleicher Reihe, Linie oder Diagonale mit diesem liegen. 
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public override List<Square> GetValidMoves(Square currentLocation)
        {
            var validSquares = new List<Square>();
            int x = currentLocation.XCoordinate;
            int y = currentLocation.YCoordinate;

            // Zwei Listen für mögliche x und y Koordinaten werden erstellt. 
            // Die Items der beiden Listen gehören zusammen, also possibleX[1] und possibleY[1]. So können mit einer for schleife alle Felder abgefragt werden. 
            // Das erste Feld ist oben rechts neben dem aktuellen Feld. Reihenfolge dann im Uhrzeigersinn.
            List<int> possibleX = new List<int>(8) { x+1, x+2, x+2, x+1, x-1, x-2, x-2, x-1 };
            List<int> possibleY = new List<int>(8) { y+2, y+1, y-1, y-2, y-2, y-1, y+1, y+2 };

            for (int i = 0; i < 8; i++)
            {
                Square potentialSquare = this.m_chessboard.GetSquare(possibleX[i], possibleY[i]);

                if (potentialSquare != null)
                {
                    if (potentialSquare.OccupyingPiece == null || potentialSquare.OccupyingPiece.Alignment != this.Alignment)
                    {
                        validSquares.Add(potentialSquare);
                    }
                }
            }
            return validSquares;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
