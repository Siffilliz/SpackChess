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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Chessboard m_chessboard = new Chessboard();

        public MainWindow()
        {
            InitializeComponent();
            this.GrMain.Children.Add(m_chessboard);
        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            string fileName = this.m_chessboard.ActualGameNotation.GetFileName(true);
            this.m_chessboard.ActualGameNotation.WriteFile(fileName);
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {  
            string fileName = m_chessboard.ActualGameNotation.GetFileName(false);

            if (fileName != null)
            {
                this.m_chessboard.ResetGame();

                List<string> moves = m_chessboard.ActualGameNotation.LoadSaveGame(fileName);

                foreach (string move in moves)
                {  
                    //Rochade rausfiltern
                    Square oldSquare;
                    Square newSquare;
                    PieceBase promotedPiece = null;
                    if (move == "0-0")
                    {
                        //Zuordnung ob weiß oder schwarz => ist item move in moves gerade (weiß) oder ungerade (schwarz)? 
                        if ((moves.IndexOf(move) % 2) == 0)
                        {
                            oldSquare = this.m_chessboard.GetSquare("e1");
                            newSquare = this.m_chessboard.GetSquare("g1");
                        }
                        else
                        {
                            oldSquare = this.m_chessboard.GetSquare("e8");
                            newSquare = this.m_chessboard.GetSquare("g8");
                        }
                    }
                    else if (move == "0-0-0")
                    {
                        //Zuordnung ob weiß oder schwarz => ist item move in moves gerade (weiß) oder ungerade (schwarz)? 
                        if ((moves.IndexOf(move) % 2) == 0)
                        {
                            oldSquare = this.m_chessboard.GetSquare("e1");
                            newSquare = this.m_chessboard.GetSquare("c1");
                        }
                        else
                        {
                            oldSquare = this.m_chessboard.GetSquare("e8");
                            newSquare = this.m_chessboard.GetSquare("c8");
                        }
                    }
                    else
                    {
                        string[] startSquareEndSquare;
                        if (move.Contains("-"))
                        {
                            startSquareEndSquare = move.Split('-');
                        }
                        else
                        {
                            startSquareEndSquare = move.Split('x');
                        }

                        string moveOldSquare, moveNewSquare;
                        if (startSquareEndSquare[0].Length > 2)
                        {
                            moveOldSquare = startSquareEndSquare[0].Substring(1, 2);
                        }
                        else
                        {
                            moveOldSquare = startSquareEndSquare[0];
                        }

                        if (startSquareEndSquare[1].Length > 2)
                        {
                            //Promotion?      
                            moveNewSquare = startSquareEndSquare[1].Substring(0, 2);
                            newSquare = this.m_chessboard.GetSquare(moveNewSquare);

                            string isPromotion = startSquareEndSquare[1].Substring(startSquareEndSquare[1].Length - 1, 1);
                            switch (isPromotion)
                            {
                                case "Q":
                                    promotedPiece = this.m_chessboard.PromotedPiece(newSquare, ChessPieces.Queen);
                                    break;
                                case "R":
                                    promotedPiece = this.m_chessboard.PromotedPiece(newSquare, ChessPieces.Rook);
                                    break;
                                case "B":
                                    promotedPiece = this.m_chessboard.PromotedPiece(newSquare, ChessPieces.Bishop);
                                    break;
                                case "N":
                                    promotedPiece = this.m_chessboard.PromotedPiece(newSquare, ChessPieces.Knight);
                                    break;                                    
                            }                           
                        }
                        else
                        {
                            moveNewSquare = startSquareEndSquare[1];
                        }
                        oldSquare = this.m_chessboard.GetSquare(moveOldSquare);
                        newSquare = this.m_chessboard.GetSquare(moveNewSquare);                       
                    }
                    this.m_chessboard.ExecuteSelectedMove(oldSquare, newSquare, promotedPiece);
                }
            }           
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            //warum muss ich hier doch das optionale Argument string reinschreiben? Dachte visual studio weiß was gemeint ist? So steht es zumindest im Buch...
            MessageBoxResult result = MessageBox.Show("Spiel wirklich zurücksetzen?", "Reset?", MessageBoxButton.OKCancel);    

            if (result == MessageBoxResult.OK)
            {
                this.m_chessboard.ResetGame();
            } 
        }
    }
}
