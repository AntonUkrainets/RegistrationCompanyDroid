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
            nextButton.Click += NextButton_Click;
        }

        private void ResetForm()
        {
            ClearFieldErrorState(Resource.Id.DescriptionTextView);
            ClearFieldErrorState(Resource.Id.VersionTextView);
            ClearFieldErrorState(Resource.Id.WebsiteTextView);
        }

        private void ClearFieldErrorState(int textViewId)
        {
            var textView = FindViewById<TextView>(textViewId);

            textView.Visibility = ViewStates.Invisible;
            textView.SetTextColor(Color.Transparent);
        }

        private void SetFieldErrorState(int textViewId)
        {
            var textView = FindViewById<TextView>(textViewId);

            textView.Visibility = ViewStates.Visible;
            textView.SetTextColor(Color.Red);
        }

        private bool ValidateForm()
        {
            var isValid = true;

            if (IsTextFieldEmpty(Resource.Id.DescriptionEditText))
            {
                isValid = false;
                SetFieldErrorState(Resource.Id.DescriptionTextView);
            }

            if (IsTextFieldEmpty(Resource.Id.VersionEditText))
            {
                isValid = false;
                SetFieldErrorState(Resource.Id.VersionTextView);
            }

            if (IsTextFieldEmpty(Resource.Id.WebsiteEditText))
            {
                isValid = false;
                SetFieldErrorState(Resource.Id.WebsiteTextView);
            }

            {
                var IsAgreedCheckBox = FindViewById<CheckBox>(Resource.Id.IsAgreedCheckBox);
                var agreedTextView = FindViewById<TextView>(Resource.Id.AgreedTextView);

                if (!IsAgreedCheckBox.Checked)
                {
                    agreedTextView.SetTextColor(Color.Red);
                    isValid = false;
                }
                else
                {
                    agreedTextView.SetTextColor(Color.Gray);
                }
            }

            return isValid;
        }

        private void NextButton_Click(object sender, System.EventArgs e)
        {
            ResetForm();

            var formIsValid = ValidateForm();

            if (!formIsValid)
                return;

            NavigateToCompanyActivity();
        }

        private void NavigateToCompanyActivity()
        {
            var descriptionEditText = FindViewById<EditText>(Resource.Id.DescriptionEditText);
            var versionEditText = FindViewById<EditText>(Resource.Id.VersionEditText);
            var websiteEditText = FindViewById<EditText>(Resource.Id.WebsiteEditText);

            Intent intent = new Intent(this, typeof(AboutCompanyActivity));

            intent.PutExtra("desc", descriptionEditText.Text);
            intent.PutExtra("version", versionEditText.Text);
            intent.PutExtra("web", websiteEditText.Text);

            StartActivity(intent);
        }

        private bool IsTextFieldEmpty(int editTextId)
        {
            var editText = FindViewById<EditText>(editTextId);

            return string.IsNullOrWhiteSpace(editText.Text);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}