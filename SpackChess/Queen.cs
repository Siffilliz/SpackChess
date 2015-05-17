using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Queen : Piece
    {
        public Queen(IChessboard chessboard, Alignments color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignments.Black)
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
            Square potentialSquare;

            while (i <= 8)
            {
                if (canMoveUpRight)
                {
                    potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + i, currentLocation.YCoordinate + i);
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
                    potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - i, currentLocation.YCoordinate + i);
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
                    potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + i, currentLocation.YCoordinate - i);
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
                    potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - i, currentLocation.YCoordinate - i);
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
            
            //Zusätzlich die Abfragen von Rook
            int x = currentLocation.XCoordinate;
            int y = currentLocation.YCoordinate;
            
            // Richtungen können mittels 4 for Schleifen durchlaufen werden, Startwert ist die aktuelle Position, Zähler einmal +1, einmal -1
            // Ist ein Feld belegt, kann die Zählung abgebrochen werden 
            // in x Richtung nach rechts
            for (i = x + 1; i <= 8; i++)            // prüfen was die for Schleife macht, wenn startkoordinate = 8 ist.
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
            for (i = x - 1; i >= 1; i--)          // prüfen was die for Schleife macht, wenn startkoordinate = 1 ist.
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
            for (i = y + 1; i <= 8; i++)            // prüfen was die for Schleife macht, wenn startkoordinate = 8 ist.
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
            for (i = y - 1; i >= 1; i--)          // prüfen was die for Schleife macht, wenn startkoordinate = 1 ist.
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
            return "Q";
        }
    }
}
