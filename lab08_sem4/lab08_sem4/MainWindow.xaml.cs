using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Threading;

namespace lab08_sem4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        #region Variables
        string dec_sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        /// <summary>
        /// восстановить в исходное положение форму
        /// </summary>
        private bool mRestoreIfMove = false;
        /// <summary>
        /// первое число
        /// </summary>
        private decimal _firstnumber = 0;
        /// <summary>
        /// второе число
        /// </summary>
        private decimal _secondnumber = 0;
        /// <summary>
        /// результат
        /// </summary>
        private decimal result = 0;
        /// <summary>
        /// действие
        /// </summary>
        private string _act = "";
        /// <summary>
        /// последнее действие
        /// </summary>
        private string last;
        /// <summary>
        /// количество запятых
        /// </summary>
        private int countofdot = 0;
        /// <summary>
        /// максимальное количество цифр в числе
        /// </summary>
        private int MAXTEXTCHARS = 7;
        /// <summary>
        /// память
        /// </summary>
        private decimal memory = 0;

        #endregion

        /// <summary>
        /// конструктор
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.VirtualScreenHeight;
            MaxWidth = SystemParameters.VirtualScreenWidth + 10;
            
        }

        #region Methods

        /// <summary>
        /// вычитаем последний символ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minus_last_symbol(object sender, EventArgs e)
        {
            //убираем последний символ (119 -> 11)

            if (textBlock.Text.Length == 2)
            {
                if (textBlock.Text[0] == '-')
                    textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 2);
                else
                {
                    if (textBlock.Text[textBlock.Text.Length - 1] != ',')
                    {
                        textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 1);
                    }
                    else
                    {
                        textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 1);
                        countofdot = 0;
                    }
                }
            }
            else if(textBlock.Text.Length > 2 || textBlock.Text.Length == 1)
            {
                if (textBlock.Text[textBlock.Text.Length - 1] != ',')
                {
                    textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 1);
                }
                else if (textBlock.Text[textBlock.Text.Length - 1] == ',')
                {
                    textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 1);
                    countofdot = 0;
                }
            }
            if (textBlock.Text.Length == 0)
            {
                textBlock.Text = "0";
            }

        }
        /// <summary>
        /// действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Actions_Click(object sender, RoutedEventArgs e)
        {
            if (actionBlock.Text == "0" || actionBlock.Text == "Не число")
            {
                Decimal.TryParse(textBlock.Text.Replace(",", dec_sep), out _firstnumber);

                actionBlock.Text = _firstnumber + ((Button)sender).Content.ToString();

                textBlock.Text = "0";

                countofdot = 0;
            }
            else
            {
                Calc calc = new Calc();
                Decimal.TryParse(textBlock.Text.Replace(",", dec_sep), out _secondnumber);

                countofdot = 0;

                textBlock.Text = "0";

                switch (last)
                {
                    case "+":
                        _firstnumber = calc.Add(_firstnumber, _secondnumber);
                        break;
                    case "-":
                        _firstnumber = calc.Subtract(_firstnumber, _secondnumber);
                        break;
                    case "*":
                        _firstnumber = calc.Multiply(_firstnumber, _secondnumber);
                        break;
                    case "/":
                        _firstnumber = calc.Divide(_firstnumber, _secondnumber);
                        break;
                }
            }

            switch (((Button)sender).Content.ToString())
            {
                case "+":
                    last = "+";
                    break;
                case "-":
                    last = "-";
                    break;
                case "×":
                    last = "*";
                    break;
                case "÷":
                    last = "/";
                    break;
            }
            _act = ((Button)sender).Content.ToString();
            actionBlock.Text = _firstnumber.ToString() + ((Button)sender).Content.ToString();


        }
        /// <summary>
        /// одиночные действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleAction(object sender, RoutedEventArgs e)
        {

            Calc calc = new Calc();
            switch (((Button)sender).Content.ToString())
            {
                case "x²":
                    result = calc.square(Convert.ToDecimal(textBlock.Text.Replace(",", dec_sep)));
                    actionBlock.Text = ((Button)sender).Content.ToString();
                    textBlock.Text = result.ToString();
                    break;
                case "²√x":
                    try
                    {
                        result = calc.squareroot(Convert.ToDecimal(textBlock.Text.Replace(",", dec_sep)));
                        actionBlock.Text = ((Button)sender).Content.ToString();
                        textBlock.Text = result.ToString();
                    }
                    catch
                    {
                        actionBlock.Text = "Не число";
                        _firstnumber = 0;
                    }
                    break;
                case "1/x":
                    try
                    {
                        result = calc.onedivx(Convert.ToDecimal(textBlock.Text));
                        actionBlock.Text = ((Button)sender).Content.ToString();
                        textBlock.Text = result.ToString();
                    }
                    catch
                    {
                        actionBlock.Text = "Не число";
                        _firstnumber = 0;
                    }
                    break;
                case "cos()":
                    try
                    {
                        result = calc.cos(Convert.ToDecimal(textBlock.Text));
                        actionBlock.Text = ((Button)sender).Content.ToString();
                        textBlock.Text = result.ToString();
                    }
                    catch
                    {
                        actionBlock.Text = "Не число";
                        _firstnumber = 0;
                    }
                    break;
                case "+/-":
                    result = Convert.ToDecimal(textBlock.Text) * -1;
                    textBlock.Text = result.ToString();
                    break;
            }
        }
        /// <summary>
        /// ввод данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if (actionBlock.Text.Contains('='))
                if (sender == Button0 || sender == Button1 || sender == Button2 || sender == Button3 || sender == Button4
                || sender == Button5 || sender == Button6 || sender == Button7 || sender == Button8 || sender == Button9)
                {
                    actionBlock.Text = "0";
                    textBlock.Text = "0";
                }
            if (((Button)sender).Content.ToString() == ",")
            {
                //если запятых нет прибавляем запятую
                if (countofdot < 1 && textBlock.Text.Length < MAXTEXTCHARS)
                {
                    if (textBlock.Text.Length != 0)
                    {
                        textBlock.Text += ((Button)sender).Content.ToString();
                        countofdot += 1;
                    }
                }
            }//если запятые есть
            else
            {
                if (textBlock.Text.Length < MAXTEXTCHARS)
                {
                    //если первоначальное значение 0, убираем 0 и ставим другое значение
                    if (textBlock.Text.Length == 1 && textBlock.Text == "0")
                    {
                        textBlock.Text = ((Button)sender).Content.ToString();
                    }
                    else//если не 0 просто добавляем цифру
                    {
                        if (textBlock.Text.Length < MAXTEXTCHARS)
                        {
                            textBlock.Text += ((Button)sender).Content.ToString();
                        }
                    }
                }
            }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// работа с клавиатурой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_PreviewKeyDown(object sender, KeyEventArgs e)
        {//умножение ( переменная чтобы не писалась 8 при умножении через shift + 8 )
            bool mult = false;

            //когда показывается результат и нажимается действие
            // && (e.Key == Key.Multiply || e.Key == Key.Divide || e.Key == Key.Subtract || e.Key == Key.Add || e.Key == Key.Back || sender == ButtonMul || sender == ButtonDiv || sender == ButtonSub || sender == ButtonPlus || sender == ButtonBack)
            if (actionBlock.Text.Contains('='))
            {

                _firstnumber = result; 
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                {
                    //+
                    if (e.Key == Key.OemPlus)
                    {
                        last = "+";
                        textBlock.Text = "0";
                    }
                    //*
                    else if (e.Key == Key.D8)
                    {
                        textBlock.Text = "1";
                        last = "*";
                        mult = true;
                    }
                    //divide
                    else if (e.Key == Key.OemBackslash)
                    {
                        textBlock.Text = "1";
                        last = "/";
                    }
                }
                switch (e.Key)
                {
                    case Key.Multiply:
                        textBlock.Text = "1";
                        last = "*";
                        break;
                    case Key.Divide:
                        textBlock.Text = "1";
                        last = "/";
                        break;
                    case Key.Subtract:
                        textBlock.Text = "0";
                        last = "-";
                        break;
                    case Key.Add:
                        last = "+";
                        textBlock.Text = "0";
                        break;
                    case Key.Back:
                        ButtonC_Click(ButtonC, e);
                        break;
                }
                if (sender == ButtonMul)
                {
                    textBlock.Text = "1";
                    last = "*";
                }
                else if (sender == ButtonDiv) 
                {
                    textBlock.Text = "1";
                    last = "/";
                }
                else if (sender == ButtonSub)
                {
                    textBlock.Text = "0";
                    last = "-";
                }
                else if (sender == ButtonPlus)
                {
                    last = "+";
                    textBlock.Text = "0";
                }
                else if (sender == ButtonBack)
                {
                    ButtonC_Click(ButtonC, e);
                }
                if (e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 ||
                    e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D9 || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 ||
                    e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                {
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                    {
                        if (e.Key == Key.D8)
                        {
                            actionBlock.Text = "0";
                            textBlock.Text = "0";
                        }
                    }
                    else
                    {
                        actionBlock.Text = "0";
                        textBlock.Text = "0";
                    }
                }
                if (actionBlock.Text.Contains('='))
                    if (sender == Button0 || sender == Button1 || sender == Button2 || sender == Button3 || sender == Button4
                    || sender == Button5 || sender == Button6 || sender == Button7 || sender == Button8 || sender == Button9)
                    {
                        actionBlock.Text = "0";
                        textBlock.Text = "0";
                    }
            }


            //нажата шифт



            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
            {
                //+
                if (e.Key == Key.OemPlus)
                {
                    Actions_Click(ButtonPlus, e);
                }
                //*
                else if (e.Key == Key.D8)
                {
                    Actions_Click(ButtonMul, e);
                    mult = true;
                }
                //divide
                else if (e.Key == Key.OemBackslash)
                {
                    Actions_Click(ButtonDiv, e);
                }
            }


            switch (e.Key)
            {
                case Key.D0:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button0, e);
                    break;
                case Key.NumPad0:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button0, e);
                    break;
                case Key.D1:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button1, e);
                    break;
                case Key.NumPad1:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button1, e);
                    break;
                case Key.D2:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button2, e);
                    break;
                case Key.NumPad2:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button2, e);
                    break;
                case Key.D3:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button3, e);
                    break;
                case Key.NumPad3:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button3, e);
                    break;
                case Key.D4:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button4, e);
                    break;
                case Key.NumPad4:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button4, e);
                    break;
                case Key.D5:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button5, e);
                    break;
                case Key.NumPad5:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button5, e);
                    break;
                case Key.D6:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button6, e);
                    break;
                case Key.NumPad6:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button6, e);
                    break;
                case Key.D7:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button7, e);
                    break;
                case Key.NumPad7:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button7, e);
                    break;
                case Key.D8:
                    if (mult)
                        mult = false;
                    else
                    {
                        if (actionBlock.Text.Contains('='))
                            textBlock.Text = "0";
                        Number_Click(Button8, e);
                    }
                    break;
                case Key.NumPad8:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button8, e);
                    break;
                case Key.D9:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button9, e);
                    break;
                case Key.NumPad9:
                    if (actionBlock.Text.Contains('='))
                        textBlock.Text = "0";
                    Number_Click(Button9, e);
                    break;
                case Key.OemComma:
                    Number_Click(ButtonDot, e);
                    break;
                case Key.Add:
                    Actions_Click(ButtonPlus, e);
                    break;
                case Key.Subtract:
                    Actions_Click(ButtonSub, e);
                    break;
                case Key.OemMinus:
                    Actions_Click(ButtonSub, e);
                    break;
                case Key.Multiply:
                    Actions_Click(ButtonMul, e);
                    break;
                case Key.Divide:
                    Actions_Click(ButtonDiv, e);
                    break;
                case Key.Enter:
                    ResultClick(ButtonRes, e);
                    break;
                case Key.Back:
                    minus_last_symbol(ButtonBack, e);
                    break;
                default:
                    break;

            }
        }
        /// <summary>
        /// результат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (actionBlock.Text.Contains('='))
                {

                }
                else if (last != " ")
                {
                    Decimal.TryParse(textBlock.Text.Replace(",", dec_sep), out _secondnumber);
                    Calc calc = new Calc();

                    switch (last)
                    {
                        case "+":
                            result = calc.Add(_firstnumber, _secondnumber);
                            actionBlock.Text = _firstnumber.ToString() + _act + _secondnumber.ToString() + '=';
                            textBlock.Text = result.ToString();
                            break;
                        case "-":
                            result = calc.Subtract(_firstnumber, _secondnumber);
                            actionBlock.Text = _firstnumber.ToString() + _act + _secondnumber.ToString() + '=';
                            textBlock.Text = result.ToString();
                            break;
                        case "*":
                            result = calc.Multiply(_firstnumber, _secondnumber);
                            actionBlock.Text = _firstnumber.ToString() + _act + _secondnumber.ToString() + '=';
                            textBlock.Text = result.ToString();
                            break;
                        case "/":
                            try
                            {
                                result = calc.Divide(_firstnumber, _secondnumber);
                                actionBlock.Text = _firstnumber.ToString() + _act + _secondnumber.ToString() + '=';
                                textBlock.Text = result.ToString();
                            }
                            catch
                            {
                                actionBlock.Text = "Не число";
                                _firstnumber = 0;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else { actionBlock.Text = textBlock.Text.Replace(",", dec_sep); }
            }
            catch { }
        }
        /// <summary>
        /// очистить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Content.ToString())
            {
                case "C":
                    result = 0;
                    _firstnumber = 0;
                    _secondnumber = 0;
                    countofdot = 0;
                    last = " ";
                    textBlock.Text = "0";
                    actionBlock.Text = "0";
                    break;
                case "CE":
                    result = 0;
                    _firstnumber = 0;
                    _secondnumber = 0;
                    countofdot = 0;
                    last = " ";
                    textBlock.Text = "0";
                    actionBlock.Text = "0";
                    break;
            }
        }

        /// <summary>
        /// работа с памятью
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemoryButtons(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Content.ToString())
            {
                case "MC":
                    memory = 0;
                    break;
                case "M+":
                    Decimal.TryParse(textBlock.Text.Replace(",", dec_sep), out memory);
                    break;
                case "M-":
                    memory = Convert.ToDecimal(textBlock.Text.Replace(",", dec_sep)) * -1;
                    break;
                case "MR":
                    textBlock.Text = memory.ToString();
                    textBlock.Text = textBlock.Text.Replace(",", dec_sep);
                    break;
                case "MS":
                    Decimal.TryParse(textBlock.Text.Replace(",", dec_sep), out memory);
                    break;
            }
        }

        #endregion

        #region UI

        /// <summary>
        /// закрыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// развернуть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Max_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwitchWindowState();
        }
        /// <summary>
        /// свернуть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Min_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// развернуть/свернуть
        /// </summary>
        private void SwitchWindowState()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    {
                        WindowState = WindowState.Maximized;
                        break;
                    }
                case WindowState.Maximized:
                    {
                        WindowState = WindowState.Normal;
                        mRestoreIfMove = false;
                        break;
                    }
            }
        }
        /// <summary>
        /// при развернутом приложении сворачивание с зажатой левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            if (mRestoreIfMove)
            {
                mRestoreIfMove = false;

                //координаты курсора
                double percentHorizontal = e.GetPosition(this).X / ActualWidth;
                double targetHorizontal = RestoreBounds.Width * percentHorizontal;

                double percentVertical = e.GetPosition(this).Y / ActualHeight;
                double targetVertical = RestoreBounds.Height * percentVertical;

                //обычный 
                WindowState = WindowState.Normal;

                //курсор
                POINT lMousePosition;
                GetCursorPos(out lMousePosition);

                //устанавливаем форму под курсором
                Left = lMousePosition.X - targetHorizontal;
                Top = lMousePosition.Y - targetVertical;

                //чтобы форма двигалась
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            }
        }
        
        //библиотека для позиции курсора
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        //структура для координат
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        /// <summary>
        /// по 2 кликам уменьшаем форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.ClickCount == 2)
                {
                    SwitchWindowState();

                    return;
                }
                if (WindowState == WindowState.Maximized)
                {
                    mRestoreIfMove = true;
                    return;
                }
                DragMove();
            }
        }



        #endregion

       
    }

    public class Calc
    {
        #region Class_CALC

        /// <summary>
        /// прибавляем
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }
        /// <summary>
        /// вычитаем
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public decimal Subtract(decimal a, decimal b)
        {
            return a - b;
        }
        /// <summary>
        /// умножаем
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }
        /// <summary>
        /// делим
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal Divide(decimal a, decimal b)
        {
            if (b == 0)
            {
                throw new ArgumentException("divide by zero");
            }
            else
                return a / b;
        }
        /// <summary>
        /// квадрат
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal square(decimal a)
        {
            return a * a;
        }
        /// <summary>
        /// 1/х
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal onedivx(decimal a)
        {
            if (a == 0)
                throw new ArgumentException("divide by zero");
            else
                return 1 / a;
        }
        /// <summary>
        /// корень х
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal squareroot(decimal a, decimal? guess = null)
        {
            if (a < 0)
            {
                throw new Exception();
            }
            else
            {
                var ourGuess = guess.GetValueOrDefault(a / 2m);
                var result = a / ourGuess;
                var average = (ourGuess + result) / 2m;

                if (average == ourGuess) // This checks for the maximum precision possible with a decimal.
                    return Math.Round(average, 0);
                else
                    return squareroot(a, average);
            }
        }
        /// <summary>
        /// косинус х
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public decimal cos(decimal a)
        {
            a *= (decimal)(Math.PI / 180);
            return (decimal)Math.Cos((double)a);
        }
        #endregion
    }

}
