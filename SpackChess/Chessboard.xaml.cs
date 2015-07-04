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
        private Alignments m_whoIsChecked;
      
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
              //todo: Meldung wenn Figur ausgewählt wird, die man nicht ziehen kann. Aber keine Nervmeldung
        private void Square_MouseUp(object sender, MouseButtonEventArgs e)
        {            
            Square selectedSquare = sender as Square;  
            
            if (m_previousSelectedSquare == selectedSquare)        // gleiches Feld wieder gewählt => mögliche Züge nicht mehr markieren
            {   //todo: if (m_previousSelectedSquare == selectedSquare) ist übrigens auch nicht schön
                //DrunkenSqrl: Was du machen solltest ist IEquatable implementieren und dann this.m_previousSelectedSquare.Equals(selectedSquare)
                // weil ich nur auf die Referenz prüfe und nicht auf den Wert
                // Darum eignetlich immer .Equals() oder object.ReferenceEquals() nutzen
                // DrunkenSqrl: Bei == auf Objekten ist das Problem dass du nur auf die Referenz prüfst, was in dem Fall keine Rolle spielt weil es wirklich das gleiche Objekt ist
                //DrunkenSqrl: Im Endeffekt ist es nicht explizit genug weil wenn sich jemand anderes den Code mal anschaut weiß der nicht ob du wirklich auf die Referenz oder auf den Wert prüfen wolltest
                //DrunkenSqrl: Darum eignetlich immer .Equals() oder object.ReferenceEquals() nutzen
                //DrunkenSqrl: Ersteres musst du halt überschreiben und selbst implementieren und am besten machst du das wenn du IEquatable implementierst, weil dann ist von außen auch klar dass du sie unterstützt

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
                    m_previousSelectedSquare.OccupyingPiece = this.PromotedPiece(selectedSquare);  
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

                //todo: Abfrage für Schachmatt
                Square enemyKingLocation = this.GetKingLocation(m_whosTurnIsItNot);
                if (this.SquareAttacked(enemyKingLocation, this.WhosTurnIsIt))// && (enemyKingLocation.OccupyingPiece.GetValidMoves(enemyKingLocation).Count == 0))
                {
                    enemyKingLocation.Highlight(true);
                    this.m_whoIsChecked = m_whosTurnIsItNot;
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
                           //todo: Zusätzliche Abfrage, falls ein Feld geschlagen werden soll. Beim Prüfen der Bedrohung für dieses Feld, wird es nicht mit in Betracht gezogen, 
                            //      da ja eine eigene Figur drauf steht. D.h. wenn der König eine Figur auf dem Feld schlagen will, dieses Feld aber z.B. von der Dame bedroht wird,
                            //      fällt das im Programm nicht auf => Abfrage ob Feld von Gegner besetzt => Wenn ja, Feld "temporär" mit eigenem König besetzen => Bedrohung dieses
                            //      Feldes prüfen => Feld als Zug zulassen oder auch nicht => Figuren wieder den ursprünglichen Feldern zuweisen...
                            //todo: Es muss geprüft werden, ob das Wegziehen einer Figur ein Schach verursacht => Figur darf nicht weggezogen werden, außer sie schlägt die schachgebende figur bzw zieht dorthin, wo immer noch kein schach ist. Vorgehen wie im Beispiel drüber, z
                            //todo: Rochade einbauen. Abfrage ob Rochade druchgeführt wurde (König zieht mehr als ein Feld in x-Richtung) beim tatsächlichen Zug.
                               
                            possibleSquare.Highlight();
                            m_previousPossibleSquares.Add(possibleSquare);
                        }

                        m_previousSelectedSquare = selectedSquare;
                    }                   
                }
            }           
        }

        private void WriteLastMove(Square startSquare, Square endSquare, PieceBase movedPiece, bool enPassant = false)
        {
            if (enPassant)
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + "x" + endSquare.ToString() + " e.p.";
            }
            else
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + "-" + endSquare.ToString();
            }
            //todo: Promotion notieren!
        }

        private PieceBase PromotedPiece(Square selectedSquare)
        {
            Promotion choosePromotion = new Promotion();
            choosePromotion.ShowDialog();
            PieceBase chosenPiece;
            switch (choosePromotion.PromotedTo)
            {
                case ChessPieces.Queen:
                    chosenPiece = new Queen(this, WhosTurnIsIt) { OccupiedSquare = selectedSquare };
                    return chosenPiece;
                case ChessPieces.Rook:
                    chosenPiece = new Rook(this, WhosTurnIsIt) { OccupiedSquare = selectedSquare };
                    return chosenPiece;
                case ChessPieces.Knight:
                    chosenPiece = new Knight(this, WhosTurnIsIt) { OccupiedSquare = selectedSquare };
                    return chosenPiece;
                case ChessPieces.Bishop:
                    chosenPiece = new Bishop(this, WhosTurnIsIt) { OccupiedSquare = selectedSquare };
                    return chosenPiece;
                default:
                    return null;
            }  
        }

        private Square GetKingLocation(Alignments color)
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
            var allEnemyPiecesOnChessboard = new List<PieceBase>();            
            foreach (Square possibleSquare in this.m_allSqaures)
            {
                if (possibleSquare.OccupyingPiece != null && possibleSquare.OccupyingPiece.Alignment == attackingAlignment)
                {
                    allEnemyPiecesOnChessboard.Add(possibleSquare.OccupyingPiece);
                }
            }
            
            foreach (PieceBase enemyPiece in allEnemyPiecesOnChessboard)
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
            //todo: Schachmatt prüfen

            return false;
        }
    }
}
