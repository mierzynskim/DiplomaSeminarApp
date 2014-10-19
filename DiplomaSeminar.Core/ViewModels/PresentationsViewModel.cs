using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.Interfaces;
using DiplomaSeminar.Core.Model;

namespace DiplomaSeminar.Core.ViewModels
{
    public class PresentationsViewModel: ViewModelBase
    {
        private readonly IPresentationService presentationService;

        public PresentationsViewModel()
        {
            presentationService = ServiceContainer.Resolve<IPresentationService>();
            presentations.Add(new Presentation {Date = DateTime.Now});
        }

        public bool NeedsUpdate { get; set; }

        private ObservableCollection<Presentation> presentations = new ObservableCollection<Presentation>();

        public ObservableCollection<Presentation> Presentations
        {
            get { return presentations; }
            set { presentations = value; OnPropertyChanged("Presentations"); }
        }

        private async Task UpdatePresentations()
        {
            Presentations.Clear();
            NeedsUpdate = false;
            try
            {
                var exps = await presentationService.GetPresentations();

                foreach (var expense in exps)
                    Presentations.Add(expense);

            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to query and gather presentations");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand syncPresentationsCommand;

        public ICommand SyncPresentationsCommand
        {
            get { return syncPresentationsCommand ?? (syncPresentationsCommand = new RelayCommand(async () => await ExecuteSyncPresentationsCommand())); }
        }

        public async Task ExecuteSyncPresentationsCommand()
        {
            await UpdatePresentations();
        }

        private RelayCommand loadPresentationsCommand;

        public ICommand LoadPresentationsCommand
        {
            get { return loadPresentationsCommand ?? (loadPresentationsCommand = new RelayCommand(async () => await ExecuteLoadPresentationsCommand())); }
        }


        public async Task ExecuteLoadPresentationsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await UpdatePresentations();
        }
    }
}
