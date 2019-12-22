using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Company
{
    [Activity(Label = "AboutCompanyActivity")]
    public class AboutCompanyActivity : Activity
    {
        private TextView versionTextView;
        private TextView descTextView;

        private TextView websiteTextView;
        private TextView contactsTextView;
        private TextView socialTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_about_company);

            versionTextView = FindViewById<TextView>(Resource.Id.VersionTextView);
            descTextView = FindViewById<TextView>(Resource.Id.DescriptionTextView);

            websiteTextView = FindViewById<TextView>(Resource.Id.WebsiteTextView);
            contactsTextView = FindViewById<TextView>(Resource.Id.ContactsTextView);
            socialTextView = FindViewById<TextView>(Resource.Id.SocialTextView);

            FillFieldsCompanyActivity();
            FillWebsiteTextViews();

            var moveBackToolbar = FindViewById<Toolbar>(Resource.Id.MoveBackToolBar);
            moveBackToolbar.Click += MoveBackToolbar_Click;

            var textSwitcher = FindViewById<Switch>(Resource.Id.TextSwitcher);
            textSwitcher.Click += TextSwitcher_Click;

            websiteTextView.Click += MoveToWebsite_Click;
            contactsTextView.Click += MoveToWebsite_Click;
            socialTextView.Click += MoveToWebsite_Click;
        }

        private void MoveBackToolbar_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void MoveToWebsite_Click(object sender, EventArgs e)
        {
            string url = websiteTextView.Text;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;

            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            StartActivity(browserIntent);
        }

        private void TextSwitcher_Click(object sender, EventArgs e)
        {
            var switcher = sender as Switch;
            if(switcher.Checked)
            {
                versionTextView.Text = "Version";
                descTextView.Text = "Description";

                websiteTextView.Text = "Website";
                contactsTextView.Text = "Contacts";
                socialTextView.Text = "Social";
            }
            else
            {
                FillFieldsCompanyActivity();
                FillWebsiteTextViews();
            }
        }

        private void FillFieldsCompanyActivity()
        {
            descTextView.Text = Intent.GetStringExtra("desc");
            versionTextView.Text = Intent.GetStringExtra("version");
        }

        private void FillWebsiteTextViews()
        {
            var link = Intent.GetStringExtra("web");

            websiteTextView.Text = link;
            contactsTextView.Text = link;
            socialTextView.Text = link;
        }
    }
}