using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpackChess
{
    class Pawn : Piece
    {
        /// <summary>
        /// Bauer darf ein Feld nach vorne wenn er sich nicht bewegt hat. Als ersten Zug zwei.
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        /// 
        
        public Pawn(IChessboard chessboard, Alignments color)  
            : base(chessboard, color)
        {
            m_graphic = new System.Windows.Controls.Image();

            if (this.Alignment == Alignments.Black)
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/PB.png"));
            }
            else
            {
                m_graphic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,/Pictures/PW.png"));
            }
            
            this.GrPiece.Children.Add(this.m_graphic);
        }

        public override List<Square> GetValidMoves(Square currentLocation)
        {
            var validSquares = new List<Square>();
            Square helpSquare;
            if (currentLocation.OccupyingPiece.Alignment == Alignments.White)
            {
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate + 2);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece == null)
                    {
                        if (m_hasMoved == false)
                        {
                            validSquares.Add(helpSquare);
                        }
                    }
                }
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate + 1);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece == null)
                    {
                        validSquares.Add(helpSquare);
                    }
                }
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate + 1);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece != null)
                    {
                        if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                        {
                            validSquares.Add(helpSquare);
                        }
                    }
                }
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate + 1);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece != null)
                    {
                        if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                        {
                            validSquares.Add(helpSquare);
                        }
                    }
                }
            }
            else
            {
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate - 2);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece == null)
                    {
                        if (m_hasMoved == false)
                        {
                            validSquares.Add(helpSquare);
                        }
                    }
                }
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate - 1);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece == null)
                    {
                        validSquares.Add(helpSquare);
                    }
                }
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate - 1);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece != null)
                    {
                        if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                        {
                            validSquares.Add(helpSquare);
                        }
                    }
                }
                helpSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate - 1);
                if (helpSquare != null)
                {
                    if (helpSquare.OccupyingPiece != null)
                    {
                        if (helpSquare.OccupyingPiece.Alignment != this.Alignment)
                        {
                            validSquares.Add(helpSquare);
                        }
                    }
                }
            }       
            return validSquares;
        }        
    }
}
