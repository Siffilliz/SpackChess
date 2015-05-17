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
        private Alignments m_whosTurnIsItNot = Alignments.Black;
      
        public List<Square> AllSquares
        {
            get { return m_allSqaures; }
        }       
        public Square GetSquare(int x, int y)
        {
            return this.m_allSqaures.FirstOrDefault(s => s.XCoordinate == x && s.YCoordinate == y);
        }
        public string LastMove
        {
            get;
            set;
        }
        public Alignments WhosTurnIsIt
        {
            get { return m_whosTurnIsIt; }
            set
            {
                m_whosTurnIsItNot = m_whosTurnIsIt;
                m_whosTurnIsIt = value;                
            }
        }
        public Alignments WhosTurnIsItNot
        {
            get { return m_whosTurnIsItNot; }
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
                foreach (Square possibleSquare in m_allSqaures)
                {
                    possibleSquare.UnHighlight();
                }
                
                //Abfrage wegen en passant. Später wahrscheinlich auch noch Rochade
                if (m_previousSelectedSquare.OccupyingPiece is Pawn && selectedSquare.XCoordinate != m_previousSelectedSquare.XCoordinate && selectedSquare.OccupyingPiece == null)
                {                                      
                    this.WriteLastMove(m_previousSelectedSquare, selectedSquare, m_previousSelectedSquare.OccupyingPiece, true);
                    if (WhosTurnIsIt == Alignments.White)
                    {
                        this.GetSquare(selectedSquare.XCoordinate, selectedSquare.YCoordinate - 1).OccupyingPiece = null;
                    }
                    else
                    {
                        this.GetSquare(selectedSquare.XCoordinate, selectedSquare.YCoordinate + 1).OccupyingPiece = null;
                    }                    
                }
                else if (m_previousSelectedSquare.OccupyingPiece is Pawn && (selectedSquare.YCoordinate == 1 | selectedSquare.YCoordinate == 8))
                {
                    selectedSquare.OccupyingPiece = this.PromotedPiece(selectedSquare);  
                }                
                else
                {
                    this.WriteLastMove(m_previousSelectedSquare, selectedSquare, m_previousSelectedSquare.OccupyingPiece);
                }
               
                m_previousSelectedSquare.GrSquare.Children.Clear();
                selectedSquare.OccupyingPiece = m_previousSelectedSquare.OccupyingPiece;
                selectedSquare.OccupyingPiece.OccupiedSquare = selectedSquare;
                selectedSquare.OccupyingPiece.m_hasMoved = true;  
                m_previousSelectedSquare.OccupyingPiece = null;
                m_previousPossibleSquares.Clear();
                m_previousSelectedSquare = null;
                
                var possibleCheckSquares = new List<Square>();
                possibleCheckSquares = selectedSquare.OccupyingPiece.GetValidMoves(selectedSquare);
                Square enemyKingLocation = this.getKingLocation(m_whosTurnIsItNot);
                if (possibleCheckSquares.Contains(enemyKingLocation))
                {                    
                    if (!this.SquareAttacked(selectedSquare, this.WhosTurnIsIt) && (enemyKingLocation.OccupyingPiece.GetValidMoves(enemyKingLocation).Count == 0))
                    {
                        MessageBox.Show("GAME OVER");
                    }                    
                    enemyKingLocation.Highlight(true);
                }
               
                m_previousPossibleSquares.Clear();
                this.WhosTurnIsIt = this.m_whosTurnIsItNot;
            }
            else                                                                        
            {   
                if (selectedSquare.OccupyingPiece != null)
                {
                    if (selectedSquare.OccupyingPiece.Alignment == WhosTurnIsIt)
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

        private void WriteLastMove(Square startSquare, Square endSquare, Piece movedPiece, bool enPassant = false)
        {
            if (enPassant)
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + "x" + endSquare.ToString() + " e.p.";
            }
            else
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + "-" + endSquare.ToString();
            }
            
        }

        private Piece PromotedPiece(Square selectedSquare)
        {
            Promotion choosePromotion = new Promotion();
            choosePromotion.ShowDialog();
            Piece chosenPiece;
            switch (choosePromotion.PromotedTo)
            {
                case ChessPieces.Queen:
                    if (WhosTurnIsIt == Alignments.White)
                    {
                        chosenPiece = new Queen(this, Alignments.White) { OccupiedSquare = selectedSquare };
                    }
                    else
                    {
                        chosenPiece = new Queen(this, Alignments.Black) { OccupiedSquare = selectedSquare };
                    }
                    return chosenPiece;
                case ChessPieces.Rook:
                    if (WhosTurnIsIt == Alignments.White)
                    {
                        chosenPiece = new Rook(this, Alignments.White) { OccupiedSquare = selectedSquare };
                    }
                    else
                    {
                        chosenPiece = new Rook(this, Alignments.Black) { OccupiedSquare = selectedSquare };
                    }
                    return chosenPiece;
                case ChessPieces.Knight:
                    if (WhosTurnIsIt == Alignments.White)
                    {
                        chosenPiece = new Knight(this, Alignments.White) { OccupiedSquare = selectedSquare };
                    }
                    else
                    {
                        chosenPiece = new Knight(this, Alignments.Black) { OccupiedSquare = selectedSquare };
                    }
                    return chosenPiece;
                case ChessPieces.Bishop:
                    if (WhosTurnIsIt == Alignments.White)
                    {
                        chosenPiece = new Bishop(this, Alignments.White) { OccupiedSquare = selectedSquare };
                    }
                    else
                    {
                        chosenPiece = new Bishop(this, Alignments.Black) { OccupiedSquare = selectedSquare };
                    }
                    return chosenPiece;
                default:
                    return null;
            }  
        }

        private Square getKingLocation(Alignments color)
        {            
            foreach (Square possibleSquare in this.m_allSqaures)
            {               
                if (possibleSquare.OccupyingPiece is King && possibleSquare.OccupyingPiece.Alignment == color)
                {
                    return possibleSquare;
                }               
            }
            return null;
        }

        public Boolean SquareAttacked(Square squareToExamine, Alignments attackingAlignment)
        {
            var allEnemyPiecesOnChessboard = new List<Piece>();            
            foreach (Square possibleSquare in this.m_allSqaures)
            {
                if (possibleSquare.OccupyingPiece != null && possibleSquare.OccupyingPiece.Alignment == attackingAlignment)
                {
                    allEnemyPiecesOnChessboard.Add(possibleSquare.OccupyingPiece);
                }
            }
            
            foreach (Piece enemyPiece in allEnemyPiecesOnChessboard)
            {
                var enemyCheckedSquares = new List<Square>();
                enemyCheckedSquares = enemyPiece.GetValidMoves(enemyPiece.OccupiedSquare);
                foreach (Square enemyCheckedSquare in enemyCheckedSquares)
                {
                    if (enemyCheckedSquare == squareToExamine)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Boolean verifyCheckMate(Square attackingSquare, Square attackedSquare)
        {


            return false;
        }
    }
}
