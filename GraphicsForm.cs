using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CoursWork_Etap1.Services;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities;
using CoursWork_Etap1.Utilities.Interfaces;
using CoursWork_Etap1.Constants;


namespace CoursWork_Etap1
{
    public partial class GraphicsForm : Form
    {
        private readonly ISelectShapeService _selectShapeService;
        private readonly ISerializeShapeService _serializeShapeService;
        private readonly IDeserializeService _deserializeService;
        private readonly ICalculateAreaService _calculateAreaService;
        List<Shape> _shapes = new List<Shape>();
       
        private Stack<IAction> _redoStack = new Stack<IAction>();
        private Stack<IAction> _actions = new Stack<IAction>();

        private ColorDialog colorDialog = new ColorDialog();
        private Pen pen = new Pen(Color.Black, 0.4f);
        SolidBrush brush = new SolidBrush(Color.Transparent);

        private Shape _selectedShape;
        private Shape _copiedShape;
        private Vector2 _firstPoint;
        private Vector2 _secondPoint;
        private Vector2 _currentPosition;
        private Vector2 _pastePosition;
        private Vector2 _startPoint;
  
        private int ClickNum = 1;

        private bool active_drawing = false;
        private string DrawCase = "None";
        private Color _defultBorderColor = Color.Black;
        private Color _defultFillColor = Color.Transparent;

        public GraphicsForm()
        {
            this._selectShapeService = new SelectShapeService();
            this._serializeShapeService = new SerializeShapeService();
            this._deserializeService = new DeserializeService();
            this._calculateAreaService = new CalculateAreaService();

            InitializeComponent();

        }

        public PictureBox GetPictureBox()
        {
            return this.PictureBox;
        }

        //намиране на разделителната способност на екрана (DPI-dots per inch)
        private float DPI
        {
            get
            {
                using (var g = CreateGraphics())
                    return g.DpiX;
            }
        }

        //метод за конвертиране на пикселите в милиметри
        private float PixelToMm(float pixel)
        {
            return pixel * 25.4f / DPI;
        }

        //преобразуваме точка от екранните координати в координати на декартова система
        private Vector2 PointToCartesian(Point point)
        {
            return new Vector2(PixelToMm(point.X), PixelToMm(PictureBox.Height - point.Y));
        }   


