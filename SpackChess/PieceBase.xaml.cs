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

        public Alignments Alignment
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

        public PieceBase(IChessboard chessboard, Alignments color)
        {
            this.m_chessboard = chessboard;
           
            Alignment = color;
            InitializeComponent();
        }

        public abstract List<Square> GetValidMoves(Square currentLocation);

        public abstract override string ToString();
    }

    /*
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
     */

}
