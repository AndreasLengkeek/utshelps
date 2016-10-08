using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using UTSHelps.Droid.Helpers;
using UTSHelps.Shared.Models;

namespace UTSHelps.Droid
{
    [Activity(Label = "UTSHelps", MainLauncher = true)]
    public class LoginActivity : MainActivity
    {
        protected override int LayoutResource
        {
            get { return Resource.Layout.Activity_Login; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // TODO:
            //if (CurrentUser != null)
            //{
            //    SwitchActivity();
            //}
        }

        [Java.Interop.Export()]
        public async void Login(View view)
        {
            var studentId = FindViewById<EditText>(Resource.Id.loginStudentId).Text;
            var password = FindViewById<EditText>(Resource.Id.loginPassword).Text;

            try
            {
                TestCredentials(studentId, password);
            } catch (Exception ex)
            {
                DialogHelper.ShowDialog(this, "An error occured", ex.Message);
                return;
            }

            var progress = DialogHelper.CreateProgressDialog("Signing In...", this);
            progress.Show();
            var response = (StudentResponse)await ServiceHelper.Student.GetStudent(studentId.ToInt());
            progress.Hide();
            LoginOrRegister(response.Student);
        }

        private void TestCredentials(string studentId, string password)
        {
            if (String.IsNullOrEmpty(studentId) || studentId.Length != 8)
            {
                throw new Exception("Student Id is invalid");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Password cannot be empty");
            }
        }

        private void LoginOrRegister(Student student)
        {
            if (student == null)
            {
                var intent = new Intent(this, typeof(RegisterActivity));
                this.StartActivity(intent);
            } else
            {
                // go to dashboard
                CurrentUser = student;

                var intent = new Intent(this, typeof(DashboardActivity));
                this.StartActivity(intent);
            }

        }
    }
}

