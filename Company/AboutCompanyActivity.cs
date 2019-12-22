using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Company
{
    [Activity(Label = "AboutCompanyActivity")]
    public class AboutCompanyActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_about_company);

            // Create your application here

            Company company = GetIntentData();

            FillFieldsCompanyActivity(company);

            var websiteButton = FindViewById<Button>(Resource.Id.WebsiteButton);
            var contactsButton = FindViewById<Button>(Resource.Id.ContactsButton);
            var socialButton = FindViewById<Button>(Resource.Id.SocialButton);

            //var switcher = FindViewById<Switch>(Resource.Id.Switcher);

            websiteButton.Click += GoToWebsite_Click;
            contactsButton.Click += GoToWebsite_Click;
            socialButton.Click += GoToWebsite_Click;
        }

        private void GoToWebsite_Click(object sender, EventArgs e)
        {
            string url = Intent.GetStringExtra("web");

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;

            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            StartActivity(browserIntent);
        }

        private void FillFieldsCompanyActivity(Company company)
        {
            var versionEditText = FindViewById<EditText>(Resource.Id.VersionEditText);
            var descEditText = FindViewById<EditText>(Resource.Id.DescriptionEditText);

            versionEditText.Text = company.Version;
            descEditText.Text = company.Description;
        }

        private Company GetIntentData()
        {
            var company = new Company
            {
                Description = Intent.GetStringExtra("desc"),
                Version = Intent.GetStringExtra("vers"),
                Website = Intent.GetStringExtra("web"),
            };

            return company;
        }
    }
}