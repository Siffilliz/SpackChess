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
        protected List<Square> m_allSquares = new List<Square>();
        protected Square m_previousSelectedSquare;
        protected List<Square> m_previousPossibleSquares = new List<Square>();
        protected Alignment m_whosTurnIsIt = Alignment.White;
        protected Alignment m_whosTurnIsItNot = Alignment.Black;
        protected Alignment m_whoIsChecked;
        protected Notation m_actualGameNotation = new Notation();
      
        public Notation ActualGameNotation
        {
            get { return m_actualGameNotation; }
            set
            {
                m_actualGameNotation = value;
            }
        }

        public List<Square> AllSquares
        {
            get { return m_allSquares; }
        }       
        public Square GetSquare(int x, int y)
        {
            return this.AllSquares.FirstOrDefault(s => s.XCoordinate == x && s.YCoordinate == y);
        }
        public Square GetSquare(string squareName)
        {
            return this.AllSquares.FirstOrDefault(s => s.ToString() == squareName);
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
                    this.AllSquares.Add(newSquare);
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
            ActualGameNotation.GameRecord.Clear();
            if (this.WhosTurnIsIt == Alignment.Black)
            {
                this.WhosTurnIsIt = Alignment.White;
            }           
            
            foreach (Square square in this.AllSquares)
            {
                square.OccupyingPiece = null;
            }
            
            foreach (Square squareToOccupy in this.AllSquares)
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

            if (selectedSquare.Equals(m_previousSelectedSquare) && this.m_previousPossibleSquares.Count != 0)       // gleiches Feld wieder gewählt => mögliche Züge nicht mehr markieren
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
                // Umwandlung?
                if (m_previousSelectedSquare.OccupyingPiece is Pawn && (selectedSquare.YCoordinate == 1 | selectedSquare.YCoordinate == 8))
                {
                    PieceBase promotedTo = this.PromotedPiece(selectedSquare); ;
                    this.ExecuteSelectedMove(m_previousSelectedSquare, selectedSquare, promotedTo);                    
                }                               
                else
                {
                    this.ExecuteSelectedMove(m_previousSelectedSquare, selectedSquare);
                }
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
                            possibleSquare.Highlight();
                            m_previousPossibleSquares.Add(possibleSquare);
                        }

                        m_previousSelectedSquare = selectedSquare;
                    }                   
                }
            }           
        }       
   
        public void ExecuteSelectedMove(Square originSquare, Square targetSquare, PieceBase promotedTo = null)
        {
            string capture = "-";
            if (targetSquare.OccupyingPiece != null)
            {
                capture = "x";
            }

            foreach (Square possibleSquare in AllSquares)
            {
                possibleSquare.UnHighlight();
            }
            // en Passant?
            if (originSquare.OccupyingPiece is Pawn && targetSquare.XCoordinate != originSquare.XCoordinate && targetSquare.OccupyingPiece == null)
            {
                this.WriteLastMove(originSquare, targetSquare, originSquare.OccupyingPiece, enPassant: true, capture: "x");
                if (WhosTurnIsIt == Alignment.White)
                {
                    this.GetSquare(targetSquare.XCoordinate, targetSquare.YCoordinate - 1).OccupyingPiece = null;
                }
                else
                {
                    this.GetSquare(targetSquare.XCoordinate, targetSquare.YCoordinate + 1).OccupyingPiece = null;
                }
            }
            // kurze Rochade?
            else if (originSquare.OccupyingPiece is King && targetSquare.XCoordinate == originSquare.XCoordinate + 2)
            {
                if (WhosTurnIsIt == Alignment.White)
                {
                    MovePiece(this.GetSquare(8, 1), this.GetSquare(6, 1));
                }
                else
                {
                    MovePiece(this.GetSquare(8, 8), this.GetSquare(6, 8));
                }
                WriteLastMove(originSquare, targetSquare, originSquare.OccupyingPiece, capture, rochade: "0-0");
            }
            // lange Rochade?
            else if (originSquare.OccupyingPiece is King && targetSquare.XCoordinate == originSquare.XCoordinate - 2)
            {
                if (WhosTurnIsIt == Alignment.White)
                {
                    MovePiece(this.GetSquare(1, 1), this.GetSquare(4, 1));
                }
                else
                {
                    MovePiece(this.GetSquare(1, 8), this.GetSquare(4, 8));
                }
                WriteLastMove(originSquare, targetSquare, originSquare.OccupyingPiece, capture, rochade: "0-0-0");
            }
            // Umwandlung?
            else if (promotedTo != null)
            {
                PieceBase pawnForNotation = originSquare.OccupyingPiece;
                originSquare.OccupyingPiece = promotedTo;
                this.WriteLastMove(originSquare, targetSquare, pawnForNotation, capture, promotedTo: originSquare.OccupyingPiece);
            }
            else
            {
                this.WriteLastMove(originSquare, targetSquare, originSquare.OccupyingPiece, capture);
            }

            MovePiece(originSquare, targetSquare);
            m_previousPossibleSquares.Clear();
            originSquare = null;

            //prüfen ob Gegner im Schach ist, wenn ja, Feld markieren.
            Square enemyKingLocation = this.GetKingLocation(m_whosTurnIsItNot);
            if (this.IsKingThreatened(this.m_whosTurnIsItNot))
            {
                enemyKingLocation.Highlight(true);
                this.m_whoIsChecked = m_whosTurnIsItNot;

                if (this.verifyCheckMate(this.m_whosTurnIsItNot))
                {
                    if (this.m_whosTurnIsItNot == Alignment.White)
                    {
                        MessageBox.Show("Spieler Weiß ist Schachmatt!");
                    }
                    else
                    {
                        MessageBox.Show("Spieler Schwarz ist Schachmatt!");
                    }
                }
            }

            m_previousPossibleSquares.Clear();
            this.WhosTurnIsIt = this.m_whosTurnIsItNot;
        }

        public void MovePiece(Square oldSquare, Square newSquare)
        {    
            oldSquare.GrSquare.Children.Clear();
            newSquare.OccupyingPiece = oldSquare.OccupyingPiece;
            newSquare.OccupyingPiece.OccupiedSquare = newSquare;
            newSquare.OccupyingPiece.HasMoved = true;
            oldSquare.OccupyingPiece = null;
        }

        private void WriteLastMove(Square startSquare, Square endSquare, PieceBase movedPiece, string capture, bool enPassant = false, string rochade = "", PieceBase promotedTo = null)
        {
            if (enPassant)
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + capture + endSquare.ToString() + "e.p.";
            }
            else if (!string.IsNullOrEmpty(rochade))
            {
                LastMove = rochade;
            }
            else if (promotedTo != null)
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + capture + endSquare.ToString() + "=" + promotedTo.ToString();
            }
            else
            {
                LastMove = movedPiece.ToString() + startSquare.ToString() + capture + endSquare.ToString();
            }

            ActualGameNotation.GameRecord.Add(LastMove);
        }
           
        public PieceBase PromotedPiece(Square selectedSquare, ChessPieces promotedTo = ChessPieces.Pawn)
        {
            if (promotedTo == ChessPieces.Pawn)
            {
                Promotion choosePromotion = new Promotion();
                choosePromotion.ShowDialog();
            }          
          
            PieceBase chosenPiece;
            switch (promotedTo)
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
            foreach (Square possibleSquare in this.AllSquares)
            {               
                if (possibleSquare.OccupyingPiece is King && possibleSquare.OccupyingPiece.Alignment == color)
                {
                    return possibleSquare;
                }               
            }
            return null;
        }
                
        private bool verifyCheckMate(Alignment color)
        {           
            foreach (Square occupiedSquare in this.AllSquares)
            {
                if (occupiedSquare.OccupyingPiece != null && occupiedSquare.OccupyingPiece.Alignment == color)
                {
                    List<Square> validMoves = occupiedSquare.OccupyingPiece.GetValidMoves(occupiedSquare);                    
                    occupiedSquare.OccupyingPiece.SimulateMove(validMoves);
                    if (validMoves.Count != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
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
            if (kingLocation.OccupyingPiece.EnemyAlignment == Alignment.Black)
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

    //todo: Promotion funktioniert nicht
    //todo: Schach4.txt laden geht nicht
}
