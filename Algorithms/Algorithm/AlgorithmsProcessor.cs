using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
    /// <summary>
    /// Главный механизм запуска алгоритмов. Управляет временем выполнения.
    /// </summary>
	public class AlgorithmsProcessor : INotifyPropertyChanged
	{
        private ObservableCollection<int> array;
        private int countFrom, countTo;
        private int selectedElement;
        private bool isComplite;
        private bool isPause;
        private bool isRunning;
        private double speed;

        public ObservableCollection<int> Array { get { return array; } }
        public int CountFrom
        {
            get { return countFrom; }
            set
            {
                countFrom = value;
                OnPropertyChanged("CountFrom");
                UpdateArray();
			}
		}
        public int CountTo
        {
            get { return countTo; }
            set
            {
                countTo = value;
                OnPropertyChanged("countTo");
                UpdateArray();
            }
        }
        public int SelectedElement
        {
            get { return selectedElement; }
            set
            {
                selectedElement = value;
                OnPropertyChanged("SelectedElement");
            }
        }
		public bool IsComplite
		{
			get { return isComplite; }
		}
		public bool IsRunning
        {
            get { return isRunning; }
        }
        public bool IsPause
        {
            get { return isPause; }
            set
            {
                isPause = value;
                OnPropertyChanged("IsPause");
			}
		}
        public double Speed
        {
            get { return speed; }
            set
            {
                if (value > 0 && value < 100)
                {
                    speed = Math.Round(value, 1);
                    if (speed == 0)
                        speed = 1;
                    OnPropertyChanged("Speed");
                }
            }
        }

        public delegate bool AlgorithmHundler();
        public event AlgorithmHundler AlgorithmEventHandler;

        public delegate void AlgorithmRestart();
        public event AlgorithmRestart RestartEventHandler;

        public AlgorithmsProcessor()
        {
            array = new ObservableCollection<int>();
            CountFrom = 1;
            CountTo = 100;
            SelectedElement = CountFrom - 1;
            isComplite = true;
            IsPause = true;
            isRunning = false;
            Speed = 1;
            RestartEventHandler += RestartAlgorithm;
        }

        /// <summary>
        /// Асинхронный запуск алгоритма.
        /// </summary>
        public async void Run()
        {
            if (!isRunning)
            {
                RestartEvent();

                isRunning = true;
                isComplite = false;
                IsPause = false;
                await Task.Run(() => StartAlgorithm());
                IsPause = true;
                isComplite = true;
                isRunning = false;
            }
        }

        /// <summary>
        /// Останавливает работу алгоритма.
        /// </summary>
        public void Close()
        {
            isComplite = true;
            isPause = true;
        }

        /// <summary>
        /// Запускает событие перезагрузки.
        /// </summary>
        public void RestartEvent()
        {
            RestartEventHandler.Invoke();
        }

        private void StartAlgorithm()
        {
            bool again;
            while (!isComplite)
            {
                Thread.Sleep((int)(1000 / speed));
                if (isPause)
                {
                    if (isComplite) break;
                    Thread.Sleep(100);
                    continue;
                }

                again = AlgorithmEventHandler.Invoke();
                if (!again) return;
            }
        }
        private void UpdateArray()
        {
            array.Clear();
            for (int i = countFrom; i < countTo + 1; i++)
                array.Add(i);
            OnPropertyChanged("Array");
        }
        private void RestartAlgorithm()
        {
            UpdateArray();
            SelectedElement = countFrom - 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
