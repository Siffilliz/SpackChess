using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Bishop : Piece
    {
        public Bishop(IChessboard chessboard, Alignments color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignments.Black)
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/BB.png"));
            }
            else
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/BW.png"));
            }
            
            this.GrPiece.Children.Add(this.m_graphic);
        }

        /// <summary>
        /// Läufer darf diagonal ziehen, so weit wie keine andere Figur im Weg steht.
        /// Abfrage 
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public override List<Square> GetValidMoves(Square currentLocation)
        {
            var validSquares = new List<Square>();

            int i = 1;
            bool canMoveUpLeft = true;
            bool canMoveUpRight = true;
            bool canMoveDownLeft = true;
            bool canMoveDownRight = true;

            while (i <= 8)
            {
                if (canMoveUpRight)
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
                            canMoveUpRight = false;
                        }
                    }
                }
                if (canMoveUpLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - i, currentLocation.YCoordinate + i);
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
                if (canMoveDownRight)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + i, currentLocation.YCoordinate - i);
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
                            canMoveDownRight = false;
                        }
                    }
                }
                if (canMoveDownLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - i, currentLocation.YCoordinate - i);
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
                            canMoveDownLeft = false;
                        }
                    }
                }
                i++;
            }

            return validSquares;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
