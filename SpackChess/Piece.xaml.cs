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
    /// Interaction logic for Piece.xaml
    /// </summary>
    public abstract partial class Piece : UserControl
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

        public Piece()
        {
            InitializeComponent();
        }

        public Piece(IChessboard chessboard, Alignments color)
        {
            this.m_chessboard = chessboard;
           
            Alignment = color;
            InitializeComponent();
        }

        public abstract List<Square> GetValidMoves(Square currentLocation);

        public abstract override string ToString();
    }
}
