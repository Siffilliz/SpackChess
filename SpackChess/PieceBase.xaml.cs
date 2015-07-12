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
            private set;
        }

        public Alignment EnemyAlignment
        {
            get
            {
                if (this.Alignment == Alignment.White)
                {
                    return Alignment.Black;
                }
                else
                {
                    return Alignment.White;
                }
            }
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
}
