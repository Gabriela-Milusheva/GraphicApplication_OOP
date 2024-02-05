using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoursWork_Etap1.Services;
using CoursWork_Etap1.Shapes;

namespace CoursWork_Etap1
{
    public partial class AreaForm : Form
    {
        private string _function;
        private Type _type;
        private double _area;

        public AreaForm(string function, Type type, double area)
        {
            InitializeComponent();

            _function = function;
            _type = type;
            _area = area;

            functionLabel.Text = _function;
            typeLabel.Text = _type.Name; 
            areaLabel.Text = _area.ToString("F3") + " mm";

        }

    }
}
