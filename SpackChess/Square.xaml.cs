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
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class Square : UserControl
    {
        /// <summary>
        /// Gets or sets the value of the XCoordinate.
        /// </summary>
        public int XCoordinate
        {
            private set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int YCoordinate
        {
            private set;
            get;
        }
        /// <summary>
        /// Bool ob das Feld besetzt ist. Muss abgefragt werden, wenn die gültigen Züge berechnet werden. Wird im Konstruktur auf false gesetzt.
        /// </summary>
        public bool Occupied
        {
            get;
            set;
        }

        public Piece OccupyingPiece
        {
            get;
            set;
        }

        public Square(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;

            int sum = x + y;

            if (sum % 2 == 0)
            {
                this.Background = new SolidColorBrush(Colors.Brown);
            }
            else
            {
                this.Background = new SolidColorBrush(Colors.Beige);
            }

            Occupied = false;

            InitializeComponent();            
        }

        /// <summary>
        /// Gültige Züge sollen gehighlighted werden. Muss beim Ausführen des Zuges irgendwie rückgängig gemacht werden.
        /// </summary>
        public void Highlight()
        {
            this.Background = new SolidColorBrush(Colors.Green);
        }

        public override string ToString()
        {
            string xAsChar = string.Empty;

            switch (XCoordinate)
            {
                case 1:
                    xAsChar = "a";
                    break;
                case 2:
                    xAsChar = "b";
                    break;
                case 3:
                    xAsChar = "c";
                    break;
                case 4:
                    xAsChar = "d";
                    break;
                case 5:
                    xAsChar = "e";
                    break;
                case 6:
                    xAsChar = "f";
                    break;
                case 7:
                    xAsChar = "g";
                    break;
                case 8:
                    xAsChar = "h";
                    break;            
            }

            return xAsChar + YCoordinate.ToString();
        }
    }
}
