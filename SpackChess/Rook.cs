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
                    
            int i = 1;
            bool canMoveLeft = true;
            bool canMoveRight = true;
            bool canMoveUp = true;
            bool canMoveDown = true;

            while (i <= 8)
            {
                if (canMoveRight)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + i, currentLocation.YCoordinate);
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
                            canMoveRight = false;
                        }
                    }
                }
                if (canMoveLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - i, currentLocation.YCoordinate);
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
                            canMoveLeft = false;
                        }
                    }
                }
                if (canMoveDown)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate - i);
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
                            canMoveDown = false;
                        }
                    }
                }
                if (canMoveUp)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate + i);
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
                            canMoveUp = false;
                        }
                    }
                }
                i++;
            }        

            return validSquares;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
