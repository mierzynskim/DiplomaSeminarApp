using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.ViewModels;

namespace DiplomaSeminar.Droid.Views
{
    [Activity(Label = "New presentation", Icon = "@drawable/ic_launcher")]
    public class AddActivity : Activity
    {
        private AddViewModel viewModel;
        private EditText lastname, name, subject;
        private DatePicker date;
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.view_add);

            var id = Intent.GetIntExtra("ID", -1);
            viewModel = ServiceContainer.Resolve<AddViewModel>();
            await viewModel.Init(id);


            name = FindViewById<EditText>(Resource.Id.name);
            date = FindViewById<DatePicker>(Resource.Id.date);
            lastname = FindViewById<EditText>(Resource.Id.lastName);
            subject = FindViewById<EditText>(Resource.Id.subject);

            name.Text = viewModel.SpeakerName;
            lastname.Text = viewModel.SpeakerLastName;
            date.DateTime = viewModel.Date;
            subject.Text = viewModel.Subject;

        }

        protected override void OnStart()
        {
            base.OnStart();
            DiplomaSeminarApp.CurrentActivity = this;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_add, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case (Resource.Id.menu_save_presentation):
                    viewModel.SpeakerName = name.Text;
                    viewModel.SpeakerLastName = lastname.Text;
                    viewModel.Date = date.DateTime;
                    viewModel.Subject = subject.Text;
                    Task.Run(async () =>
                    {
                        await viewModel.ExecuteSavePresentationCommand();

                        if (!viewModel.CanNavigate)
                            return;

                        Finish();
                    });
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}