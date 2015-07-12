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
        private Alignment m_whosTurnIsIt = Alignment.White;
        private Alignment m_whosTurnIsItNot = Alignment.Black;
        private Alignment m_whoIsChecked;
      
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
        public Alignment WhosTurnIsIt
        {
            get { return m_whosTurnIsIt; }
            set
            {
                m_whosTurnIsItNot = m_whosTurnIsIt;
                m_whosTurnIsIt = value;                
            }
        }
        public Alignment WhosTurnIsItNot
        {
            get 
            { 
                return m_whosTurnIsItNot; 
            }
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
                                            newRook = new Rook(this, Alignment.White) { OccupiedSquare = squareToOccupy };                                            
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newRook = new Rook(this, Alignment.Black) { OccupiedSquare = squareToOccupy };
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
                                            newKnight = new Knight(this, Alignment.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newKnight = new Knight(this, Alignment.Black) { OccupiedSquare = squareToOccupy };
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
                                            newBishop = new Bishop(this, Alignment.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newBishop = new Bishop(this, Alignment.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newBishop;
                                        break;
                                    }
                                case 5:
                                    {
                                        King newKing;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newKing = new King(this, Alignment.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newKing = new King(this, Alignment.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newKing;
                                        break;
                                    }
                                case 4:
                                    {
                                        Queen newQueen;
                                        if (squareToOccupy.YCoordinate == 1)
                                        {
                                            newQueen = new Queen(this, Alignment.White) { OccupiedSquare = squareToOccupy };
                                        }
                                        else // if (squareToOccupy.YCoordinate == 8)
                                        {
                                            newQueen = new Queen(this, Alignment.Black) { OccupiedSquare = squareToOccupy };
                                        }
                                        squareToOccupy.OccupyingPiece = newQueen;
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            var newPawn = new Pawn(this, Alignment.White) { OccupiedSquare = squareToOccupy };
                            squareToOccupy.OccupyingPiece = newPawn;
                            break;
                        }
                    case 7:
                        {
                            var newPawn = new Pawn(this, Alignment.Black) { OccupiedSquare = squareToOccupy };
                            squareToOccupy.OccupyingPiece = newPawn;
                            break;
                        }
                }
            }
        }
         
        private void Square_MouseUp(object sender, MouseButtonEventArgs e)
        {            
            Square selectedSquare = sender as Square;

            if (selectedSquare.Equals(m_previousSelectedSquare))       // gleiches Feld wieder gewählt => mögliche Züge nicht mehr markieren
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
                    if (WhosTurnIsIt == Alignment.White)
                    {
                        this.GetSquare(selectedSquare.XCoordinate, selectedSquare.YCoordinate - 1).OccupyingPiece = null;
                    }
                    else
                    {
                        this.GetSquare(selectedSquare.XCoordinate, selectedSquare.YCoordinate + 1).OccupyingPiece = null;
                    }                    
                }   //else if Umwandlung
                else if (m_previousSelectedSquare.OccupyingPiece is Pawn && (selectedSquare.YCoordinate == 1 | selectedSquare.YCoordinate == 8))
                {
                    m_previousSelectedSquare.OccupyingPiece = this.PromotedPiece(selectedSquare);  
                }                
                else
                {
                    this.WriteLastMove(m_previousSelectedSquare, selectedSquare, m_previousSelectedSquare.OccupyingPiece);
                }
                             
                MovePiece(m_previousSelectedSquare, selectedSquare);
                m_previousPossibleSquares.Clear();
                m_previousSelectedSquare = null;

                //todo: Abfrage für Schachmatt

                //prüfen ob Gegner im Schach ist, wenn ja, Feld markieren.
                Square enemyKingLocation = this.GetKingLocation(m_whosTurnIsItNot);
                if (this.IsKingThreatened(this.m_whosTurnIsItNot))
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

                        // Meldung, dass kein gültiger Zug vorhanden ist
                        if (validMoves.Count == 0)
                        {
                            SplashNoMoves();
                        }

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
            //todo: Promotion notieren, Notation überhaupt richtig stellen. Dann permament speichern. Außerdem Mechanismus zum Einlesen überlegen, dann könnten gespeicherte Spiele fortgesetzt werden.
        }
        
        public void MovePiece(Square oldSquare, Square newSquare)
        {
            oldSquare.GrSquare.Children.Clear();
            newSquare.OccupyingPiece = oldSquare.OccupyingPiece;
            newSquare.OccupyingPiece.OccupiedSquare = newSquare;
            newSquare.OccupyingPiece.m_hasMoved = true;
            oldSquare.OccupyingPiece = null;
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

        public Square GetKingLocation(Alignment color)
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
                
        private bool verifyCheckMate(Square attackingSquare, Square attackedSquare)
        {
            //todo: Schachmatt prüfen

            return false;
        }

        public bool IsKingThreatened(Alignment color)
        {
            //Gerade Linie prüfen
            int i = 1;
            bool canMoveLeft = true;
            bool canMoveRight = true;
            bool canMoveUp = true;
            bool canMoveDown = true;

            Square kingLocation = this.GetKingLocation(color);

            // Geraden prüfen
            while (i <= 8)
            {
                if (canMoveRight)
                {
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate + i, kingLocation.YCoordinate);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate - i, kingLocation.YCoordinate);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate, kingLocation.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate, kingLocation.YCoordinate - i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Rook || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate + i, kingLocation.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Bishop || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate - i, kingLocation.YCoordinate + i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Bishop || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate + i, kingLocation.YCoordinate - i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Bishop || potentialSquare.OccupyingPiece is Queen))
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
                    var potentialSquare = this.GetSquare(kingLocation.XCoordinate - i, kingLocation.YCoordinate - i);
                    if (potentialSquare != null)
                    {
                        if (potentialSquare.OccupyingPiece != null)
                        {
                            if (potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && (potentialSquare.OccupyingPiece is Bishop || potentialSquare.OccupyingPiece is Queen))
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

            //Springer prüfen
            int x = kingLocation.XCoordinate;
            int y = kingLocation.YCoordinate;
            // Zwei Listen für mögliche x und y Koordinaten werden erstellt. 
            // Die Items der beiden Listen gehören zusammen, also possibleX[1] und possibleY[1]. So können mit einer for schleife alle Felder abgefragt werden. 
            // Das erste Feld ist oben rechts neben dem aktuellen Feld. Reihenfolge dann im Uhrzeigersinn.
            List<int> possibleX = new List<int>(8) { x + 1, x + 2, x + 2, x + 1, x - 1, x - 2, x - 2, x - 1 };
            List<int> possibleY = new List<int>(8) { y + 2, y + 1, y - 1, y - 2, y - 2, y - 1, y + 1, y + 2 };

            for (i = 0; i < 8; i++)
            {
                Square potentialSquare = this.GetSquare(possibleX[i], possibleY[i]);

                if (potentialSquare != null)
                {
                    if (potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment && potentialSquare.OccupyingPiece is Knight)
                    {
                        return true;
                    }
                }
            }

            //Bauer prüfen           
            if (kingLocation.OccupyingPiece.EnemyAlignment == Alignment.White)
            {
                Square potentialSquare;
                potentialSquare = this.GetSquare(kingLocation.XCoordinate + 1, kingLocation.YCoordinate + 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment)
                {
                    return true;
                }
                potentialSquare = this.GetSquare(kingLocation.XCoordinate - 1, kingLocation.YCoordinate + 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment)
                {
                    return true;
                }
            }
            else
            {
                Square potentialSquare;
                potentialSquare = this.GetSquare(kingLocation.XCoordinate + 1, kingLocation.YCoordinate - 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment)
                {
                    return true;
                }
                potentialSquare = this.GetSquare(kingLocation.XCoordinate - 1, kingLocation.YCoordinate - 1);
                if (potentialSquare != null && potentialSquare.OccupyingPiece != null && potentialSquare.OccupyingPiece.Alignment == kingLocation.OccupyingPiece.EnemyAlignment)
                {
                    return true;
                }
            }

            return false;
        }

        private void SplashNoMoves()
        {
            SplashNoMoves snm = new SplashNoMoves();
            snm.Show();
            delay(500);
            snm.Close();
            snm = null;
        }

        private void delay(int zeit)
        {
            int zeit1 = System.Environment.TickCount;
            while ((System.Environment.TickCount - zeit1) < zeit) ;
        }

    }
}
