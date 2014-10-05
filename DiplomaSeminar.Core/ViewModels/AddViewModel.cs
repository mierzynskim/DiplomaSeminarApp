using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.Interfaces;
using DiplomaSeminar.Core.Model;

namespace DiplomaSeminar.Core.ViewModels
{
    public class AddViewModel : ViewModelBase
    {
        public AddViewModel()
        {
            presentationService = ServiceContainer.Resolve<IPresentationService>();
        }

        public bool CanNavigate { get; set; }

        private readonly IPresentationService presentationService;

        public AddViewModel(IPresentationService expenseService)
        {
            presentationService = expenseService;
        }

        private Presentation currentPresentation;
        public async Task Init(int id)
        {
            if (id >= 0)
                currentPresentation = await presentationService.GetPresentation(id);
            else
                currentPresentation = null;
            Init();
        }

        public void Init(Presentation presentation)
        {
            currentPresentation = presentation;
            Init();
        }

        private void Init()
        {
            CanNavigate = true;
            if (currentPresentation == null)
            {
                SpeakerName = string.Empty;
                Date = DateTime.Now;
                SpeakerLastName = string.Empty;
                Subject = string.Empty;
                return;
            }

            SpeakerName = currentPresentation.SpeakerName;
            SpeakerLastName = currentPresentation.SpeakerLastName;
            Date = currentPresentation.Date;
            Subject = currentPresentation.Subject;
        }
        private string speakerName = string.Empty;
        public string SpeakerName
        {
            get { return speakerName; }
            set { speakerName = value; OnPropertyChanged("SpeakerName"); }
        }

        private string speakerLastName = string.Empty;
        public string SpeakerLastName
        {
            get { return speakerLastName; }
            set { speakerLastName = value; OnPropertyChanged("SpeakerLastName"); }
        }

        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }


        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; OnPropertyChanged("Subject"); }
        }

        private RelayCommand savePresentationCommand;

        public ICommand SavePresentationCommand
        {
            get { return savePresentationCommand ?? (savePresentationCommand = new RelayCommand(async () => await ExecuteSavePresentationCommand())); }
        }

        public async Task ExecuteSavePresentationCommand()
        {
            if (IsBusy)
                return;

            CanNavigate = false;
            if (currentPresentation == null)
                currentPresentation = new Presentation();

            currentPresentation.Subject = Subject;
            currentPresentation.Date = Date.ToUniversalTime();
            currentPresentation.SpeakerName = SpeakerName;
            currentPresentation.SpeakerLastName = SpeakerLastName;
            try
            {
                IsBusy = true;
                await presentationService.SavePresentation(currentPresentation);
                ServiceContainer.Resolve<PresentationsViewModel>().NeedsUpdate = true;
                CanNavigate = true;
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
