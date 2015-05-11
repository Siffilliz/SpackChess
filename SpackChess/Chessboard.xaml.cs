using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpackChess
{
    public partial class Chessboard : IChessboard
    {
        private List<Square> m_allSqaures = new List<Square>();
        private Square m_previousSelectedSquare;
        private List<Square> m_previousPossibleSquares = new List<Square>();

        public List<Square> AllSquares
        {
            get { return m_allSqaures; }
        }
        public Chessboard()
        {
            InitializeComponent();

            for (int x = 1; x <= 8; ++x)
            {
                for (int y = 1; y <= 8; ++y)
                {
                    var newSquare = new Square(x, y);
                    newSquare.SetValue(Grid.RowProperty, 8 - y);
                    newSquare.SetValue(Grid.ColumnProperty, x - 1);
                    newSquare.MouseUp += this.Square_MouseUp;
                    this.m_allSqaures.Add(newSquare);
                    this.GrChessboard.Children.Add(newSquare);
                }
            }

            this.ResetGame();
        }
        public Square GetSquare(int x, int y)
        {
            return this.m_allSqaures.FirstOrDefault(s => s.XCoordinate == x && s.YCoordinate == y);
        }

        /// <summary>
        /// Chessbpard auf Standardanfangsaufstellung bringen
        /// </summary>
        public void ResetGame()
        {
            foreach (Square squareToOccupy in this.m_allSqaures)
            {
                switch (squareToOccupy.YCoordinate)
                {
                    case 1:
                    case 8:
                        {
                            switch (squareToOccupy.XCoordinate)
                            {
                                case 1:
                                case 8:
                                    {
                                        Rook newRook;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newRook = new Rook(this, Alignments.White) { OccupiedSquare = squareToOccupy };                                            
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newRook = new Rook(this, Alignments.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newRook;
                                        break;
                                    }
                                case 2:
                                case 7:
                                    {
                                        Knight newKnight;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newKnight = new Knight(this, Alignments.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newKnight = new Knight(this, Alignments.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newKnight;                              
                                        break;
                                    }
                                case 3:
                                case 6:
                                    {
                                        Bishop newBishop;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newBishop = new Bishop(this, Alignments.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newBishop = new Bishop(this, Alignments.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newBishop;
                                        break;
                                    }
                                case 5:
                                    {
                                        King newKing;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newKing = new King(this, Alignments.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newKing = new King(this, Alignments.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newKing;
                                        break;
                                    }
                                case 4:
                                    {
                                        Queen newQueen;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newQueen = new Queen(this, Alignments.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newQueen = new Queen(this, Alignments.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newQueen;
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            var newPawn = new Pawn(this, Alignments.White) { OccupiedSquare = squareToOccupy };
                            squareToOccupy.OccupyingPiece = newPawn;
                            break;
                        }
                    case 7:
                        {
                            var newPawn = new Pawn(this, Alignments.Black) { OccupiedSquare = squareToOccupy };
                            squareToOccupy.OccupyingPiece = newPawn;
                            break;
                        }
                }
            }
        }
        private void Square_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Square selectedSquare = sender as Square;

            if (m_previousSelectedSquare == null)
            {
                if (selectedSquare != null && selectedSquare.OccupyingPiece != null)
                {
                    m_previousSelectedSquare = selectedSquare;
                    List<Square> validMoves;
                    validMoves = selectedSquare.OccupyingPiece.GetValidMoves(selectedSquare);

                    foreach (Square possibleSquare in validMoves)
                    {
                        possibleSquare.Highlight();
                        m_previousPossibleSquares.Add(possibleSquare);
                    }
                }
            }
            else if (m_previousSelectedSquare == selectedSquare)
            {                         
                foreach (Square possibleSquare in m_previousPossibleSquares)
                {
                    possibleSquare.UnHighlight();                    
                }
                m_previousSelectedSquare = null;
                m_previousPossibleSquares.Clear();
            }
            else if (m_previousPossibleSquares.Contains(selectedSquare))
            {
                foreach (Square possibleSquare in m_previousPossibleSquares)
                {
                    possibleSquare.UnHighlight();    
                }

                m_previousSelectedSquare.OccupyingPiece.OccupiedSquare = selectedSquare;
                m_previousSelectedSquare.OccupyingPiece.m_hasMoved = true;
                m_previousSelectedSquare.GrSquare.Children.Clear();
                selectedSquare.OccupyingPiece = m_previousSelectedSquare.OccupyingPiece;
                m_previousSelectedSquare.OccupyingPiece = null;
              
                m_previousPossibleSquares.Clear();
                m_previousSelectedSquare = null;
            }
            else
            {
                m_previousSelectedSquare = null;

                foreach (Square possibleSquare in m_previousPossibleSquares)
                {
                    possibleSquare.UnHighlight();
                }
                m_previousPossibleSquares.Clear();

                if (selectedSquare != null && selectedSquare.OccupyingPiece != null)
                {
                    m_previousSelectedSquare = selectedSquare;
                    List<Square> validMoves;
                    validMoves = selectedSquare.OccupyingPiece.GetValidMoves(selectedSquare);

                    foreach (Square possibleSquare in validMoves)
                    {
                        possibleSquare.Highlight();
                        m_previousPossibleSquares.Add(possibleSquare);
                    }
                }
            }
        }
    }
}