        //-ИЗБОР НА ЦВЯТ НА КОНТУРА И ЗАПЪЛВАНЕ
        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog.Color;
            }
        }

        private void colorFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                brush.Color = colorDialog.Color;
            }
        }

        //ОТЧИТАНЕ НА ДВИЖЕНИЕТО НА МИШКАТА И СВЪРЗАНИТЕ С НЕГО ДЕЙСТВИЯ
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //-координати на курсора върху PictureBox-а
            _currentPosition = PointToCartesian(e.Location);
            label1.Text = String.Format("X= {0,0:F3} mm,  X= {1} pxl", _currentPosition.X, e.Location.X);
            label2.Text = String.Format("Y= {0,0:F3} mm,  Y= {1} pxl", _currentPosition.Y, e.Location.Y);

            //-местене на фигури
            if (DrawCase == "Move")
            {
                Vector2 d = _currentPosition - _startPoint;

                foreach (var shape in _shapes)
                {
                    if (shape.IsSelected)
                    {
                        IAction action = new MoveShapeAction(new List<Shape> { shape }, d);
                        action.Execute();
                        _actions.Push(action);
                    }
                }

                _startPoint = PointToCartesian(e.Location);
                Refresh();
            }          

            PictureBox.Refresh();
        }

        //-изтриване на селектирани фигури
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IAction action = new DeleteShapeAction(_shapes);
            action.Execute();      
            _actions.Push(action);

            PictureBox.Refresh();
        }
        private void deleteSelectedShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IAction action = new DeleteShapeAction(_shapes);
            action.Execute();
            _actions.Push(action);

            PictureBox.Refresh();
        }


        //-изтриване на всички фигури
        private void Clear_Click(object sender, EventArgs e)
        {
            IAction action = new ClearAllShapesAction(_shapes);
            action.Execute();
            _actions.Push(action);
            PictureBox.Refresh();
            AreaLabel.Text = "";
        }
 
        private void CalculateAreaButton_Click(object sender, EventArgs e)
        {
            DrawCase = "Area"; // Влизане в режим на изчисляване на площта на избраната фигура

            foreach (var shape in _shapes)
            {
                if (shape.IsSelected)
                {
                    AreaLabel.Text = $"Area: {shape.Area:F3} mm \n Type: {shape.Type} ";
                    shape.IsSelected = false; // Премахване на избора на фигурата
                    break;
                }
            }

            if (AreaLabel.Text == "")
            {
                AreaLabel.Text = "No shape selected";
            }
        }

        private void Circle_Click(object sender, EventArgs e)
        {
            DrawCase = "Circle";  //enter circle drawing mode
            active_drawing = true;
        }

        private void Square_Click(object sender, EventArgs e)
        {
            DrawCase = "Square";  //enter square drawing mode
            active_drawing = true;
        }

        private void Rectangle_Click(object sender, EventArgs e)
        {
            DrawCase = "Rectangle";  //enter rectangle drawing mode
            active_drawing = true;
        }

        private void TriangleButton_Click(object sender, EventArgs e)
        {
            DrawCase = "Triangle";  //enter equilateral triangle drawing mode
            active_drawing = true;
        }

        //ДЕЙСТВИЯ, ИЗОБРАЗЯВАЩИ СЕ ВЪРХУ PICTURE BOX-A
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(PixelToMm(PictureBox.Height));

            Pen drawpen = new Pen(_defultBorderColor, 0.3f);
            drawpen.DashPattern = new float[] { 1, 1 };
            SolidBrush drawbrush = new SolidBrush(_defultFillColor);


            //drawing all the shapes, that are in the list of shapes
            foreach (var shape in _shapes)
            {
                Pen pen = new Pen(shape.ColorBorder, 0.4f);
                SolidBrush brush = new SolidBrush(shape.FillColor);

                shape.DrawShape(e.Graphics, pen, brush);
            }
         
            //ИЗОБРАЗЯВАНЕ НА КОНТУРА НА ЧЕРТАЕНЕ
            switch (DrawCase)
            {
                case "Circle": //circle draw border
                    if (ClickNum == 2)
                    {
                        double r = _firstPoint.DistanceFrom(_currentPosition);
                        Shapes.Circle circle = new Shapes.Circle(_firstPoint.X, _firstPoint.Y, r, _defultBorderColor, _defultFillColor);
                        circle.DrawShape(e.Graphics, drawpen, drawbrush);
                    }
                    break;

                case "Square": //suare draw border
                    if (ClickNum == 2)
                    {
                        double sideLength = Math.Abs(_firstPoint.X - _currentPosition.X);

                        double startX = Math.Min(_firstPoint.X, _currentPosition.X);

                        if (_firstPoint.Y > _currentPosition.Y)
                        {
                            Shapes.Square squareDown = new Shapes.Square(startX, _firstPoint.Y - sideLength, sideLength, _defultBorderColor, _defultFillColor); //drawing from down
                            squareDown.DrawShape(e.Graphics, drawpen, drawbrush);
                        }

                        else if (_firstPoint.Y < _currentPosition.Y)
                        {

                            Shapes.Square squareUp = new Shapes.Square(startX, _firstPoint.Y, sideLength, _defultBorderColor, Color.Transparent); //drawing from up
                            squareUp.DrawShape(e.Graphics, drawpen, drawbrush);
                            break;

                        }
                    }
                    break;

                case "Rectangle": //rectangle draw porder
                    if (ClickNum == 2)
                    {
                        double length = Math.Abs(_firstPoint.X - _currentPosition.X);
                        double height = Math.Abs(_firstPoint.Y - _currentPosition.Y);

                        double startX = Math.Min(_firstPoint.X, _currentPosition.X);
                        double startY = Math.Min(_firstPoint.Y, _currentPosition.Y);

                        if (_firstPoint.Y > _currentPosition.Y)
                        {
                            Shapes.Rectangle rectangleDown = new Shapes.Rectangle(startX, startY, length, height, _defultBorderColor, _defultFillColor); //down
                            rectangleDown.DrawShape(e.Graphics, drawpen, drawbrush);
                        }

                        else if (_firstPoint.Y < _currentPosition.Y)
                        {
                            Shapes.Rectangle rectangleUp = new Shapes.Rectangle(startX, startY, length, height, _defultBorderColor, _defultFillColor); //up
                            rectangleUp.DrawShape(e.Graphics, drawpen, drawbrush);
                        }
                    }
                    break;

                case "Triangle": //equilateral triangle draw border
                    if (ClickNum == 2)
                    {
                        double side = _firstPoint.DistanceFrom(_currentPosition);
                        Shapes.EquilateralTriangle triangle = new Shapes.EquilateralTriangle(_firstPoint.X, _firstPoint.Y, side, _defultBorderColor, _defultFillColor);
                        triangle.DrawShape(e.Graphics, drawpen, drawbrush);
                    }
                    break;
            }
        }

        //ДЕЙСТВИЯ, АКТИВИРАЩИ СЕ ПРИ НАТИСНАТА МИШКА
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //-десен бутон на мишката
            if (e.Button == MouseButtons.Right)
            {
                // "Copy" and "Paste" menu items are visible only if one shape is selected
                copyToolStripMenuItem.Visible = (_shapes.Count(shape => shape.IsSelected) == 1);
                pasteToolStripMenuItem.Visible = (_copiedShape != null);

                changeFillColorToolStripMenuItem.Visible = (_shapes.Count(shape => shape.IsSelected) == 1);
                changeBorderColorToolStripMenuItem.Visible = (_shapes.Count(shape => shape.IsSelected) == 1);

                // If no shapes are selected, "Move" and "Delete" menu items are not visible
                moveToolStripMenuItem.Visible = (_shapes.Any(shape => shape.IsSelected));
                deleteToolStripMenuItem.Visible = (_shapes.Any(shape => shape.IsSelected));

                if (_selectedShape != null && DrawCase == "Move" || _copiedShape != null)
                {
                    _pastePosition = PointToCartesian(e.Location);
                }
            }
          
            _currentPosition = PointToCartesian(e.Location);

            //-ляв бутон на мишката
            if (e.Button == MouseButtons.Left)
            {
                if (active_drawing)
                {
                    switch (DrawCase)
                    {
                        case "Circle": //circle
                            switch (ClickNum)
                            {
                                case 1: //-първо щракване
                                    _firstPoint = _currentPosition;
                                    ClickNum++;
                                    break;
                                case 2: //-второ щракване
                                    double r = _firstPoint.DistanceFrom(_currentPosition);
                                    //-създава се нов обект от тип Circle
                                    Circle circle = new Circle(_firstPoint.X, _firstPoint.Y, r, pen.Color, brush.Color);
                                    //-създава се команда action за  добавяне на обекта към списъка с фигурите
                                    IAction action = new AddShapeAction(circle,_shapes);
                                    action.Execute();
                                    _actions.Push(action);
                                    ClickNum = 1;
                                    break;
                            }
                            break;


                        case "Square": //square
                            switch (ClickNum)
                            {
                                case 1:
                                    _firstPoint = _currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    double side = Math.Abs(_firstPoint.X - _currentPosition.X);
                                    _secondPoint = _currentPosition;
                                    if (_firstPoint.X > _secondPoint.X && _firstPoint.Y > _secondPoint.Y) //top-right to bottom-left
                                    {
                                        Square square = new Square(_firstPoint.X - side, _firstPoint.Y - side, side, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(square, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    else if (_firstPoint.X < _secondPoint.X && _firstPoint.Y < _secondPoint.Y) //bottom-left to top-right
                                    {
                                        Square square = new Square(_firstPoint.X, _firstPoint.Y, side, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(square, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    else if (_firstPoint.X < _secondPoint.X && _firstPoint.Y > _secondPoint.Y) //top-left to bottom-right
                                    {
                                        Square square = new Square(_firstPoint.X, _firstPoint.Y - side, side, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(square, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    else if (_firstPoint.X > _secondPoint.X && _firstPoint.Y < _secondPoint.Y) //bottom-right to top-left
                                    {
                                        Square square = new Square(_firstPoint.X - side, _firstPoint.Y, side, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(square, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    ClickNum = 1;
                                    break;

                            }
                            break;

                        case "Rectangle": //rectangle
                            switch (ClickNum)
                            {
                                case 1:
                                    _firstPoint = _currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    double length = Math.Abs(_firstPoint.X - _currentPosition.X);
                                    double height = Math.Abs(_firstPoint.Y - _currentPosition.Y);
                                    _secondPoint = _currentPosition;
                                    if (_firstPoint.X > _secondPoint.X && _firstPoint.Y > _secondPoint.Y) //top-right to bottom-left
                                    {
                                        Shapes.Rectangle rectangle = new Shapes.Rectangle(_firstPoint.X - length, _firstPoint.Y - height, length, height, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(rectangle, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    else if (_firstPoint.X < _secondPoint.X && _firstPoint.Y < _secondPoint.Y) //bottom-left to top-right
                                    {
                                        Shapes.Rectangle rectangle = new Shapes.Rectangle(_firstPoint.X, _firstPoint.Y, length, height, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(rectangle, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    else if (_firstPoint.X < _secondPoint.X && _firstPoint.Y > _secondPoint.Y) //top-left to bottom-right
                                    {
                                        Shapes.Rectangle rectangle = new Shapes.Rectangle(_firstPoint.X, _firstPoint.Y - height, length, height, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(rectangle, _shapes);
                                        action.Execute();
                                        _actions.Push(action);
                                    }
                                    else if (_firstPoint.X > _secondPoint.X && _firstPoint.Y < _secondPoint.Y) //bottom-right to top-left
                                    {
                                        Shapes.Rectangle rectangle = new Shapes.Rectangle(_firstPoint.X - length, _firstPoint.Y, length, height, pen.Color, brush.Color);
                                        IAction action = new AddShapeAction(rectangle, _shapes);
                                        action.Execute();
                                        _actions.Push(action);

                                    }
                                    ClickNum = 1;
                                    break;

                            }
                            break;

                        case "Triangle": //equilateral triangle
                            switch (ClickNum)
                            {
                                case 1:
                                    _firstPoint = _currentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    double side = _firstPoint.DistanceFrom(_currentPosition);
                                    EquilateralTriangle triangle = new EquilateralTriangle(_firstPoint.X, _firstPoint.Y, side, pen.Color, brush.Color);
                                    IAction action = new AddShapeAction(triangle, _shapes);
                                    action.Execute();
                                    _actions.Push(action);
                                    ClickNum = 1;
                                    break;
                            }
                            break;

                    }
                    PictureBox.Refresh();
                }
            }
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "Move";
            PictureBox.Focus();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (DrawCase == "Move")
            {
                DrawCase = "None";
 
                _shapes
                    .Where(shape => shape.IsSelected)
                    .ToList()
                    .ForEach(shape => shape.IsSelected = false);

                PictureBox.Refresh();
            }
        }

        private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //-принцип на селектиране на фигурите
            _startPoint = PointToCartesian(e.Location);
            DrawCase = "Selecting";
            foreach (var shape in _shapes)
            {
                if (shape.ContainsPoint(Vector2.ToPointF(PointToCartesian(e.Location))))
                {
                    //selecting the current shape
                    _selectedShape = shape;
                    _selectedShape.IsSelected = true;
                    PictureBox.Refresh();
                    _firstPoint = _selectedShape.Location;
                    break;
                }
            }           
        }

        //КОПИРАНЕ И ПОСТАВЯНЕ НА ФИГУРА
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {          
                _copiedShape = _selectedShape;
                PictureBox.Refresh();          
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_copiedShape != null)
            {
                IAction action = new CopyPasteShapeAction(_copiedShape, _shapes, _pastePosition);
                action.Execute();
                _actions.Push(action);
                PictureBox.Refresh();

                //деселектиране
                foreach (var shape in _shapes)
                {
                    if (shape.IsSelected)
                    {
                        shape.IsSelected = false;
                        PictureBox.Refresh();
                    }
                }
            }
        }

        //UNDO И REDO БУТОНИ
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_redoStack.Count > 0)
            {
                IAction action = _redoStack.Pop();
                action.Execute();
                _actions.Push(action);
                Refresh();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_actions.Count > 0)
            {
                IAction action = _actions.Pop();
                action.Undo();
                _redoStack.Push(action);
                Refresh();
            }
        }

        //ОПЦИИ ЗА СЕЛЕКТИРАНЕ
        private void selectAllShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectShapeService.SelectAllShapes(_shapes);
            PictureBox.Refresh();
        }
        private void allCirclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectShapeService.SelectAllShapesByType(_shapes, typeof(Circle));
            PictureBox.Refresh();
        }
        private void allSquaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectShapeService.SelectAllShapesByType(_shapes, typeof(Square));
            PictureBox.Refresh();
        }
        private void allRectanglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectShapeService.SelectAllShapesByType(_shapes, typeof(Shapes.Rectangle));
            PictureBox.Refresh();
        }
        private void allEquilaeralTrianglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectShapeService.SelectAllShapesByType(_shapes, typeof(EquilateralTriangle));
            PictureBox.Refresh();
        }

        private void GraphicsForm_Load(object sender, EventArgs e)
        {
          _shapes = _deserializeService.DeserializeSave(FileLocation.FileLocationBinary);

        }

        //АВТОМАТИЧНА СЕРИАЛИЗАЦИЯ ПРИ ПРИ ЗАТВАРЯН НА ПРОГРАМАТА
        private void GraphicsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serializeShapeService.SerializeSave(_shapes, FileLocation.FileLocationBinary);
          
        }

        //ИЗЛИЗАНЕ ОТ ПРОГРАМАТА
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //ОТВАРЯНЕ/ЗАРЖДАНЕ НА ФИГУРИ ОТ ФАЙЛ
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                //setting the initial directory and filter for the file dialog
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "TXT files (*.txt)|*.txt";
                openFileDialog.FileName = FileLocation.FileLocationBinary;
                openFileDialog.InitialDirectory = FileLocation.FilesFolderPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //reading the selected file and displaying its contents on the PictureBox
                    var fileContent = System.IO.File.ReadAllText(openFileDialog.FileName);

                    //deserializing the shapes from the file
                    var shapesToAdd = _deserializeService.DeserializeSave(openFileDialog.FileName);

                    //adding the shapes to the already existing list of shapes
                    _shapes.AddRange(shapesToAdd);
                    PictureBox.Refresh();
                    MessageBox.Show($"{openFileDialog.FileName} contents:\n{fileContent}", "File opened");

                }
            }
        }

        //ЕКСПОРТИРАНЕ И ЗАПАЗВАНЕ НА ФИГУРИТЕ
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportForm exportForm = new ExportForm(_shapes);
            do
            {
                exportForm.ShowDialog();
            } 
            while (exportForm.DialogResult == DialogResult.Retry);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TXT files (*.txt)|*.txt";
            saveFileDialog.FileName = FileLocation.FileLocationBinary;
            saveFileDialog.InitialDirectory = FileLocation.FilesFolderPath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
               _serializeShapeService.SerializeSave(_shapes, saveFileDialog.FileName);
            }
        }

        //ФУНКЦИИ ЗА ПРЕСМЯТАНЕ НА ЛИЦЕТО
        private void totalAreaOfAllShapesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double areaOfAllShapes = _calculateAreaService.TotalAreaOfAllShapes(_shapes);
            AreaForm areaForm = new AreaForm("Total Area of All Shapes", typeof(Shape), areaOfAllShapes);            
            
            areaForm.ShowDialog();           
        }

        private void circleToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            double areaOfAllCircles = _calculateAreaService.TotalAreaOfAllShapesFromType(_shapes, typeof(Circle));
            AreaForm areaForm = new AreaForm("Total Area of All Circles", typeof(Circle), areaOfAllCircles);

            areaForm.ShowDialog();
        }

        private void equilateralTriangleToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            double areaOfAllEquilateralTriangle = _calculateAreaService.TotalAreaOfAllShapesFromType(_shapes, typeof(EquilateralTriangle));
            AreaForm areaForm = new AreaForm("Total Area of All Equilateral Triangles", typeof(EquilateralTriangle), areaOfAllEquilateralTriangle);

            areaForm.ShowDialog();
        }

        private void reactangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double areaOfAllRectangles = _calculateAreaService.TotalAreaOfAllShapesFromType(_shapes, typeof(Shapes.Rectangle));
            AreaForm areaForm = new AreaForm("Total Area of All Rectangles", typeof(Shapes.Rectangle), areaOfAllRectangles);

            areaForm.ShowDialog();
        }

        private void squareToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            double areaOfAllSquares = _calculateAreaService.TotalAreaOfAllShapesFromType(_shapes, typeof(Square));
            AreaForm areaForm = new AreaForm("Total Area of All Squares", typeof(Square), areaOfAllSquares);

            areaForm.ShowDialog();
        }

        private void biggestAreaOfAllShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double biggestAreaOfAllShapes = _calculateAreaService.BiggestAreaOfAllShapes(_shapes);
            Type biggestShapeType = _shapes.Single(s => s.Area == biggestAreaOfAllShapes)?.GetType();
            AreaForm areaForm = new AreaForm("Biggest Area of All Shapes", biggestShapeType, biggestAreaOfAllShapes);

            areaForm.ShowDialog();
        }

        private void smallestAreaOfAllShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double smallestAreaOfAllShapes = _calculateAreaService.SmallestAreaOfAllShapes(_shapes);
            Type smallestShapeType = _shapes.Single(s => s.Area == smallestAreaOfAllShapes)?.GetType();
            AreaForm areaForm = new AreaForm("Smallest Area of All Shapes", smallestShapeType, smallestAreaOfAllShapes);

            areaForm.ShowDialog();
           
        }

        private void circleToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            double biggestAreaFromTypeCircle = _calculateAreaService.BiggestAreaFromType(_shapes, typeof(Circle));           
            AreaForm areaForm = new AreaForm("Biggest Area from Type Circle", typeof(Circle), biggestAreaFromTypeCircle);

            areaForm.ShowDialog();
        }

        private void equilateralTriangleToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            double biggestAreaFromTypeTriangle = _calculateAreaService.BiggestAreaFromType(_shapes, typeof(EquilateralTriangle));
            AreaForm areaForm = new AreaForm("Biggest Area from Type Equilateral Triangle", typeof(EquilateralTriangle), biggestAreaFromTypeTriangle);

            areaForm.ShowDialog();
        }

        private void rectangleToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            double biggestAreaFromTypeReactangle = _calculateAreaService.BiggestAreaFromType(_shapes, typeof(Shapes.Rectangle));
            AreaForm areaForm = new AreaForm("Biggest Area from Type Rectangles", typeof(Shapes.Rectangle), biggestAreaFromTypeReactangle);

            areaForm.ShowDialog();
        }

        private void squareToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            double biggestAreaFromTypeSquare = _calculateAreaService.BiggestAreaFromType(_shapes, typeof(Square));
            AreaForm areaForm = new AreaForm("Biggest Area from Type Squares", typeof(Square), biggestAreaFromTypeSquare);

            areaForm.ShowDialog();
        }

        private void circleToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            double smallestAreaFromTypeCircle = _calculateAreaService.SmallestAreaFromType(_shapes, typeof(Circle));
            AreaForm areaForm = new AreaForm("Smallest Area from Type Circle", typeof(Circle), smallestAreaFromTypeCircle);

            areaForm.ShowDialog();
        }

        private void equilateralTriangleToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            double smallestAreaFromTypeEquilateralTriangle = _calculateAreaService.SmallestAreaFromType(_shapes, typeof(EquilateralTriangle));
            AreaForm areaForm = new AreaForm("Smallest Area from Type Equilateral Triangle", typeof(EquilateralTriangle), smallestAreaFromTypeEquilateralTriangle);

            areaForm.ShowDialog();
        }

        private void rectangleToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            double smallestAreaFromTypeRectangle = _calculateAreaService.SmallestAreaFromType(_shapes, typeof(Shapes.Rectangle));
            AreaForm areaForm = new AreaForm("Smallest Area from Type Rectangle", typeof(Shapes.Rectangle), smallestAreaFromTypeRectangle);

            areaForm.ShowDialog();
        }

        private void squareToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            double smallestAreaFromTypeSquare = _calculateAreaService.SmallestAreaFromType(_shapes, typeof(Square));
            AreaForm areaForm = new AreaForm("Smallest Area from Type Square", typeof(Square), smallestAreaFromTypeSquare);

            areaForm.ShowDialog();
        }

        //ПРОМЯНА НА ЦВЕТА НА ЗАПЪЛВАНЕ И КОНТУР
        private void changeFillColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
             using (var colorDialog = new ColorDialog())
             {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var shape in _shapes)
                    {

                        if (shape.IsSelected)
                        {
                            shape.FillColor = colorDialog.Color;
                            shape.IsSelected = false;
                            PictureBox.Refresh();
                        }

                    }
                }
                
             }
        }

        private void changeBorderColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var shape in _shapes)
                    {

                        if (shape.IsSelected)
                        {
                            shape.ColorBorder = colorDialog.Color;
                            shape.IsSelected = false;
                            PictureBox.Refresh();
                        }

                    }
                }

            }
        }
    }
}

