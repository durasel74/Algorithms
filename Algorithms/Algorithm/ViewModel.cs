using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;




using System.Windows;




namespace Algorithms.Algorithm
{
    public class ViewModel : INotifyPropertyChanged
    {
        private AlgorithmsProcessor algorithm;

        #region Временное
        private bool showButtonList = false;
        public bool ShowButtonList
        {
            get { return showButtonList; }
            set
            {
                showButtonList = value;
                OnPropertyChanged("ShowButtonList");
            }
        }
        private ButtonCommand authorInfo;
        public ButtonCommand AuthorInfo
        {
            get
            {
                return authorInfo ??
                    (authorInfo = new ButtonCommand(obj =>
                    { 
                        MessageBox.Show("Автор: Коротовский Дмитрий, " +
                            "курсант 331 группы Троицкого Авиационного колледжа."); 
                    }));
            }
        }
        private ButtonCommand programInfo;
        public ButtonCommand ProgramInfo
        {
            get
            {
                return programInfo ??
                    (programInfo = new ButtonCommand(obj =>
                    {
                        MessageBox.Show("Программа для визуализации " +
                            "популярных алгоритмов.");
                    }));
            }
        }
        private ButtonCommand exitProgram;
        public ButtonCommand ExitProgram
        {
            get
            {
                return exitProgram ??
                    (exitProgram = new ButtonCommand(obj =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }
        #endregion

        // Команда старта алгоритма
        private ButtonCommand startCommnad;
        public ButtonCommand StartCommand
        {
            get
            {
                return startCommnad ??
                    (startCommnad = new ButtonCommand(obj =>
                    { algorithm.Run(); }));
            }
        }

        // Команда перезапуска алгоритма
        private ButtonCommand restartCommand;
        public ButtonCommand RestartCommand
        {
            get
            {
                return restartCommand ??
                    (restartCommand = new ButtonCommand(obj =>
                    { algorithm.Restart(); }));
            }
        }

        public AlgorithmsProcessor Algorithm
        {
            get { return algorithm; }
            set
            {
                algorithm = value;
                OnPropertyChanged("Algorithm");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
