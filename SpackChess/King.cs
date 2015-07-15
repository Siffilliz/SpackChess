using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class King : PieceBase
    {
        public King(IChessboard chessboard, Alignment color)
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignment.Black)
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
                    if ((potentialSquare.OccupyingPiece == null || potentialSquare.OccupyingPiece.Alignment != this.Alignment))
                    {
                        validSquares.Add(potentialSquare);
                    }
                }
            }
            
            base.SimulateMove(validSquares);

            if (!this.HasMoved)
            {
                if (this.Alignment == Alignment.White)
                {
                    if (this.m_chessboard.GetSquare(1, 1).OccupyingPiece != null && this.m_chessboard.GetSquare(2,1).OccupyingPiece == null)
                    {
                        if (!this.m_chessboard.GetSquare(1, 1).OccupyingPiece.HasMoved)
                        {
                            if (validSquares.Contains(this.m_chessboard.GetSquare(4, 1)))
                            {
                                var castlingSquare = new List<Square>();
                                castlingSquare.Add(this.m_chessboard.GetSquare(3, 1));
                                base.SimulateMove(castlingSquare);

                                if (castlingSquare.Count == 1)
                                {
                                    validSquares.Add(this.m_chessboard.GetSquare(3, 1));
                                }
                            }
                        }
                    }

                    if (this.m_chessboard.GetSquare(8, 1).OccupyingPiece != null && this.m_chessboard.GetSquare(7, 1).OccupyingPiece == null)
                    {
                        if (!this.m_chessboard.GetSquare(8, 1).OccupyingPiece.HasMoved)
                        {
                            if (validSquares.Contains(this.m_chessboard.GetSquare(6, 1)))
                            {
                                var castlingSquare = new List<Square>();
                                castlingSquare.Add(this.m_chessboard.GetSquare(7, 1));
                                base.SimulateMove(castlingSquare);

                                if (castlingSquare.Count == 1)
                                {
                                    validSquares.Add(this.m_chessboard.GetSquare(7, 1));
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.m_chessboard.GetSquare(1, 8).OccupyingPiece != null && this.m_chessboard.GetSquare(2, 8).OccupyingPiece == null)
                    {
                        if (!this.m_chessboard.GetSquare(1, 8).OccupyingPiece.HasMoved)
                        {
                            if (validSquares.Contains(this.m_chessboard.GetSquare(4, 8)))
                            {
                                var castlingSquare = new List<Square>();
                                castlingSquare.Add(this.m_chessboard.GetSquare(3, 8));
                                base.SimulateMove(castlingSquare);

                                if (castlingSquare.Count == 1)
                                {
                                    validSquares.Add(this.m_chessboard.GetSquare(3, 8));
                                }
                            }
                        }
                    }

                    if (this.m_chessboard.GetSquare(8, 8).OccupyingPiece != null && this.m_chessboard.GetSquare(7, 8).OccupyingPiece == null)
                    {
                        if (!this.m_chessboard.GetSquare(8, 8).OccupyingPiece.HasMoved)
                        {
                            if (validSquares.Contains(this.m_chessboard.GetSquare(6, 8)))
                            {
                                var castlingSquare = new List<Square>();
                                castlingSquare.Add(this.m_chessboard.GetSquare(7, 8));
                                base.SimulateMove(castlingSquare);

                                if (castlingSquare.Count == 1)
                                {
                                    validSquares.Add(this.m_chessboard.GetSquare(7, 8));
                                }
                            }
                        }
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
