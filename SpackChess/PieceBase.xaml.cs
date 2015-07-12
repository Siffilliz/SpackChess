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
    /// <summary>
    /// Interaction logic for PieceBase.xaml
    /// </summary>
    public abstract partial class PieceBase : UserControl
    {
        internal IChessboard m_chessboard;
        internal Image m_graphic;
        internal Square m_occupiedSquare;
        internal bool m_hasMoved = false;

        public Image Graphic      
        {
            get
            {
                return m_graphic;
            }
            set
            {
                m_graphic = value;
            }
        }

        public Alignment Alignment
        {
            get;
            set;
        }

        public Alignment EnemyAlignment
        {
            get;
            set;
        }

        public Square OccupiedSquare
        {
            get
            {
                return m_occupiedSquare;
            }
            set
            {
                m_occupiedSquare = value;
            }
        }

        public PieceBase()
        {
            InitializeComponent();
        }

        public PieceBase(IChessboard chessboard, Alignment color)
        {
            this.m_chessboard = chessboard;
           
            Alignment = color;

            if (color == Alignment.White)
            {
                EnemyAlignment = Alignment.Black;
            }
            else
            {
                EnemyAlignment = Alignment.White;
            }
            
            InitializeComponent();
        }

        public abstract List<Square> GetValidMoves(Square currentLocation);

        public abstract override string ToString();

        public void SimulateMove(List<Square> potentialMoves)
        {
            Square oldLocation = this.m_occupiedSquare;
            var movesToRemove = new List<Square>();

            foreach (Square potentialMove in potentialMoves)
            {
                this.m_chessboard.MovePiece(oldLocation, potentialMove);
                
                if (this.m_chessboard.IsKingThreatened(this.Alignment))
                {
                    movesToRemove.Add(potentialMove);
                }

                this.m_chessboard.MovePiece(potentialMove, oldLocation);
            }

            foreach (Square removeMove in movesToRemove)
            {
                potentialMoves.Remove(removeMove);
            }
        }
    }

    /*todo:Mechanismus
        DrunkenSqrl: Am einfachsten wäre es glaub ich wenn du dir nen Mechanismus überlegst mit dem zu Züge "simulieren" kannst und dann einfach schaust ob die ziehende Partei nach dem Zug im Schach steht
        vigh: ja, an sowas hab ich auch schon gedacht
        vigh: also wirklich!
        vigh: wäre das dann ne funktion im chessboard, oder?
        DrunkenSqrl: Das zum Zug simulieren? Ne
        DrunkenSqrl: Das wäre im Square. Ideal verhält sich das Square bei einem echten Zug und beim simulierten Zug gleich, sodass du die gleichen Methoden beim echten und beim Simulierten Zug nutzen kannst
        DrunkenSqrl: Und die Logik für die simulierten Züge wäre dann im Piece
        vigh: ?? sicher?
        vigh: aber den normalen zug mach ich doch auch im chessboard
        DrunkenSqrl: Den normalen Zug ja, aber wo schaust du denn welche Figur wohin ziehen kann?
        vigh: im jeweiligen piece
        DrunkenSqrl: Eben
        vigh: ok
        DrunkenSqrl: Also machst du das auch dort
        vigh: kann ich das dann direkt im piece machen, oder?
        vigh: mmmh...weiß nicht genau
        vigh: ein teil davon ist sicherlich für alle gleich...
        DrunkenSqrl: Ja kannst du. Am Ende von der Methode wo du die möglichen Züge zurückgibst gibst du einer Methode im Piece (welches du übrigens in PieceBase umbennen solltest) die Liste und dort werden dann alle Züge simuliert und entsprechend gefiltert
   
     * vigh: dass ich die möglichen züge an eine methode im piece übergebe zur prüfung => daraus erfolgt doch wieder ein endlosschleife, oder?
DrunkenSqrl: Warum?
vigh: ja, bin mir nicht ganz sicher^^
vigh: ich glaub ich hab das mit dem simulierten zug schon mehr oder weniger im king
vigh: das muss ich glaub nur noch so hinbekommen und wo anderst hinschreiben, dass es für alle gilt
DrunkenSqrl: Du hast doch schon eine Methode die dir sagt ob ein König im Schach steht
vigh: ja, im kinh
vigh: king
vigh: die meinte ich
DrunkenSqrl: Und was macht dann   public Boolean SquareAttacked(Square squareToExamine, Alignments attackingAlignment) im Chessboard?
vigh: das ist glaub noch ein überbleibsel
vigh: bzw, wollte ich das nutzen, hat aber glaub nicht funktioniert
vigh: oder soll ich das nehmen?
DrunkenSqrl: Nimm das, sofern es funktioniert
vigh: wir wolltens halt ins piece machen eigentlich und nicht im chessboard
DrunkenSqrl: In PieceBase simulierst du dann den Zug, schaust ob er möglich ist, machst ihn dann "rückgängig"
DrunkenSqrl: Aber du solltest das vielleicht sinnvoller umbenenen in IsKingThreatened oder so
DrunkenSqrl: Also im Chessboard, und dann nur das Alignment übergeben
DrunkenSqrl: Und du solltest dir angewöhnen nicht benutzten Code zu löschen
DrunkenSqrl: v.a. wenn du nicht weißt ob er funktioniert
vigh: ja...
vigh: aber jetzt nochmal kurz: das squareattacked im boolean brauch ich dann ja doch nicht, wenn ich so vorgeh wie du grad geschrieben hast
DrunkenSqrl: du meinst im Chessboard
DrunkenSqrl: Ne
vigh: ne heißt du stimmst mir zu^^?
DrunkenSqrl: Ja
DrunkenSqrl: Das im King wirst du dann auch nicht brauchen
DrunkenSqrl: Aber du kannst wahrscheinlich den Code davon für die Methode im Chessboard recyceln
vigh: ja. das im king sollte ich ins piecebase nehmen und umbauen
vigh: ja genau^^
DrunkenSqrl: Und wie gesagt, allein Logisch sollte das Chessboard eine Methode haben mit der man abfragen kann ob jemand gerade im Schach steht
vigh: häää? jetzt bin ich komplett verwirrt
DrunkenSqrl: Stell dir vor du willst später in deinem UI außerhalb des Chessboards anzeigen dass jemand im Schach steht
vigh: ok, ich muss nochmal in mich gehen
     * 
     * vigh: glaub ich checks jetzt endlich wie ichs machen soll
vigh: nachdem alle möglichen züge im jeweiligen piece bestimmt wurden, übergeb ich die liste an ne methode im piece base
vigh: diese "führt" den zug durch und ruft die methode im chessboard auf, die prüft ob der könig gefährdet ist
vigh: wenn ja, wird der zug aus der liste gestrichen
vigh: danach wird der "zug" rückgängig gemacht
DrunkenSqrl: hab ich das nicht alles genauso gestern und davor erklärt?^^
vigh: ja, eigentlich schon^^
vigh: aber jetzt hoff ich nur, das da nicht ne endlosschleife dabei rauskommt?
vigh: ah ja, deswegen funktioniert auch die aktuelle methode im square attacked nicht
vigh: die ruft ja wiederum jedes piece auf, dass es die validmoves ermitteln soll
vigh: und dann geht das ganze ja wieder von vorne los
vigh: deswegen nehm ich die methode, die ich jetzt im king hab, und schieb sie ins piece oder ins chessboard und machs mit der
vigh: die andere kann ich dann eigentlich direkt löschen


     * */

}
