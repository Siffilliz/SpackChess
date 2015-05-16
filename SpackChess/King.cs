using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class King : Piece
    {
        public King(IChessboard chessboard, Alignments color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignments.Black)
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/KB.png"));
            }
            else
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/KW.png"));
            }
            
            this.GrPiece.Children.Add(this.m_graphic);
        }

        /// <summary>
        /// König darf ein Feld in jede Richtung gehen. 
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
            // Das erste Feld ist rechts neben dem aktuellen Feld. Reihenfolge dann im Uhrzeigersinn.
            List<int> possibleX = new List<int>(8) {x+1, x+1, x, x-1, x-1, x-1, x, x+1};
            List<int> possibleY = new List<int>(8) {y, y-1, y-1, y-1, y, y+1, y+1, y+1};

            for (int i = 0; i < 8; i++)
            {
                Square helpSquare = this.m_chessboard.GetSquare(possibleX[i], possibleY[i]);

                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece == null || helpSquare.OccupyingPiece.Alignment != this.Alignment)
                    {
                        validSquares.Add(helpSquare);
                    }
                }
            }
            return validSquares;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
