using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CWLOTPH.DataObject;


namespace CWLOTPH.Visuality
{
    class InputHandler
    {
        public static InputDataObject CheckInput(UserWindow uWindow)
        {
            var inpDObj = new InputDataObject();
            var checkPassed = true;

            // Проверка геометрических параметров
            var geomVector = new GeometryVector();

            geomVector.L = CheckTextBox(uWindow.lengthTextBox);
            if (geomVector.L == 0) checkPassed = false;
            geomVector.W = CheckTextBox(uWindow.widthTextBox);
            if (geomVector.W == 0) checkPassed = false;
            geomVector.H = CheckTextBox(uWindow.heightTextBox);
            if (geomVector.H == 0) checkPassed = false;

            // Добавление во входной объект
            inpDObj.GeometryVector = geomVector;

            // Проверка режимных параметров
            var modeVector = new ModeVector();
            modeVector.Tu = CheckTextBox(uWindow.temperatureTextBox);
            if (modeVector.Tu == 0) checkPassed = false;
            modeVector.Vu = CheckTextBox(uWindow.velocityTextBox);
            if (modeVector.Vu == 0) checkPassed = false;

            // Добавление во входной объект
            inpDObj.ModeVector = modeVector;

            inpDObj.DiscretizationStep = CheckTextBox(uWindow.stepTextBox);
            if (inpDObj.DiscretizationStep == 0) checkPassed = false;

            return !checkPassed ? null : inpDObj;
        }

        private static double CheckTextBox(TextBox textBox)  // Функция проверит значение, если вернется не 0, то все условия выполнены
        {
            double checkedString = 0;

            try
            {
                checkedString = Convert.ToDouble(textBox.Text);
                if (textBox.TextDecorations != null) textBox.TextDecorations.Clear();
            }
            catch (FormatException)
            {
                var myUnderline = new TextDecoration();

                // Create a linear gradient pen for the text decoration.
                var myPen = new Pen();
                myPen.Brush = new LinearGradientBrush(Colors.Red, Colors.Red, new Point(0, 0.5), new Point(1, 0.5));
                myPen.Brush.Opacity = 0.5;
                myPen.Thickness = 1.5;
                myPen.DashStyle = DashStyles.DashDot;
                myUnderline.Pen = myPen;
                myUnderline.PenThicknessUnit = TextDecorationUnit.FontRecommended;

                // Set the underline decoration to a TextDecorationCollection and add it to the text block.
                var myDecorationCollection = new TextDecorationCollection {myUnderline};
                textBox.TextDecorations = myDecorationCollection;
            }

            return checkedString;
        }
    }
}
