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
    public partial class Square : UserControl, IEquatable<Square>
    {
        private PieceBase m_occupyingPiece;
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

        public PieceBase OccupyingPiece
        {
            get
            {
                return (m_occupyingPiece);
            }
            set
            {
                m_occupyingPiece = value;
                this.GrSquare.Children.Clear();
                if (value != null)
                {
                    this.GrSquare.Children.Add(value);
                }
            }
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

            InitializeComponent();
        }

        /// <summary>
        /// Gültige Züge sollen gehighlighted werden. Muss beim Ausführen des Zuges irgendwie rückgängig gemacht werden.
        /// </summary>
        public void Highlight(bool check = false)
        {
            if (check)
            {
                this.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.BorderBrush = new SolidColorBrush(Colors.Green);               
            }
            this.BorderThickness = new Thickness(5);
        }

        public void UnHighlight()
        {
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1);
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

        // Start Implementierung IEquatable
        public bool Equals(Square other)
        {
            if (other == null)
                return false;

            if ((this.XCoordinate == other.XCoordinate) && (this.YCoordinate == other.YCoordinate))
                return true;
            else
                return false;
        }
        /*
         * Laut https://msdn.microsoft.com/en-us/library/ms131190.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2  
         * sollte die Equals Methode von object.Equals und die GetHashCode auch überschrieben werden. Die
         * führt allerdings zur Fehlermeldung beim Compilieren: 
         * "Fehler	1	'SpackChess.Square.Equals(object)': Der geerbte Member 'System.Windows.DependencyObject.Equals(object)' kann nicht überschrieben werden, da er versiegelt ist.
         * Das gleiche für GetHashCode

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Square squareobj = obj as Square;
            if (squareobj == null)
                return false;
            else
                return Equals(squareobj);
        }

        public override int GetHashCode()
        {
            return this.XCoordinate.GetHashCode() + this.YCoordinate.GetHashCode();
        }
       
        public static bool operator == (Square square1, Square square2)
        {
            if (square1 == null || square2 == null)
                return Object.Equals(square1, square2);

            return square1.Equals(square2);
        }

        public static bool operator !=(Square square1, Square square2)
        {
            if (square1 == null || square2 == null)
                return !Object.Equals(square1, square2);

            return !(square1.Equals(square2));
        }
         */
        // Ende Implementierung IEquatable
    }
}
