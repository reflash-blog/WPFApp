using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CWLOTPH.Visuality;

namespace CWLOTPH
{
    public class GUI_Controller
    {
        public void Main(string GUI_name)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (GUI_name == "admin")
                Application.Run(new Admin_GUI());
            else
                if (GUI_name == "user")
                    Application.Run(new User_GUI());
                else
                {
                    MessageBox.Show("Ошибка в GUI_Controller. Неверный ввод параметра запуска формы.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
    }
}
