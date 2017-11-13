using SimpleAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAuthWinForm
{
    public partial class Form1 : Form, IAuthView, IAuthRouter
    {
        public Form1()
        {
            InitializeComponent();

            var request = new Request();
            var interactor = new AuthInteractor(request);

            var presenter = new AuthPresenter(interactor, this, this);
            btnLogin.Click += (sender, args) =>
            {
                presenter.Authorize(textBoxLogin.Text, textBoxPass.Text);
            };
        }

        public void GoToMainPage()
        {
            MainForm formNext = new MainForm();
            formNext.Show();
        }

        public void ShowMessage(EAuthResponse response)
        {
             MessageBox.Show(this, response.ToString());
        }
    }
}
