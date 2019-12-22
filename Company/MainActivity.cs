using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using Android.Content;

namespace Company
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var nextButton = FindViewById<Button>(Resource.Id.NextButton);
            nextButton.Click += NextButton_Click; ;
        }

        private void NextButton_Click(object sender, System.EventArgs e)
        {
            HaveEmptyFields(Resource.Id.DescriptionEditText, Resource.Id.DescriptionTextView);
            HaveEmptyFields(Resource.Id.VersionEditText, Resource.Id.VersionTextView);
            HaveEmptyFields(Resource.Id.WebsiteEditText, Resource.Id.WebsiteTextView);

            var descriptionEditText = FindViewById<EditText>(Resource.Id.DescriptionEditText);
            var versionEditText = FindViewById<EditText>(Resource.Id.VersionEditText);
            var websiteEditText = FindViewById<EditText>(Resource.Id.WebsiteEditText);

            var IsAgreedCheckBox = FindViewById<CheckBox>(Resource.Id.IsAgreedCheckBox);
            var agreedTextView = FindViewById<TextView>(Resource.Id.AgreedTextView);

            if (!IsAgreedCheckBox.Checked)
            {
                agreedTextView.SetTextColor(Color.Red);
                return;
            }
            else
            {
                agreedTextView.SetTextColor(Color.Transparent);
            }

            //var company = new Company
            //{
            //    Description = descriptionEditText.Text,
            //    Version = versionEditText.Text,
            //    Website = websiteEditText.Text
            //};

            Intent intent = new Intent(this, typeof(AboutCompanyActivity));

            intent.PutExtra("desc", descriptionEditText.Text);
            intent.PutExtra("vers", versionEditText.Text);
            intent.PutExtra("web", websiteEditText.Text);

            TransitionToCompanyActivity(intent);
        }

        private void TransitionToCompanyActivity(Intent intent)
        {
            StartActivity(intent);
        }

        private void HaveEmptyFields(int editText, int textView)
        {
            var value1 = FindViewById<EditText>(editText);
            var value2 = FindViewById<TextView>(textView);

            if (string.IsNullOrWhiteSpace(value1.Text))
            {
                value2.Visibility = ViewStates.Visible;
                value2.SetTextColor(Color.Red);
            }
            else
            {
                value2.Visibility = ViewStates.Invisible;
                value2.SetTextColor(Color.Transparent);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}