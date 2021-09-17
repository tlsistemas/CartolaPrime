using CartoPrime.Modules.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Team.ViewModel
{
    public class ClimbViewModel : BaseViewModel<ClimbViewModel>
    {
        #region Propertes
        private ClimbModel _athlete01;
        public ClimbModel Athlete01
        {
            get { return _athlete01; }
            set
            {
                SetProperty(ref _athlete01, value);
            }
        }

        private ClimbModel _athlete02;
        public ClimbModel Athlete02
        {
            get { return _athlete02; }
            set
            {
                SetProperty(ref _athlete02, value);
            }
        }

        private ClimbModel _athlete03;
        public ClimbModel Athlete03
        {
            get { return _athlete03; }
            set
            {
                SetProperty(ref _athlete03, value);
            }
        }

        private ClimbModel _athlete04;
        public ClimbModel Athlete04
        {
            get { return _athlete04; }
            set
            {
                SetProperty(ref _athlete04, value);
            }
        }

        private ClimbModel _athlete05;
        public ClimbModel Athlete05
        {
            get { return _athlete05; }
            set
            {
                SetProperty(ref _athlete05, value);
            }
        }

        private ClimbModel _athlete06;
        public ClimbModel Athlete06
        {
            get { return _athlete06; }
            set
            {
                SetProperty(ref _athlete06, value);
            }
        }

        private ClimbModel _athlete07;
        public ClimbModel Athlete07
        {
            get { return _athlete07; }
            set
            {
                SetProperty(ref _athlete07, value);
            }
        }

        private ClimbModel _athlete08;
        public ClimbModel Athlete08
        {
            get { return _athlete08; }
            set
            {
                SetProperty(ref _athlete08, value);
            }
        }

        private ClimbModel _athlete09;
        public ClimbModel Athlete09
        {
            get { return _athlete09; }
            set
            {
                SetProperty(ref _athlete09, value);
            }
        }

        private ClimbModel _athlete10;
        public ClimbModel Athlete10
        {
            get { return _athlete10; }
            set
            {
                SetProperty(ref _athlete10, value);
            }
        }

        private ClimbModel _athlete11;
        public ClimbModel Athlete11
        {
            get { return _athlete11; }
            set
            {
                SetProperty(ref _athlete11, value);
            }
        }

        private ClimbModel _athlete12;
        public ClimbModel Athlete12
        {
            get { return _athlete12; }
            set
            {
                SetProperty(ref _athlete12, value);
            }
        }
        #endregion

        #region Comands
        public ICommand AlterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Set343().ConfigureAwait(true);
                });
            }
        }
        public ICommand FindAthleteCommand { get; set; }
        #endregion

        #region Construtor
        public ClimbViewModel()
        {
            Title = "Escalação";
            FindAthleteCommand = new Command<object>(async (item) => await FindAthletePosition(item));
            GetInitial().ConfigureAwait(true);
        }
        #endregion

        #region Metodos
        private async Task GetInitial()
        {
            await Set433().ConfigureAwait(true);
        }

        private async Task FindAthletePosition(object position)
        {

        }

        private async Task Set433()
        {
            ShowLoading(Resources.AppResources.LOADING);
            Athlete01 = new ClimbModel { Area = new Rectangle(30, 410, 40, 40), Position = "TEC", PositionId = 6 };
            Athlete02 = new ClimbModel { Area = new Rectangle(160, 380, 40, 40), Position = "GOL", PositionId = 1 };
            Athlete03 = new ClimbModel { Area = new Rectangle(210, 330, 40, 40), Position = "LAT", PositionId = 2 };
            Athlete04 = new ClimbModel { Area = new Rectangle(110, 330, 40, 40), Position = "LAT", PositionId = 2 };
            Athlete05 = new ClimbModel { Area = new Rectangle(269, 300, 40, 40), Position = "ZAG", PositionId = 3 };
            Athlete06 = new ClimbModel { Area = new Rectangle(40, 300, 40, 40), Position = "ZAG", PositionId = 3 };
            Athlete07 = new ClimbModel { Area = new Rectangle(260, 200, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete08 = new ClimbModel { Area = new Rectangle(160, 190, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete09 = new ClimbModel { Area = new Rectangle(60, 200, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete10 = new ClimbModel { Area = new Rectangle(250, 70, 40, 40), Position = "ATA", PositionId = 5 };
            Athlete11 = new ClimbModel { Area = new Rectangle(159, 30, 40, 40), Position = "ATA", PositionId = 5 };
            Athlete12 = new ClimbModel { Area = new Rectangle(70, 70, 40, 40), Position = "ATA", PositionId = 5 };
            HideLoading();
        }

        private async Task Set343()
        {
            ShowLoading(Resources.AppResources.LOADING);
            Athlete01 = new ClimbModel { Area = new Rectangle(30, 410, 40, 40), Position = "TEC", PositionId = 6 };
            Athlete02 = new ClimbModel { Area = new Rectangle(160, 380, 40, 40), Position = "GOL", PositionId = 1 };
            Athlete03 = new ClimbModel { Area = new Rectangle(155, 300, 40, 40), Position = "ZAG", PositionId = 3 };
            Athlete04 = new ClimbModel { Area = new Rectangle(60, 300, 40, 40), Position = "ZAG", PositionId = 3 };
            Athlete05 = new ClimbModel { Area = new Rectangle(255, 300, 40, 40), Position = "ZAG", PositionId = 3 };
            Athlete06 = new ClimbModel { Area = new Rectangle(110, 210, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete07 = new ClimbModel { Area = new Rectangle(260, 160, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete08 = new ClimbModel { Area = new Rectangle(200, 210, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete09 = new ClimbModel { Area = new Rectangle(45, 160, 40, 40), Position = "MEI", PositionId = 4 };
            Athlete10 = new ClimbModel { Area = new Rectangle(250, 70, 40, 40), Position = "ATA", PositionId = 5 };
            Athlete11 = new ClimbModel { Area = new Rectangle(159, 30, 40, 40), Position = "ATA", PositionId = 5 };
            Athlete12 = new ClimbModel { Area = new Rectangle(70, 70, 40, 40), Position = "ATA", PositionId = 5 };

            HideLoading();
        }
        #endregion
    }

    public class ClimbModel
    {
        public Rectangle Area { get; set; }
        public string Position { get; set; }
        public int PositionId { get; set; }
    }
}
