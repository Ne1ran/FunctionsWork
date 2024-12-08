using System.Diagnostics;

namespace FunctionsWork
{
    public partial class Functions : Form
    {
        private Graphics _pictureBox1Graphics = null!; // �������������� ������� ������� ��� ������ PictureBox
        private Graphics _pictureBox2Graphics = null!; // = null! ��������, ��� ������ � ������� ����� ������������������.
        private Graphics _pictureBox3Graphics = null!;
        private Pen _basePen = null!; // �������������� ����� ��� ���� ������� � ��� �������
        private Pen _func1Pen = null!;
        private Pen _func2Pen = null!;
        private Pen _func3Pen = null!;
        private float _calculatingStep; // ��� ��� X. ��� ������ - ��� ����� ������ ��������
        private int _maxX_func1; // ����������� �� X ��� ��������. ��� �������� ��������, ������� X �� ������������� � �������
        private int _maxX_func2;
        private int _maxX_func3;
        private int _maxY_func1; // ����������� �� Y ��� ��������. ��� �������� ��������, ������� Y �� ������������� � �������
        private int _maxY_func2;
        private int _maxY_func3;

        public Functions()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _pictureBox1Graphics = pictureBox1.CreateGraphics(); // ������� ������� PictureBox
            _pictureBox2Graphics = pictureBox2.CreateGraphics();
            _pictureBox3Graphics = pictureBox3.CreateGraphics();
            _basePen = new Pen(Color.Black); // ������� ����� ��� ������ �����
            _func1Pen = new Pen(Color.Red);
            _func2Pen = new Pen(Color.Blue);
            _func3Pen = new Pen(Color.Violet);
            _calculatingStep = 0.1f; // ������ ���������� ����� � �������. ��� ������, ��� ����� ������ �� ���������
            _maxX_func1 = 10; 
            _maxX_func2 = 3; 
            _maxX_func3 = 2; 
            _maxY_func1 = 10;
            _maxY_func2 = 1; 
            _maxY_func3 = 2; 
            this.Text = "Drawing functions";
        }

        private async void Func1Btn_Click(object sender, EventArgs e) // ������� �� ������� ������ ��� ������ �������
        {
            pictureBox1.Refresh(); // ��������� �������
            var actualSize = pictureBox1.Size; // ����� ������
            DrawBase(_pictureBox1Graphics, actualSize); // ������� ������ ��� �������
            await Task.Delay(1000); // ��������� ��������
            DrawFirstFunction(_pictureBox1Graphics, actualSize); // ������ ��� ������
        }

        private async void Func2Btn_Click(object sender, EventArgs e) // ������� �� ������� ������ ��� ������ �������
        {
            pictureBox2.Refresh(); // ���� �����, ��� ����. ������ ������ PictureBox
            var actualSize = pictureBox2.Size;
            DrawBase(_pictureBox2Graphics, actualSize);
            await Task.Delay(1000);
            DrawSecondFunction(_pictureBox2Graphics, actualSize);
        }

        private async void Func3Btn_Click(object sender, EventArgs e) // ������� �� ������� ������ ��� ������ �������
        {
            pictureBox3.Refresh(); // ���� �����
            var actualSize = pictureBox3.Size;
            DrawBase(_pictureBox3Graphics, actualSize);
            await Task.Delay(1000);
            DrawThirdFunction(_pictureBox3Graphics, actualSize);
        }

        private void DrawFirstFunction(Graphics graphics, Size actualSize) // ������ �������
        {
            DrawFunction(graphics, actualSize, 1, _func1Pen, _maxX_func1, _maxY_func1);
        }

        private void DrawSecondFunction(Graphics graphics, Size actualSize) // ������ �������
        {
            DrawFunction(graphics, actualSize, 2, _func2Pen, _maxX_func2, _maxY_func2);
        }

        private void DrawThirdFunction(Graphics graphics, Size actualSize) // ������ �������
        {
            DrawFunction(graphics, actualSize, 3, _func3Pen, _maxX_func3, _maxY_func3);
        }

        private void DrawBase(Graphics graphics, Size actualSize) // ��������� ������� ������� (������������ ���������)
        {
            var startPoint = new Point(0, 0); // �������� ������. ����� ������� ����
            var drawSize = new Size(actualSize.Width - 1, actualSize.Height - 1); // ������ ������. ����� ������ �� 1 �� �������, ����� ������ ������������ ����
            graphics.DrawRectangle(_basePen, new Rectangle(startPoint, drawSize)); // ������ �������������
            var lineStartPoint = new Point(0, actualSize.Height / 2); // ������� ����� �� ��������
            var lineEndPoint = new Point(actualSize.Width, actualSize.Height / 2);
            graphics.DrawLine(_basePen, lineStartPoint, lineEndPoint); // ������ ��
        }

        private void DrawFunction(Graphics graphics, Size actualSize, int functionId, Pen pen, int maxX, int maxY)
        {
            float width = actualSize.Width;
            float height = actualSize.Height;
            float halfHeight = actualSize.Height / 2;
            List<PointF> points = new();

            for (float i = 0f; i < maxX; i += _calculatingStep)
            {
                try // ������� � try...catch ��� ���������� ��������� �������� ��� ������� �� ��������
                {
                    var calc_point_X = i;
                    var calc_point_Y = CalculateYPoint(functionId, calc_point_X); // ������ Y �� �������

                    var x_graph_position = calc_point_X * width / maxX; // ������������ ����� �� ������������ �������� � ����������� � �������
                    var y_graph_position = (-calc_point_Y * height / maxY) + halfHeight; // ������ �� -1, �.�. ������� ���� ������ ����

                    if (float.IsNaN(y_graph_position)) // ������� 0 / 0 ���� NaN, ����������� ����� ������
                    {
                        continue;
                    }

                    if (y_graph_position < 0f) // ���� ����� ��� ������ �������, �� ������� �� ��� 0
                    {
                        y_graph_position = 0f;
                    }

                    if (y_graph_position > actualSize.Height) // ���� ����� ��� ������ �������, �� ������� �� ��� ������
                    {
                        y_graph_position = actualSize.Height;
                    }

                    points.Add(new PointF(x_graph_position, y_graph_position));
                }
                catch (Exception) // ����� ������
                {
                    Debug.WriteLine("�������� ������. ����� �� ����� ��������� ��� �������."); // ��� ����� ���� ������� �� 0 � ������������������ ��������
                }
            }

            graphics.DrawCurve(pen, points.ToArray()); // ������ ������ ������� �� �����������
        }

        private float CalculateYPoint(int functionId, float x)
        {
            switch (functionId)
            {
                case 1:
                    return MathF.Pow(x, 3f) * (MathF.Cos(x) / MathF.Sin(x)); // ������ Y �� �������
                case 2:
                    {
                        var step1_result = MathF.Pow(x + 1f, 1f / 3f) / (MathF.Pow(x, 2f) + 1f);
                        var step2_result = MathF.Pow(MathF.Pow(x, 2f) - 2f * x, 1f / 3f) / (MathF.Pow(x, 2f) - 1f);
                        return step1_result - step2_result; // ������ ������ �������
                    }

                case 3:
                    {
                        var step1_result = MathF.Pow(x, 3f / 2f) * x / 2f;
                        var step2_result = MathF.Pow(x, 2f) / 2f * MathF.Acos(x);
                        return step1_result + step2_result; // ������ ������� �������
                    }

                default:
                    return 0;
            }
        }
    }
}
