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
using System.Windows.Shapes;

namespace SpackChess
{
    /// <summary>
    /// Interaktionslogik für Promotion.xaml
    /// </summary>
    public partial class Promotion : Window
    {
        public ChessPieces PromotedTo
        {
            get;
            set;
        }

        public Promotion()
        {
            InitializeComponent();
        }

        private void btnQueen_Click(object sender, RoutedEventArgs e)
        {
            this.PromotedTo = ChessPieces.Queen;
            this.Close();
        }

        private void btnRook_Click(object sender, RoutedEventArgs e)
        {
            this.PromotedTo = ChessPieces.Rook;
            this.Close();
        }

        private void btnKnight_Click(object sender, RoutedEventArgs e)
        {
            this.PromotedTo = ChessPieces.Knight;
            this.Close();
        }

        private void btnBishop_Click(object sender, RoutedEventArgs e)
        {
            this.PromotedTo = ChessPieces.Bishop;
            this.Close();
        }
    }
}
