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
            Square potentialSquare;
            if (currentLocation.OccupyingPiece.Alignment == Alignments.White)
            {
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate + 2);               
                if (potentialSquare != null && potentialSquare.OccupyingPiece == null && m_hasMoved == false)
                {
                    if (this.m_chessboard.GetSquare(potentialSquare.XCoordinate, potentialSquare.YCoordinate - 1).OccupyingPiece == null)
                    {
                        validSquares.Add(potentialSquare);
                    }                   
                }
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate + 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece == null)
                {
                    validSquares.Add(potentialSquare);
                }
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate + 1);               
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment != this.Alignment)
                {
                    validSquares.Add(potentialSquare);
                }
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate + 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment != this.Alignment)
                {
                    validSquares.Add(potentialSquare);
                }
            }
            else
            {
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate - 2);
                if (potentialSquare != null && potentialSquare.OccupyingPiece == null && m_hasMoved == false)
                {
                    if (this.m_chessboard.GetSquare(potentialSquare.XCoordinate, potentialSquare.YCoordinate + 1).OccupyingPiece == null)
                    {
                        validSquares.Add(potentialSquare);
                    }
                }
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate, currentLocation.YCoordinate - 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece == null)
                {
                    validSquares.Add(potentialSquare);
                }
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate - 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment != this.Alignment)
                {
                    validSquares.Add(potentialSquare);
                }
                potentialSquare = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate - 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment != this.Alignment)
                {
                    validSquares.Add(potentialSquare);
                }
            }
            potentialSquare = enPassant(currentLocation);
            if (potentialSquare != null)
            {
                validSquares.Add(potentialSquare);
            }
            return validSquares;
        }
        
        public override string ToString()
        {
            return "";
        }
       
        private Square enPassant(Square currentLocation)
        {            
            Square enemyLocation;
            String neededPawnPreviousMove;

            if (currentLocation.OccupyingPiece.Alignment == Alignments.White && currentLocation.YCoordinate == 5)
            {
                enemyLocation = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate);
                if (enemyLocation != null)
                {
                    neededPawnPreviousMove = this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate + 2).ToString() + "-" + enemyLocation.ToString();
                    if (this.m_chessboard.LastMove == neededPawnPreviousMove)
                    {
                        return this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate + 1);
                    }
                }

                enemyLocation = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate);
                if (enemyLocation != null)
                {
                    neededPawnPreviousMove = this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate + 2).ToString() + "-" + enemyLocation.ToString();
                    if (this.m_chessboard.LastMove == neededPawnPreviousMove)
                    {
                        return this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate + 1);
                    }
                }

                return null;
            }
            else if (currentLocation.OccupyingPiece.Alignment == Alignments.Black && currentLocation.YCoordinate == 4)
            {
                enemyLocation = this.m_chessboard.GetSquare(currentLocation.XCoordinate - 1, currentLocation.YCoordinate);
                if (enemyLocation != null)
                {
                    neededPawnPreviousMove = this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate - 2).ToString() + "-" + enemyLocation.ToString();
                    if (this.m_chessboard.LastMove == neededPawnPreviousMove)
                    {
                        return this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate - 1);
                    }
                }

                enemyLocation = this.m_chessboard.GetSquare(currentLocation.XCoordinate + 1, currentLocation.YCoordinate);
                if (enemyLocation != null)
                {
                    neededPawnPreviousMove = this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate - 2).ToString() + "-" + enemyLocation.ToString();
                    if (this.m_chessboard.LastMove == neededPawnPreviousMove)
                    {
                        return this.m_chessboard.GetSquare(enemyLocation.XCoordinate, enemyLocation.YCoordinate - 1);
                    }
                }

                return null;
            }
           
            return null;            
        }       
    }
}
