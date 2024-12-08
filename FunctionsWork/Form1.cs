using System.Diagnostics;

namespace FunctionsWork
{
    public partial class Functions : Form
    {
        private Graphics _pictureBox1Graphics = null!; // Инициализируем объекты графики под каждый PictureBox
        private Graphics _pictureBox2Graphics = null!; // = null! означает, что объект в будущем будет проинициализирован.
        private Graphics _pictureBox3Graphics = null!;
        private Pen _basePen = null!; // Инициализируем кисти для всех функций и для контура
        private Pen _func1Pen = null!;
        private Pen _func2Pen = null!;
        private Pen _func3Pen = null!;
        private float _calculatingStep; // Шаг для X. Чем меньше - тем лучше график выглядит
        private int _maxX_func1; // Ограничение по X для графиков. Это значение отражает, сколько X мы подразумеваем в графике
        private int _maxX_func2;
        private int _maxX_func3;
        private int _maxY_func1; // Ограничение по Y для графиков. Это значение отражает, сколько Y мы подразумеваем в графике
        private int _maxY_func2;
        private int _maxY_func3;

        public Functions()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _pictureBox1Graphics = pictureBox1.CreateGraphics(); // Создаем графики PictureBox
            _pictureBox2Graphics = pictureBox2.CreateGraphics();
            _pictureBox3Graphics = pictureBox3.CreateGraphics();
            _basePen = new Pen(Color.Black); // Создаем кисти под разные цвета
            _func1Pen = new Pen(Color.Red);
            _func2Pen = new Pen(Color.Blue);
            _func3Pen = new Pen(Color.Violet);
            _calculatingStep = 0.1f; // Задаем количество точек в графике. Чем больше, тем более четким он получится
            _maxX_func1 = 10; 
            _maxX_func2 = 3; 
            _maxX_func3 = 2; 
            _maxY_func1 = 10;
            _maxY_func2 = 1; 
            _maxY_func3 = 2; 
            this.Text = "Drawing functions";
        }

        private async void Func1Btn_Click(object sender, EventArgs e) // Событие на нажатие кнопки под первую функцию
        {
            pictureBox1.Refresh(); // Обновляем графику
            var actualSize = pictureBox1.Size; // Берем размер
            DrawBase(_pictureBox1Graphics, actualSize); // Создаем каркас для графика
            await Task.Delay(1000); // Небольшая задержка
            DrawFirstFunction(_pictureBox1Graphics, actualSize); // Рисуем сам график
        }

        private async void Func2Btn_Click(object sender, EventArgs e) // Событие на нажатие кнопки под вторую функцию
        {
            pictureBox2.Refresh(); // Тоже самое, что выше. Меняем только PictureBox
            var actualSize = pictureBox2.Size;
            DrawBase(_pictureBox2Graphics, actualSize);
            await Task.Delay(1000);
            DrawSecondFunction(_pictureBox2Graphics, actualSize);
        }

        private async void Func3Btn_Click(object sender, EventArgs e) // Событие на нажатие кнопки под третью функцию
        {
            pictureBox3.Refresh(); // Тоже самое
            var actualSize = pictureBox3.Size;
            DrawBase(_pictureBox3Graphics, actualSize);
            await Task.Delay(1000);
            DrawThirdFunction(_pictureBox3Graphics, actualSize);
        }

        private void DrawFirstFunction(Graphics graphics, Size actualSize) // Первая функция
        {
            DrawFunction(graphics, actualSize, 1, _func1Pen, _maxX_func1, _maxY_func1);
        }

        private void DrawSecondFunction(Graphics graphics, Size actualSize) // Вторая функция
        {
            DrawFunction(graphics, actualSize, 2, _func2Pen, _maxX_func2, _maxY_func2);
        }

        private void DrawThirdFunction(Graphics graphics, Size actualSize) // Третья функция
        {
            DrawFunction(graphics, actualSize, 3, _func3Pen, _maxX_func3, _maxY_func3);
        }

        private void DrawBase(Graphics graphics, Size actualSize) // Зарисовка каркаса графика (координатной плоскости)
        {
            var startPoint = new Point(0, 0); // Помечаем начало. Левый верхний угол
            var drawSize = new Size(actualSize.Width - 1, actualSize.Height - 1); // Задаем размер. Важно убрать по 1 от размера, чтобы видеть нарисованный край
            graphics.DrawRectangle(_basePen, new Rectangle(startPoint, drawSize)); // Рисуем прямоугольник
            var lineStartPoint = new Point(0, actualSize.Height / 2); // Создаем линию по середине
            var lineEndPoint = new Point(actualSize.Width, actualSize.Height / 2);
            graphics.DrawLine(_basePen, lineStartPoint, lineEndPoint); // Рисуем ее
        }

        private void DrawFunction(Graphics graphics, Size actualSize, int functionId, Pen pen, int maxX, int maxY)
        {
            float width = actualSize.Width;
            float height = actualSize.Height;
            float halfHeight = actualSize.Height / 2;
            List<PointF> points = new();

            for (float i = 0f; i < maxX; i += _calculatingStep)
            {
                try // Обернул в try...catch для устранений аварийных ситуации при ошибках на расчетах
                {
                    var calc_point_X = i;
                    var calc_point_Y = CalculateYPoint(functionId, calc_point_X); // Расчет Y по функции

                    var x_graph_position = calc_point_X * width / maxX; // Конвертируем точку из вычисленного значения в отображение в графике
                    var y_graph_position = (-calc_point_Y * height / maxY) + halfHeight; // Множим на -1, т.к. рассчет идет сверху вниз

                    if (float.IsNaN(y_graph_position)) // Деление 0 / 0 дает NaN, отбрасываем такие случаи
                    {
                        continue;
                    }

                    if (y_graph_position < 0f) // Если точка вне нашего размера, то считаем ее как 0
                    {
                        y_graph_position = 0f;
                    }

                    if (y_graph_position > actualSize.Height) // Если точка вне нашего размера, то считаем ее как высоту
                    {
                        y_graph_position = actualSize.Height;
                    }

                    points.Add(new PointF(x_graph_position, y_graph_position));
                }
                catch (Exception) // Ловим ошибку
                {
                    Debug.WriteLine("Возникла ошибка. Точка не будет построена для графика."); // Тут может быть деление на 0 в тригонометрических функциях
                }
            }

            graphics.DrawCurve(pen, points.ToArray()); // Рисуем кривую графика по координатам
        }

        private float CalculateYPoint(int functionId, float x)
        {
            switch (functionId)
            {
                case 1:
                    return MathF.Pow(x, 3f) * (MathF.Cos(x) / MathF.Sin(x)); // Расчет Y по функции
                case 2:
                    {
                        var step1_result = MathF.Pow(x + 1f, 1f / 3f) / (MathF.Pow(x, 2f) + 1f);
                        var step2_result = MathF.Pow(MathF.Pow(x, 2f) - 2f * x, 1f / 3f) / (MathF.Pow(x, 2f) - 1f);
                        return step1_result - step2_result; // Расчет второй функции
                    }

                case 3:
                    {
                        var step1_result = MathF.Pow(x, 3f / 2f) * x / 2f;
                        var step2_result = MathF.Pow(x, 2f) / 2f * MathF.Acos(x);
                        return step1_result + step2_result; // Расчет третьей функции
                    }

                default:
                    return 0;
            }
        }
    }
}
