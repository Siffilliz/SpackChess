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
        private Alignments m_whosTurnIsIt = Alignments.White;
        private string m_lastMove;

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
        /// Chessboard auf Standardanfangsaufstellung bringen
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
            
            if (m_previousSelectedSquare == selectedSquare)        // gleiches Feld wieder gewählt => mögliche Züge nicht mehr markieren
            {
                foreach (Square possibleSquare in m_previousPossibleSquares)
                {
                    possibleSquare.UnHighlight();
                }
                m_previousSelectedSquare = null;
                m_previousPossibleSquares.Clear();
            }
            else if (m_previousPossibleSquares.Contains(selectedSquare))        // ein Feld der möglichen Züge wurde gewählt => Zug durchführen
            {
                foreach (Square possibleSquare in m_previousPossibleSquares)
                {
                    possibleSquare.UnHighlight();
                }


                m_previousSelectedSquare.OccupyingPiece.OccupiedSquare = selectedSquare;
                m_previousSelectedSquare.GrSquare.Children.Clear();
                selectedSquare.OccupyingPiece = m_previousSelectedSquare.OccupyingPiece;
                //Abfrage wegen en passant. Später wahrscheinlich auch noch Rochade
                if (m_previousSelectedSquare.OccupyingPiece is Pawn && selectedSquare.OccupyingPiece != null && selectedSquare.XCoordinate != m_previousSelectedSquare.XCoordinate)
                {                                      
                    this.WriteLastMove(m_previousSelectedSquare, selectedSquare, m_previousSelectedSquare.OccupyingPiece, true);
                    if (m_whosTurnIsIt == Alignments.White)
                    {
                        this.GetSquare(selectedSquare.XCoordinate, selectedSquare.YCoordinate - 1).OccupyingPiece = null;
                    }
                    else
                    {
                        this.GetSquare(selectedSquare.XCoordinate, selectedSquare.YCoordinate + 1).OccupyingPiece = null;
                    }                    
                }
                else
                {    
                    this.WriteLastMove(m_previousSelectedSquare, selectedSquare, m_previousSelectedSquare.OccupyingPiece);                   
                }
                m_previousSelectedSquare.OccupyingPiece.m_hasMoved = true;  
                m_previousSelectedSquare.OccupyingPiece = null;
                m_previousPossibleSquares.Clear();
                m_previousSelectedSquare = null;
                
                if (m_whosTurnIsIt == Alignments.White)
                {
                    m_whosTurnIsIt = Alignments.Black;
                }
                else
                {
                    m_whosTurnIsIt = Alignments.White;
                }
            }
            else                                                                        
            {   
                if (selectedSquare.OccupyingPiece != null)
                {
                    if (selectedSquare.OccupyingPiece.Alignment == m_whosTurnIsIt)
                    {
                        // die Markierung der vorher ausgewählten Spielzüge wird aufgehoben 
                        foreach (Square possibleSquare in m_previousPossibleSquares)
                        {
                            possibleSquare.UnHighlight();
                        }
                        m_previousPossibleSquares.Clear();

                        // mögliche Züge ermitteln
                        List<Square> validMoves;                                     
                        validMoves = selectedSquare.OccupyingPiece.GetValidMoves(selectedSquare);
                        foreach (Square possibleSquare in validMoves)
                        {
                            possibleSquare.Highlight();
                            m_previousPossibleSquares.Add(possibleSquare);
                        }

                        m_previousSelectedSquare = selectedSquare;
                    }                   
                }
            }           
        }

        public string LastMove
        {
            get { return m_lastMove; }
            set { m_lastMove = value; }
        }

        public void WriteLastMove(Square startSquare, Square endSquare, Piece movedPiece)
        {
            m_lastMove = movedPiece.ToString() + startSquare.ToString() + "-" + endSquare.ToString();
        }
        public void WriteLastMove(Square startSquare, Square endSquare, Piece movedPiece, bool enPassant)
        {
            m_lastMove = movedPiece.ToString() + startSquare.ToString() + "x" + endSquare.ToString() + " e.p.";
        }
    }
}
