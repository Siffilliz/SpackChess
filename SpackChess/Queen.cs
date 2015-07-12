using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Queen : PieceBase
    {
        public Queen(IChessboard chessboard, Alignment color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignment.Black)
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/QB.png"));
            }
            else
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/QW.png"));
            }
            
            this.GrPiece.Children.Add(this.m_graphic);
        }

        /// <summary>
        /// Dame darf wie der Läufer diagonal ziehen, und wie der Turm auf der Linie so weit wie keine andere Figur im Weg steht.
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

            i = 1;
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

            base.SimulateMove(validSquares);

            return validSquares;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
