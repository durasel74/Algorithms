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
        private const int timeDelay = 5000;
        private const int pauseTime = 100;

        private ObservableCollection<int> array;
        private int countFrom, countTo;
        private int selectedElement;
        private bool isComplite;
        private bool isPause;
        private bool isRunning;
        private double speed;
        private int attempt;
        private bool showInfo;
        private string algorithmInfo;

        public int CountFromLimit { get; } = -500;
        public int CountToLimit { get; } = 500;
        public ObservableCollection<int> Array { get { return array; } }

        public int CountFrom
        {
            get { return countFrom; }
            set
            {
                if (value < CountFromLimit)
                    countFrom = CountFromLimit;
                else if (value > CountTo)
                    countFrom = CountTo;
                else
                    countFrom = value;

                if (RestartEventHandler != null)
                    RestartEvent();
				OnPropertyChanged("CountFrom");
			}
		}
        public int CountTo
        {
            get { return countTo; }
            set
            {
                if (value > CountToLimit)
                    countTo = CountToLimit;
                else if (value < CountFrom)
                    countTo = CountFrom;
                else
                    countTo = value;

                if (RestartEventHandler != null)
                    RestartEvent();
                OnPropertyChanged("CountTo");
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
            set
            {
                isComplite = value;
                OnPropertyChanged("IsComplite");
			}
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
                speed = value;
                OnPropertyChanged("Speed");
            }
        }
        public int Attempt 
        { 
            get { return attempt; } 
            set
            {
                attempt = value;
                OnPropertyChanged("Attempt");
			}
        }
        public bool ShowInfo
        {
            get { return showInfo; }
            set
            {
                showInfo = value;
                OnPropertyChanged("ShowInfo");
			}
		}
        public string AlgorithmInfo
        {
            get { return algorithmInfo; }
            set
            {
                algorithmInfo = value;
                OnPropertyChanged("AlgorithmInfo");
            }
        }

        public delegate void AlgorithmHundler();
        public event AlgorithmHundler _algorithmEventHandler;
        public event AlgorithmHundler AlgorithmEventHandler
        {
            add
            {
                _algorithmEventHandler += value;
                UpdateInfo();
            }
            remove
            {
                _algorithmEventHandler -= value;
            }
        }

        public delegate void AlgorithmRestart();
        public event AlgorithmRestart RestartEventHandler;

        public AlgorithmsProcessor()
        {
            array = new ObservableCollection<int>();
            CountTo = 100;
            CountFrom = 1;
            SelectedElement = CountFrom - 1;
            IsComplite = true;
            IsPause = true;
            isRunning = false;
            Speed = 15;
            ShowInfo = true;
            RestartEventHandler += RestartAlgorithm;
            UpdateInfo();
            RestartEvent();
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
                IsComplite = false;
                IsPause = false;
                await Task.Run(() => StartAlgorithm());
                IsPause = true;
                IsComplite = true;
                isRunning = false;
                UpdateInfo();
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

        /// <summary>
        /// Обновление информации о работе алгоритма.
        /// </summary>
        public virtual void UpdateInfo()
        {
            string info = "" +
            $"Состояние: {GetState()}\n" +
            $"Количество элементов: {Array.Count}\n" +
            $"Попытка: {Attempt}";
            AlgorithmInfo = info;
        }

        /// <summary>
        /// Получение текущего состояния работы алгоритма.
        /// </summary>
        /// <returns>Строковое состояние</returns>
        public string GetState()
        {
            if (!IsRunning)
                return "Готово";
            else if (IsPause)
                return "Пауза";
            else
                return "Запущено";
        }

        /// <summary>
        /// Рестарт алгоритма.
        /// </summary>
        public void RestartAlgorithm()
        {
            Attempt = 0;
            UpdateArray();
            SelectedElement = CountFromLimit - 1;
            UpdateInfo();
        }

        /// <summary>
        /// Управляет скоростью выполнения алгоритма и паузами.
        /// </summary>
        /// <returns>Был ли выход непринужденным</returns>
        public bool TimeManagement()
        {
            UpdateInfo();
            for (int i = 0; i < timeDelay / pauseTime; i++)
            {
                while (isPause)
                {
                    if (isComplite) return false;
                    Thread.Sleep((int)pauseTime);
                    UpdateInfo();
                }

                if (isComplite) return false;
                Thread.Sleep((int)(pauseTime / (speed / 2)));
			}
            return true;
        }

        private void StartAlgorithm()
        {
            _algorithmEventHandler.Invoke();
        }
        private void UpdateArray()
        {
            array.Clear();
            for (int i = countFrom; i < countTo + 1; i++)
                array.Add(i);
            OnPropertyChanged("Array");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
