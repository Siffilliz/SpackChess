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
                Square potentialSquare = this.m_chessboard.GetSquare(possibleX[i], possibleY[i]);

                if (potentialSquare != null)
                {
                    if ((potentialSquare.OccupyingPiece == null || potentialSquare.OccupyingPiece.Alignment != this.Alignment) && !this.SquareAttacked(potentialSquare))
                    {
                        validSquares.Add(potentialSquare);
                    }
                }
            }
            return validSquares;
        }

        public override string ToString()
        {
            return "K";
        }

        public Boolean SquareAttacked(Square squareToExamine)
        {
            //Gerade Linie prüfen
            int i = 1;
            bool canMoveLeft = true;
            bool canMoveRight = true;
            bool canMoveUp = true;
            bool canMoveDown = true;

            while (i <= 8)
            {
                if (canMoveRight)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate + i, squareToExamine.YCoordinate);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;     
                            }
                            else
                            {
                                canMoveRight = false;  
                            }
                        }                                                
                    }
                    else 
                    {
                        canMoveRight = false;  
                    }
                }
                if (canMoveLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate - i, squareToExamine.YCoordinate);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveLeft = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveLeft = false;
                    }
                }
                if (canMoveUp)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate, squareToExamine.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveUp = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveUp = false;
                    }
                }
                if (canMoveDown)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate, squareToExamine.YCoordinate - i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveDown = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveDown = false;
                    }
                }
                i++;
            }        

            //Diagonale prüfen
            i = 1;
            bool canMoveUpLeft = true;
            bool canMoveUpRight = true;
            bool canMoveDownLeft = true;
            bool canMoveDownRight = true;

            while (i <= 8)
            {
                if (canMoveUpRight)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate + i, squareToExamine.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveUpRight = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveUpRight = false;
                    }
                }
                if (canMoveUpLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate - i, squareToExamine.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveUpLeft = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveUpLeft = false;
                    }
                }
                if (canMoveDownRight)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate + i, squareToExamine.YCoordinate - i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveDownRight = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveDownRight = false;
                    }
                }
                if (canMoveDownLeft)
                {
                    var potentialSquare = this.m_chessboard.GetSquare(squareToExamine.XCoordinate - i, squareToExamine.YCoordinate - i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment != this.Alignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
                            {
                                return true;
                            }
                            else
                            {
                                canMoveDownLeft = false;
                            }
                        }
                    }
                    else
                    {
                        canMoveDownLeft = false;
                    }
                }
                i++;
            }        

            return false;
        }
    }
}
