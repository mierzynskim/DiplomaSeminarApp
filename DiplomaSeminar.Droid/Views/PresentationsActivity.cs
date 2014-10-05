using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.ViewModels;
using DiplomaSeminar.Droid.Adapters;

namespace DiplomaSeminar.Droid.Views
{
    [Activity(Label = "Diploma Seminar", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class PresentationsActivity : ListActivity
    {
        private PresentationsViewModel viewModel;
        private ProgressBar progressBar;
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.view_presentations);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            viewModel = ServiceContainer.Resolve<PresentationsViewModel>();
            viewModel.IsBusyChanged = (busy) =>
            {
                progressBar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
            };

            ListAdapter = new PresentationAdapter(this, viewModel);

            //ListView.ItemLongClick += async (sender, args) =>
            //{
            //    await viewModel.ExecuteDeleteExpenseCommand(viewModel.Expenses[args.Position]);
            //    RunOnUiThread(() => ((ExpenseAdapter)ListAdapter).NotifyDataSetChanged());
            //};

            if (!viewModel.IsSynced)
            {
                await viewModel.ExecuteSyncExpensesCommand();
                RunOnUiThread(() => ((PresentationAdapter)ListAdapter).NotifyDataSetChanged());
            }

        }

        protected async override void OnStart()
        {
            base.OnStart();

            DiplomaSeminarApp.CurrentActivity = this;



            if (viewModel.NeedsUpdate && viewModel.IsSynced)
            {
                await viewModel.ExecuteLoadPresentationsCommand();
                RunOnUiThread(() => ((PresentationAdapter)ListAdapter).NotifyDataSetChanged());
            }
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            var intent = new Intent(this, typeof(AddActivity));
            intent.PutExtra("ID", (int)id);
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_presentations, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case (Resource.Id.menu_new_presentation):
                    var intent = new Intent(this, typeof(AddActivity));
                    StartActivity(intent);
                    return true;
                case Resource.Id.menu_refresh:
                    Sync();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private async Task Sync()
        {
            await viewModel.ExecuteSyncExpensesCommand();
            RunOnUiThread(() => ((PresentationAdapter)ListAdapter).NotifyDataSetChanged());
        }
    }
}