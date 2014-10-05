using Android.App;
using Android.Views;
using Android.Widget;
using DiplomaSeminar.Core.ViewModels;
using Presentation = DiplomaSeminar.Core.Model.Presentation;

namespace DiplomaSeminar.Droid.Adapters
{
    public class PresentationWrapper : Java.Lang.Object
    {
        public TextView SpeakerName { get; set; }
        public TextView SpeakerLastName { get; set; }
        public TextView Subject { get; set; }
        public TextView Date { get; set; }
    }
    public class PresentationAdapter : BaseAdapter<Presentation>
    {
        private PresentationsViewModel viewModel;
        private Activity context;
        public PresentationAdapter(Activity context, PresentationsViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.context = context;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            PresentationWrapper wrapper = null;
            var view = convertView;
            if (convertView == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.item_presentation, null);
                wrapper = new PresentationWrapper();
                wrapper.SpeakerName = view.FindViewById<TextView>(Resource.Id.name);
                wrapper.SpeakerLastName = view.FindViewById<TextView>(Resource.Id.lastName);
                wrapper.Date = view.FindViewById<TextView>(Resource.Id.date);
                wrapper.Subject = view.FindViewById<TextView>(Resource.Id.subject);
                view.Tag = wrapper;
            }
            else
            {
                wrapper = convertView.Tag as PresentationWrapper;
            }

            var expense = viewModel.Presentations[position];
            if (wrapper == null) return view;
            wrapper.SpeakerName.Text = expense.SpeakerName;
            wrapper.SpeakerLastName.Text = expense.SpeakerLastName;
            wrapper.Subject.Text = expense.Subject;
            wrapper.Date.Text = expense.Date.ToString();

            return view;
        }

        public override Presentation this[int position]
        {
            get
            {
                return viewModel.Presentations != null ? viewModel.Presentations[position] : null;
            }
        }

        public override int Count
        {
            get
            {
                return viewModel.Presentations != null ? viewModel.Presentations.Count : 0;
            }
        }

        public override long GetItemId(int position)
        {
            return viewModel.Presentations != null ? viewModel.Presentations[position].Id : 0;
        }
    }
}
